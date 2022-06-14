using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium2.Models
{
    public class MusicDbContext : DbContext
    {
        public DbSet<Musician> Musicians { get; set; }
        public DbSet<MusicianTrack> MusicianTracks { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<MusicLabel> MusicLabels { get; set; }
        public MusicDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Musician>(e =>
            {
                e.HasKey(e => e.IdMusician);
                e.Property(e => e.FirstName).HasMaxLength(30).IsRequired();
                e.Property(e => e.LastName).HasMaxLength(50).IsRequired();
                e.Property(e => e.Nickname).HasMaxLength(20);
                e.ToTable("Musician");
            });

            modelBuilder.Entity<MusicianTrack>(e =>
            {
                e.HasKey(e => new {e.IdMusician, e.IdTrack });
                e.HasOne(e => e.Musician).WithMany(e => e.MusicianTracks).HasForeignKey(e => e.IdMusician)
                .OnDelete(DeleteBehavior.Cascade);
                e.HasOne(e => e.Track).WithMany(e => e.MusicianTracks).HasForeignKey(e => e.IdTrack)
                .OnDelete(DeleteBehavior.Cascade);
                e.ToTable("Musician_Track");
            });

            modelBuilder.Entity<Track>(e =>
            {
                e.HasKey(e => e.IdTrack);
                e.Property(e => e.TrackName).HasMaxLength(20).IsRequired();
                e.Property(e => e.Duration).IsRequired();
                e.HasOne(e => e.Album).WithMany(e => e.Tracks).HasForeignKey(e => e.IdMusicAlbum)
                .OnDelete(DeleteBehavior.Cascade);
                e.ToTable("Track");
            });

            modelBuilder.Entity<Album>(e =>
            {
                e.HasKey(e => e.IdAlbum);
                e.Property(e => e.AlbumName).HasMaxLength(30).IsRequired();
                e.Property(e => e.PublishDate).IsRequired();
                e.HasOne(e => e.MusicLabel).WithMany(e => e.Albums).HasForeignKey(e => e.IdMusicLabel)
                .OnDelete(DeleteBehavior.Cascade);
                e.ToTable("Album");
            });

            modelBuilder.Entity<MusicLabel>(e =>
            {
                e.HasKey(e => e.IdMusicLabel);
                e.Property(e => e.Name).HasMaxLength(50).IsRequired();
                e.ToTable("MusicLabel");
            });

        }
    }
}
