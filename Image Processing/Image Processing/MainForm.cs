using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Priority_Queue;
namespace Image_Processing
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private PixelRGB[,] ImageMatrix;
        private string path;
        private void btnChooseImage_Click(object sender, EventArgs e)
        {
            pboxResult.Image = null;
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                path = ofd.FileName;
                pboxSource.ImageLocation = path;
            }
            ImageMatrix = ImageOperations.GetPixelsFromImage(path);

        }
        private void btnRefine_Click(object sender, EventArgs e)
        {
            List<int> L = ImageOperations.GetDistinctPixels(ImageMatrix);
            List<Edge> MSTList = ImageOperations.PrimMST(L);
            float w = 0;
            foreach (var unit in MSTList)
            {
                w = w + unit.Weight;
            }
            MessageBox.Show("Distinct colors= " + L.Count.ToString() + "\nTotal weight= " + w);
            List<HashSet<int>> C = ImageOperations.Cluster2(MSTList, (int)numericUpDown1.Value);
            MessageBox.Show(C.Count.ToString());
            Dictionary<int, int> P = ImageOperations.Palette(ref C);
            ImageOperations.Quantize(ref ImageMatrix, ref P);
            pboxResult.Image = ImageOperations.ToBitmap(ImageMatrix);
            pboxResult.Image.Save(@"C:\Users\user\Desktop\Hello", System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(path))
                ImageMatrix = ImageOperations.GetPixelsFromImage(path);
        }

       
        
       
    }
}
