using Microsoft.EntityFrameworkCore;
using EL.Domain;

namespace EL.Repository
{
    public class ElibraryContext : DbContext
    {
        public static readonly string connectionString = "server=127.0.0.1;uid=root;pwd=сдфыы123;database=elibrary_v2;port=3306;GuidFormat=Binary16;";

        public DbSet<Person> Persons { get; set; }

        public DbSet<Status> Statuses { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Author> Authors { get; set; }
        
        public DbSet<Book> Books { get; set; }
        public DbSet<PrintedBook> PrintedBooks { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<CartItem> CartItems { get; set; }


        public ElibraryContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("Persons");
            //modelBuilder.Entity<Person>().UseTptMappingStrategy();
            modelBuilder.Entity<User>().ToTable("Users",
                tableBuilder => tableBuilder.Property(user => user.Id).HasColumnName("user_id"));
            modelBuilder.Entity<Author>().ToTable("Authors",
                tableBuilder => tableBuilder.Property(author => author.Id).HasColumnName("author_id"));

            modelBuilder.Entity<User>()
            .HasOne(u => u.Status)
            .WithMany() 
            .HasForeignKey(u => u.StatusId)
            .OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<User>().Ignore(user => user.Status);
            //modelBuilder.Entity<User>().HasOne(user => user.Status).WithOne(status => status.Id);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Authors)
                .WithMany(a => a.Books)
                .UsingEntity(
                    "Books_Authors",
                    l => l.HasOne(typeof(Author)).WithMany().HasForeignKey("author_id").HasPrincipalKey(nameof(Author.Id)),
                    r => r.HasOne(typeof(Book)).WithMany().HasForeignKey("book_id").HasPrincipalKey(nameof(Book.Id)),
                    j => j.HasKey("author_id", "book_id")
                );

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Genres)
                .WithMany(g => g.Books)
                .UsingEntity(
                    "Books_Genres",
                    l => l.HasOne(typeof(Genre)).WithMany().HasForeignKey("genre_id").HasPrincipalKey(nameof(Genre.Id)),
                    r => r.HasOne(typeof(Book)).WithMany().HasForeignKey("book_id").HasPrincipalKey(nameof(Book.Id)),
                    j => j.HasKey("genre_id", "book_id")
                );

            modelBuilder.Entity<Application>()
                .HasMany(b => b.PrintedBooks)
                .WithMany(g => g.Applications)
                .UsingEntity(
                    "Applications_PrintedBooks",
                    l => l.HasOne(typeof(PrintedBook)).WithMany().HasForeignKey("printed_book_id").HasPrincipalKey(nameof(PrintedBook.Id)),
                    r => r.HasOne(typeof(Application)).WithMany().HasForeignKey("application_id").HasPrincipalKey(nameof(Application.Id)),
                    j => j.HasKey("printed_book_id", "application_id")
                );

            modelBuilder.Entity<Book>()
                .HasMany(b => b.PrintedBooks)
                .WithOne(pb => pb.Book)
                .HasForeignKey(pb => pb.BookId);

            modelBuilder.Entity<PrintedBook>()
                .HasOne(pb => pb.Publisher)
                .WithMany(p => p.PrintedBooks)
                .HasForeignKey(pb => pb.PublisherId);

            /*modelBuilder.Entity<PrintedBook>()
                .HasOne(pb => pb.Book)
                .WithMany(b => b.PrintedBooks)
                .HasForeignKey(pb => pb.BookId);*/
        }
    }
}
