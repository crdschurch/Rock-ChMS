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
using System.Linq;

using Rock.Data;

namespace Rock.Model
{
    /// <summary>
    /// Site Service class
    /// </summary>
    public partial class SiteService : Service<Site>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SiteService"/> class
        /// </summary>
        public SiteService()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteService"/> class
        /// </summary>
        public SiteService(IRepository<Site> repository) : base(repository)
        {
        }

        /// <summary>
        /// Determines whether this instance can delete the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <returns>
        ///   <c>true</c> if this instance can delete the specified item; otherwise, <c>false</c>.
        /// </returns>
        public bool CanDelete( Site item, out string errorMessage )
        {
            errorMessage = string.Empty;
 
            if ( new Service<Page>().Queryable().Any( a => a.SiteId == item.Id ) )
            {
                errorMessage = string.Format( "This {0} is assigned to a {1}.", Site.FriendlyTypeName, Page.FriendlyTypeName );
                return false;
            }  
            return true;
        }
    }

    /// <summary>
    /// Generated Extension Methods
    /// </summary>
    public static class SiteExtensionMethods
    {
        /// <summary>
        /// Clones this Site object to a new Site object
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="deepCopy">if set to <c>true</c> a deep copy is made. If false, only the basic entity properties are copied.</param>
        /// <returns></returns>
        public static Site Clone( this Site source, bool deepCopy )
        {
            if (deepCopy)
            {
                return source.Clone() as Site;
            }
            else
            {
                var target = new Site();
                target.IsSystem = source.IsSystem;
                target.Name = source.Name;
                target.Description = source.Description;
                target.Theme = source.Theme;
                target.DefaultPageId = source.DefaultPageId;
                target.FaviconUrl = source.FaviconUrl;
                target.AppleTouchIconUrl = source.AppleTouchIconUrl;
                target.FacebookAppId = source.FacebookAppId;
                target.FacebookAppSecret = source.FacebookAppSecret;
                target.LoginPageReference = source.LoginPageReference;
                target.RegistrationPageReference = source.RegistrationPageReference;
                target.ErrorPage = source.ErrorPage;
                target.Id = source.Id;
                target.Guid = source.Guid;

            
                return target;
            }
        }
    }
}
