namespace Backend.Models;

public class Song
{
    public int ID { get; private set; }

    public string Title { get; set; }
    public int Length { get; set; }
    
    public Album SongAlbum { get; set; }
}