﻿using PracticalLab.BLL;
using PracticalLab.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using PracticalLab.DLL;

namespace PracticalLab
{
    public class Program : IProgramBehaviour
    {
        private IEdgeDetection edgeDetection;
        private IDLLManagerBehaviour dllManager;
        private Bitmap originalImage, resultImage;
        private IUIManipulation mainForm;
        
        public Program(MainForm mf)
        {
            mainForm = mf;
            edgeDetection = new EdgeDetectionLaplacian5x5(this);
            dllManager = new DLLManager(this);
        }

        public void load(string fileName)
        {
            if (fileName != "")
            { 
                originalImage = dllManager.loadFromDisk(fileName);
                resultImage = originalImage;
                applyResult(originalImage);
            }
        }

        public void save(Bitmap image, String filename, ImageFormat imgf)
        {
            if (resultImage != null) {
                
                
                dllManager.saveToFile(resultImage, filename, imgf);
            }
        }

        public Bitmap getImage()
        {
            return resultImage ;
        }

        public void setImage(Bitmap img)
        {
            this.originalImage = img;
            this.resultImage = this.originalImage;
        }

        public void applyResult(Bitmap image)
        {
            if (image != null)
                mainForm.display(image);
        }

        public void applyDetection(Bitmap img)
        {
            if (img != null)
            {
                edgeDetection.startDetection(img);
            }
            else
            {
                edgeDetection.startDetection(originalImage);
            }

            resultImage = edgeDetection.getImage();
            applyResult(resultImage);
        }
    }
}
