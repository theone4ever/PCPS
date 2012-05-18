using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Vaaan.PictureCode.PositionScan.Lib.BarCode
{
    /// <summary>
    /// 条形码扫描分析类
    /// </summary>
    public class BarCodePositionAnalyser
    {
        #region 公有访问器

        private Point[][] allScannedRectangles;

        /// <summary>
        /// 获取在图中所有扫描到的图形码范围
        /// </summary>
        public Point[][] AllScannedRectangles
        {
            get { return allScannedRectangles; }
        }

        private ProductBagEdgeScanner _productBagEdgeScanner = new ProductBagEdgeScanner();

        /// <summary>
        /// 获取产品包装袋参数生成器
        /// </summary>
        public ProductBagEdgeScanner ProductBagEdgeParameterGenerator
        {
            get { return _productBagEdgeScanner; }
        }

        private BarCodeScanner _barCodeScanner = new BarCodeScanner();

        public BarCodeScanner BarCodeParameterGenerator
        {
            get { return _barCodeScanner; }
        }

        private BarCodePositionScanResultType barCodePositionScanResult = BarCodePositionScanResultType.NoBarCode;

        /// <summary>
        /// 获取条形码识别结果
        /// </summary>
        public BarCodePositionScanResultType BarCodePositionScanResult
        {
            get { return barCodePositionScanResult; }
        }

        private double barcodeTipInnerThreshold = 0;

        /// <summary>
        /// 获取条形码区域内部阀值
        /// </summary>
        public double BarcodeTipInnerThreshold
        {
            get { return barcodeTipInnerThreshold; }
        }

        #endregion

        #region 私有变量
        // 当前分析位图
        Bitmap currentScanBitmap;
        BarCodePositionStandardConfig barCodePositionStandardConfig;
        #endregion

        /// <summary>
        /// 扫描图片
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        /// hello
        public void Scan(Bitmap bitmap, BarCodePositionStandardConfig barCodePositionStandardConfig)
        {
            this.barCodePositionStandardConfig = barCodePositionStandardConfig;
            currentScanBitmap = bitmap;
            // 获得产品包装袋斜率
            Point[] productBagCorners = _productBagEdgeScanner.GetProductBagCorners(bitmap);
            if (productBagCorners == null || productBagCorners.Length == 0)
            {
                barCodePositionScanResult = BarCodePositionScanResultType.NoProductBagEdge;
                return;
            }
            // 获取条形码区域
            _barCodeScanner.ProductBagRate = _productBagEdgeScanner.ProductBagRate;
            _barCodeScanner.ProductBagCorners = productBagCorners;
            _barCodeScanner.BarCodePositionStandardConfig = barCodePositionStandardConfig;
            if (barCodePositionStandardConfig == null || productBagCorners == null)
                allScannedRectangles = _barCodeScanner.GetAllScannedRectangles(bitmap);
            else
            {
                // 计算只需要进行分析的区域
                int minX = Math.Min(productBagCorners[0].X, productBagCorners[1].X);
                int maxX = Math.Max(productBagCorners[2].X, productBagCorners[3].X);
                int minY = Math.Min(productBagCorners[0].Y, productBagCorners[3].Y);
                int maxY = Math.Max(productBagCorners[1].Y, productBagCorners[2].Y);
                double startXIndex = (maxX - minX) * barCodePositionStandardConfig.BarCodeTipMinLeftRate + minX;
                double endXIndex = (maxX - minX) * barCodePositionStandardConfig.BarCodeTipMaxLeftRate + minX;
                double startYIndex = maxY - (maxY - minY) * barCodePositionStandardConfig.BarCodeTipMaxBottomRate;
                double endYIndex = maxY - (maxY - minY) * barCodePositionStandardConfig.BarCodeTipMinBottomRate;
                allScannedRectangles = _barCodeScanner.GetAllScannedRectangles(bitmap, (int)startXIndex, (int)endXIndex, (int)startYIndex, (int)endYIndex);
            }
            if (allScannedRectangles == null || allScannedRectangles.GetLength(0) == 0)
            {
                barCodePositionScanResult = BarCodePositionScanResultType.NoBarCode;
                return;
            }
            else if (allScannedRectangles.GetLength(0) > 1)
            {
                barCodePositionScanResult = BarCodePositionScanResultType.TooManyRectangleArea;
                return;
            }
            // 判断条形码是否上下颠倒
            barCodePositionScanResult = !IsBarCodeUpSideOk() ? BarCodePositionScanResultType.BarCodeUpSideDown : BarCodePositionScanResultType.Good;
        }

        #region 判断条形码方向

        // 判断条形码是否上下方向正常
        private bool IsBarCodeUpSideOk()
        {
            Point[] rec = allScannedRectangles[0];
            List<Point> blackPoints = GetBlackPoints(_barCodeScanner.PointsInRectangleDic[rec]);
            // 确定两边顶点
            Point topestPoint = rec[0];
            Point leftestPoint = rec[1];
            Point bottomestPoint = rec[2];
            Point rightestPoint = rec[3];
            Point leftTopPoint = Point.Empty;
            Point rightTopPoint = Point.Empty;
            Point leftBottomPoint = Point.Empty;
            Point rightBottomPoint = Point.Empty;
            double lineLeft = Utility.GetDistanceBetweenPoint(topestPoint, leftestPoint);
            double lineTop = Utility.GetDistanceBetweenPoint(topestPoint, rightestPoint);
            if (lineLeft > lineTop)
            {
                leftTopPoint = topestPoint;
                rightTopPoint = rightestPoint;
                leftBottomPoint = leftestPoint;
                rightBottomPoint = bottomestPoint;
            }
            else
            {
                leftTopPoint = leftestPoint;
                rightTopPoint = topestPoint;
                leftBottomPoint = bottomestPoint;
                rightBottomPoint = rightestPoint;
            }
            // 计算两边从底到顶斜率
            double leftRate = Utility.GetSlope(leftTopPoint, leftBottomPoint);
            double rightRate = Utility.GetSlope(rightTopPoint, rightBottomPoint);
            double testDistance = Utility.GetDistanceBetweenPoint(leftTopPoint, rightTopPoint) / 6;
            int leftBlackNumber = 0;
            int rightBlackNumber = 0;
            foreach (Point blackPoint in blackPoints)
            {
                double toLeftDistance = Utility.GetDistanceToLine(blackPoint, leftTopPoint, leftRate);
                double toRightDistance = Utility.GetDistanceToLine(blackPoint, rightTopPoint, rightRate);
                if (toLeftDistance < toRightDistance && toLeftDistance <= testDistance)
                    leftBlackNumber++;
                else if (toRightDistance < toLeftDistance && toRightDistance <= testDistance)
                    rightBlackNumber++;
            }
            if (barCodePositionStandardConfig == null || barCodePositionStandardConfig.IsBarCodeTipCharAtLeft)
                return leftBlackNumber >= rightBlackNumber;
            else
                return rightBlackNumber > leftBlackNumber;
        }

        // 从矩形中获得判定属于黑色点列表
        private List<Point> GetBlackPoints(List<Point> allPointlist)
        {
            List<Point> result = new List<Point>();
            List<double> toWhiteDistanceList = new List<double>();
            // 扫描所有点，判定颜色阀值
            foreach (Point tempPoint in allPointlist)
            {
                Color color = currentScanBitmap.GetPixel(tempPoint.X, tempPoint.Y);
                double distance = Utility.GetDistanceToWhite(color);
                if (!toWhiteDistanceList.Contains(distance))
                    toWhiteDistanceList.Add(distance);
            }
            toWhiteDistanceList.Sort();
            barcodeTipInnerThreshold = toWhiteDistanceList[toWhiteDistanceList.Count / 2];
            foreach (Point tempPoint in allPointlist)
            {
                Color color = currentScanBitmap.GetPixel(tempPoint.X, tempPoint.Y);
                double distance = Utility.GetDistanceToWhite(color);
                if (distance >= barcodeTipInnerThreshold)
                {
                    result.Add(new Point(tempPoint.X, tempPoint.Y));
                }
            }
            return result;
        }

        #endregion
    }
}
