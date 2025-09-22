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

OrderDetails
	.GroupBy(od => od.Product.ProductName)
	.Select(g => new
	{
		Product = g.Key,
		TotalQty = g.Sum(od => od.Quantity),
		TotalSales = g.Sum(od => od.Quantity * od.UnitPrice)
	}).ToList()
	.Dump();