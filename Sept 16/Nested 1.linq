<Query Kind="Program">
  <Connection>
    <ID>63f7df99-61e5-497e-b9e3-230360397903</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>.</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Contoso</Database>
    <DriverData>
      <LegacyMFA>false</LegacyMFA>
    </DriverData>
  </Connection>
</Query>

void Main()
{
	GetProductCategories().Dump();
}

public List<ProductCategorySummaryView> GetProductCategories()
{
	return ProductCategories
		.Select(pc => new ProductCategorySummaryView
		{
			ProductCategoryName = pc.ProductCategoryName,
			SubCategories = pc.ProductSubcategories
							.Select(psc => new ProductSubcategorySummaryView
							{
								SubCategoryName = psc.ProductSubcategoryName,
								Description = psc.ProductSubcategoryDescription
							}).ToList()
		}).ToList();		
}



#region View Models
public class ProductCategorySummaryView
{
	public string ProductCategoryName { get; set; }
	public List<ProductSubcategorySummaryView> SubCategories { get; set; }
}

public class ProductSubcategorySummaryView
{
	public string SubCategoryName { get; set; }
	public string Description { get; set; }
}

#endregion