using galdino.bloodOrnage.application.core.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace galdino.bloodOrnage.application.core.Entity.Base
{
    public class BaseEntity : IEntity
    {
        [Column("dh_DateInclusionRegistration")]
        public DateTime DateInclusionRegistration { get; set; }

        [Column("dh_DateChangeRegistration")]
        public DateTime DateChangeRegistration { get; set; }

        [Column("Active")]
        public bool Active { get; set; }
    }
}
