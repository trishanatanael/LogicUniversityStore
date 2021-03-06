namespace LogicUniversityStore.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Notification")]
    [Serializable]
    public partial class Notification
    {
        public int NotificationID { get; set; }

        [StringLength(150)]
        public string Type { get; set; }

        [StringLength(150)]
        public string Description { get; set; }

        public int? SenderUserID { get; set; }

        public int? ReciverUserID { get; set; }

        [StringLength(50)]
        public string status { get; set; }

        public int? ReceiverRoleID { get; set; }

        public int? ReceiverDeptID { get; set; }

        public virtual LUUser LUUser { get; set; }

        public virtual LUUser LUUser1 { get; set; }

        public virtual Role ReceiverRole { get; set; }
    }
}
