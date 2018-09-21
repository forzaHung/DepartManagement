﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Dispatch
{
    public partial class CustomerEntity
    {
        #region Properties
        ///----------------------------
        /// Generated By:   Sushi-Sashimi using CodeSmith 6.0.0.0
        /// Template:       TableEntity.cst
        /// Date Generated: April 10, 2017
        ///----------------------------
        public int Id { get; set; }

        ///----------------------------
        /// Generated By:   Sushi-Sashimi using CodeSmith 6.0.0.0
        /// Template:       TableEntity.cst
        /// Date Generated: April 10, 2017
        ///----------------------------
        public int ParentId { get; set; }

        ///----------------------------
        /// Generated By:   Sushi-Sashimi using CodeSmith 6.0.0.0
        /// Template:       TableEntity.cst
        /// Date Generated: April 10, 2017
        ///----------------------------
        [Required(ErrorMessage ="Điền tên khách hàng")]
        public string Name { get; set; }

        ///----------------------------
        /// Generated By:   Sushi-Sashimi using CodeSmith 6.0.0.0
        /// Template:       TableEntity.cst
        /// Date Generated: April 10, 2017
        ///----------------------------
        [Required(ErrorMessage ="Điền địa chỉ khách hàng")]
        public string Address { get; set; }

        ///----------------------------
        /// Generated By:   Sushi-Sashimi using CodeSmith 6.0.0.0
        /// Template:       TableEntity.cst
        /// Date Generated: April 10, 2017
        ///----------------------------

        public string Customer_TypeId { get; set; }

        ///----------------------------
        /// Generated By:   Sushi-Sashimi using CodeSmith 6.0.0.0
        /// Template:       TableEntity.cst
        /// Date Generated: April 10, 2017
        ///----------------------------
        public System.DateTime Createdate { get; set; }

        ///----------------------------
        /// Generated By:   Sushi-Sashimi using CodeSmith 6.0.0.0
        /// Template:       TableEntity.cst
        /// Date Generated: April 10, 2017
        ///----------------------------
        public System.DateTime ModifiedDate { get; set; }

        ///----------------------------
        /// Generated By:   Sushi-Sashimi using CodeSmith 6.0.0.0
        /// Template:       TableEntity.cst
        /// Date Generated: April 10, 2017
        ///----------------------------
        public bool IsDel { get; set; }

        ///----------------------------
        /// Generated By:   Sushi-Sashimi using CodeSmith 6.0.0.0
        /// Template:       TableEntity.cst
        /// Date Generated: April 10, 2017
        ///----------------------------
        public bool IsActive { get; set; }

        #endregion
    }
}

