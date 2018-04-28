namespace WebApplication5.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Kullanici")]
    public partial class Kullanici
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kullanici()
        {
            Kitaps = new HashSet<Kitap>();
            Yorums = new HashSet<Yorum>();
        }

        [Key]
        public int kul_id { get; set; }

        [Required]
        [StringLength(20)]
        public string kullanici_adi { get; set; }

        [Required]
        [StringLength(20)]
        public string kul_sifre { get; set; }

        [Required]
        [StringLength(20)]
        public string kul_isim { get; set; }

        [Required]
        [StringLength(20)]
        public string kul_soyisim { get; set; }

        [Required]
        [StringLength(200)]
        public string kul_mail { get; set; }

        public int kul_yetki { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kitap> Kitaps { get; set; }

        public virtual Yetki Yetki { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Yorum> Yorums { get; set; }
    }
}
