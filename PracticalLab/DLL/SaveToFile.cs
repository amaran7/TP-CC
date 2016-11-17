using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalLab
{
    public class SaveToFile : ISaveBehaviour
    {
        public void save(Bitmap image, String filename, ImageFormat format)
        {
            try
            {
                StreamWriter streamWriter = new StreamWriter(filename, false);
                image.Save(streamWriter.BaseStream, format);
                streamWriter.Flush();
                streamWriter.Close();
            }catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
