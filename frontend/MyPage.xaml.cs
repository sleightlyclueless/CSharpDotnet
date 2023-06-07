using Newtonsoft.Json;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace frontend
{
    /// <summary>
    /// Interaction logic for ArtistInfo.xaml
    /// </summary>
    public partial class MyPage : Window
    {
        
        private const string querySongsByArtistID = "http://localhost:5073/song/getAllByArtistID?id="; // Get all songs by artist ID
        private const string queryAlbumsByArtistID = "http://localhost:5073/album/getAllByArtistID?id="; // Get all albums by artist ID
        private const string queryAlbumByID = "http://localhost:5073/album/"; // Get album by its id
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string artistName { get; set; }

        AlbumInfo albumInfo;
        SongInfo songInfo;
       

        Album getAlbumByID(int albumId)
        {
            // albumById - localhost addr
            var data = Task.Run(() => MainWindow.getRequestUrlOneParam(queryAlbumByID, albumId.ToString()));
            data.Wait();
            // Auswerten der Response
            string response = data.Result;

            Album al = JsonConvert.DeserializeObject<Album>(response);
            return al;
        }

        List<Album> getAlbumsFromArtist()
        {
            var data = Task.Run(() => MainWindow.getRequestUrlOneParam(queryAlbumsByArtistID, id.ToString()));
            data.Wait();

            string response = data.Result;
            if (string.Compare(response, "-1") == 0 || response == string.Empty || response == null) // Invalid response
            {
                MessageBox.Show("No connection to server");
                return new List<Album>();
            }
            else
            {
                // success
                List<Album> al = JsonConvert.DeserializeObject<List<Album>>(response);
                if (al == null)
                { return new List<Album>(); }
                return al;
            }
        }

        List<Song> getSongsFromArtist()
        {
            var data = Task.Run(() => MainWindow.getRequestUrlOneParam(querySongsByArtistID, id.ToString()));
            data.Wait();

            string response = data.Result;
            if (string.Compare(response, "-1") == 0 || response == string.Empty || response == null) // Invalid response
            {
                MessageBox.Show("No connection to server");
                return new List<Song>();
            }
            else
            {
                // success
                List<Song> al = JsonConvert.DeserializeObject<List<Song>>(response);
                if (al == null)
                { return new List<Song>(); }
                return al;
            }
        }
       
        public MyPage(int id, string firstName, string lastName, string artistName)
        {
            InitializeComponent();

            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.artistName = artistName;
          
            TB_ID.Text = id.ToString();
            TB_Fname.Text = firstName;
            TB_Lname.Text = lastName;
            TB_ArtistName.Text = artistName;

            LB_Albums.ItemsSource = getAlbumsFromArtist();
            LB_Albums.DisplayMemberPath = "albumName";

            LB_Songs.ItemsSource = getSongsFromArtist();
            LB_Songs.DisplayMemberPath = "title";

            
        }

        private void LB_SongSelectionChanged(object sender, RoutedEventArgs e)
        {
            Song selected = (Song)LB_Songs.SelectedItem;
            if (selected != null && selected.title != null)
            {
               
                Album albumFromSong = getAlbumByID(selected.albumID);

                if (songInfo != null) { songInfo.Close(); }

                songInfo = new SongInfo(selected.title, selected.length, selected.albumID, albumFromSong.albumName, artistName, id);

                songInfo.Show();
            }
        }

        private void LB_AlbumSelectionChanged(object sender, RoutedEventArgs e)
        {
            Album selected = (Album)LB_Albums.SelectedItem;
            if (selected != null && selected.albumName != null)
            {

                if(albumInfo != null) { albumInfo.Close(); }

                albumInfo = new AlbumInfo(selected.albumName, artistName, selected.misc, selected.albumID, id);

                albumInfo.Show();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Your code here
            // This event is fired when the window is being closed, but it can be canceled by setting e.Cancel = true
            if(albumInfo != null)
            {
                albumInfo.Close();
            }
            if(songInfo != null)
            { 
                songInfo.Close(); 
            }

        }

    }
}

