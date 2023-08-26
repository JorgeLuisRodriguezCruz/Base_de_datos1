using Microsoft.EntityFrameworkCore;
using Tarea_1.Models;

namespace Tarea_1.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions <ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<EntidadArticulo> Articulo { get; set; }

    }
}
