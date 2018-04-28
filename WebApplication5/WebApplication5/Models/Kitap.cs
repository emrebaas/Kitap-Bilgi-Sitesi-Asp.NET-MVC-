namespace WebApplication5.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Kitap")]
    public partial class Kitap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kitap()
        {
            Yorums = new HashSet<Yorum>();
        }

        [Key]
        public int kitap_id { get; set; }

        [Required]
        public string Kitap_resim { get; set; }

        [Required]
        [StringLength(50)]
        public string kitap_adi { get; set; }

        [Required]
        [StringLength(50)]
        public string kitap_yazar { get; set; }

        [Required]
        [StringLength(50)]
        public string kitap_yayinevi { get; set; }

        [Required]
        [StringLength(250)]
        public string kitap_ozet { get; set; }

        [Required]
        public string kitap_icerik { get; set; }

        public int kitap_kategori_id { get; set; }

        public int kitap_kul_id { get; set; }

        public virtual Kategori Kategori { get; set; }

        public virtual Kullanici Kullanici { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Yorum> Yorums { get; set; }
    }
}
