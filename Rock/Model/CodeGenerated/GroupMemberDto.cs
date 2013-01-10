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

namespace Rock.Model
{
    /// <summary>
    /// Data Transfer Object for GroupMember object
    /// </summary>
    [Serializable]
    [DataContract]
    public partial class GroupMemberDto : DtoSecured<GroupMemberDto>
    {
        /// <summary />
        [DataMember]
        public bool IsSystem { get; set; }

        /// <summary />
        [DataMember]
        public int GroupId { get; set; }

        /// <summary />
        [DataMember]
        public int PersonId { get; set; }

        /// <summary />
        [DataMember]
        public int GroupRoleId { get; set; }

        /// <summary>
        /// Instantiates a new DTO object
        /// </summary>
        public GroupMemberDto ()
        {
        }

        /// <summary>
        /// Instantiates a new DTO object from the entity
        /// </summary>
        /// <param name="groupMember"></param>
        public GroupMemberDto ( GroupMember groupMember )
        {
            CopyFromModel( groupMember );
        }

        /// <summary>
        /// Creates a dictionary object.
        /// </summary>
        /// <returns></returns>
        public override Dictionary<string, object> ToDictionary()
        {
            var dictionary = base.ToDictionary();
            dictionary.Add( "IsSystem", this.IsSystem );
            dictionary.Add( "GroupId", this.GroupId );
            dictionary.Add( "PersonId", this.PersonId );
            dictionary.Add( "GroupRoleId", this.GroupRoleId );
            return dictionary;
        }

        /// <summary>
        /// Creates a dynamic object.
        /// </summary>
        /// <returns></returns>
        public override dynamic ToDynamic()
        {
            dynamic expando = base.ToDynamic();
            expando.IsSystem = this.IsSystem;
            expando.GroupId = this.GroupId;
            expando.PersonId = this.PersonId;
            expando.GroupRoleId = this.GroupRoleId;
            return expando;
        }

        /// <summary>
        /// Copies the model property values to the DTO properties
        /// </summary>
        /// <param name="model">The model.</param>
        public override void CopyFromModel( IEntity model )
        {
            base.CopyFromModel( model );

            if ( model is GroupMember )
            {
                var groupMember = (GroupMember)model;
                this.IsSystem = groupMember.IsSystem;
                this.GroupId = groupMember.GroupId;
                this.PersonId = groupMember.PersonId;
                this.GroupRoleId = groupMember.GroupRoleId;
            }
        }

        /// <summary>
        /// Copies the DTO property values to the entity properties
        /// </summary>
        /// <param name="model">The model.</param>
        public override void CopyToModel ( IEntity model )
        {
            base.CopyToModel( model );

            if ( model is GroupMember )
            {
                var groupMember = (GroupMember)model;
                groupMember.IsSystem = this.IsSystem;
                groupMember.GroupId = this.GroupId;
                groupMember.PersonId = this.PersonId;
                groupMember.GroupRoleId = this.GroupRoleId;
            }
        }

    }

    /// <summary>
    /// 
    /// </summary>
    public static class GroupMemberDtoExtension
    {
        /// <summary>
        /// To the model.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static GroupMember ToModel( this GroupMemberDto value )
        {
            GroupMember result = new GroupMember();
            value.CopyToModel( result );
            return result;
        }

        /// <summary>
        /// To the model.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static List<GroupMember> ToModel( this List<GroupMemberDto> value )
        {
            List<GroupMember> result = new List<GroupMember>();
            value.ForEach( a => result.Add( a.ToModel() ) );
            return result;
        }

        /// <summary>
        /// To the dto.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static List<GroupMemberDto> ToDto( this List<GroupMember> value )
        {
            List<GroupMemberDto> result = new List<GroupMemberDto>();
            value.ForEach( a => result.Add( a.ToDto() ) );
            return result;
        }

        /// <summary>
        /// To the dto.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static GroupMemberDto ToDto( this GroupMember value )
        {
            return new GroupMemberDto( value );
        }

    }
}