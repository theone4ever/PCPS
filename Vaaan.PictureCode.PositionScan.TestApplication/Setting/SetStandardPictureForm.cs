using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Vaaan.PictureCode.PositionScan.Lib.BarCode;
using System.IO;
using Vaaan.PictureCode.PositionScan.Lib;

namespace Vaaan.PictureCode.PositionScan.TestApplication.Setting
{
    public partial class SetStandardPictureForm : Form
    {
        public SetStandardPictureForm()
        {
            InitializeComponent();
            if (File.Exists(GetConfigPictureFilePath()) && File.Exists(GetConfigSaveFilePath()))
            {
                File.Delete(GetEditConfigPictureFilePath());
                File.Copy(GetConfigPictureFilePath(), GetEditConfigPictureFilePath());
                currentStandardPictureFilePath = GetEditConfigPictureFilePath();
                currentStandardConfig = BarCodePositionStandardConfig.Deserialize(File.ReadAllText(GetConfigSaveFilePath()));
                cbCharAt.SelectedIndex = currentStandardConfig.IsBarCodeTipCharAtLeft ? 0 : 1;
                Image image = Bitmap.FromFile(currentStandardPictureFilePath);
                AnalyseProductBagEdge(image);
                selectBarCodeRangeStartPoint = currentStandardConfig.SelectBarCodeRangeCorners[0];
                selectBarCodeRangeEndPoint = currentStandardConfig.SelectBarCodeRangeCorners[1];
                AnalyseBarCodeTip();
            }
        }

        BarCodePositionStandardConfig currentStandardConfig;

        /// <summary>
        /// 获取条形码位置标准设置信息
        /// </summary>
        public BarCodePositionStandardConfig CurrentStandardConfig
        {
            get { return currentStandardConfig; }
        }

        Bitmap bitmap;
        Point[] productBagCornerPoints;
        Point[][] allScannedBarCodeTipRectangles;
        Point selectBarCodeRangeStartPoint = Point.Empty;
        Point selectBarCodeRangeEndPoint = Point.Empty;
        int brushWidth = 2;
        double xRate;
        double yRate;
        string currentStandardPictureFilePath;

        #region 设置保存载入功能

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 校验
            if (productBagCornerPoints == null)
            {
                MessageBox.Show("未加载可以识别边框的产品图片");
                return;
            }
            if (selectBarCodeRangeStartPoint == Point.Empty || selectBarCodeRangeEndPoint == Point.Empty)
            {
                MessageBox.Show("未框选并确定条形码范围");
                return;
            }
            if (allScannedBarCodeTipRectangles == null || allScannedBarCodeTipRectangles.Length == 0)
            {
                MessageBox.Show("条形码框选范围中未识别到条形码");
                return;
            }
            // 计算条形码
            int minX = Math.Min(selectBarCodeRangeStartPoint.X, selectBarCodeRangeEndPoint.X);
            int maxX = Math.Max(selectBarCodeRangeStartPoint.X, selectBarCodeRangeEndPoint.X);
            int minY = Math.Min(selectBarCodeRangeStartPoint.Y, selectBarCodeRangeEndPoint.Y);
            int maxY = Math.Max(selectBarCodeRangeStartPoint.Y, selectBarCodeRangeEndPoint.Y);
            Point leftTopCornerPoint = new Point(minX, minY);
            Point rightBottomCornerPoint = new Point(maxX, maxY);
            Point leftTopCornerPicturePoint = new Point((int)(minX * xRate), (int)(minY * yRate));
            Point rightBottomCornerPicturePoint = new Point((int)(maxX * xRate), (int)(maxY * yRate));
            // 计算框选范围
            int halfBarCodeTipWidth = Math.Max(allScannedBarCodeTipRectangles[0][3].X - allScannedBarCodeTipRectangles[0][0].X,
                 allScannedBarCodeTipRectangles[0][2].X - allScannedBarCodeTipRectangles[0][1].X) / 2;
            int halfBarCodeTipHeight = Math.Max(allScannedBarCodeTipRectangles[0][1].Y - allScannedBarCodeTipRectangles[0][0].Y,
                 allScannedBarCodeTipRectangles[0][2].Y - allScannedBarCodeTipRectangles[0][3].Y) / 2;
            minX = Math.Min(productBagCornerPoints[0].X, productBagCornerPoints[1].X);
            maxX = Math.Max(productBagCornerPoints[2].X, productBagCornerPoints[3].X);
            minY = Math.Min(productBagCornerPoints[0].Y, productBagCornerPoints[3].Y);
            maxY = Math.Max(productBagCornerPoints[1].Y, productBagCornerPoints[2].Y);
            int productBagWidth = maxX - minX;
            int productBagHeight = maxY - minY;
            // 序列化并保持文件
            BarCodePositionStandardConfig barCodePositionStandardConfig = new BarCodePositionStandardConfig();
            barCodePositionStandardConfig.IsBarCodeTipCharAtLeft = (cbCharAt.SelectedIndex == 0);
            barCodePositionStandardConfig.BarCodeTipMaxInclineCornerDegree = (int)nudMaxInclineDegree.Value;
            barCodePositionStandardConfig.ProductBagCorners = productBagCornerPoints;
            barCodePositionStandardConfig.SelectBarCodeRangeCorners = new Point[] { leftTopCornerPoint, rightBottomCornerPoint };
            barCodePositionStandardConfig.BarCodeTipMinLeftRate = Utility.GetDistanceToLine(
                new Point(leftTopCornerPicturePoint.X - halfBarCodeTipWidth, leftTopCornerPicturePoint.Y - halfBarCodeTipHeight), new Point(minX, 0), Double.NaN) / productBagWidth;
            barCodePositionStandardConfig.BarCodeTipMaxLeftRate = Utility.GetDistanceToLine(
                new Point(rightBottomCornerPicturePoint.X - halfBarCodeTipWidth, rightBottomCornerPicturePoint.Y - halfBarCodeTipHeight), new Point(minX, 0), Double.NaN) / productBagWidth;
            barCodePositionStandardConfig.BarCodeTipMinBottomRate = Utility.GetDistanceToLine(
                new Point(rightBottomCornerPicturePoint.X + halfBarCodeTipWidth, rightBottomCornerPicturePoint.Y + halfBarCodeTipHeight), new Point(0, maxY), 0) / productBagHeight;
            barCodePositionStandardConfig.BarCodeTipMaxBottomRate = Utility.GetDistanceToLine(
                new Point(leftTopCornerPicturePoint.X - halfBarCodeTipWidth, leftTopCornerPicturePoint.Y - halfBarCodeTipHeight), new Point(0, maxY), 0) / productBagHeight;
            File.WriteAllText(GetConfigSaveFilePath(), BarCodePositionStandardConfig.Serialize(barCodePositionStandardConfig));
            this.currentStandardConfig = barCodePositionStandardConfig;
            // 保存标准图
            File.Delete(GetConfigPictureFilePath());
            File.Copy(currentStandardPictureFilePath, GetConfigPictureFilePath());
            MessageBox.Show("标准图设置成功");
        }

