namespace REWBlogModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RELATIONS
    {
        [Key]
        public int R_ID { get; set; }

        [Required]
        public int R_FollowID { get; set; }

        [Required]
        public int R_FocusID{ get; set; }
    }
}
