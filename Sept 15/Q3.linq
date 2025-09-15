<Query Kind="Program">
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

void Main()

{

	var result = SongByPartialName("Dance");

	result.Dump();

	var title = result
	.Where(x => x.Artist
	.Contains("Black"))
	.Select(x => x);

	title.Dump();

}


public List<SongView> SongByPartialName(string name)

{

	return Tracks

	.Where(t => t.Name.Contains(name))

	.Select(t => new SongView
	{

		AlbumTitle = t.Album.Title,

		SongTitle = t.Name,

		Artist = t.Album.Artist.Name


	}).ToList();

}

//Above is an anonymous dataset because we used the word "new' after the select. If you put a word after "new" it is now a strongly typed dataset

public class SongView

{

	public string AlbumTitle { get; set; }

	public string SongTitle { get; set; }

	public string Artist { get; set; }

}
