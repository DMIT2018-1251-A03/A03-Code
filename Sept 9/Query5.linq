<Query Kind="Statements">
  <Connection>
    <ID>82b3fd26-c0df-49c0-8a56-b05c8f79c19a</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>.</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>WestWind-2024</Database>
    <DriverData>
      <LegacyMFA>false</LegacyMFA>
    </DriverData>
  </Connection>
</Query>

Customers

	.Select(x => new
	{
		Name = x.CompanyName,
		Country = x.Country,
		FaxNormal = x.Fax,
		Fax = x.Fax == null  ? "Unknown": x.Fax
	})
.OrderByDescending(x => x.Fax)	
	.ToList()
	.Dump();