using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace FCBlockchain.Models
{
    public partial class FCBlockchainContext : DbContext
    {
        public FCBlockchainContext()
        {
        }

        public FCBlockchainContext(DbContextOptions<FCBlockchainContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TransactionList> TransactionLists { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database=FCBlockchain; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TransactionList>(entity =>
            {
                entity.ToTable("TransactionList");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.ReceiveAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SendAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.ReceiverNavigation)
                    .WithMany(p => p.TransactionListReceiverNavigations)
                    .HasForeignKey(d => d.Receiver)
                    .HasConstraintName("FK__Transacti__Recei__267ABA7A");

                entity.HasOne(d => d.SenderNavigation)
                    .WithMany(p => p.TransactionListSenderNavigations)
                    .HasForeignKey(d => d.Sender)
                    .HasConstraintName("FK__Transacti__Sende__25869641");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
