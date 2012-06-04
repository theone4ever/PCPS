using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;


namespace Vaaan.PictureCode.PositionScan.ObjectDetector
{
    public partial class SetTargetObjectForm : Form
    {
        public SetTargetObjectForm()
        {
            InitializeComponent();
            if (File.Exists(GetConfigPictureFilePath()) && File.Exists(GetConfigSaveFilePath()))
            {
                File.Delete(GetEditConfigPictureFilePath());
                File.Copy(GetConfigPictureFilePath(), GetEditConfigPictureFilePath());
                currentStandardPictureFilePath = GetEditConfigPictureFilePath();
                Image image = Bitmap.FromFile(currentStandardPictureFilePath);
                bitmap = new Bitmap(image);
                pbStandard.Image = bitmap;
                
            }
        }

        // 绘制包装袋边缘
       /* private void DrawProductBagEdge(Bitmap bitmap, Point[] cornerPoints)
        {
            xRate = (double)bitmap.Width / pbStandard.Width;
            yRate = (double)bitmap.Height / pbStandard.Height;
            double lineWidth = brushWidth * xRate;
            Graphics g = Graphics.FromImage(bitmap);
            //g.DrawPolygon(new Pen(Color.Red, (int)lineWidth), cornerPoints);
            pbStandard.Image = Image.FromHbitmap(bitmap.GetHbitmap());
           // tsslInfo.Text = String.Format("条形码四角坐标:{0},{1},{2},{3}", cornerPoints[0], cornerPoints[1], cornerPoints[2], cornerPoints[3]);
        }*/


        Bitmap bitmap;
      //  Point[] productBagCornerPoints;
      //  Point[][] allScannedBarCodeTipRectangles;
        Point selectBarCodeRangeStartPoint = Point.Empty;
        Point selectBarCodeRangeEndPoint = Point.Empty;
        int brushWidth = 2;
        double xRate;
        double yRate;
        string currentStandardPictureFilePath;
        public Contour<Point> target = null;
        bool isSelectingCbRange = false;
        List<Point> drawBarCodeTipCornerList = new List<Point>();

        #region 设置保存载入功能

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        
            if (selectBarCodeRangeStartPoint == Point.Empty || selectBarCodeRangeEndPoint == Point.Empty)
            {
                MessageBox.Show("未框选并确定目标图形范围");
                return;
            }
        
            // 计算条形码
            int minX = Math.Min(selectBarCodeRangeStartPoint.X, selectBarCodeRangeEndPoint.X);
            int maxX = Math.Max(selectBarCodeRangeStartPoint.X, selectBarCodeRangeEndPoint.X);
            int minY = Math.Min(selectBarCodeRangeStartPoint.Y, selectBarCodeRangeEndPoint.Y);
            int maxY = Math.Max(selectBarCodeRangeStartPoint.Y, selectBarCodeRangeEndPoint.Y);
            // 计算框选范围
            
            // 序列化并保持文件
            
            
            // 保存标准图
            File.Delete(GetConfigPictureFilePath());
            File.Copy(currentStandardPictureFilePath, GetConfigPictureFilePath());
            MessageBox.Show("标准图设置成功");
        }

        // 获取设置保存文件路径
        private string GetConfigSaveFilePath()
        {
            return Path.Combine(Path.GetDirectoryName(typeof(SetTargetObjectForm).Assembly.Location), "BarCodePositionStandardConfigRecord.xml");
        }

        // 获取设置保存标准图片路径
        private string GetConfigPictureFilePath()
        {
            return Path.Combine(Path.GetDirectoryName(typeof(SetTargetObjectForm).Assembly.Location), "BarCodePositionStandardConfigRecord.jpg");
        }

        // 获取设置正在编辑标准图片路径
        private string GetEditConfigPictureFilePath()
        {
            return Path.Combine(Path.GetDirectoryName(typeof(SetTargetObjectForm).Assembly.Location), "BarCodePositionStandardConfigRecordEditing.jpg");
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
            Image image = Bitmap.FromFile(currentStandardPictureFilePath);
            bitmap = new Bitmap(image);
            xRate = (double)bitmap.Width / pbStandard.Width;
            yRate = (double)bitmap.Height / pbStandard.Height;
            pbStandard.Image = bitmap;

        }

        private void 框选图形范围ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            tsslInfo.Text = "正在框选目标图形范围，使用鼠标左键框选标准图区域以确定允许出现范围...";
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

            var img = new Image<Bgr, byte>(bitmap);

            int left = Math.Min(selectBarCodeRangeStartPoint.X, selectBarCodeRangeEndPoint.X);
            int right = Math.Max(selectBarCodeRangeStartPoint.X, selectBarCodeRangeEndPoint.X);
            int top = Math.Min(selectBarCodeRangeStartPoint.Y, selectBarCodeRangeEndPoint.Y);
            int buttom = Math.Max(selectBarCodeRangeStartPoint.Y, selectBarCodeRangeEndPoint.Y);
            /*int left = selectBarCodeRangeStartPoint.X;
            int right = selectBarCodeRangeEndPoint.X;
            int top = selectBarCodeRangeStartPoint.Y;
            int buttom = selectBarCodeRangeEndPoint.Y;*/

            double xR = (double)bitmap.Width / (double)pbStandard.Width;
            double yR = (double)bitmap.Height / (double)pbStandard.Height;
                          
            var detector = new ArrowSignDetector(ushort.Parse(thresholdUpDown.Value.ToString()));
             target = detector.FindExernalDefault(img, 
                (int) (left*xR), 
                (int) (right*xR), 
                (int) (top*yR), 
                (int) (buttom*yR));


             if (target == null )
                tsslInfo.Text = "未识别出目标图形";
            else
            {
                tsslInfo.Text = "已识别出目标图形";
                drawBarCodeTipCornerList.Clear();
            }
            pbStandard.Invalidate();
        }

      

        private void pbStandard_Paint(object sender, PaintEventArgs e)
        {
            if (selectBarCodeRangeStartPoint == Point.Empty || selectBarCodeRangeEndPoint == Point.Empty) return;
            int minX = Math.Min(selectBarCodeRangeStartPoint.X, selectBarCodeRangeEndPoint.X);
            int maxX = Math.Max(selectBarCodeRangeStartPoint.X, selectBarCodeRangeEndPoint.X);
            int minY = Math.Min(selectBarCodeRangeStartPoint.Y, selectBarCodeRangeEndPoint.Y);
            int maxY = Math.Max(selectBarCodeRangeStartPoint.Y, selectBarCodeRangeEndPoint.Y);
            e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Yellow), brushWidth), minX, minY, maxX - minX, maxY - minY);

            if (target != null)
            {
                double xR = (double)bitmap.Width / (double)pbStandard.Width;
                double yR = (double)bitmap.Height / (double)pbStandard.Height;
                List<Point> points = new List<Point>();
                points.Add(new Point((int)(target.BoundingRectangle.Left/xR), (int)(target.BoundingRectangle.Top/yR)));
                points.Add(new Point((int)(target.BoundingRectangle.Left/xR), (int)(target.BoundingRectangle.Bottom/yR)));
                points.Add(new Point((int)(target.BoundingRectangle.Right/xR), (int)(target.BoundingRectangle.Bottom/yR)));
                points.Add(new Point((int)(target.BoundingRectangle.Right/xR), (int)(target.BoundingRectangle.Top/yR)));

                e.Graphics.DrawPolygon(new Pen(new SolidBrush(Color.Red), brushWidth), points.ToArray());
            }
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

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
