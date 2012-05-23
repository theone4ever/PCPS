using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;

using System.IO;

namespace Vaaan.PictureCode.PositionScan.ObjectDetector
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

       

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void DetectToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (openFileDialog1.FileName == null || openFileDialog1.FileName == "") return;
            

            var ObjectList = new List<Image<Gray, byte>>();
            var ObjectBoundList = new List<Rectangle>();

            var arrowSignDetector = new ArrowSignDetector();

            Image<Bgr, byte> image = new Image<Bgr, byte>(openFileDialog1.FileName);
            Image<Gray, byte> grayImage = null;
            arrowSignDetector.DetectObject(image, ObjectList, ObjectBoundList, grayImage);



            for (int i = 0; i < ObjectList.Count; i++)
            {
                Rectangle rect = ObjectBoundList[i];
                image.Draw(rect, new Bgr(Color.Aquamarine), 2);
            }

            
            pbBlackWhite.Image = image.Bitmap;
          //  pbAfter.Image = grayImage.Bitmap;
        }
    }
}
