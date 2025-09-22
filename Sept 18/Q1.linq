<Query Kind="Statements">
  <Connection>
    <ID>e0a87a77-277f-494c-93a7-51c2205344d2</ID>
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

Albums
	//.Take(5).Dump()
	//.Where(a => a.ReleaseYear < 1980)
	.GroupBy(a => a.ReleaseYear)
	//.Take(5).Dump()
	.Select(g =>g)
	.Where(g => g.Key < 1970)
	.OrderByDescending(g => g.Key)
	.Dump();