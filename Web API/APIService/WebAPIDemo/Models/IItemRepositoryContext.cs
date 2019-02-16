using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIDemo.Models
{
    public partial class IItemRepositoryContext : IDisposable
    {
        public void OnModelCreating(DbModelBuilder modelBuilder);
        public DbSet<ITEM> ITEMs { get; set; }
        public DbSet<PODETAIL> PODETAILs { get; set; }
        public DbSet<POMASTER> POMASTERs { get; set; }
        public DbSet<SUPPLIER> SUPPLIERs { get; set; }
        int SaveChanges();
        void MarkAsModified(ITEM item);
    }
}
