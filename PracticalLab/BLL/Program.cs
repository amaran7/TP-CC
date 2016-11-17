using PracticalLab.BLL;
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
            this.initEdgeDetection(null);
            this.initDLLManager(null);
        }

        public void setMainForm(IUIManipulation iuim)
        {
            mainForm = iuim;
        }
        public void initDLLManager(IDLLManagerBehaviour idllm)
        {
            if(idllm == null)
            {
                dllManager = new DLLManager(this);
            }
            else
            {
                dllManager = idllm;
            }
        }
        public void initEdgeDetection(IEdgeDetection ied)
        {
            if (ied == null)
            {
                edgeDetection = new EdgeDetectionLaplacian5x5(this);
            }
            else
            {
                edgeDetection = ied;
            }
        }

        public void load(string fileName)
        {
            if (fileName != "")
            {
                try
                {
                    originalImage = dllManager.loadFromDisk(fileName);
                    resultImage = originalImage;
                    applyResult(originalImage);
                }catch(Exception e)
                {
                    Console.WriteLine("load error");
                }
                
            }
        }

        public void save(Bitmap image, String filename, ImageFormat imgf)
        {
            if (resultImage != null) {
                try
                {
                    dllManager.saveToFile(resultImage, filename, imgf);
                }
                catch(Exception e)
                {
                    Console.WriteLine("save error");
                }
                
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
            {
                try { 
                    mainForm.display(image);
                }catch (Exception e)
                {
                    Console.WriteLine("display error");
                }
             }
                
        }

        public void applyDetection(Bitmap img)
        {
            try
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
                
            }catch(Exception e)
            {
                Console.WriteLine("EdgeDetection Error");
                resultImage = originalImage;
            }

            applyResult(resultImage);

        }
    }
}
