using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyDemos.Models;

// Add the following Nuget packages:
//  (a) Microsoft.EntityFrameworkCore.SqlServer         (ver 3.x)
//  (b) Microsoft.EntityFrameworkCore.Tools             (ver 3.x)
// NOTE: ensure that both nuget packages have the same version!

namespace MyDemos.Data
{
    public class ApplicationDbContext
         : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<MyDemos.Models.Book> Books { get; set; }

        public DbSet<MyDemos.Models.Author> Authors { get; set; }

       

    }
}