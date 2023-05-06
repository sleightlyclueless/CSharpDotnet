using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Artist
{
    [Required]
    public int ID { get; set; }
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    [Required]
    public string ArtistName { get; set; }  // used as username

    [Required]
    public string Password { get; set; }

    /*
    public Artist(int id, string firstName, string lastName, string artistName, string password)
    {
        ID = id;
        FirstName = firstName;
        LastName = lastName;
        ArtistName = artistName;
        Password = password;
    }
    */
}