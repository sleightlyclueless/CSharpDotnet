using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frontend
{
    class Song
    {
        public int songID { get; set; }
        public string title { get; set; }
        public int length { get; set; }    
        public int albumID { get; set; }
        public Song(int id, string title, int length, int album)
        {
            this.songID = id;
            this.title = title;
            this.length = length;
            this.albumID = album;
        }
    }
}

