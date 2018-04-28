namespace WebApplication5.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Veritabani : DbContext
    {
        public Veritabani()
            : base("name=Veritabani")
        {
        }

        public virtual DbSet<Kategori> Kategoris { get; set; }
        public virtual DbSet<Kitap> Kitaps { get; set; }
        public virtual DbSet<Kullanici> Kullanicis { get; set; }
        public virtual DbSet<Yetki> Yetkis { get; set; }
        public virtual DbSet<Yorum> Yorums { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kategori>()
                .HasMany(e => e.Kitaps)
                .WithRequired(e => e.Kategori)
                .HasForeignKey(e => e.kitap_kategori_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kitap>()
                .HasMany(e => e.Yorums)
                .WithRequired(e => e.Kitap)
                .HasForeignKey(e => e.yorum_kitap_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kullanici>()
                .Property(e => e.kul_mail)
                .IsFixedLength();

            modelBuilder.Entity<Kullanici>()
                .HasMany(e => e.Kitaps)
                .WithRequired(e => e.Kullanici)
                .HasForeignKey(e => e.kitap_kul_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kullanici>()
                .HasMany(e => e.Yorums)
                .WithRequired(e => e.Kullanici)
                .HasForeignKey(e => e.yorum_kul_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Yetki>()
                .HasMany(e => e.Kullanicis)
                .WithRequired(e => e.Yetki)
                .HasForeignKey(e => e.kul_yetki)
                .WillCascadeOnDelete(false);
        }
    }
}
