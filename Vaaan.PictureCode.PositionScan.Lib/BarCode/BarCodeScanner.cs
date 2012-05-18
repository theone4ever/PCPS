using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Vaaan.PictureCode.PositionScan.Lib.BarCode
{
    /// <summary>
    /// 条形码扫描类
    /// </summary>
    public class BarCodeScanner
    {
        #region 私有变量

        // 图片灰度化后保存黑白点的矩阵
        bool[,] tipMatrix;
        // 保存图片已经经过分析后的点矩阵
        bool[,] dirtyRecordMatrix;
        // 扫描图片X偏差值
        int xGap = 0;
        // 扫描图片Y偏差值
        int yGap = 0;
        List<Point> pointsInRectangle;

        #endregion

        #region 公有访问器

        private Dictionary<Point[], List<Point>> pointsInRectangleDic;

        /// <summary>
        /// 获取记录在扫瞄到的矩形中黑色点坐标
        /// </summary>
        public Dictionary<Point[], List<Point>> PointsInRectangleDic
        {
            get { return pointsInRectangleDic; }
        }

        BarCodePositionStandardConfig barCodePositionStandardConfig;

        /// <summary>
        /// 设置条形码标准参数设置
        /// </summary>
        public BarCodePositionStandardConfig BarCodePositionStandardConfig
        {
            set { barCodePositionStandardConfig = value; }
        }

        Point[] productBagCorners;

        /// <summary>
        /// 设置产品包装角坐标
        /// </summary>
        public Point[] ProductBagCorners
        {
            set { productBagCorners = value; }
        }

        private double productBagRate = 0;

        /// <summary>
        /// 获取和设置产品包装斜率
        /// </summary>
        public double ProductBagRate
        {
            get { return productBagRate; }
            set { productBagRate = value; }
        }

        private double barcodeTipOuterThreshold = 0;

        /// <summary>
        /// 获取条形码区域外部阀值
        /// </summary>
        public double BarcodeTipOuterThreshold
        {
            get { return barcodeTipOuterThreshold; }
        }

        #endregion

        /// <summary>
        /// 获取一张图中所有类似条形码范围的点集
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public Point[][] GetAllScannedRectangles(Bitmap bitmap)
        {
            int startXIndex = bitmap.Width / 5;
            int startYIndex = bitmap.Height / 5;
            return GetAllScannedRectangles(bitmap, startXIndex, bitmap.Width - startXIndex, startYIndex, bitmap.Height - startYIndex);
        }

        /// <summary>
        /// 获取一张图中在一定范围内出现条形码范围的点集
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="p"></param>
        /// <param name="p_2"></param>
        /// <param name="p_3"></param>
        /// <param name="p_4"></param>
        /// <returns></returns>
        public Point[][] GetAllScannedRectangles(Bitmap bitmap, int startXIndex, int endXIndex, int startYIndex, int endYIndex)
        {
            xGap = startXIndex;
            yGap = startYIndex;
            pointsInRectangleDic = new Dictionary<Point[], List<Point>>();
            barcodeTipOuterThreshold = GetBarCodeTipBlackColorThreshold(bitmap, startXIndex, endXIndex, startYIndex, endYIndex);
            tipMatrix = Utility.GetScannedBlackWhiteMatrix(bitmap, (ushort)barcodeTipOuterThreshold,
                startXIndex, endXIndex, startYIndex, endYIndex);
            dirtyRecordMatrix = new bool[tipMatrix.GetLength(0), tipMatrix.GetLength(1)];
            List<Point[]> result = new List<Point[]>();
            for (int y = 2; y < tipMatrix.GetLength(1) - 2; y++)
            {
                for (int x = 2; x < tipMatrix.GetLength(0) - 2; x++)
                {
                    bool isBlack = tipMatrix[x, y];
                    if (isBlack) continue;
                    // 查看该点是否是脏点
                    if (IsPointDirty(x, y))
                    {
                        dirtyRecordMatrix[x, y] = true;
                        continue;
                    }
                    // 是白色的边缘点，并且不和脏点联通
                    pointsInRectangle = new List<Point>();
                    if (x + startXIndex >= 1149 && y + startYIndex >= 810)
                    { }
                    Point[] rec = GetRectangle(x, y);
                    List<Point> pointList = new List<Point>();
                    if (rec != null && rec.Length > 0)
                    {
                        for (int i = 0; i < rec.Length; i++)
                        {
                            Point point = rec[i];
                            pointList.Add(new Point(point.X + startXIndex, point.Y + startYIndex));
                        }
                        Point[] points = pointList.ToArray();
                        if (productBagCorners == null || barCodePositionStandardConfig == null)
                        {
                            result.Add(points);
                            pointsInRectangleDic.Add(points, pointsInRectangle);
                        }
                        else if (IsPointsMatchBarCodeStandardConfig(points))
                        {
                            result.Add(points);
                            pointsInRectangleDic.Add(points, pointsInRectangle);
                        }
                    }
                }
            }
            return result.ToArray(); ;
        }

        // 判断识别到的矩形形状符合标准参数设定范围
        private bool IsPointsMatchBarCodeStandardConfig(Point[] points)
        {
            Point topestPoint = points[0];
            Point leftestPoint = points[1];
            // 距离底部高度过大过小
            double tipTopPointToProductBagBottomEdgeDistance = Utility.GetDistanceToLine(topestPoint, productBagCorners[1], productBagRate);
            if (tipTopPointToProductBagBottomEdgeDistance >
                Math.Max(productBagCorners[1].Y - productBagCorners[0].Y, productBagCorners[2].Y - productBagCorners[3].Y) * barCodePositionStandardConfig.BarCodeTipMaxBottomRate)
                return false;
            else if (tipTopPointToProductBagBottomEdgeDistance <
                Math.Min(productBagCorners[1].Y - productBagCorners[0].Y, productBagCorners[2].Y - productBagCorners[3].Y) * barCodePositionStandardConfig.BarCodeTipMinBottomRate)
                return false;
            // 距离左侧宽度过大过小
            double tipTopPointToProductBagLeftEdgeDistance = Utility.GetDistanceToLine(leftestPoint, productBagCorners[0], Utility.GetSlope(productBagCorners[0], productBagCorners[1]));
            if (tipTopPointToProductBagLeftEdgeDistance >
                Math.Max(productBagCorners[3].X - productBagCorners[0].X, productBagCorners[2].X - productBagCorners[1].X) * barCodePositionStandardConfig.BarCodeTipMaxLeftRate)
                return false;
            else if (tipTopPointToProductBagLeftEdgeDistance <
                Math.Min(productBagCorners[3].X - productBagCorners[0].X, productBagCorners[2].X - productBagCorners[1].X) * barCodePositionStandardConfig.BarCodeTipMinLeftRate)
                return false;
            return true;
        }

        // 分析图片获得条形码白色阀值
        private double GetBarCodeTipBlackColorThreshold(Bitmap bitmap, int startXIndex, int endXIndex, int startYIndex, int endYIndex)
        {
            if (productBagCorners == null || barCodePositionStandardConfig == null) return 160;
            // 如果有产品包装信息和标准设置，更精确分析阀值
            System.Diagnostics.Debug.WriteLine("AAAAAAAA");
            return 160;
        }

        // 以X,Y为起点坐标扫描白色矩形区
        private Point[] GetRectangle(int x, int y)
        {
            Point topestPoint = new Point(x, y);
            Point leftestPoint = new Point(x, y);
            Point bottomestPoint = new Point(x, y);
            Point rightestPoint = new Point(x, y);
            Point currentTempLeftestPoint = new Point(x, y);
            Point currentTempRightestPoint = new Point(x, y);
            int currentTempY = y;
            while (true)
            {
                if (currentTempY >= tipMatrix.GetLength(1)) break;
                int startIndex = currentTempLeftestPoint.X;
                // 拓展下一行左侧
                while (!tipMatrix[startIndex, currentTempY])
                    if (startIndex <= 0) break;
                    else startIndex--;
                while (tipMatrix[startIndex, currentTempY])
                    if (startIndex >= tipMatrix.GetLength(0) - 1) break;
                    else startIndex++;
                // 拓展下一行右侧
                int endIndex = currentTempRightestPoint.X;
                while (!tipMatrix[endIndex, currentTempY])
                    if (endIndex >= tipMatrix.GetLength(0) - 1) break;
                    else endIndex++;
                while (tipMatrix[endIndex, currentTempY])
                    if (endIndex <= 0) break;
                    else endIndex--;
                if (startIndex > endIndex) break;
                // 更新下一行临时变量
                currentTempLeftestPoint.X = startIndex;
                currentTempRightestPoint.X = endIndex;
                currentTempLeftestPoint.Y = currentTempY;
                currentTempRightestPoint.Y = currentTempY;
                for (int i = startIndex; i < endIndex; i++)
                {
                    if (dirtyRecordMatrix[i, currentTempY]) return null;
                    pointsInRectangle.Add(new Point(i + xGap, currentTempY + yGap));
                    dirtyRecordMatrix[i, currentTempY] = !tipMatrix[i, currentTempY];
                }
                // 查看两边的顶点是否会将极值点移动
                if (Math.Abs(Utility.GetSlope(currentTempLeftestPoint, topestPoint)) < 0.6)
                {
                    topestPoint.X = currentTempLeftestPoint.X;
                    topestPoint.Y = currentTempLeftestPoint.Y;
                }
                double lpSlope = Math.Abs(Utility.GetSlope(currentTempLeftestPoint, leftestPoint));
                if (lpSlope > 1.5 || Double.IsNaN(lpSlope))
                {
                    leftestPoint.X = currentTempLeftestPoint.X;
                    leftestPoint.Y = currentTempLeftestPoint.Y;
                }
                if (Math.Abs(Utility.GetSlope(currentTempRightestPoint, rightestPoint)) < 0.6)
                {
                    rightestPoint.X = currentTempRightestPoint.X;
                    rightestPoint.Y = currentTempRightestPoint.Y;
                }
                double bpSlope = Math.Abs(Utility.GetSlope(currentTempRightestPoint, bottomestPoint));
                if (bpSlope > 1.5 || Double.IsNaN(bpSlope))
                {
                    bottomestPoint.X = currentTempRightestPoint.X;
                    bottomestPoint.Y = currentTempRightestPoint.Y;
                }
                currentTempY++;
            }
            if (MatchBasicPointShapeDemand(topestPoint, leftestPoint, bottomestPoint, rightestPoint))
            {
                return new Point[] { topestPoint, leftestPoint, bottomestPoint, rightestPoint };
            }
            return null;
        }

        // 判断识别到的条形码四角坐标满足基础形状特征
        private bool MatchBasicPointShapeDemand(Point topestPoint, Point leftestPoint, Point bottomestPoint, Point rightestPoint)
        {
            // 判断左右对边相差不大
            double lineLeft = Utility.GetDistanceBetweenPoint(topestPoint, leftestPoint);
            double lineRight = Utility.GetDistanceBetweenPoint(rightestPoint, bottomestPoint);
            double gapLR = Math.Abs(lineLeft - lineRight);
            double maxLR = Math.Max(lineLeft, lineRight);
            if (gapLR / maxLR > 0.2) return false;
            // 判断上下对边相差不大
            double lineTop = Utility.GetDistanceBetweenPoint(topestPoint, rightestPoint);
            double lineBottom = Utility.GetDistanceBetweenPoint(leftestPoint, bottomestPoint);
            double gapTB = Math.Abs(lineTop - lineBottom);
            double maxTB = Math.Max(lineTop, lineBottom);
            if (gapTB / maxTB > 0.2) return false;
            // 判断对角线相差不大
            double lineLToB = Utility.GetDistanceBetweenPoint(topestPoint, bottomestPoint);
            double lineLToR = Utility.GetDistanceBetweenPoint(leftestPoint, rightestPoint);
            double gapLL = Math.Abs(lineLToB - lineLToR);
            double maxLL = Math.Max(lineLToB, lineLToR);
            if (gapLL / maxLL > 0.2) return false;
            // 判断最长边比最短边大
            double maxLine = Math.Max(maxLR, maxTB);
            double minLine = Math.Min(maxLR, maxTB);
            if (maxLine / minLine < 1.3 || maxLine / minLine > 3) return false;
            // 判断最短边不是太短
            if (minLine < 20) return false;
            // 如果矩形向右倾斜
            double rate = 0;
            if (lineTop < lineLeft)
                rate = Math.Abs(Utility.GetSlope(leftestPoint, topestPoint) - ProductBagRate);
            // 如果矩形向左倾斜
            else
                rate = Math.Abs(Utility.GetSlope(topestPoint, rightestPoint) - ProductBagRate);
            if (barCodePositionStandardConfig == null)
                return rate >= 1.7 || Double.IsNaN(rate);
            else
                return rate >= Math.Atan(Math.PI / 2 - barCodePositionStandardConfig.BarCodeTipMaxInclineCornerDegree * Math.PI / 180) || Double.IsNaN(rate);
        }

        // 判断处于坐标X Y的点是否是脏点
        private bool IsPointDirty(int x, int y)
        {
            return dirtyRecordMatrix[x - 1, y - 1] || dirtyRecordMatrix[x - 1, y] || dirtyRecordMatrix[x - 1, y + 1]
                 || dirtyRecordMatrix[x, y - 1] || dirtyRecordMatrix[x, y] || dirtyRecordMatrix[x, y + 1]
                  || dirtyRecordMatrix[x + 1, y - 1] || dirtyRecordMatrix[x + 1, y] || dirtyRecordMatrix[x + 1, y + 1];
        }
    }
}
