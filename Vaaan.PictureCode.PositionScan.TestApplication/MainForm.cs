using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Vaaan.PictureCode.PositionScan.Lib;
using Vaaan.PictureCode.PositionScan.Lib.BarCode;
using System.IO;
using Vaaan.PictureCode.PositionScan.TestApplication.Setting;

namespace Vaaan.PictureCode.PositionScan.TestApplication
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        #region 文件操作

        private void 载入图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            Image image = Bitmap.FromFile(openFileDialog1.FileName);
            pbBefore.Image = image;
            tsslFileName.Text = String.Format("当前图片:{0}", Path.GetFileName(openFileDialog1.FileName));
            tsslInfo.Text = "  ";
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        #region 图形识别

        private void 黑白图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.FileName == null || openFileDialog1.FileName == "") return;
            Image image = Bitmap.FromFile(openFileDialog1.FileName);
            // 识别黑白图
            Bitmap bitmapBlackWhite = new Bitmap(image);
            bool[,] blackMatrix = Utility.GetScannedBlackWhiteMatrix(bitmapBlackWhite, ushort.Parse(nudBlackThreshold.Value.ToString()));
            DrawBlackWhitePointToPicture(blackMatrix, bitmapBlackWhite);
            pbBlackWhite.Image = Image.FromHbitmap(bitmapBlackWhite.GetHbitmap());
        }

        private void DrawBlackWhitePointToPicture(bool[,] blackMatrix, Bitmap bitmapBlackWhite)
        {
            Graphics g = Graphics.FromImage(bitmapBlackWhite);
            Pen blackPen = new Pen(Color.Black, 1);
            Pen whitePen = new Pen(Color.White, 1);
            for (int i = 0; i < blackMatrix.GetLongLength(0); i++)
            {
                for (int j = 0; j < blackMatrix.GetLongLength(1); j++)
                {
                    g.DrawRectangle(blackMatrix[i, j] ? blackPen : whitePen, i, j, 1, 1);
                }
            }
        }

        private void 条形码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.FileName == null || openFileDialog1.FileName == "") return;
            Image image = Bitmap.FromFile(openFileDialog1.FileName);
            // 识别结果
            BarCodePositionAnalyser scanner = new BarCodePositionAnalyser();
            DateTime dtStart = DateTime.Now;
            scanner.Scan(new Bitmap(image), setStandardPictureForm.CurrentStandardConfig);
            tsslInfo.Text = String.Format("分析用时:{0},识别矩形:{1}个,条形码方向{2}",
                DateTime.Now.Subtract(dtStart), scanner.AllScannedRectangles.GetLength(0),
                scanner.BarCodePositionScanResult == BarCodePositionScanResultType.Good ? "正确" : "不正确");
            Bitmap bitmapRectangle = new Bitmap(image);
            lblScanResult.Text = String.Format("产品轮廓斜率角度:{0:0.00}\n产品左侧阀值:{1}\n产品右侧阀值:{2}\n条形码外部阀值:{3}\n条形码内部阀值:{4}",
                Math.Atan(-scanner.ProductBagEdgeParameterGenerator.ProductBagRate) * 180 / Math.PI, (ushort)scanner.ProductBagEdgeParameterGenerator.ProductBagLeftThreshold,
                (ushort)scanner.ProductBagEdgeParameterGenerator.ProductBagRightThreshold,
                (ushort)scanner.BarCodeParameterGenerator.BarcodeTipOuterThreshold, 
                (ushort)scanner.BarcodeTipInnerThreshold);
            Point[][] resultRects = scanner.AllScannedRectangles;
            if (resultRects != null)
            {
                foreach (Point[] rec in resultRects)
                {
                    DrawRedRectangleToPicture(rec, bitmapRectangle);
                    lblScanResult.Text =
                        lblScanResult.Text +
                        String.Format("\n条形码四角坐标:{0},{1},{2},{3}", rec[0], rec[1], rec[2], rec[3]);
                }
            }
            pbAfter.Image = Image.FromHbitmap(bitmapRectangle.GetHbitmap());
        }

        private void DrawRedRectangleToPicture(Point[] rec, Bitmap bitmap)
        {
            Graphics g = Graphics.FromImage(bitmap);
            g.DrawPolygon(new Pen(Color.Red, 4), rec);
        }

        #endregion

        #region 图片另存为功能
        PictureBox currentPb;

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            currentPb = (sender as ContextMenuStrip).SourceControl as PictureBox;
            if (currentPb.Image == null)
            {
                contextMenuStrip1.Enabled = false;
                return;
            }
            contextMenuStrip1.Enabled = true;
        }

        private void 图片另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentPb == null || currentPb.Image == null) return;
            if (saveFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                System.IO.FileStream fs =
                   (System.IO.FileStream)saveFileDialog1.OpenFile();
                // Saves the Image in the appropriate ImageFormat based upon the
                // File type selected in the dialog box.
                // NOTE that the FilterIndex property is one-based.
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        currentPb.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case 2:
                        currentPb.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case 3:
                        currentPb.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                }

                fs.Close();

            }
        }

        #endregion

        #region 设置

        SetStandardPictureForm setStandardPictureForm = new SetStandardPictureForm();
        private void 标准图设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setStandardPictureForm.Show();
        }

        #endregion

        private void nudBlackThreshold_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
