<Query Kind="Statements">
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

ProductSubcategories
	.GroupBy(x => x.ProductCategory.ProductCategoryName)
	.Select(g => new
	{
		CategoryName = g.Key,
		ProductSubCategories = g.Select(sc => new
		{
			SubCategoryName = sc.ProductSubcategoryName	
		})
		.OrderBy(sc => sc.SubCategoryName)
		.ToList()
	}).OrderBy(g => g.CategoryName)
	.ToList()
	.Dump();