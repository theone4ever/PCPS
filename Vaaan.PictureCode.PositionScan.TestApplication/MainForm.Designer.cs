namespace Vaaan.PictureCode.PositionScan.TestApplication
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.操作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.载入图片ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.识别ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.黑白图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.条形码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.标准图设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslFileName = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pbBefore = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pbAfter = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.图片另存为ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pbBlackWhite = new System.Windows.Forms.PictureBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblScanResult = new System.Windows.Forms.Label();
            this.nudBlackThreshold = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBefore)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAfter)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBlackWhite)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBlackThreshold)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.操作ToolStripMenuItem,
            this.识别ToolStripMenuItem,
            this.设置ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(860, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 操作ToolStripMenuItem
            // 
            this.操作ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.载入图片ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.操作ToolStripMenuItem.Name = "操作ToolStripMenuItem";
            this.操作ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.操作ToolStripMenuItem.Text = "文件";
            // 
            // 载入图片ToolStripMenuItem
            // 
            this.载入图片ToolStripMenuItem.Name = "载入图片ToolStripMenuItem";
            this.载入图片ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.载入图片ToolStripMenuItem.Text = "载入图片...";
            this.载入图片ToolStripMenuItem.Click += new System.EventHandler(this.载入图片ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // 识别ToolStripMenuItem
            // 
            this.识别ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.黑白图ToolStripMenuItem,
            this.条形码ToolStripMenuItem});
            this.识别ToolStripMenuItem.Name = "识别ToolStripMenuItem";
            this.识别ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.识别ToolStripMenuItem.Text = "识别";
            // 
            // 黑白图ToolStripMenuItem
            // 
            this.黑白图ToolStripMenuItem.Name = "黑白图ToolStripMenuItem";
            this.黑白图ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.黑白图ToolStripMenuItem.Text = "黑白图";
            this.黑白图ToolStripMenuItem.Click += new System.EventHandler(this.黑白图ToolStripMenuItem_Click);
            // 
            // 条形码ToolStripMenuItem
            // 
            this.条形码ToolStripMenuItem.Name = "条形码ToolStripMenuItem";
            this.条形码ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.条形码ToolStripMenuItem.Text = "矩形区";
            this.条形码ToolStripMenuItem.Click += new System.EventHandler(this.条形码ToolStripMenuItem_Click);
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.标准图设置ToolStripMenuItem});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // 标准图设置ToolStripMenuItem
            // 
            this.标准图设置ToolStripMenuItem.Name = "标准图设置ToolStripMenuItem";
            this.标准图设置ToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.标准图设置ToolStripMenuItem.Text = "标准图设置...";
            this.标准图设置ToolStripMenuItem.Click += new System.EventHandler(this.标准图设置ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslFileName,
            this.tsslInfo});
            this.statusStrip1.Location = new System.Drawing.Point(0, 658);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(860, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslFileName
            // 
            this.tsslFileName.Name = "tsslFileName";
            this.tsslFileName.Size = new System.Drawing.Size(16, 17);
            this.tsslFileName.Text = "  ";
            // 
            // tsslInfo
            // 
            this.tsslInfo.Name = "tsslInfo";
            this.tsslInfo.Size = new System.Drawing.Size(16, 17);
            this.tsslInfo.Text = "  ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pbBefore);
            this.groupBox1.Location = new System.Drawing.Point(0, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(428, 312);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "原图";
            // 
            // pbBefore
            // 
            this.pbBefore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbBefore.Location = new System.Drawing.Point(3, 17);
            this.pbBefore.Name = "pbBefore";
            this.pbBefore.Size = new System.Drawing.Size(422, 292);
            this.pbBefore.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbBefore.TabIndex = 0;
            this.pbBefore.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.pbAfter);
            this.groupBox2.Location = new System.Drawing.Point(0, 343);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(428, 312);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "识别矩形区";
            // 
            // pbAfter
            // 
            this.pbAfter.ContextMenuStrip = this.contextMenuStrip1;
            this.pbAfter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbAfter.Location = new System.Drawing.Point(3, 17);
            this.pbAfter.Name = "pbAfter";
            this.pbAfter.Size = new System.Drawing.Size(422, 292);
            this.pbAfter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbAfter.TabIndex = 1;
            this.pbAfter.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.图片另存为ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(146, 26);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // 图片另存为ToolStripMenuItem
            // 
            this.图片另存为ToolStripMenuItem.Name = "图片另存为ToolStripMenuItem";
            this.图片另存为ToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.图片另存为ToolStripMenuItem.Text = "图片另存为...";
            this.图片另存为ToolStripMenuItem.Click += new System.EventHandler(this.图片另存为ToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "图片文件|*.BMP;*.JPG;*.GIF";
            this.openFileDialog1.Title = "选择并打开需识别图片";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox3.Controls.Add(this.pbBlackWhite);
            this.groupBox3.Location = new System.Drawing.Point(431, 28);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(428, 312);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "识别黑白图";
            // 
            // pbBlackWhite
            // 
            this.pbBlackWhite.ContextMenuStrip = this.contextMenuStrip1;
            this.pbBlackWhite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbBlackWhite.Location = new System.Drawing.Point(3, 17);
            this.pbBlackWhite.Name = "pbBlackWhite";
            this.pbBlackWhite.Size = new System.Drawing.Size(422, 292);
            this.pbBlackWhite.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbBlackWhite.TabIndex = 1;
            this.pbBlackWhite.TabStop = false;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            this.saveFileDialog1.Title = "保存图片";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.lblScanResult);
            this.groupBox4.Controls.Add(this.nudBlackThreshold);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Location = new System.Drawing.Point(431, 343);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(429, 312);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "辅助功能";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(6, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "识别结果";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(251, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(149, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "数值越小对白色判定越严格";
            // 
            // lblScanResult
            // 
            this.lblScanResult.Location = new System.Drawing.Point(6, 78);
            this.lblScanResult.Name = "lblScanResult";
            this.lblScanResult.Size = new System.Drawing.Size(417, 220);
            this.lblScanResult.TabIndex = 6;
            this.lblScanResult.Text = "识别结果内容";
            // 
            // nudBlackThreshold
            // 
            this.nudBlackThreshold.Location = new System.Drawing.Point(125, 17);
            this.nudBlackThreshold.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.nudBlackThreshold.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudBlackThreshold.Name = "nudBlackThreshold";
            this.nudBlackThreshold.Size = new System.Drawing.Size(120, 21);
            this.nudBlackThreshold.TabIndex = 1;
            this.nudBlackThreshold.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudBlackThreshold.ValueChanged += new System.EventHandler(this.nudBlackThreshold_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "识别黑白图灰度阀值";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 680);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "图形码扫描程序测试";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbBefore)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbAfter)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbBlackWhite)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBlackThreshold)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 操作ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 载入图片ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 识别ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 条形码ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslInfo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pbBefore;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox pbAfter;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PictureBox pbBlackWhite;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 图片另存为ToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown nudBlackThreshold;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblScanResult;
        private System.Windows.Forms.ToolStripMenuItem 黑白图ToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripStatusLabel tsslFileName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 标准图设置ToolStripMenuItem;
    }
}

