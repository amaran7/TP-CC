using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalLab.DLL
{
    public class DLLManager : IDLLManagerBehaviour
    {
        public ISaveBehaviour isb;
        public ILoadBehaviour ilb;
        public Program program;

        public DLLManager(Program program)
        {
            setProgram(program);
        }

        public void setProgram(Program p)
        {
            this.program = p;
        }

        public void saveToFile(Bitmap image, String filename, ImageFormat format)
        {
            isb = new SaveToFile();

            string fileExtension = Path.GetExtension(filename).ToUpper();
            if (fileExtension == ".BMP")
            {
                format = ImageFormat.Bmp;
            }
            else if (fileExtension == ".JPG")
            {
                format = ImageFormat.Jpeg;
            }
            isb.save(image, filename, format);
        }

        public Bitmap loadFromDisk(string fileName)
        {
            try
            {
                ilb = new LoadFromDisk();
                return ilb.loadImage(fileName);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }
    }
}
