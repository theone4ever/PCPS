//----------------------------------------------------------------------------
//  Copyright (C) 2004-2011 by EMGU. All rights reserved.       
//----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;

namespace Vaaan.PictureCode.PositionScan.ObjectDetector
{
   public class ArrowSignDetector : DisposableObject
   {
      
      private MemStorage _octagonStorage;
       private MemStorage _stor;
      private Contour<Point> _octagon;

       private Contour<Point> _defaultContour;
      public ArrowSignDetector()
      {
         
          _octagonStorage = new MemStorage();
          _stor = new MemStorage();
          
         _octagon = new Contour<Point>(_octagonStorage);
         _octagon.PushMulti(new Point[] { 
            new Point(0,3),
            new Point(3, 0),
            new Point(3, 2),
            new Point(8, 2),
            new Point(8, 4),
            new Point(3, 4),
            new Point(3, 6)},
            Emgu.CV.CvEnum.BACK_OR_FRONT.FRONT);
          _defaultContour = FindDefault();

      }


      private Contour<Point> FindDefault()
      {
          var defaultImage = new Image<Bgr, byte>("C:/best.JPG");
          Image<Bgr, byte> blackWhiteImg = GetWhiteBlackImage(defaultImage);
          Image<Gray, Byte> grayImg = blackWhiteImg.Convert<Gray, Byte>();
          Image<Gray, Byte> canny = grayImg.Canny(new Gray(100), new Gray(50));


          Contour<Point> contours = canny.FindContours(
                                              Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE,
                                               Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_TREE, _stor);

          for (; contours != null; contours = contours.HNext)
          {
              //contours.ApproxPoly(contours.Perimeter * 0.02, 0, contours.Storage);
              if (contours.Area > 200)
              {
                  double ratio = CvInvoke.cvMatchShapes(_octagon, contours, Emgu.CV.CvEnum.CONTOURS_MATCH_TYPE.CV_CONTOURS_MATCH_I3, 0);

                  if (ratio > 0.3) continue;//not a good match of contour shape
                  else return contours;
              }
          }

          return null;
      }
     

      private void FindObject(Image<Bgr, byte> img, List<Image<Gray, Byte>> stopSignList, List<Rectangle> boxList, Contour<Point> contours)
      {
         
         for (; contours != null; contours = contours.HNext)
         {
            contours.ApproxPoly(contours.Perimeter * 0.02, 0, contours.Storage);
            if (contours.Area > 200)
            {
                double ratio = CvInvoke.cvMatchShapes(_defaultContour, contours, Emgu.CV.CvEnum.CONTOURS_MATCH_TYPE.CV_CONTOUR_MATCH_I1, 0);

               if (ratio > 0.1) //not a good match of contour shape
               {
                  Contour<Point> child = contours.VNext;
                  if (child != null)
                     FindObject(img, stopSignList, boxList, child);
                  continue;
               }

               Rectangle box = contours.BoundingRectangle;

               Image<Gray, Byte> candidate;
               using (Image<Bgr, Byte> tmp = img.Copy(box))
                  candidate = tmp.Convert<Gray, byte>();
                  boxList.Add(box);
                  stopSignList.Add(candidate);
          
           }
         }
      }

     

      public void DetectObject(Image<Bgr, byte> img, List<Image<Gray, Byte>> stopSignList, List<Rectangle> boxList, Image<Gray, Byte> grayImage)
      {
       
          Image<Bgr, Byte> whiteBlackImg = GetWhiteBlackImage(img);
         
          Image<Gray, Byte> grayImg = whiteBlackImg.Convert<Gray, Byte>();

          var canny = grayImg.Canny(new Gray(100), new Gray(50));
          grayImage = canny.Clone();
          
                   
              using (var stor = new MemStorage())
              {
                  Contour<Point> contours = canny.FindContours(
                     Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE,
                     Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_TREE,
                     stor);
                
                  FindObject(img, stopSignList, boxList, contours);
              }
             
      }

      

      private Image<Bgr, Byte> GetWhiteBlackImage(Image<Bgr, byte> img)
      {
          
          Image<Bgr, Byte> whiteBlackImg = img.CopyBlank();
            for (int y = 0; y < img.Width; y++)
            {
                for (int x = 0; x < img.Height; x++)
                {
                    Bgr color = img[x, y];
                    if (((int)GetDistanceToWhite(color)) < 190)
                    {
                        whiteBlackImg[x, y] = new Bgr(Color.Black);
                    }
                    else
                    {
                        whiteBlackImg[x, y] = new Bgr(Color.White);
                    }
                }
            }
            return whiteBlackImg;
      }

      private static double GetDistanceToWhite(Bgr color)
      {
          return Math.Sqrt(Math.Pow((255 - color.Red), 2) + Math.Pow((255 - color.Green), 2) + Math.Pow((255 - color.Blue), 2));
      }

      protected override void DisposeObject()
      {
         _octagonStorage.Dispose();
         _stor.Dispose(); 
      }
   }
}
