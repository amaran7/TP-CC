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
            btnApply.Enabled = false;
            btnSave.Enabled = false;

        }

        public void applyEnabler()
        {
            btnApply.Enabled = true;
        }
        
        public void saveEnabler()
        {
            btnSave.Enabled = true;
        }
        private void btnLoad_Click(object sender, EventArgs e)
        {
            //affiche la popup de chargement d'image
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select an image file.";
            ofd.Filter = "PNG Images (*.png)|*.png|JPEG Images (*.jpg)|*.jpg";
            ofd.Filter += "|BMP Images (*.bmp)|*.bmp";

            //si l'utilisateur clique sur le ok de la popup on envoie l'information à la BLL
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                program.load(ofd.FileName);
                
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            //si l'utilisateur clique sur le bouton d'application du filtre on envoie l'information à la BLL
            program.applyDetection(null);
        }

        public void display(Bitmap bitmapToDisplay, String btn)
        {
            //on affiche l'image selon la taille du canevas disponible
            previewBitmap = bitmapToDisplay.CopyToSquareCanvas(picPreview.Width);
            picPreview.Image = previewBitmap;

            switch(btn)
            {
                case "apply":
                    applyEnabler();
                    break;
                case "save":
                    saveEnabler();
                    break;
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //affiche la popup de chargement d'image
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Specify a file name and file path";
            sfd.Filter = "PNG Images (*.png)|*.png|JPEG Images (*.jpg)|*.jpg";
            sfd.Filter += "|BMP Images (*.bmp)|*.bmp";

            //si l'utilisateur clique sur le ok de la popup on envoie l'information à la BLL
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                program.save(null, sfd.FileName, ImageFormat.Png);
            }
        }
    }
}
