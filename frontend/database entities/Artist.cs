using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frontend
{
    class Artist
    {
        public int artistID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string artistName { get; set; }  // used as username
        public string password { get; set; }

        public string 어멍어스 = "sus"; // 수씨 바카


        public Artist()
        {
            // Default constructor
        }

        public Artist(int iD, string firstName, string lastName, string artistName, string password)
        {
            this.artistID = iD;
            this.firstName = firstName;
            this.lastName = lastName;
            this.artistName = artistName;
            this.password = password;
        }

        public Artist(string firstName, string lastName, string artistName, string password)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.artistName = artistName;
            this.password = password;
        }
    }
}

