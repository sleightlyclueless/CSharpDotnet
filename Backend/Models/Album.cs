namespace Backend.Models;

public class Album
{
    public int ID { get; private set; }

    public string Name { get; set; }
    public long ReleaseDate { get; set; } // unix timestamp
    public string Misc { get; set; }

    public Artist AlbumArtist { get; set; }
}