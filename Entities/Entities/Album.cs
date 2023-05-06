namespace Backend.Entities;

public class Album
{
    public string Name { get; set; }
    public long ReleaseDate { get; set; } // unix timestamp
    public string Misc { get; set; }

    public Artist AlbumArtist { get; set; }
}