using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.UI;
using Emgu.CV.Structure;
using System.Threading.Tasks;

namespace AutoPilotNet
{
    class Program
    {
        
        //DA FARE
        /*
            Ereditare image di emgu per aggiungere flag elaborato.
        */
        
        [STAThread]
        static void Main(string[] args)
        {
            VisualForm v = new VisualForm();
            v.ShowDialog();
        }
        public static void Report(String s, VisualForm v)
        {
            Console.Write(s+"\n\r");
            System.Diagnostics.Debug.WriteLine(s);
        }
    }
    
}
