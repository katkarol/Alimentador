using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class PetFeederContext : DbContext
    {
        public DbSet<Parametros> Parametros { get; set; }
        public DbSet<Horarios> Horarios { get; set; }

        
        public PetFeederContext()
            : base(@"PetFeederDB")
        {
            var ensureDLLIsCopied =
               System.Data.Entity.SqlServer.SqlProviderServices.Instance;   
             this.Configuration.LazyLoadingEnabled = false;
        }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           Database.SetInitializer<PetFeederContext>(null);
        }
    }

   
}
