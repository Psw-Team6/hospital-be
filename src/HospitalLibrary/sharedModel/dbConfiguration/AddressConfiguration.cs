// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
//
// namespace HospitalLibrary.sharedModel.dbConfiguration
// {
//     public class AddressConfiguration : IEntityTypeConfiguration<Address>
//     {
//         public void Configure(EntityTypeBuilder<Address> builder)
//         {
//             _ = builder.HasKey(x => x.Id);
//
//             _ = builder.HasMany(address => address.ApplicationUsers)
//                 .WithOne(applicationUsers => applicationUsers.Address)
//                 .HasForeignKey(applicationUsers => applicationUsers.AddressId);
//
//
//         }
//     }
// }