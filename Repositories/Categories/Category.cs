using App.Repositories.Products;

namespace App.Repositories.Categories
{
	public class Category : IAuditEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public List<Product>? Products { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? UpdatedDate { get; set; }
	}
}
