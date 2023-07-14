using Assessment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assessment.Repository
{
    public class TransactiomConfiguration : IEntityTypeConfiguration<Transactions>
    {
        public void Configure(EntityTypeBuilder<Transactions> builder)
        {
            builder.HasOne(typeof(Account)).WithMany().OnDelete(DeleteBehavior.Restrict);

           




        }

        
    }
}
