using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalLab
{
    public interface ISaveBehaviour
    {
        void save(Bitmap image, String fname, ImageFormat imgForm);
    }
}
