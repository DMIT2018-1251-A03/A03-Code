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

Products
	.Select(x => new
	{
		Name = x.ProductName,
		// with warnings
		TotalOnHand = x.Inventories.Sum(i => i.OnHandQuantity) == null ? 0
							: x.Inventories.Sum(i => i.OnHandQuantity),
		// better way of doing it
		TotalOnHand2 = x.Inventories.Count == 0 ? 0
							: x.Inventories.Sum(i => i.OnHandQuantity)
	})
	.OrderBy(x => x.Name)
	.Dump();