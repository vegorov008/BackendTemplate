using Microsoft.EntityFrameworkCore;
using SampleMicroservice.Models;

namespace SampleMicroservice.Contexts
{
    public class SomeContext : DbContext
    {
        public DbSet<SomeEntity> SomeEntities { get; set; }
    }
}
