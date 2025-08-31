using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.Categories;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
	public void Configure(EntityTypeBuilder<Category> builder)
	{
		builder.HasKey(p => p.Id);
		builder.Property(p => p.Name).IsRequired().HasMaxLength(150);


	}
}
