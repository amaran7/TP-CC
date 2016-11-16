using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PracticalLab.UI
{
    public partial class MainForm : Form, IUIManipulation
    {
        Program program;
        
        Bitmap previewBitmap;

        public MainForm()
        {
            InitializeComponent();
            program = new Program(this);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select an image file.";
            ofd.Filter = "PNG Images (*.png)|*.png|JPEG Images (*.jpg)|*.jpg";
            ofd.Filter += "|BMP Images (*.bmp)|*.bmp";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                program.load(ofd.FileName);
                
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            program.startDetection(previewBitmap);
        }

        public void display(Bitmap bitmapToDisplay)
        {
            previewBitmap = bitmapToDisplay.CopyToSquareCanvas(picPreview.Width);
            picPreview.Image = previewBitmap;

            /*
            ApplyFilter(true);
            cmbFilterList.Enabled = true;
            cmbEdgeDetection.Enabled = true;

            */
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Specify a file name and file path";
            sfd.Filter = "PNG Images (*.png)|*.png|JPEG Images (*.jpg)|*.jpg";
            sfd.Filter += "|BMP Images (*.bmp)|*.bmp";

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                program.save(null, sfd.FileName, ImageFormat.Png);
            }
        }
    }
}
