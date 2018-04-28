namespace WebApplication5.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Yorum")]
    public partial class Yorum
    {
        [Key]
        public int yorum_id { get; set; }

        [Required]
        [StringLength(300)]
        public string yorum_aciklama { get; set; }

        public int yorum_kul_id { get; set; }

        public DateTime yorum_zaman { get; set; }

        public int yorum_kitap_id { get; set; }

        public virtual Kitap Kitap { get; set; }

        public virtual Kullanici Kullanici { get; set; }
    }
}
