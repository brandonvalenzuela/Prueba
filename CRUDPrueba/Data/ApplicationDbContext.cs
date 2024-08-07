using CRUDPrueba.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace CRUDPrueba.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().ToTable("Cliente");
            modelBuilder.Entity<Cliente>().HasKey(c => c.Cliente_Id);
            modelBuilder.Entity<Cliente>().Property(c => c.Cliente_Id).HasColumnName("cliente_id");
            modelBuilder.Entity<Cliente>().Property(c => c.Nombre).HasColumnName("nombre");
            modelBuilder.Entity<Cliente>().Property(c => c.Apellido).HasColumnName("apellido");
            modelBuilder.Entity<Cliente>().Property(c => c.Tipo).HasColumnName("tipo");
        }

        public List<Cliente> ReadClientesSP()
        {
            return Clientes.FromSqlRaw("exec sp_read_cliente").ToList();
        }

        public Cliente SearchClienteSP(int cliente_id)
        {
            var cliente = Clientes.FromSqlInterpolated($"exec sp_search_cliente {cliente_id}").AsEnumerable().FirstOrDefault();
            return cliente;
        }

        public void CreateClienteSP(string nombre, string apellido, string tipo)
        {
            Database.ExecuteSqlRaw("exec sp_create_cliente {0}, {1}, {2}", nombre, apellido, tipo);
        }

        public void UpdateClienteSP(int cliente_id, string nombre, string apellido, string tipo)
        {
            Database.ExecuteSqlRaw("exec sp_update_cliente {0}, {1}, {2}, {3}", cliente_id, nombre, apellido, tipo);
        }

        public void DeleteClienteSP(int cliente_id)
        {
            Database.ExecuteSqlRaw("exec sp_delete_cliente {0}", cliente_id);
        }

    }
}
