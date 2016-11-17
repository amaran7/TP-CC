using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracticalLab
{
    public class LoadFromDisk : ILoadBehaviour
    {

        private Bitmap image;

        public Bitmap loadImage(string fileName)
        {
            if(fileName != null) { 
                StreamReader streamReader = new StreamReader(fileName);
                image = (Bitmap)Bitmap.FromStream(streamReader.BaseStream);
                streamReader.Close();
                               
            }
            return image;

        }
    }
}
