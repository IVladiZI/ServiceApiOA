﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Xml.Schema;

namespace Persistence.Configuration
{
    /// <ClientConfig>
    /// This class contains the configuration for the client and will map all the characteristics of the class to the database.
    /// </ClientConfig>
    public class ClientConfig : IEntityTypeConfiguration<Client>
    {
        /// <Configure>
        /// This method contains the characteristics used in the database, string size is placed as well as whether 
        /// it is required or not, this will mark if it is null or not, all those characteristics that the table and 
        /// its columns will have are placed here.
        /// </Configure>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Client")
                .HasOne(x=>x.User)
                .WithOne(x=>x.Client)
                .HasPrincipalKey<Client>(x=>x.ClientId)
                .HasForeignKey<User>(x=>x.UserId);
            builder.HasKey(x => x.ClientId);
            builder.Property(x => x.Name)
                .HasMaxLength(80)
                .IsRequired();
            builder.Property(x => x.Lastname)
                .HasMaxLength(80)
                .IsRequired();
            builder.Property(x => x.SecondLastName)
                .HasMaxLength(80)
                .IsRequired();
            builder.Property(x => x.Birthday)
                .HasMaxLength(80)
                .IsRequired();
            builder.Property(x => x.Email)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(x => x.Addres)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(x => x.CreateUser)
                .HasMaxLength(30);
            builder.Property(x => x.ModifyUser)
                .HasMaxLength(30);
        }
    }
}
