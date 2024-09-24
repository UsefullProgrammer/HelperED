using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AutoPilotNet
{
    
    class SuperImage:Image<Bgr, byte>
    {
        //private bool Ebaborated = false;
        public bool Elaborated { get; set; }
    }
    //Image<TColor, TDepth> : CvArray<TDepth>, IImage, IDisposable, IInputOutputArray, IInputArrayOfArrays, IOutputArrayOfArrays, IOutputArray, IInputArray, ICloneable, IEquatable<Image<TColor, TDepth>>
    //    where TColor : struct, IColor
    //    where TDepth : new()
}
