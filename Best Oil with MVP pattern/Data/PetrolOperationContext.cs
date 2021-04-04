using Best_Oil_with_MVP_pattern.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Best_Oil_with_MVP_pattern.Data
{
    public class PetrolOperationContext : DbContext
    {
        public PetrolOperationContext()
            :base("PetrolOperationDB")
        {
        }

        public DbSet<PetrolPaymentOperation> PetrolPaymentOperations { get; set; }

    }
}
