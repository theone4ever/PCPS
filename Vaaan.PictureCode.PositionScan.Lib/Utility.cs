using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Vaaan.PictureCode.PositionScan.Lib
{
    /// <summary>
    /// 工具方法类
    /// </summary>
    /// //
    public class Utility
    {
        /// <summary>
        /// 获得图中扫描到的黑白矩阵，true-黑，false-白
        /// </summary>
        /// <param name="blackColorThreshold">黑色色系阀值</param>
        /// <returns></returns>
        public static bool[,] GetScannedBlackWhiteMatrix(Bitmap bitmap, ushort blackColorThreshold)
        {
            bool[,] result2 = new bool[bitmap.Width, bitmap.Height];
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    Color color = bitmap.GetPixel(i, j);
                    result2[i, j] = GetDistanceToWhite(color) >= blackColorThreshold;
                }
            }
            return result2;
        }
        /// <summary>
        /// 获得图中扫描到的黑白矩阵，true-黑，false-白
        /// </summary>
        /// <param name="blackColorThreshold">黑色色系阀值</param>
        /// <returns></returns>
        public static bool[,] GetScannedBlackWhiteMatrix(Bitmap bitmap, ushort blackColorThreshold, int startXIndex, int endXIndex, 
            int startYIndex, int endYIndex)
        {
            bool[,] result2 = new bool[endXIndex - startXIndex, endYIndex - startYIndex];
            for (int i = startXIndex; i < endXIndex; i++)
            {
                for (int j = startYIndex; j < endYIndex; j++)
                {
                    Color color = bitmap.GetPixel(i, j);
                    result2[i - startXIndex, j - startYIndex] = GetDistanceToWhite(color) >= blackColorThreshold;
                }
            }
            return result2;
        }

        /// <summary>
        /// 获得一种颜色离标准白色的偏差
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static double GetDistanceToWhite(Color color)
        {
            return Math.Sqrt(Math.Pow((255 - color.R), 2) + Math.Pow((255 - color.G), 2) + Math.Pow((255 - color.B), 2));
        }

        /// <summary>
        /// 获取两点间距离
        /// </summary>
        /// <param name="pa"></param>
        /// <param name="pb"></param>
        /// <returns></returns>
        public static double GetDistanceBetweenPoint(Point pa, Point pb)
        {
            return Math.Sqrt(Math.Pow(pb.X - pa.X, 2) + Math.Pow(pb.Y - pa.Y, 2));
        }

        /// <summary>
        /// 获取斜率对应的角度绝对值
        /// </summary>
        /// <param name="rate">斜率</param>
        /// <returns></returns>
        public static double GetAbsInclineCorner(double rate)
        {
            return Math.Atan(rate) * 180 / Math.PI;
        }

        /// <summary>
        /// 获得两点之间的斜率
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        public static double GetSlope(Point point1, Point point2)
        {
            if (point2.X == point1.X) return Double.NaN;
            return (double)(point2.Y - point1.Y) / (point2.X - point1.X);
        }

        /// <summary>
        /// 获取两条直线的交点
        /// </summary>
        /// <param name="line1Point1">直线1 点1</param>
        /// <param name="line1Point2">直线1 点2</param>
        /// <param name="line2Point1">直线2 点1</param>
        /// <param name="line2Point2">直线2 点2</param>
        /// <returns></returns>
        public static Point GetIntersectionPoint(Point line1Point1, Point line1Point2, Point line2Point1, Point line2Point2)
        {
            double line1a = GetSlope(line1Point1, line1Point2);
            double line1b = Double.IsNaN(line1a) ? Double.NaN : line1Point1.Y - line1Point1.X * line1a;
            double line2a = GetSlope(line2Point1, line2Point2);
            double line2b = Double.IsNaN(line2a) ? Double.NaN : line2Point1.Y - line2Point1.X * line2a;
            // 平行线不相交
            if ((Double.IsNaN(line1a) && Double.IsNaN(line2a))
                || line1a == line1b) return Point.Empty;
            // 一条线斜率无穷大
            if (Double.IsNaN(line1a))
                return new Point(line1Point1.X, (int)(line1Point1.X * line2a + line2b));
            else if (Double.IsNaN(line2a))
                return new Point(line2Point1.X, (int)(line2Point1.X * line1a + line1b));
            // 计算一般情况
            double x = (line1b - line2b) / (line2a - line1a);
            double y = x * line2a + line2b;
            return new Point((int)x, (int)y);
        }

        /// <summary>
        /// 获取一个点到另一条线的垂直距离
        /// </summary>
        /// <param name="movePoint"></param>
        /// <param name="linePoint"></param>
        /// <param name="lineSlope"></param>
        /// <returns></returns>
        public static double GetDistanceToLine(Point movePoint, Point linePoint, double lineSlope)
        {
            if (Double.IsNaN(lineSlope))
                return Math.Abs(movePoint.X - linePoint.X);
            double moveSlope = Utility.GetSlope(movePoint, linePoint);
            double corner = Math.Atan(Math.Abs((moveSlope - lineSlope) / (1 + moveSlope * lineSlope)));
            double inclineDistance = Utility.GetDistanceBetweenPoint(movePoint, linePoint);
            return Math.Abs(Math.Sin(corner)) * inclineDistance;
        }
    }
}
