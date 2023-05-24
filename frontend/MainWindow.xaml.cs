using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Windows.Controls.Primitives;
using System.Collections;

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
        static string querySearchSongBySubstring = "http://localhost:5073/song/getAllByTitle?title="; // Get all songs containing a substring in title
        static string querySearchArtistBySubstring = "http://localhost:5073/artist/getByName?name="; // get all artists by substr
        static string querySearchAlbumBySubstring = "http://localhost:5073/album/getByName?name=";          // get all albums by substr
        static string queryAlbumByID = "http://localhost:5073/album/getByID?id=";
        static string queryArtistByID = "http://localhost:5073/artist/getByID?id=";
            

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
    }
}
