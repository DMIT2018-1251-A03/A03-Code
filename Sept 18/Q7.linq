<Query Kind="Expression">
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

Orders
	.GroupBy(o => o.Employee.EmployeeID)
	.Select(g => new
	{
		Sales = //Employees.Where(e => e.EmployeeID == g.Key)
			g.Select(e => e.Employee.FirstName + " " + e.Employee.LastName).FirstOrDefault(),
		Orders = g.Select(o => new
		{
			OrderId =o.OrderID,
			OrderDate = o.OrderDate,
			Customer = o.Customer.CompanyName
		})

		.Take(5)
	})