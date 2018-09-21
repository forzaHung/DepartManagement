﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Dispatch
{
    
    public partial class Customer_TypeEntity
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
        [Required(ErrorMessage ="Nhập tên loại khách hàng")]
        public string Name { get; set; }

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

        public string Title { get; set; }
        #endregion
    }
}

