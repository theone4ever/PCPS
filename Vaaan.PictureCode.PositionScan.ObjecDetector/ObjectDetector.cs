//----------------------------------------------------------------------------
//  Copyright (C) 2004-2011 by EMGU. All rights reserved.       
//----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.Util;

namespace Vaaan.PictureCode.PositionScan.ObjectDetector
{
    public class ArrowSignDetector : DisposableObject
    {
        private readonly ushort _blackThreshold;

        //private Contour<Point> _defaultContour;
        
        
        private readonly MemStorage _stor;
        public Bitmap _grayImg;
        public Bitmap _canndyImg;

        public ArrowSignDetector(ushort blackThreshold)
        {
            _blackThreshold = blackThreshold;
            
            _stor = new MemStorage();
         
        }



        public Contour<Point> FindExernalDefault (Image<Bgr, byte> defaultImage, int left, int right, int top, int buttom)
        {
            double areaSize = (buttom - top)*(right - left)*0.2;
            Image<Bgr, byte> blackWhiteImg = GetWhiteBlackImage(defaultImage);
            Image<Gray, Byte> grayImg = blackWhiteImg.Convert<Gray, Byte>();
            Image<Gray, Byte> canny = grayImg.Canny(new Gray(100), new Gray(50));
           

            Contour<Point> contours = canny.FindContours(
                CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE,
                RETR_TYPE.CV_RETR_LIST, _stor);

            for (; contours != null; contours = contours.HNext)
            {
                contours.ApproxPoly(contours.Perimeter * 0.02, 0, contours.Storage);
                if (contours.Area > areaSize)
                {
                   /*defaultImage.Draw(contours.BoundingRectangle, new Bgr(Color.Aquamarine), 2);
                    CvInvoke.cvShowImage(win1, defaultImage); //Show the image
                    CvInvoke.cvWaitKey(0);  //Wait for the key pressing event
                    CvInvoke.cvDestroyWindow(win1); //Destory the window*/
                    if (contours.BoundingRectangle.Left >= left &&
                        contours.BoundingRectangle.Right <= right &&
                        contours.BoundingRectangle.Top >= top &&
                        contours.BoundingRectangle.Bottom <= buttom)
                    {
                        /*defaultImage.Draw(contours.BoundingRectangle, new Bgr(Color.Aquamarine), 2);
                        CvInvoke.cvShowImage(win1, defaultImage); //Show the image
                        CvInvoke.cvWaitKey(0);  //Wait for the key pressing event
                        CvInvoke.cvDestroyWindow(win1); //Destory the window*/
                        return contours;
                    }
                }
            }
            return null; 
        } 

        private Contour<Point> FindDefault()
        {
            var defaultImage = new Image<Bgr, byte>("best.JPG");
            Image<Bgr, byte> blackWhiteImg = GetWhiteBlackImage(defaultImage);
            Image<Gray, Byte> grayImg = blackWhiteImg.Convert<Gray, Byte>();
            Image<Gray, Byte> canny = grayImg.Canny(new Gray(100), new Gray(50));


            Contour<Point> contours = canny.FindContours(
                CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE,
                RETR_TYPE.CV_RETR_TREE, _stor);

            for (; contours != null; contours = contours.HNext)
            {
                //contours.ApproxPoly(contours.Perimeter * 0.02, 0, contours.Storage);
                if (contours.Area > 2000)
                {
                   return contours;
                }
            }

            return null;
        }


        private void FindObject(Image<Bgr, byte> img, List<Image<Gray, Byte>> stopSignList, List<Rectangle> boxList,
                                Contour<Point> contours, Contour<Point> exampleContour)
        {
            for (; contours != null; contours = contours.HNext)
            {
              //  contours.ApproxPoly(contours.Perimeter*0.02, 0, contours.Storage);
              
                if (contours.Area > 200)
                {
                    double ratio = CvInvoke.cvMatchShapes(exampleContour, contours,
                                                          CONTOURS_MATCH_TYPE.CV_CONTOUR_MATCH_I1, 0);

                    if (ratio > 0.1) //not a good match of contour shape
                    {
                        img.Draw(contours.BoundingRectangle, new Bgr(Color.Red), 2);
                        Contour<Point> child = contours.VNext;
                        if (child != null)
                            FindObject(img, stopSignList, boxList, child, exampleContour);
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


        public Bitmap DetectObject(Image<Bgr, byte> img, List<Image<Gray, Byte>> stopSignList, List<Rectangle> boxList,
                                   Image<Gray, Byte> grayImage, Contour<Point> exampleContour)
        {
            Image<Bgr, Byte> whiteBlackImg = GetWhiteBlackImage(img);
            this._grayImg = whiteBlackImg.Bitmap;

            Image<Gray, Byte> grayImg = whiteBlackImg.Convert<Gray, Byte>();

            Image<Gray, byte> canny = grayImg.Canny(new Gray(50), new Gray(100));
            this._canndyImg = canny.Bitmap;

           // Image<Gray, byte> canny = grayImg.Canny(new Gray(100), new Gray(50));

            using (var stor = new MemStorage())
            {
                Contour<Point> contours = canny.FindContours(
                    CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE,
                    RETR_TYPE.CV_RETR_TREE,
                    stor);

                if (exampleContour != null)
                {
                    FindObject(img, stopSignList, boxList, contours, exampleContour);
                }
            }
            return this._grayImg;
        }


        private Image<Bgr, Byte> GetWhiteBlackImage(Image<Bgr, byte> img)
        {
            Image<Bgr, Byte> whiteBlackImg = img.CopyBlank();
            for (int y = 0; y < img.Width; y++)
            {
                for (int x = 0; x < img.Height; x++)
                {
                    Bgr color = img[x, y];
                    if (((int) GetDistanceToWhite(color)) < _blackThreshold)
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
            return
                Math.Sqrt(Math.Pow((255 - color.Red), 2) + Math.Pow((255 - color.Green), 2) +
                          Math.Pow((255 - color.Blue), 2));
        }

        protected override void DisposeObject()
        {
            _stor.Dispose();
        }
    }
}