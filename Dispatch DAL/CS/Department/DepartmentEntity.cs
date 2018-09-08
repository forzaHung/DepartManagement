using System;
using System.ComponentModel.DataAnnotations;

namespace Dispatch
{
    [MetadataType(typeof(DepartmentMetadata))]
    public partial class DepartmentEntity
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
		/// Gets or sets the Name value.
		/// 2017-02-16 15:28:25Z
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// MinhDuongVan generator
		/// Gets or sets the CreatedDate value.
		/// 2017-02-16 15:28:25Z
		/// </summary>
		public DateTime CreatedDate { get; set; }

		/// <summary>
		/// MinhDuongVan generator
		/// Gets or sets the ModifiedDate value.
		/// 2017-02-16 15:28:25Z
		/// </summary>
		public DateTime ModifiedDate { get; set; }

        public string Location { get; set; }
        #endregion
    }
}
