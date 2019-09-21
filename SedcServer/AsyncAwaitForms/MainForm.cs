using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncAwaitForms
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
        }

        private async void btnGetGallery_Click(object sender, EventArgs e)
        {
            var galleryUrl = "http://off.net.mk/files/styles/1200px/public/galerija/2019/09/18/273139-kombo-fotografii.jpg";
            var combos = new OffNetDownloader(galleryUrl, 39, 65);
            await combos.ProcessGallery(@"D:\Source\SEDC\OffNet\Combos", ImageHandler);
        }

        private void ImageHandler(byte[] imageData, int index)
        {
            var rowIndex = index / 7;
            var colIndex = index % 7;
            var pb = new PictureBox
            {
                Size = new Size(200, 200),
                Location = new Point(colIndex * 210, rowIndex * 210),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            MemoryStream ms = new MemoryStream(imageData);
            pb.Image = Image.FromStream(ms);
            Controls.Add(pb);
            // Refresh();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }
    }
}
