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
    /// Device Service class
    /// </summary>
    public partial class DeviceService : Service<Device>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceService"/> class
        /// </summary>
        public DeviceService()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceService"/> class
        /// </summary>
        public DeviceService(IRepository<Device> repository) : base(repository)
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
        public bool CanDelete( Device item, out string errorMessage )
        {
            errorMessage = string.Empty;
 
            if ( new Service<Device>().Queryable().Any( a => a.PrinterDeviceId == item.Id ) )
            {
                errorMessage = string.Format( "This {0} is assigned to a {1}.", Device.FriendlyTypeName, Device.FriendlyTypeName );
                return false;
            }  
 
            if ( new Service<Location>().Queryable().Any( a => a.PrinterDeviceId == item.Id ) )
            {
                errorMessage = string.Format( "This {0} is assigned to a {1}.", Device.FriendlyTypeName, Location.FriendlyTypeName );
                return false;
            }  
            return true;
        }
    }

    /// <summary>
    /// Generated Extension Methods
    /// </summary>
    public static class DeviceExtensionMethods
    {
        /// <summary>
        /// Clones this Device object to a new Device object
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="deepCopy">if set to <c>true</c> a deep copy is made. If false, only the basic entity properties are copied.</param>
        /// <returns></returns>
        public static Device Clone( this Device source, bool deepCopy )
        {
            if (deepCopy)
            {
                return source.Clone() as Device;
            }
            else
            {
                var target = new Device();
                target.Name = source.Name;
                target.Description = source.Description;
                target.GeoPoint = source.GeoPoint;
                target.GeoFence = source.GeoFence;
                target.DeviceTypeValueId = source.DeviceTypeValueId;
                target.IPAddress = source.IPAddress;
                target.PrinterDeviceId = source.PrinterDeviceId;
                target.PrintFrom = source.PrintFrom;
                target.PrintToOverride = source.PrintToOverride;
                target.Id = source.Id;
                target.Guid = source.Guid;

            
                return target;
            }
        }
    }
}
