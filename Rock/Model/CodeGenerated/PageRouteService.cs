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
    /// PageRoute Service class
    /// </summary>
    public partial class PageRouteService : Service<PageRoute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PageRouteService"/> class
        /// </summary>
        public PageRouteService()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PageRouteService"/> class
        /// </summary>
        public PageRouteService(IRepository<PageRoute> repository) : base(repository)
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
        public bool CanDelete( PageRoute item, out string errorMessage )
        {
            errorMessage = string.Empty;
            return true;
        }
    }

    /// <summary>
    /// Generated Extension Methods
    /// </summary>
    public static class PageRouteExtensionMethods
    {
        /// <summary>
        /// Copies all the entity properties from another PageRoute entity
        /// </summary>
        public static void CopyPropertiesFrom( this PageRoute target, PageRoute source )
        {
            target.IsSystem = source.IsSystem;
            target.PageId = source.PageId;
            target.Route = source.Route;
            target.Id = source.Id;
            target.Guid = source.Guid;

        }
    }
}