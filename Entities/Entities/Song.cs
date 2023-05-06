namespace Backend.Entities;

public class Song
{
    public string Title { get; set; }
    public int Length { get; set; }
    
    public Album SongAlbum { get; set; }
}