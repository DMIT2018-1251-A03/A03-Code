<Query Kind="Statements">
  <Connection>
    <ID>5369a8b5-dcd6-4128-b2f6-d33725f2277b</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>.</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Chinook-2025</Database>
    <DriverData>
      <LegacyMFA>false</LegacyMFA>
    </DriverData>
  </Connection>
</Query>

//  Employee Table
//  FullName, Title, City

Employees
	//  ordering by last name
	.OrderBy(e => e.LastName)
	.Select(e => new
	{
		Name = e.FirstName + " " + e.LastName,
		Name2 = $"{e.FirstName} {e.LastName}",
		Position = e.Title,
		City = e.City
	})	
	//  ordering by name (first name)
	//.OrderBy(x => x.Name)
	.ToList()
	.Dump();