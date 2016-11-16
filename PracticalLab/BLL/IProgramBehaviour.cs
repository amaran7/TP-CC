﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalLab.BLL
{
    public interface IProgramBehaviour
    {
        void load(String fileName);

        void save(Bitmap image, String filename, ImageFormat format);

        void applyResult(Bitmap image);
    }
}
