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
    /// Fund Service class
    /// </summary>
    public partial class FundService : Service<Fund>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FundService"/> class
        /// </summary>
        public FundService()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FundService"/> class
        /// </summary>
        public FundService(IRepository<Fund> repository) : base(repository)
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
        public bool CanDelete( Fund item, out string errorMessage )
        {
            errorMessage = string.Empty;
 
            if ( new Service<Fund>().Queryable().Any( a => a.ParentFundId == item.Id ) )
            {
                errorMessage = string.Format( "This {0} is assigned to a {1}.", Fund.FriendlyTypeName, Fund.FriendlyTypeName );
                return false;
            }  
 
            if ( new Service<Pledge>().Queryable().Any( a => a.FundId == item.Id ) )
            {
                errorMessage = string.Format( "This {0} is assigned to a {1}.", Fund.FriendlyTypeName, Pledge.FriendlyTypeName );
                return false;
            }  
            return true;
        }
    }

    /// <summary>
    /// Generated Extension Methods
    /// </summary>
    public static class FundExtensionMethods
    {
        /// <summary>
        /// Clones this Fund object to a new Fund object
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="deepCopy">if set to <c>true</c> a deep copy is made. If false, only the basic entity properties are copied.</param>
        /// <returns></returns>
        public static Fund Clone( this Fund source, bool deepCopy )
        {
            if (deepCopy)
            {
                return source.Clone() as Fund;
            }
            else
            {
                var target = new Fund();
                target.Name = source.Name;
                target.PublicName = source.PublicName;
                target.Description = source.Description;
                target.ParentFundId = source.ParentFundId;
                target.IsTaxDeductible = source.IsTaxDeductible;
                target.Order = source.Order;
                target.IsActive = source.IsActive;
                target.StartDate = source.StartDate;
                target.EndDate = source.EndDate;
                target.IsPledgable = source.IsPledgable;
                target.GlCode = source.GlCode;
                target.FundTypeValueId = source.FundTypeValueId;
                target.Entity = source.Entity;
                target.EntityId = source.EntityId;
                target.Id = source.Id;
                target.Guid = source.Guid;

            
                return target;
            }
        }
    }
}
