using Domain.Domain_Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using Domain.Identity_Models;

namespace Repository
{
    public class ApplicationDbContext : IdentityDbContext<IntegratedSystemsUser>
    {
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<BookInShoppingCart> BookInShoppingCarts { get; set; }
        public virtual DbSet<BookInOrder> BookInOrders { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.AllBooks)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Publisher)
                .WithMany(p => p.Books)
                .HasForeignKey(b => b.PublisherId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BookInOrder>()
                .HasOne(bio => bio.Order)
                .WithMany(o => o.BooksInOrder)
                .HasForeignKey(bio => bio.OrderId)
                .OnDelete(DeleteBehavior.NoAction); 

            modelBuilder.Entity<BookInOrder>()
                .HasOne(bio => bio.Book)
                .WithMany()
                .HasForeignKey(bio => bio.BookId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BookInShoppingCart>()
                .HasOne(bisc => bisc.ShoppingCart)
                .WithMany(sc => sc.BookInShoppingCarts)
                .HasForeignKey(bisc => bisc.ShoppingCartId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookInShoppingCart>()
                .HasOne(bisc => bisc.Book)
                .WithMany()
                .HasForeignKey(bisc => bisc.BookId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Order>()
                .HasMany(o => o.BooksInOrder)
                .WithOne(bio => bio.Order)
                .HasForeignKey(bio => bio.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    
    }
}
