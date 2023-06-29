using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dogs
{
    public partial class Dogs : Form
    {
        public Dogs()
        {
            InitializeComponent();
        }

        private async void showButton_Click(object sender, EventArgs e)
        {

            string url = String.Format("https://dog.ceo/api/breed/hound/images/random");
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            var responseJson = JsonConvert.DeserializeObject<ApiResponse>(content);

            textBox.Text = responseJson.Message;

            HttpClient client2 = new HttpClient();
            var responseImg = await client2.GetAsync(textBox.Text);
            responseImg.EnsureSuccessStatusCode();
            var imageData = await responseImg.Content.ReadAsByteArrayAsync();
            var image = Image.FromStream(new System.IO.MemoryStream(imageData));
            pictureBox.Image = image;



        }

        private async void downloadButton_Click(object sender, EventArgs e)
        {
            string path = "";
            SaveFileDialog dialog = new SaveFileDialog();
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                path = dialog.FileName;
                dialog.Filter = "JPEG Image|*.jpg|PNG Image|*.png|Bitmap Image|*.bmp";
                dialog.Title = "Save an Image File";
            }
            
            using (var httpClient = new HttpClient())
            {
                var imageResponse = await httpClient.GetAsync("https://images.dog.ceo/breeds/hound-basset/n02088238_1454.jpg");

                if (imageResponse.IsSuccessStatusCode)
                {
                    var imageData = await imageResponse.Content.ReadAsByteArrayAsync();
                    
                    File.WriteAllBytes(path, imageData);
                }
            }
        }
    }

    public class ApiResponse
    {
        public string Message { get; set; }
        public string Status { get; set; }
    }

}
