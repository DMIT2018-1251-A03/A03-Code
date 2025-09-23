<Query Kind="Statements">
  <Connection>
    <ID>bf2de8d9-a3de-41ab-8ceb-e145025ece12</ID>
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
	.Where(x => x.AlbumId > 0)	
	.First()
	.Dump();

Albums	
	.First(x => x.AlbumId > 0)
	.Dump();

Albums
	.Where(x => x.AlbumId == 1000)
	.First()
	.Dump();
