<Query Kind="Statements">
  <Connection>
    <ID>1bfb5c87-51ed-48c7-bcc8-43c9aa2fab31</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <Server>.</Server>
    <Database>WestWind-2025-Jan-TH3</Database>
    <DisplayName>WestWind-2025-Jan-TH3</DisplayName>
    <DriverData>
      <EncryptSqlTraffic>True</EncryptSqlTraffic>
      <PreserveNumeric1>True</PreserveNumeric1>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
    </DriverData>
  </Connection>
</Query>

Products
	.GroupBy(p => p.Category.CategoryID)
	.Select(x => new
	{
		Categories = x.Key,
		Products = x.ToList()
	}).ToList()
	.Dump();

Products
	.GroupBy(p => p.Category.CategoryID)
	.Select(g => new
	{
		Categories = g.Key,
		Products = g.Select(p => new
		{
			ProductID = p.ProductID,
			ProductName = p.ProductName
		}).ToList()
	}).ToList()
	.Dump();