using PracticalLab.BLL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalLab
{
    public class EdgeDetectionLaplacian5x5 : BaseEdgeDetection
    {
        Program program;
        double firstValue;
        int secondValue;
        bool grayscale;


        public EdgeDetectionLaplacian5x5(Program prog)
        {
            this.program = prog;
            this.firstValue = 1.0;
            this.secondValue = 0;
            this.grayscale = true;
            setMatrix();
        }

        public override void startDetection(Bitmap img)
        {
            applyEdgeDetectionWithConvolution(img, matrix, firstValue, secondValue, grayscale);
            
        }


        public void setMatrix()
        {
            matrix = new double[,] {
                { -1, -1, -1, -1, -1, },
                  { -1, -1, -1, -1, -1, },
                  { -1, -1, 24, -1, -1, },
                  { -1, -1, -1, -1, -1, },
                  { -1, -1, -1, -1, -1  }};
        }

        public Bitmap getImage()
        {
            return tempImage;
        }
    }

}
