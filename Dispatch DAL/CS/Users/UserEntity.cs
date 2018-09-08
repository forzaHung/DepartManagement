using System;
using System.ComponentModel.DataAnnotations;

namespace Dispatch
{
    [MetadataType(typeof(UserMetadata))]
    public partial class UserEntity
	{
		#region Properties
		/// <summary>
		/// MinhDuongVan generator
		/// Gets or sets the Id value.
		/// 2017-02-16 15:28:25Z
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// MinhDuongVan generator
		/// Gets or sets the UserName value.
		/// 2017-02-16 15:28:25Z
		/// </summary>
		public string UserName { get; set; }

		/// <summary>
		/// MinhDuongVan generator
		/// Gets or sets the Password value.
		/// 2017-02-16 15:28:25Z
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		/// MinhDuongVan generator
		/// Gets or sets the LastPass value.
		/// 2017-02-16 15:28:25Z
		/// </summary>
		public string LastPass { get; set; }

		/// <summary>
		/// MinhDuongVan generator
		/// Gets or sets the RoleId value.
		/// 2017-02-16 15:28:25Z
		/// </summary>
		public int RoleId { get; set; }

		/// <summary>
		/// MinhDuongVan generator
		/// Gets or sets the EmployeeId value.
		/// 2017-02-16 15:28:25Z
		/// </summary>
		public int EmployeeId { get; set; }

		/// <summary>
		/// MinhDuongVan generator
		/// Gets or sets the LastChangePassword value.
		/// 2017-02-16 15:28:25Z
		/// </summary>
		public DateTime LastChangePassword { get; set; }

		/// <summary>
		/// MinhDuongVan generator
		/// Gets or sets the LastLogin value.
		/// 2017-02-16 15:28:25Z
		/// </summary>
		public DateTime LastLogin { get; set; }

		/// <summary>
		/// MinhDuongVan generator
		/// Gets or sets the Token value.
		/// 2017-02-16 15:28:25Z
		/// </summary>
		public string Token { get; set; }
        public bool IsActive { get; set; }

		#endregion
	}
}
