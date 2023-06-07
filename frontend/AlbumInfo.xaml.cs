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
    /// Interaction logic for AlbumInfo.xaml
    /// </summary>
    public partial class AlbumInfo : Window
    {

        private string QuerySongsFromAlbum = "http://localhost:5073/song/getAllByAlbumID?id=";
        private string QueryArtistByArtistID = "http://localhost:5073/artist/";


        public string AlbumName;
        public string ArtistName;
        public string AlbumMisc;
        public int AlbumID;
        public int ArtistID;
        //private Album thisAlbum;
        private Artist thisArtist;

        private SongInfo songInfo;
        private ArtistInfo artistInfo;

        
        private List<Song> getAlbumSongs() // Get a list of songs from the album (by id)
        {
            var data = Task.Run(() => MainWindow.getRequestUrlOneParam(QuerySongsFromAlbum, AlbumID.ToString()));
            data.Wait();

            string response = data.Result;
            if (string.Compare(response, "-1") == 0 || response == string.Empty || response == null) // Invalid response
            {
                MessageBox.Show("No connection to sus");
                return new List<Song>();
            }
            else
            {
                // success
                List<Song> sl = JsonConvert.DeserializeObject<List<Song>>(response);
                if(sl == null)
                { return new List<Song>(); }
                return sl;
            }
        }


        private Artist GetArtistByAlbumArtistID(int id) 
        {
            var data = Task.Run(() => MainWindow.getRequestUrlOneParam(QueryArtistByArtistID, id.ToString()));
            data.Wait();
            // Auswerten der Response
            var response = data.Result;

            Artist ar = JsonConvert.DeserializeObject<Artist>(response);
            return ar;
        }

        public AlbumInfo(string albumName, string artistName, string albumMisc, int albumID, int artistID)
        {
            InitializeComponent();
            

            this.AlbumName = albumName;
            this.ArtistName = artistName;
            this.AlbumMisc = albumMisc;
            this.AlbumID = albumID;
            this.ArtistID = artistID;


            this.TB_AlbumName.Text = AlbumName;
            this.TB_ArtistName.Text = ArtistName;
            this.TB_Misc.Text = AlbumMisc;
            this.TB_ID.Text = AlbumID.ToString();

            LB_Songs.ItemsSource = getAlbumSongs();
            LB_Songs.DisplayMemberPath = "title";
            thisArtist = GetArtistByAlbumArtistID(artistID);

        }

        private void LB_SongSelectionChanged(object sender, RoutedEventArgs e)
        {
            Song selected = (Song)LB_Songs.SelectedItem;
            if (selected != null && selected.title != null)
            {

                if (songInfo != null) { songInfo.Close(); }
                songInfo = new SongInfo(selected.title, selected.length, selected.albumID, AlbumName, thisArtist.artistName, thisArtist.artistID);

                songInfo.Show();
            }
        }

        private void ShowArtist(object sender, RoutedEventArgs e)
        {
            if (artistInfo != null) { artistInfo.Close(); }

            Artist a = thisArtist;

            artistInfo = new ArtistInfo(a.artistID, a.firstName, a.lastName, a.artistName);
            artistInfo.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Your code here
            if(artistInfo != null) { artistInfo.Close(); }
            if(songInfo != null) { songInfo.Close(); }
        }
    }
}

