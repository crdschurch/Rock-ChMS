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
    /// PersonAccount Service class
    /// </summary>
    public partial class PersonAccountService : Service<PersonAccount>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonAccountService"/> class
        /// </summary>
        public PersonAccountService()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonAccountService"/> class
        /// </summary>
        public PersonAccountService(IRepository<PersonAccount> repository) : base(repository)
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
        public bool CanDelete( PersonAccount item, out string errorMessage )
        {
            errorMessage = string.Empty;
            return true;
        }
    }

    /// <summary>
    /// Generated Extension Methods
    /// </summary>
    public static class PersonAccountExtensionMethods
    {
        /// <summary>
        /// Copies all the entity properties from another PersonAccount entity
        /// </summary>
        public static void CopyPropertiesFrom( this PersonAccount target, PersonAccount source )
        {
            target.PersonId = source.PersonId;
            target.Account = source.Account;
            target.Id = source.Id;
            target.Guid = source.Guid;

        }
    }
}