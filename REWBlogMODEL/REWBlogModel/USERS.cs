namespace REW的空间Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class USERS
    {
        [Key]
        public int UID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "用户名")]
        public string NAME { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "密码")]
        public string PWD { get; set; }

        public string ROLE { get; set; }

        [Required]
        public int POINTS { get; set; }
    }
}
