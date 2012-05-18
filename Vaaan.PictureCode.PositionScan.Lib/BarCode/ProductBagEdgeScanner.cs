using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Vaaan.PictureCode.PositionScan.Lib.BarCode
{
    /// <summary>
    /// 包装袋边缘扫描类
    /// </summary>
    public class ProductBagEdgeScanner
    {
        private double productBagRate = 0;

        /// <summary>
        /// 获取在图中所有扫描到的包装袋斜率
        /// </summary>
        public double ProductBagRate
        {
            get { return productBagRate; }
        }

        private double productBagLeftThreshold = 0;

        /// <summary>
        /// 获取包装袋左侧阀值
        /// </summary>
        public double ProductBagLeftThreshold
        {
            get { return productBagLeftThreshold; }
        }

        private double productBagRightThreshold = 0;

        /// <summary>
        /// 获取包装袋右侧阀值
        /// </summary>
        public double ProductBagRightThreshold
        {
            get { return productBagRightThreshold; }
        }

        /// <summary>
        /// 根据产品图片获得产品底部边缘斜率
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public double GetProductBagBottomEdgeSlope(Bitmap bitmap)
        {
            int xGap = bitmap.Width / 10;
            int yGap = bitmap.Height / 3;
            // 获得产品轮廓左下角坐标
            Point productLeftDownCornerPoint =
                GetProductBagEdgePoint(bitmap, xGap * 2, xGap * 3, yGap * 2, yGap * 3, out productBagLeftThreshold, 3);
            // 获得产品轮廓右下角坐标
            Point productRightDownCornerPoint =
                GetProductBagEdgePoint(bitmap, xGap * 7, xGap * 8, yGap * 2, yGap * 3, out productBagRightThreshold, 3);
            productBagRate = Utility.GetSlope(productLeftDownCornerPoint, productRightDownCornerPoint);
            return productBagRate;
        }

        /// <summary>
        /// 根据产品图片获得产品包装角
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public Point[] GetProductBagCorners(Bitmap bitmap)
        {
            double thresHold = 0;
            // 获取上下边轮廓点
            int xGap = bitmap.Width / 10;
            int yGap = bitmap.Height / 2;
            // 底部点扫描并修补
            Point productBottomPoint1 =
                GetProductBagEdgePoint(bitmap, xGap * 2, xGap * 3, yGap, yGap * 2, out productBagLeftThreshold, 3);
            if (productBottomPoint1 == Point.Empty)
                productBottomPoint1 =
                GetProductBagEdgePoint(bitmap, xGap * 4, xGap * 5, yGap, yGap * 2, out productBagLeftThreshold, 3);
            Point productBottomPoint2 =
                GetProductBagEdgePoint(bitmap, xGap * 7, xGap * 8, yGap, yGap * 2, out productBagRightThreshold, 3);
            if (productBottomPoint2 == Point.Empty)
                productBottomPoint2 =
                GetProductBagEdgePoint(bitmap, xGap * 5, xGap * 6, yGap, yGap * 2, out productBagRightThreshold, 3);
            // 顶部点扫描并修补
            Point productTopPoint1 =
                GetProductBagEdgePoint(bitmap, xGap * 2, xGap * 3, 0, yGap, out thresHold, 1);
            if (productTopPoint1 == Point.Empty)
                productTopPoint1 =
                GetProductBagEdgePoint(bitmap, xGap * 4, xGap * 5, 0, yGap, out thresHold, 1);
            Point productTopPoint2 =
                GetProductBagEdgePoint(bitmap, xGap * 7, xGap * 8, 0, yGap, out thresHold, 1);
            if (productTopPoint2 == Point.Empty)
                productTopPoint2 =
                GetProductBagEdgePoint(bitmap, xGap * 5, xGap * 6, 0, yGap, out thresHold, 1);
            productBagRate = Utility.GetSlope(productBottomPoint1, productBottomPoint2);
            // 获取左右轮廓点
            Point left1Point = Utility.GetIntersectionPoint(productTopPoint1, productTopPoint2, new Point(0, 0), new Point(0, 1));
            Point left2Point = Utility.GetIntersectionPoint(productBottomPoint1, productBottomPoint2, new Point(0, 0), new Point(0, 1));
            Point right1Point = Utility.GetIntersectionPoint(productTopPoint1, productTopPoint2, new Point(bitmap.Width - 1, 0), new Point(bitmap.Width - 1, 1));
            Point right2Point = Utility.GetIntersectionPoint(productBottomPoint1, productBottomPoint2, new Point(bitmap.Width - 1, 0), new Point(bitmap.Width - 1, 1));
            xGap = bitmap.Width / 3;
            yGap = (left2Point.Y - left1Point.Y) / 10;
            // 左侧点扫描并修补
            Point productLeftPoint1 =
                GetProductBagEdgePoint(bitmap, 0, xGap, left1Point.Y + yGap * 3, left1Point.Y + yGap * 4, out thresHold, 2);
            if(productLeftPoint1==Point.Empty)
                productLeftPoint1 =
                GetProductBagEdgePoint(bitmap, 0, xGap, left1Point.Y + yGap * 4, left1Point.Y + yGap * 5, out thresHold, 2);
            Point productLeftPoint2 =
                GetProductBagEdgePoint(bitmap, 0, xGap, left1Point.Y + yGap * 7, left1Point.Y + yGap * 8, out thresHold, 2);
            if (productLeftPoint2 == Point.Empty)
                productLeftPoint2 =
                GetProductBagEdgePoint(bitmap, 0, xGap, left1Point.Y + yGap * 6, left1Point.Y + yGap * 7, out thresHold, 2);
            // 右侧点扫描并修补
            yGap = (right2Point.Y - right1Point.Y) / 10;
            Point productRightPoint1 =
                GetProductBagEdgePoint(bitmap, xGap * 2, xGap * 3, right1Point.Y + yGap * 3, right1Point.Y + yGap * 4, out thresHold, 4);
            if (productRightPoint1 == Point.Empty)
                productRightPoint1 =
                GetProductBagEdgePoint(bitmap, 0, xGap, right1Point.Y + yGap * 4, right1Point.Y + yGap * 5, out thresHold, 4);
            Point productRightPoint2 =
                GetProductBagEdgePoint(bitmap, xGap * 2, xGap * 3, right1Point.Y + yGap * 7, right1Point.Y + yGap * 8, out thresHold, 4);
            if (productRightPoint2 == Point.Empty)
                productRightPoint2 =
                GetProductBagEdgePoint(bitmap, 0, xGap, right1Point.Y + yGap * 6, right1Point.Y + yGap * 7, out thresHold, 4);
            // 判断并返回角落坐标
            if (productBottomPoint1 == Point.Empty || productBottomPoint2 == Point.Empty || productTopPoint1 == Point.Empty || productTopPoint2 == Point.Empty
                 || productLeftPoint1 == Point.Empty || productLeftPoint2 == Point.Empty || productRightPoint1 == Point.Empty || productRightPoint2 == Point.Empty)
                return null;
            return new Point[] 
            { 
                Utility.GetIntersectionPoint(productTopPoint1, productTopPoint2, productLeftPoint1, productLeftPoint2), 
                Utility.GetIntersectionPoint(productBottomPoint1, productBottomPoint2, productLeftPoint1, productLeftPoint2), 
                Utility.GetIntersectionPoint(productBottomPoint1, productBottomPoint2, productRightPoint1, productRightPoint2), 
                Utility.GetIntersectionPoint(productTopPoint1, productTopPoint2, productRightPoint1, productRightPoint2)
            };
        }

        /// <summary>
        /// 获取图片中的产品包装边缘点
        /// </summary>
        /// <param name="bitmap">产品图片</param>
        /// <param name="startXIndex">起始X</param>
        /// <param name="endXIndex">结束X</param>
        /// <param name="startYIndex">起始Y</param>
        /// <param name="endYIndex">结束Y</param>
        /// <param name="thresholdResult">自动计算并传出的阀值</param>
        /// <param name="direction">扫描方向，1-上边缘，2-左边缘，3-下边缘，4-右边缘</param>
        /// <returns></returns>
        private Point GetProductBagEdgePoint(Bitmap bitmap, int startXIndex, int endXIndex,
            int startYIndex, int endYIndex, out double thresholdResult, int direction)
        {
            // 根据图片情况动态获取灰度阀值，在一条线上取N个随机点的白色偏差
            double testPointNumber = 80;
            int middleXIndex = (endXIndex + startXIndex) / 2;
            int middleYIndex = (endYIndex + startYIndex) / 2;
            int scanStartXIndex = 0, scanStartYIndex = 0, tempXIndex = 0, tempYIndex = 0;
            double xGap = 0, yGap = 0;
            switch (direction)
            {
                case 1:
                    {
                        scanStartYIndex = startYIndex;
                        tempYIndex = endYIndex;
                        scanStartXIndex = middleXIndex;
                        tempXIndex = middleXIndex;
                        xGap = 0;
                        yGap = (startYIndex - endYIndex) / testPointNumber;
                        break;
                    }
                case 2:
                    {
                        scanStartYIndex = middleYIndex;
                        tempYIndex = middleYIndex;
                        scanStartXIndex = startXIndex;
                        tempXIndex = endXIndex;
                        xGap = (startXIndex - endXIndex) / testPointNumber;
                        yGap = 0;
                        break;
                    }
                case 3:
                    {
                        scanStartYIndex = startYIndex;
                        tempYIndex = startYIndex;
                        scanStartXIndex = middleXIndex;
                        tempXIndex = middleXIndex;
                        xGap = 0;
                        yGap = (endYIndex - startYIndex) / testPointNumber;
                        break;
                    }
                case 4:
                    {
                        scanStartYIndex = middleYIndex;
                        tempYIndex = middleYIndex;
                        scanStartXIndex = startXIndex;
                        tempXIndex = startXIndex;
                        xGap = (endXIndex - startXIndex) / testPointNumber;
                        yGap = 0;
                        break;
                    }
            }
            double thresHold = 0.0;
            double currentMaxGap = 120;
            for (int i = 0; i < testPointNumber; i++)
            {
                int currentX = (int)(tempXIndex + xGap * i);
                int currentY = (int)(tempYIndex + yGap * i);
                Color color = bitmap.GetPixel(currentX, currentY);
                double distance = Utility.GetDistanceToWhite(color);
                if (thresHold>0 && distance - thresHold > currentMaxGap)
                {
                    currentMaxGap = distance - thresHold;
                    thresHold = distance;
                }
                else if (thresHold == 0 || (Math.Abs(distance - thresHold) < 120 && distance < thresHold))
                    thresHold = distance;
                //thresHold = thresHold == 0 || (distance - thresHold > 120 && thresHold < 250) || (Math.Abs(distance - thresHold) < 120 && distance < thresHold) ? distance : thresHold;
            }
            thresholdResult = thresHold;
            // 重新转换黑白矩阵，计算边缘点区域
            bool[,] productBagRateMatrix = Utility.GetScannedBlackWhiteMatrix(bitmap, (ushort)thresHold,
                scanStartXIndex, endXIndex, scanStartYIndex, endYIndex);
            Point currentScanPoint = Point.Empty;
            bool isLastPointWhite = false;
            int calculateAllWhitePoint = 0;
            while (true)
            {
                currentScanPoint = GetNextScanPoint(currentScanPoint, productBagRateMatrix, direction);
                if (currentScanPoint == Point.Empty) break;
                bool isBlack = productBagRateMatrix[currentScanPoint.X, currentScanPoint.Y];
                if (!isBlack)
                {
                    if (isLastPointWhite && ++calculateAllWhitePoint >= 20)
                    {
                        int x = currentScanPoint.X + scanStartXIndex;
                        int y = scanStartYIndex + currentScanPoint.Y;
                        if (direction == 1)
                            y -= 20;
                        else if (direction == 2)
                            x -= 20;
                        else if (direction == 3)
                            y += 20;
                        else if (direction == 4)
                            x += 20;
                        Point productCornerPoint = new Point(x, y);
                        return productCornerPoint;
                    }
                    isLastPointWhite = true;
                }
                else
                {
                    isLastPointWhite = false;
                    calculateAllWhitePoint = 0;
                }
            }
            return Point.Empty;
        }

        // 获取下一个扫描点坐标
        private Point GetNextScanPoint(Point currentScanPoint, bool[,] productBagRateMatrix, int direction)
        {
            // 如果是第一个点
            if (currentScanPoint == Point.Empty)
            {
                if (direction == 1 || direction == 2)
                {
                    return new Point(1, 1);
                }
                else if (direction == 3)
                {
                    return new Point(0, productBagRateMatrix.GetLength(1) - 1);
                }
                else if (direction == 4)
                {
                    return new Point(productBagRateMatrix.GetLength(0) - 1, 0);
                }
            }
            // 如果不是第一个点
            if (direction == 1)
            {
                if (currentScanPoint.X == productBagRateMatrix.GetLength(0) - 1
                    && currentScanPoint.Y == productBagRateMatrix.GetLength(1) - 1)
                    return Point.Empty;
                else if (currentScanPoint.Y == productBagRateMatrix.GetLength(1) - 1)
                    return new Point(currentScanPoint.X + 1, 0);
                else
                    return new Point(currentScanPoint.X, currentScanPoint.Y + 1);
            }
            else if (direction == 2)
            {
                if (currentScanPoint.X == productBagRateMatrix.GetLength(0) - 1
                    && currentScanPoint.Y == productBagRateMatrix.GetLength(1) - 1)
                    return Point.Empty;
                else if (currentScanPoint.X == productBagRateMatrix.GetLength(0) - 1)
                    return new Point(0, currentScanPoint.Y + 1);
                else
                    return new Point(currentScanPoint.X + 1, currentScanPoint.Y);
            }
            else if (direction == 3)
            {
                if (currentScanPoint.X == productBagRateMatrix.GetLength(0) - 1
                    && currentScanPoint.Y == 0)
                    return Point.Empty;
                else if (currentScanPoint.Y == 0)
                    return new Point(currentScanPoint.X + 1, productBagRateMatrix.GetLength(1) - 1);
                else
                    return new Point(currentScanPoint.X, currentScanPoint.Y - 1);
            }
            else if (direction == 4)
            {
                if (currentScanPoint.X == 0
                    && currentScanPoint.Y == productBagRateMatrix.GetLength(1) - 1)
                    return Point.Empty;
                else if (currentScanPoint.X == 0)
                    return new Point(productBagRateMatrix.GetLength(0) - 1, currentScanPoint.Y + 1);
                else
                    return new Point(currentScanPoint.X - 1, currentScanPoint.Y);
            }
            return Point.Empty;
        }
    }
}
