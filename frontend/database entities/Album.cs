using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace frontend
{
    class Album 
    { 
        public int albumID { get; set; }
        public string albumName { get; set; }
        public long releaseDate { get; set; } // unix timestamp
        public string misc { get; set; }
        public int artistID { get; set; }

       // public string artistName { get; set; }

        public Album(int iD, string name, long releaseDate, string misc, int aid, string artistName)
        {
            albumID = iD;
            albumName = name;
            this.releaseDate = releaseDate;
            this.misc = misc;
            artistID = aid;
           // this.artistName = artistName;
        }

   
    }
}
