//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the Rock.CodeGeneration project
//     Changes to this file will be lost when the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
//
// THIS WORK IS LICENSED UNDER A CREATIVE COMMONS ATTRIBUTION-NONCOMMERCIAL-
// SHAREALIKE 3.0 UNPORTED LICENSE:
// http://creativecommons.org/licenses/by-nc-sa/3.0/
//
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.Serialization;

using Rock.Data;

namespace Rock.Util
{
    /// <summary>
    /// Data Transfer Object for WorkflowType object
    /// </summary>
    [Serializable]
    [DataContract]
    public partial class WorkflowTypeDto : IDto, DotLiquid.ILiquidizable
    {
        /// <summary />
        [DataMember]
        public bool IsSystem { get; set; }

        /// <summary />
        [DataMember]
        public bool? IsActive { get; set; }

        /// <summary />
        [DataMember]
        public string Name { get; set; }

        /// <summary />
        [DataMember]
        public string Description { get; set; }

        /// <summary />
        [DataMember]
        public int? CategoryId { get; set; }

        /// <summary />
        [DataMember]
        public int Order { get; set; }

        /// <summary />
        [DataMember]
        public int? FileId { get; set; }

        /// <summary />
        [DataMember]
        public string WorkTerm { get; set; }

        /// <summary />
        [DataMember]
        public int? EntryActivityTypeId { get; set; }

        /// <summary />
        [DataMember]
        public int? ProcessingIntervalSeconds { get; set; }

        /// <summary />
        [DataMember]
        public bool IsPersisted { get; set; }

        /// <summary />
        [DataMember]
        public WorkflowLoggingLevel LoggingLevel { get; set; }

        /// <summary />
        [DataMember]
        public int Id { get; set; }

        /// <summary />
        [DataMember]
        public Guid Guid { get; set; }

        /// <summary>
        /// Instantiates a new DTO object
        /// </summary>
        public WorkflowTypeDto ()
        {
        }

        /// <summary>
        /// Instantiates a new DTO object from the entity
        /// </summary>
        /// <param name="workflowType"></param>
        public WorkflowTypeDto ( WorkflowType workflowType )
        {
            CopyFromModel( workflowType );
        }

        /// <summary>
        /// Creates a dictionary object.
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<string, object> ToDictionary()
        {
            var dictionary = new Dictionary<string, object>();
            dictionary.Add( "IsSystem", this.IsSystem );
            dictionary.Add( "IsActive", this.IsActive );
            dictionary.Add( "Name", this.Name );
            dictionary.Add( "Description", this.Description );
            dictionary.Add( "CategoryId", this.CategoryId );
            dictionary.Add( "Order", this.Order );
            dictionary.Add( "FileId", this.FileId );
            dictionary.Add( "WorkTerm", this.WorkTerm );
            dictionary.Add( "EntryActivityTypeId", this.EntryActivityTypeId );
            dictionary.Add( "ProcessingIntervalSeconds", this.ProcessingIntervalSeconds );
            dictionary.Add( "IsPersisted", this.IsPersisted );
            dictionary.Add( "LoggingLevel", this.LoggingLevel );
            dictionary.Add( "Id", this.Id );
            dictionary.Add( "Guid", this.Guid );
            return dictionary;
        }

        /// <summary>
        /// Creates a dynamic object.
        /// </summary>
        /// <returns></returns>
        public virtual dynamic ToDynamic()
        {
            dynamic expando = new ExpandoObject();
            expando.IsSystem = this.IsSystem;
            expando.IsActive = this.IsActive;
            expando.Name = this.Name;
            expando.Description = this.Description;
            expando.CategoryId = this.CategoryId;
            expando.Order = this.Order;
            expando.FileId = this.FileId;
            expando.WorkTerm = this.WorkTerm;
            expando.EntryActivityTypeId = this.EntryActivityTypeId;
            expando.ProcessingIntervalSeconds = this.ProcessingIntervalSeconds;
            expando.IsPersisted = this.IsPersisted;
            expando.LoggingLevel = this.LoggingLevel;
            expando.Id = this.Id;
            expando.Guid = this.Guid;
            return expando;
        }

        /// <summary>
        /// Copies the model property values to the DTO properties
        /// </summary>
        /// <param name="model">The model.</param>
        public void CopyFromModel( IEntity model )
        {
            if ( model is WorkflowType )
            {
                var workflowType = (WorkflowType)model;
                this.IsSystem = workflowType.IsSystem;
                this.IsActive = workflowType.IsActive;
                this.Name = workflowType.Name;
                this.Description = workflowType.Description;
                this.CategoryId = workflowType.CategoryId;
                this.Order = workflowType.Order;
                this.FileId = workflowType.FileId;
                this.WorkTerm = workflowType.WorkTerm;
                this.EntryActivityTypeId = workflowType.EntryActivityTypeId;
                this.ProcessingIntervalSeconds = workflowType.ProcessingIntervalSeconds;
                this.IsPersisted = workflowType.IsPersisted;
                this.LoggingLevel = workflowType.LoggingLevel;
                this.Id = workflowType.Id;
                this.Guid = workflowType.Guid;
            }
        }

        /// <summary>
        /// Copies the DTO property values to the entity properties
        /// </summary>
        /// <param name="model">The model.</param>
        public void CopyToModel ( IEntity model )
        {
            if ( model is WorkflowType )
            {
                var workflowType = (WorkflowType)model;
                workflowType.IsSystem = this.IsSystem;
                workflowType.IsActive = this.IsActive;
                workflowType.Name = this.Name;
                workflowType.Description = this.Description;
                workflowType.CategoryId = this.CategoryId;
                workflowType.Order = this.Order;
                workflowType.FileId = this.FileId;
                workflowType.WorkTerm = this.WorkTerm;
                workflowType.EntryActivityTypeId = this.EntryActivityTypeId;
                workflowType.ProcessingIntervalSeconds = this.ProcessingIntervalSeconds;
                workflowType.IsPersisted = this.IsPersisted;
                workflowType.LoggingLevel = this.LoggingLevel;
                workflowType.Id = this.Id;
                workflowType.Guid = this.Guid;
            }
        }

        /// <summary>
        /// Converts to liquidizable object for dotLiquid templating
        /// </summary>
        /// <returns></returns>
        public object ToLiquid()
        {
            return this.ToDictionary();
        }

    }

    /// <summary>
    /// 
    /// </summary>
    public static class WorkflowTypeDtoExtension
    {
        /// <summary>
        /// To the model.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static WorkflowType ToModel( this WorkflowTypeDto value )
        {
            WorkflowType result = new WorkflowType();
            value.CopyToModel( result );
            return result;
        }

        /// <summary>
        /// To the model.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static List<WorkflowType> ToModel( this List<WorkflowTypeDto> value )
        {
            List<WorkflowType> result = new List<WorkflowType>();
            value.ForEach( a => result.Add( a.ToModel() ) );
            return result;
        }

        /// <summary>
        /// To the dto.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static List<WorkflowTypeDto> ToDto( this List<WorkflowType> value )
        {
            List<WorkflowTypeDto> result = new List<WorkflowTypeDto>();
            value.ForEach( a => result.Add( a.ToDto() ) );
            return result;
        }

        /// <summary>
        /// To the dto.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static WorkflowTypeDto ToDto( this WorkflowType value )
        {
            return new WorkflowTypeDto( value );
        }

    }
}