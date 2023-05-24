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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace frontend
{
    /// <summary>
    /// Interaktionslogik für SongInfo.xaml
    /// </summary>
    public partial class SongInfo : Window
    {
        private const string queryArtistById = "http://localhost:5073/artist/getByID?id="; // Get Artist belonging to this Song
        private const string queryAlbumById = "http://localhost:5073/album/getByID?id="; // Get Album belonging to this Song

        public string SongTitle;
        public int Duration;
        public int AlbumID;
        public string AlbumName;
        public string ArtistName;
        public int ArtistID;


        public SongInfo(string songTitle, int duration, int albumID, string albumName, string artistName, int artistID) // Can only parse primitive datatypes
        {
            InitializeComponent();

            SongTitle = songTitle;
            Duration = duration;
            AlbumID = albumID;
            AlbumName = albumName;
            ArtistName = artistName;
            ArtistID = artistID;
            

            TB_Title.Text = songTitle;
            TB_Album.Text = albumName;
            TB_Artist.Text = artistName;
            TB_Duration.Text = Duration.ToString();

        }

        private void BT_ViewAlbum(object sender, RoutedEventArgs e)
        {

            var data = Task.Run(() => MainWindow.getRequestUrlOneParam(queryAlbumById, AlbumID.ToString()));
            data.Wait();

            string response = data.Result;
            if (string.Compare(response, "-1") == 0 || response == string.Empty || response == null) // Invalid response
            {
                MessageBox.Show("No connection to server");
            }
            else
            {
                // success
                Album a = JsonConvert.DeserializeObject<Album>(response);
                if (a != null)
                {
                    // Liste Setzen
                    AlbumInfo ai = new AlbumInfo(a.albumName, this.ArtistName, a.misc, a.albumID, a.artistID);
                    ai.Show();
                }

            }
        }
        private void BT_ViewArtist(object sender, RoutedEventArgs e)
        {
            var data = Task.Run(() => MainWindow.getRequestUrlOneParam(queryArtistById, ArtistID.ToString()));
            data.Wait();

            // neues Fenster mit Info erzeugen....
            string response = data.Result;
            if (string.Compare(response, "-1") == 0 || response == string.Empty || response == null) // Invalid response
            {
                MessageBox.Show("No connection to server");
            }
            else
            {
                // success
                Artist a = JsonConvert.DeserializeObject<Artist>(response);
                if (a != null)
                {
                    // Liste Setzen
                    ArtistInfo ai = new ArtistInfo(a.artistID,a.firstName, a.lastName, a.artistName);
                    ai.Show();
                }

            }
        }
    }
}
