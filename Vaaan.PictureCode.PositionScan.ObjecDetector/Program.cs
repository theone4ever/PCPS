//----------------------------------------------------------------------------
//  Copyright (C) 2004-2011 by EMGU. All rights reserved.       
//----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Vaaan.PictureCode.PositionScan.ObjectDetector
{
   class Program
   {

     
           /// <summary>
           /// 应用程序的主入口点。
           /// </summary>
           [STAThread]
           static void Main()
           {
               Application.EnableVisualStyles();
               Application.SetCompatibleTextRenderingDefault(false);
               Application.Run(new MainForm());
           
       }
      //static void Main(string[] args)
      //{
      //   if (!IsPlaformCompatable()) return;

         
      //   var ObjectList = new List<Image<Gray, byte>>();
      //   var ObjectBoundList = new List<Rectangle>();
      
      //   var arrowSignDetector = new ArrowSignDetector();

      //   var image = new Image<Bgr, byte>("C:/1-2.jpg");
      //    Image<Gray, Byte> grayImg = null;
      //    arrowSignDetector.DetectObject(image, ObjectList, ObjectBoundList, grayImg);
          
         

      //   for (int i = 0; i < ObjectList.Count; i++)
      //   {
      //       Rectangle rect = ObjectBoundList[i];
      //       image.Draw(rect, new Bgr(Color.Aquamarine), 2);
      //   }
          
      //   String win1 = "Test Window" ; //The name of the window
      //   CvInvoke.cvShowImage(win1, image); //Show the image
      //   CvInvoke.cvWaitKey(0);  //Wait for the key pressing event
      //   CvInvoke.cvDestroyWindow(win1); //Destory the window
         

       
      //}

      /// <summary>
      /// Check if both the managed and unmanaged code are compiled for the same architecture
      /// </summary>
      /// <returns>Returns true if both the managed and unmanaged code are compiled for the same architecture</returns>
      static bool IsPlaformCompatable()
      {
         int clrBitness = Marshal.SizeOf(typeof(IntPtr)) * 8;
         if (clrBitness != CvInvoke.UnmanagedCodeBitness)
         {
            MessageBox.Show(String.Format("Platform mismatched: CLR is {0} bit, C++ code is {1} bit."
               + " Please consider recompiling the executable with the same platform target as C++ code.",
               clrBitness, CvInvoke.UnmanagedCodeBitness));
            return false;
         }
         return true;
      }
   }
}
