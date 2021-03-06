﻿//
// THIS WORK IS LICENSED UNDER A CREATIVE COMMONS ATTRIBUTION-NONCOMMERCIAL-
// SHAREALIKE 3.0 UNPORTED LICENSE:
// http://creativecommons.org/licenses/by-nc-sa/3.0/
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;

using Rock;
using Rock.Constants;
using Rock.Data;
using Rock.Model;
using Rock.Web.Cache;
using Rock.Web.UI;
using Rock.Web.UI.Controls;

namespace RockWeb.Blocks.Reporting
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ReportDetail : RockBlock, IDetailBlock
    {
        #region Control Methods

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
        protected override void OnInit( EventArgs e )
        {
            base.OnInit( e );

            gReport.GridRebind += gReport_GridRebind;
            btnDelete.Attributes["onclick"] = string.Format( "javascript: return confirmDelete(event, '{0}');", Report.FriendlyTypeName );
            btnSecurity.EntityType = typeof( Rock.Model.Report );
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Load" /> event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs" /> object that contains the event data.</param>
        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad( e );

            if ( !Page.IsPostBack )
            {
                string itemId = PageParameter( "ReportId" );
                string parentCategoryId = PageParameter( "parentCategoryId" );
                if ( !string.IsNullOrWhiteSpace( itemId ) )
                {
                    if ( string.IsNullOrWhiteSpace( parentCategoryId ) )
                    {
                        ShowDetail( "ReportId", int.Parse( itemId ) );
                    }
                    else
                    {
                        ShowDetail( "ReportId", int.Parse( itemId ), int.Parse( parentCategoryId ) );
                    }
                }
                else
                {
                    pnlDetails.Visible = false;
                }
            }
        }

        #endregion

        #region Edit Events

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void btnCancel_Click( object sender, EventArgs e )
        {
            if ( hfReportId.Value.Equals( "0" ) )
            {
                // Cancelling on Add.  Return to tree view with parent category selected
                var qryParams = new Dictionary<string, string>();

                string parentCategoryId = PageParameter( "parentCategoryId" );
                if ( !string.IsNullOrWhiteSpace( parentCategoryId ) )
                {
                    qryParams["CategoryId"] = parentCategoryId;
                }
                NavigateToPage( this.CurrentPage.Guid, qryParams );
            }
            else
            {
                // Cancelling on Edit.  Return to Details
                ReportService service = new ReportService();
                Report item = service.Get( int.Parse( hfReportId.Value ) );
                ShowReadonlyDetails( item );
            }
        }

        /// <summary>
        /// Handles the Click event of the btnEdit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void btnEdit_Click( object sender, EventArgs e )
        {
            var service = new ReportService();
            var item = service.Get( int.Parse( hfReportId.Value ) );
            ShowEditDetails( item );
        }

        /// <summary>
        /// Handles the Click event of the btnDelete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void btnDelete_Click( object sender, EventArgs e )
        {
            int? categoryId = null;

            var reportService = new ReportService();
            var report = reportService.Get( int.Parse( hfReportId.Value ) );

            if ( report != null )
            {
                string errorMessage;
                if ( !reportService.CanDelete( report, out errorMessage ) )
                {
                    ShowReadonlyDetails( report );
                    mdDeleteWarning.Show( errorMessage, ModalAlertType.Information );
                }
                else
                {
                    categoryId = report.CategoryId;

                    reportService.Delete( report, CurrentPersonId );
                    reportService.Save( report, CurrentPersonId );

                    // reload page, selecting the deleted data view's parent
                    var qryParams = new Dictionary<string, string>();
                    if ( categoryId != null )
                    {
                        qryParams["CategoryId"] = categoryId.ToString();
                    }

                    NavigateToPage( this.CurrentPage.Guid, qryParams );
                }
            }
        }

        /// <summary>
        /// Sets the edit mode.
        /// </summary>
        /// <param name="editable">if set to <c>true</c> [editable].</param>
        private void SetEditMode( bool editable )
        {
            pnlEditDetails.Visible = editable;
            fieldsetViewDetails.Visible = !editable;
        }

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void btnSave_Click( object sender, EventArgs e )
        {
            Report report = null;

            using ( new UnitOfWorkScope() )
            {
                ReportService service = new ReportService();

                int reportId = int.Parse( hfReportId.Value );

                if ( reportId == 0 )
                {
                    report = new Report();
                    report.IsSystem = false;
                }
                else
                {
                    report = service.Get( reportId );
                }

                report.Name = tbName.Text;
                report.Description = tbDescription.Text;
                report.CategoryId = ddlCategory.SelectedValueAsInt();
                report.EntityTypeId = ddlEntityType.SelectedValueAsInt();
                report.DataViewId = ddlDataView.SelectedValueAsInt();

                // check for duplicates within Category
                if ( service.Queryable()
                    .Where( g => ( g.CategoryId == report.CategoryId ) )
                    .Count( a => a.Name.Equals( report.Name, StringComparison.OrdinalIgnoreCase ) &&
                        !a.Id.Equals( report.Id ) ) > 0 )
                {
                    tbName.ShowErrorMessage( WarningMessage.DuplicateFoundMessage( "name", Report.FriendlyTypeName ) );
                    return;
                }

                if ( !Page.IsValid )
                {
                    return;
                }

                if ( !report.IsValid )
                {
                    // Controls will render the error messages                    
                    return;
                }

                RockTransactionScope.WrapTransaction( () =>
                {
                    if ( report.Id.Equals( 0 ) )
                    {
                        service.Add( report, CurrentPersonId );
                    }

                    service.Save( report, CurrentPersonId );

                } );
            }

            var qryParams = new Dictionary<string, string>();
            qryParams["ReportId"] = report.Id.ToString();
            NavigateToPage( this.CurrentPage.Guid, qryParams );
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Loads the drop downs.
        /// </summary>
        private void LoadDropDowns()
        {
            ddlCategory.Items.Clear();
            ddlCategory.Items.Add( new ListItem( None.Text, None.Id.ToString() ) );

            var service = new CategoryService();
            LoadChildCategories( service, null, Rock.Web.Cache.EntityTypeCache.GetId( "Rock.Model.Report" ), 0 );

            ddlEntityType.DataSource = new DataViewService().GetAvailableEntityTypes().ToList();
            ddlEntityType.DataBind();
            ddlEntityType.Items.Insert( 0, new ListItem( string.Empty, "0" ) );
        }

        private void LoadChildCategories( CategoryService service, int? parentId, int? entityTypeId, int level )
        {
            foreach ( var category in service.Get( parentId, entityTypeId ) )
            {
                ddlCategory.Items.Add( new ListItem( ( new string( '-', level ) ) + category.Name, category.Id.ToString() ) );
                LoadChildCategories( service, category.Id, entityTypeId, level + 1 );
            }
        }

        private void LoadDataViewDropdown(int? entityTypeId)
        {
            if ( entityTypeId.HasValue )
            {
                ddlDataView.DataSource = new DataViewService().GetByEntityTypeId( entityTypeId.Value ).ToList();
                ddlDataView.DataBind();
                ddlDataView.Items.Insert(0, new ListItem( string.Empty, "0") );
            }
            else
            {
                ddlDataView.Items.Clear();
            }
        }

        /// <summary>
        /// Shows the detail.
        /// </summary>
        /// <param name="itemKey">The item key.</param>
        /// <param name="itemKeyValue">The item key value.</param>
        public void ShowDetail( string itemKey, int itemKeyValue )
        {
            ShowDetail( itemKey, itemKeyValue, null );
        }

        /// <summary>
        /// Shows the detail.
        /// </summary>
        /// <param name="itemKey">The item key.</param>
        /// <param name="itemKeyValue">The item key value.</param>
        /// <param name="parentCategoryId">The parent category id.</param>
        public void ShowDetail( string itemKey, int itemKeyValue, int? parentCategoryId )
        {
            pnlDetails.Visible = false;
            if ( !itemKey.Equals( "ReportId" ) )
            {
                return;
            }

            var reportService = new ReportService();
            Report report = null;

            if ( !itemKeyValue.Equals( 0 ) )
            {
                report = reportService.Get( itemKeyValue );
            }
            else
            {
                report = new Report { Id = 0, IsSystem = false, CategoryId = parentCategoryId };
            }

            if ( report == null || !report.IsAuthorized( "View", CurrentPerson ) )
            {
                return;
            }

            pnlDetails.Visible = true;
            hfReportId.Value = report.Id.ToString();

            // render UI based on Authorized and IsSystem
            bool readOnly = false;

            nbEditModeMessage.Text = string.Empty;
            if ( !report.IsAuthorized( "Edit", CurrentPerson ) )
            {
                readOnly = true;
                nbEditModeMessage.Text = EditModeMessage.ReadOnlyEditActionNotAllowed( Report.FriendlyTypeName );
            }

            if ( report.IsSystem )
            {
                readOnly = true;
                nbEditModeMessage.Text = EditModeMessage.ReadOnlySystem( Report.FriendlyTypeName );
            }

            btnSecurity.Visible = report.IsAuthorized( "Administrate", CurrentPerson );
            btnSecurity.Title = "Secure " + report.Name;
            btnSecurity.EntityTypeId = report.Id;

            if ( readOnly )
            {
                btnEdit.Visible = false;
                btnDelete.Visible = false;
                ShowReadonlyDetails( report );
            }
            else
            {
                btnEdit.Visible = true;
                string errorMessage = string.Empty;
                btnDelete.Visible = reportService.CanDelete( report, out errorMessage );
                if ( report.Id > 0 )
                {
                    ShowReadonlyDetails( report );
                }
                else
                {
                    ShowEditDetails( report );
                }
            }
        }

        /// <summary>
        /// Shows the edit.
        /// </summary>
        /// <param name="report">The data view.</param>
        public void ShowEditDetails( Report report )
        {
            if ( report.Id > 0 )
            {
                lActionTitle.Text = ActionTitle.Edit( Report.FriendlyTypeName );
            }
            else
            {
                lActionTitle.Text = ActionTitle.Add( Report.FriendlyTypeName );
            }

            LoadDropDowns();
            LoadDataViewDropdown( report.EntityTypeId );

            SetEditMode( true );

            tbName.Text = report.Name;
            tbDescription.Text = report.Description;
            ddlCategory.SetValue( report.CategoryId );
            ddlEntityType.SetValue( report.EntityTypeId );
            ddlDataView.SetValue( report.DataViewId );
        }

        /// <summary>
        /// Shows the readonly details.
        /// </summary>
        /// <param name="report">The data view.</param>
        private void ShowReadonlyDetails( Report report )
        {
            SetEditMode( false );
            hfReportId.SetValue( report.Id );
            lReadOnlyTitle.Text = report.Name;
            lblMainDetails.Text = report.Description;

            BindGrid( report );
        }

        /// <summary>
        /// Shows the preview.
        /// </summary>
        /// <param name="entityTypeId">The entity type id.</param>
        /// <param name="filter">The filter.</param>
        private void BindGrid( Report report )
        {
            if (report != null && report.EntityTypeId.HasValue)
            {
                var cachedEntityType = EntityTypeCache.Read( report.EntityTypeId.Value );
                if ( cachedEntityType != null && cachedEntityType.AssemblyName != null )
                {
                    Type entityType = cachedEntityType.GetEntityType();
                    if ( entityType != null )
                    {
                        BuildGridColumns( entityType );

                        Type[] modelType = { entityType };
                        Type genericServiceType = typeof( Rock.Data.Service<> );
                        Type modelServiceType = genericServiceType.MakeGenericType( modelType );

                        object serviceInstance = Activator.CreateInstance( modelServiceType );

                        if ( serviceInstance != null )
                        {
                            MethodInfo getMethod = serviceInstance.GetType().GetMethod( "GetList", new Type[] { typeof( ParameterExpression ), typeof( Expression ) } );

                            if ( getMethod != null )
                            {
                                DataViewFilter filter = null;
                                if (report.DataView != null && report.DataView.DataViewFilter != null)
                                {
                                    filter = report.DataView.DataViewFilter;
                                }
                                var paramExpression = serviceInstance.GetType().GetProperty( "ParameterExpression" ).GetValue( serviceInstance ) as ParameterExpression;
                                var whereExpression = filter != null ? filter.GetExpression( paramExpression ) : null;
                                gReport.DataSource = getMethod.Invoke( serviceInstance, new object[] { paramExpression, whereExpression } );
                                gReport.DataBind();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Builds the grid columns.
        /// </summary>
        /// <param name="modelType">Type of the model.</param>
        private void BuildGridColumns( Type modelType )
        {
            gReport.Columns.Clear();

            var previewColumns = new Dictionary<string, BoundField>();
            var allColumns = new Dictionary<string, BoundField>();

            foreach ( var property in modelType.GetProperties() )
            {
                if ( property.GetCustomAttributes( typeof( Rock.Data.PreviewableAttribute ) ).Count() > 0 )
                {
                    previewColumns.Add( property.Name, GetGridField( property ) );
                }
                else if ( previewColumns.Count == 0 && property.GetCustomAttributes( typeof( System.Runtime.Serialization.DataMemberAttribute ) ).Count() > 0 )
                {
                    allColumns.Add( property.Name, GetGridField( property ) );
                }
            }

            Dictionary<string, BoundField> columns = previewColumns.Count > 0 ? previewColumns : allColumns;

            foreach ( var column in columns )
            {
                var bf = column.Value;
                bf.DataField = column.Key;
                bf.SortExpression = column.Key;
                bf.HeaderText = column.Key.SplitCase();
                gReport.Columns.Add( bf );
            }
        }

        /// <summary>
        /// Gets the grid field.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        private BoundField GetGridField( PropertyInfo property )
        {
            BoundField bf = new BoundField();

            if ( property.PropertyType == typeof( Boolean ) )
            {
                bf = new BoolField();
            }
            else if ( property.PropertyType == typeof( DateTime ) )
            {
                bf = new DateField();
            }
            else if ( property.PropertyType.IsEnum )
            {
                bf = new EnumField();
            }

            return bf;
        }

        #endregion

        #region Activities and Actions

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlEntityType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void ddlEntityType_SelectedIndexChanged( object sender, EventArgs e )
        {
            LoadDataViewDropdown( ddlEntityType.SelectedValueAsInt() );
        }

        /// <summary>
        /// Handles the GridRebind event of the gReport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void gReport_GridRebind( object sender, EventArgs e )
        {
            BindGrid( new ReportService().Get( hfReportId.ValueAsInt() ) );
        }


        #endregion

    }
}