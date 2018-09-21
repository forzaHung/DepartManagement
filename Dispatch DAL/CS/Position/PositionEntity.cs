﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Dispatch
{
    [MetadataType(typeof(PositionMetadata))]
    public partial class PositionEntity
    {
        #region Properties
        ///----------------------------
        /// Generated By:   Sushi-Sashimi using CodeSmith 6.0.0.0
        /// Template:       TableEntity.cst
        /// Date Generated: March 3, 2017
        ///----------------------------
        public int Id { get; set; }

        ///----------------------------
        /// Generated By:   Sushi-Sashimi using CodeSmith 6.0.0.0
        /// Template:       TableEntity.cst
        /// Date Generated: March 3, 2017
        ///----------------------------
        public string Name { get; set; }

        ///----------------------------
        /// Generated By:   Sushi-Sashimi using CodeSmith 6.0.0.0
        /// Template:       TableEntity.cst
        /// Date Generated: March 3, 2017
        ///----------------------------
        public System.DateTime CreatedDate { get; set; }

        #endregion
    }
}
