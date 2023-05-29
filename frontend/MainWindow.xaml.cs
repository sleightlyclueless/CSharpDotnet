using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Security.Cryptography;
using System.Text;
using System.Security.Policy;
using Newtonsoft.Json.Linq;

namespace frontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 


    public partial class MainWindow : Window
    {
        List<Album> loadedAlbums;
        List<Song> loadedSongs;
        List<Artist> loadedArtists;
        Artist? loggedInAs = null;
        static string querySearchSongBySubstring = "http://localhost:5073/song/getAllByTitle?title="; // Get all songs containing a substring in title
        static string querySearchArtistBySubstring = "http://localhost:5073/artist/getByName?name="; // get all artists by substr
        static string querySearchAlbumBySubstring = "http://localhost:5073/album/getByName?name=";          // get all albums by substr
        static string queryAlbumByID = "http://localhost:5073/album/getByID?id=";
        static string queryArtistByID = "http://localhost:5073/artist/getByID?id=";
        static string putArtistByID = "http://localhost:5073/artist/update?id=";
        static string querySearchArtistByName = "http://localhost:5073/artist/getByName?name=";

        public static async Task postUserToDB(string username, string firstname, string lastname, string pwhash)
        {
            var client = new HttpClient();

            JObject jobject = new JObject();
            jobject["firstName"] = firstname;
            jobject["username"] = username;
            jobject["password"] = pwhash;
            jobject["lastname"] = lastname;

            var jsonData = jobject.ToString();
            try
            {
               
                
                // Create the HTTP content with the JSON data
                HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                // Send the POST request
                HttpResponseMessage response = await client.PostAsync(putArtistByID, content);

                // Check the response status
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("POST request was successful");
                }
                else
                {
                    Console.WriteLine("POST request failed with status code: " + response.StatusCode);
                }
               
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong while logging in");
            }
        }

        // http get request to search for string
        public static async Task<string> SearchSongByString(string s)
        {

            var response = string.Empty;

            var url = querySearchSongBySubstring + s;

            var client = new HttpClient();
            try
            {
                HttpResponseMessage result = await client.GetAsync(url);
                response = await result.Content.ReadAsStringAsync();
                return response;
            }
            catch (Exception)
            {
                return "-1";
            }

        }

        public static async Task<string> SearchArtistByString(string s)
        {

            var response = string.Empty;

            var url = querySearchArtistBySubstring + s;

            var client = new HttpClient();
            try
            {
                HttpResponseMessage result = await client.GetAsync(url);
                response = await result.Content.ReadAsStringAsync();
                return response;
            }
            catch (Exception)
            {
                return "-1";
            }

        }

        public static async Task<string> SearchAlbumByString(string s)
        {

            var response = string.Empty;

            var url = querySearchAlbumBySubstring + s;

            var client = new HttpClient();
            try
            {
                HttpResponseMessage result = await client.GetAsync(url);
                response = await result.Content.ReadAsStringAsync();
                return response;
            }
            catch (Exception)
            {
                return "-1";
            }

        }
        
        // url must include final slash
        public static async Task<string> getRequestUrlOneParam(string url, string arg)
        {
            var response = string.Empty;

            var complete = url + arg;

            var client = new HttpClient();
            try
            {
                HttpResponseMessage result = await client.GetAsync(complete);
                response = await result.Content.ReadAsStringAsync();
                return response;
            }
            catch (Exception)
            {
                return "-1";
            }
        }
        
        private static Album GetAlbumFromSong(Song song)
        {
            var data = Task.Run(() => getRequestUrlOneParam(queryAlbumByID, song.albumID.ToString()));
            data.Wait();
            // Auswerten der Response
            string response = data.Result;

            Album al = JsonConvert.DeserializeObject<Album>(response);
            return al;
        }

        private static Artist getArtistByName(string username)
        {
            // Get the artist
            var data = Task.Run(() => getRequestUrlOneParam(querySearchArtistByName, username));
            data.Wait();
            // Auswerten der Response
            var response = data.Result;
            
            if(response == "-1")
            {
                return null;
            }

            List<Artist> ar = JsonConvert.DeserializeObject<List<Artist>>(response);
            return ar[0];
        }

        private static Artist GetArtistFromAlbum(Album album)
        {
            // Get the artist
            var data = Task.Run(() => getRequestUrlOneParam(queryArtistByID, album.artistID.ToString()));
            data.Wait();
            // Auswerten der Response
            var response = data.Result;

            Artist ar = JsonConvert.DeserializeObject<Artist>(response);
            return ar;
        }


        public MainWindow()
        {
            InitializeComponent();
            // add stuff
            LB_Songs.Items.Clear(); // Alle default einträge clearen
            LB_Artists.Items.Clear();
            LB_Albums.Items.Clear();

            loadedSongs = new List<Song>();
            loadedArtists = new List<Artist>();
            loadedAlbums = new List<Album>();         


            LB_Songs.ItemsSource = loadedSongs; // Den Inhalt der LB auf die liste setzen
            LB_Songs.DisplayMemberPath = "title"; // das Anzeigeattribut auf Title setzen

            // Gleiches für Album u Artist
            LB_Albums.ItemsSource = loadedAlbums;
            LB_Albums.DisplayMemberPath = "albumName";

            LB_Artists.ItemsSource = loadedArtists;
            LB_Artists.DisplayMemberPath = "artistName";

        }

        private void B_Search(object sender, RoutedEventArgs e)
        {
            string s = TB_SearchInput.Text;
            var dataSongs = Task.Run(() => SearchSongByString(s));
            dataSongs.Wait();

            var dataArtists = Task.Run(() => SearchArtistByString(s)); // sus
            dataArtists.Wait();

            var dataAlbums = Task.Run(() => SearchAlbumByString(s));
            dataAlbums.Wait();


            // Auswerten der Response
            string response = dataArtists.Result;
           
            if (string.Compare(response, "-1") == 0 || response == string.Empty || response == null) // Invalid response
            {
                MessageBox.Show("No connection to server");
            }
            else
            {
                // success
                loadedArtists = JsonConvert.DeserializeObject<List<Artist>>(response);           

                if(loadedArtists == null )
                {
                    MessageBox.Show("Artists is null.");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(loadedArtists.Count);
                    LB_Artists.ItemsSource = loadedArtists;

                }

            }


            // Albums
            response = dataAlbums.Result;
            if (string.Compare(response, "-1") == 0 || response == string.Empty || response == null) // Invalid response
            {
                MessageBox.Show("No connection to server");
            }
            else
            {
                // success
                loadedAlbums = JsonConvert.DeserializeObject<List<Album>>(response);

                if (loadedAlbums == null)
                {
                    MessageBox.Show("Albums is null.");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(loadedAlbums.Count);
                    LB_Albums.ItemsSource = loadedAlbums;

                }

            }

            // Songs
            response = dataSongs.Result;
            if (string.Compare(response, "-1") == 0 || response == string.Empty || response == null) // Invalid response
            {
                MessageBox.Show("No connection to server");
            }
            else
            {
                // success
                loadedSongs = JsonConvert.DeserializeObject<List<Song>>(response);

                if (loadedSongs == null)
                {
                    MessageBox.Show("Songs is null.");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(loadedSongs.Count);
                    LB_Songs.ItemsSource = loadedSongs;

                }

            }
        }

        private void LB_SongsSelectionChanged(object sender, RoutedEventArgs e)
        {
            Song selected = (Song)LB_Songs.SelectedItem;
            if (selected != null && selected.title != null) 
            {
            
                // Es fehlen Artist name und artist ID.. Die werden anhand des albums geholt
               
                Album album = GetAlbumFromSong(selected);
                Artist artist = GetArtistFromAlbum(album);

                SongInfo wnd = new SongInfo(selected.title, selected.length, selected.albumID,album.albumName, artist.artistName, artist.artistID);

                wnd.Show();
            }
        }

        private void LB_ArtistSelectionChanged(object sender, RoutedEventArgs e)
        {
            Artist selected = (Artist)LB_Artists.SelectedItem;
            if (selected != null && selected.artistName != null)
            {               
                ArtistInfo wnd = new ArtistInfo(selected.artistID, selected.firstName, selected.lastName, selected.artistName);

                wnd.Show();
            }
        }

        private void LB_AlbumSelectionChanged(object sender, RoutedEventArgs e)
        {
            Album selected = (Album)LB_Albums.SelectedItem;
            if (selected != null && selected.albumName != null)
            {
                Artist a = GetArtistFromAlbum(selected);

                AlbumInfo wnd = new AlbumInfo(selected.albumName,a.artistName, selected.misc, selected.albumID, selected.artistID);

                wnd.Show();
            }
        }

        private void AmogusButtonClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Amogus!");
        }

        // Login 
        private void B_Login_Click(object sender, RoutedEventArgs e)
        {
            if(loggedInAs == null) // Login
            {
                Artist foundArtist = getArtistByName(TB_Username.Text.ToString());
                if (foundArtist == null)
                {
                    MessageBox.Show("Could not find user with that name");
                    return;
                }

                using (SHA256 sha256 = SHA256.Create())
                {
                    // Convert the input string to bytes
                    byte[] inputBytes = Encoding.UTF8.GetBytes(PB_Password.Password);

                    // Compute the hash
                    byte[] hashBytes = sha256.ComputeHash(inputBytes);

                    // Convert the hash bytes to a hexadecimal string
                    string hashedInput = BitConverter.ToString(hashBytes).Replace("-", "");

                    //Clipboard.SetText(hashedInput);

                    if (hashedInput == foundArtist.password)
                    {
                        loggedInAs = foundArtist;
                        MessageBox.Show("Logged in as " + loggedInAs.artistName);
                        // Change functionality
                        B_Login.Content = "Logout";
                        TB_LoggedInAs.Text = "Logged in as " + loggedInAs.artistName;
                    }
                    else
                    {
                        MessageBox.Show("Could not auth");
                    }

                }
            }
            else // logout
            {
                loggedInAs = null;
                B_Login.Content = "Login";
                TB_LoggedInAs.Text = "";
            }

        }
    }
}
