﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalLab.DLL
{
    public interface IDLLManagerBehaviour
    {
        void setProgram(Program p);
        void saveToFile(Bitmap image, String filename, ImageFormat format);
        Bitmap loadFromDisk(string fileName);
    }
}