        // 获取设置保存文件路径
        private string GetConfigSaveFilePath()
        {
            return Path.Combine(Path.GetDirectoryName(typeof(SetStandardPictureForm).Assembly.Location), "BarCodePositionStandardConfigRecord.xml");
        }

        // 获取设置保存标准图片路径
        private string GetConfigPictureFilePath()
        {
            return Path.Combine(Path.GetDirectoryName(typeof(SetStandardPictureForm).Assembly.Location), "BarCodePositionStandardConfigRecord.jpg");
        }

        // 获取设置正在编辑标准图片路径
        private string GetEditConfigPictureFilePath()
        {
            return Path.Combine(Path.GetDirectoryName(typeof(SetStandardPictureForm).Assembly.Location), "BarCodePositionStandardConfigRecordEditing.jpg");
        }

        #endregion

        #region 设置菜单功能

        private void 选择标准图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            currentStandardPictureFilePath = openFileDialog1.FileName;
            selectBarCodeRangeStartPoint = Point.Empty;
            selectBarCodeRangeEndPoint = Point.Empty;
            drawBarCodeTipCornerList.Clear();
            Image image = Bitmap.FromFile(openFileDialog1.FileName);
            AnalyseProductBagEdge(image);
        }

        // 分析识别产品包装袋
        private void AnalyseProductBagEdge(Image image)
        {
            bitmap = new Bitmap(image);
            productBagCornerPoints = new ProductBagEdgeScanner().GetProductBagCorners(bitmap);
            if (productBagCornerPoints == null)
            {
                tsslInfo.Text = "识别产品包装不成功";
                pbStandard.Image = null;
                bitmap = null;
                pbArea.Image = null;
                return;
            }
            DrawProductBagEdge(bitmap, productBagCornerPoints);
        }

        // 绘制包装袋边缘
        private void DrawProductBagEdge(Bitmap bitmap, Point[] cornerPoints)
        {
            xRate = (double)bitmap.Width / pbStandard.Width;
            yRate = (double)bitmap.Height / pbStandard.Height;
            double lineWidth = brushWidth * xRate;
            Graphics g = Graphics.FromImage(bitmap);
            g.DrawPolygon(new Pen(Color.Red, (int)lineWidth), cornerPoints);
            pbStandard.Image = Image.FromHbitmap(bitmap.GetHbitmap());
            tsslInfo.Text = String.Format("条形码四角坐标:{0},{1},{2},{3}", cornerPoints[0], cornerPoints[1], cornerPoints[2], cornerPoints[3]);
        }

        bool isSelectingCbRange = false;

        private void 框选条码范围ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("此操作将清空之前框选的条码范围\n是否继续操作", "确认操作", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                return;
            tsslInfo.Text = "正在框选条形码范围，使用鼠标左键框选标准图区域以确定条码允许出现范围...";
            isSelectingCbRange = true;
            selectBarCodeRangeStartPoint = Point.Empty;
            selectBarCodeRangeEndPoint = Point.Empty;
            drawBarCodeTipCornerList.Clear();
            pbStandard.Invalidate();
        }

        private void pbStandard_MouseDown(object sender, MouseEventArgs e)
        {
            if (!isSelectingCbRange) return;
            selectBarCodeRangeStartPoint = e.Location;
        }

        private void pbStandard_MouseMove(object sender, MouseEventArgs e)
        {
            if(bitmap==null) return;
            int x = (int)(e.Location.X * xRate);
            int y = (int)(e.Location.Y * yRate);
            if (x - 100 < 0)
                x = 100;
            else if (x + 100 > bitmap.Width)
                x = bitmap.Width - 100;
            if (y - 100 < 0)
                y = 100;
            else if (y + 100 > bitmap.Height)
                y = bitmap.Height - 100;
            Bitmap areaBitmap = bitmap.Clone(new Rectangle(x - 100, y - 100, 200, 200), bitmap.PixelFormat);
            pbArea.Image = areaBitmap;
            // 框选功能
            if (!isSelectingCbRange) return;
            selectBarCodeRangeEndPoint = e.Location;
            pbStandard.Invalidate();
        }

        private void pbStandard_MouseUp(object sender, MouseEventArgs e)
        {
            if (!isSelectingCbRange) return;
            tsslInfo.Text = "  ";
            isSelectingCbRange = false;
            AnalyseBarCodeTip();
        }

        // 分析条形码贴纸
        private void AnalyseBarCodeTip()
        {
            // 查看并计算条形码特征值
            int minX = Math.Min(selectBarCodeRangeStartPoint.X, selectBarCodeRangeEndPoint.X);
            int maxX = Math.Max(selectBarCodeRangeStartPoint.X, selectBarCodeRangeEndPoint.X);
            int minY = Math.Min(selectBarCodeRangeStartPoint.Y, selectBarCodeRangeEndPoint.Y);
            int maxY = Math.Max(selectBarCodeRangeStartPoint.Y, selectBarCodeRangeEndPoint.Y);
            Point leftTopCornerPoint = new Point(minX);
            Point rightBottomCornerPoint = new Point(minY);
            // 分析条形码范围
            BarCodeScanner barCodeScanner = new BarCodeScanner();
            barCodeScanner.ProductBagRate = Utility.GetSlope(productBagCornerPoints[1], productBagCornerPoints[2]);
            allScannedBarCodeTipRectangles = barCodeScanner.GetAllScannedRectangles(
                bitmap, (int)(minX * xRate), (int)(maxX * xRate), (int)(minY * yRate), (int)(maxY * yRate));
            if (allScannedBarCodeTipRectangles == null || allScannedBarCodeTipRectangles.Length == 0
                || allScannedBarCodeTipRectangles.Length > 1)
                tsslInfo.Text = "未识别出条形码";
            else
            {
                tsslInfo.Text = "已识别出条形码";
                drawBarCodeTipCornerList.Clear();
                foreach (Point point in allScannedBarCodeTipRectangles[0])
                {
                    drawBarCodeTipCornerList.Add(new Point((int)(point.X / xRate), (int)(point.Y / yRate)));
                }
            }
            pbStandard.Invalidate();
        }

        List<Point> drawBarCodeTipCornerList = new List<Point>();

        private void pbStandard_Paint(object sender, PaintEventArgs e)
        {
            if (selectBarCodeRangeStartPoint == Point.Empty || selectBarCodeRangeEndPoint == Point.Empty) return;
            int minX = Math.Min(selectBarCodeRangeStartPoint.X, selectBarCodeRangeEndPoint.X);
            int maxX = Math.Max(selectBarCodeRangeStartPoint.X, selectBarCodeRangeEndPoint.X);
            int minY = Math.Min(selectBarCodeRangeStartPoint.Y, selectBarCodeRangeEndPoint.Y);
            int maxY = Math.Max(selectBarCodeRangeStartPoint.Y, selectBarCodeRangeEndPoint.Y);
            e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Yellow), brushWidth), minX, minY, maxX - minX, maxY - minY);
            if (drawBarCodeTipCornerList.Count==0) return;
            e.Graphics.DrawPolygon(new Pen(new SolidBrush(Color.Red), brushWidth), drawBarCodeTipCornerList.ToArray());
        }

        #endregion

        private void 取消ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void SetStandardPictureForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Visible = false;
            e.Cancel = true;
        }

        private void pbStandard_Click(object sender, EventArgs e)
        {

        }
    }
}
