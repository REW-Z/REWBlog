namespace REWBlogModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ARTICLES
    {
        [Key]
        public int A_ID { get; set; }

        [StringLength(50)]
        public string A_NAME { get; set; }

        [StringLength(50)]
        public string A_AUTHOR { get; set; }

        public DateTime? A_DATE { get; set; }

        public int A_TID { get; set; }

        public string A_CONTENT { get; set; }

        [StringLength(50)]
        public string A_CATALOG { get; set; }

        public virtual TYPES TYPES { get; set; }
    }
}
