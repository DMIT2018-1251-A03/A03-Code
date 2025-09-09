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

Albums
	.Where(x => x.AlbumId < 6)
	.Select(x => new
	{
		Album = x.Title,
		Artist = x.Artist.Name,
		Year = x.ReleaseYear,
		Label = x.ReleaseLabel,
		TrackCount = x.Tracks.Count(),		
		TotalPrice = x.Tracks.Sum(t => t.UnitPrice)		
	})
	.OrderBy(x => x.Album)
	.ToList()
	.Dump();