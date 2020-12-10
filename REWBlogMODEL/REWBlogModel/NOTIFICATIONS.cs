namespace REW的空间Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NOTIFICATIONS
    {
        [Key]       
        public int NID { get; set; }

        [Required]
        public string USERNAME { get; set; }

        [Required]
        public string WHOSE { get; set; }

        [Required]
        public int NOTI_TYPE { get; set; }
        
        [Required]
        public string MESSAGE { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool READ { get; set; }
    }
}
