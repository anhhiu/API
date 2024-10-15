using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace API.Services
{
    public class ApplicationDbcontext : DbContext
    {
        public ApplicationDbcontext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Client> Clients { get; set; }
    }
}
