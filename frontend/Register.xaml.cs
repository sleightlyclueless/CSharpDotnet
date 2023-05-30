using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Channels;
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
    /// Interaktionslogik für Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private async void Register_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve the values from the input fields
            string artistname = ArtistNameTB.Text;
            string firstname = FirstNameTB.Text;
            string lastname = LastNameTB.Text;
            string password = PasswordTB.Password;

            if (
                string.IsNullOrWhiteSpace(artistname)
                || string.IsNullOrWhiteSpace(firstname)
                || string.IsNullOrWhiteSpace(lastname)
                || string.IsNullOrWhiteSpace(password)
            )
            {
                MessageBox.Show("Please fill out all fields.");
                return; // Stop further execution
            }
            await PostUserToDB(artistname, firstname, lastname, password);
        }

        public static async Task PostUserToDB(
            string artistname,
            string firstname,
            string lastname,
            string password
        )
        {
            try
            {
                // Hash the password using SHA256
                string hashedPassword = HashPassword(password);

                // Create the Artist object with the provided details
                Artist artist = new Artist(artistname, firstname, lastname, hashedPassword);

                // Serialize the Artist object to JSON
                string jsonData = JsonConvert.SerializeObject(artist);

                // Create the HttpClient instance
                using (HttpClient client = new HttpClient())
                {
                    // Set the request URL
                    string requestUrl = "http://localhost:5073/artist";

                    // Create the HTTP content with the JSON data
                    HttpContent content = new StringContent(
                        jsonData,
                        Encoding.UTF8,
                        "application/json"
                    );

                    // Send the POST request
                    HttpResponseMessage response = await client.PostAsync(requestUrl, content);

                    // Check the response status
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("User created!");
                    }
                    else
                    {
                        MessageBox.Show(
                            "POST request failed with status code: " + response.StatusCode
                        );
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong while posting user data to the database");
            }
        }

        private static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convert the input string to bytes
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);

                // Compute the hash
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                // Convert the hash bytes to a hexadecimal string
                string hashedInput = BitConverter.ToString(hashBytes).Replace("-", "");

                return hashedInput;
            }
        }
    }
}
