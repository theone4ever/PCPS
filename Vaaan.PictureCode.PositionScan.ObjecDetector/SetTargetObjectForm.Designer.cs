namespace Vaaan.PictureCode.PositionScan.ObjectDetector
{
    partial class SetTargetObjectForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.操作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.取消ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.选择标准图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.框选图形范围ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pbStandard = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pbArea = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbCharAt = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nudMaxInclineDegree = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.thresholdUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbStandard)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbArea)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxInclineDegree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thresholdUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.操作ToolStripMenuItem,
            this.设置ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(752, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 操作ToolStripMenuItem
            // 
            this.操作ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.保存ToolStripMenuItem,
            this.取消ToolStripMenuItem});
            this.操作ToolStripMenuItem.Name = "操作ToolStripMenuItem";
            this.操作ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.操作ToolStripMenuItem.Text = "操作";
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.保存ToolStripMenuItem.Text = "保存";
            this.保存ToolStripMenuItem.Click += new System.EventHandler(this.保存ToolStripMenuItem_Click);
            // 
            // 取消ToolStripMenuItem
            // 
            this.取消ToolStripMenuItem.Name = "取消ToolStripMenuItem";
            this.取消ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.取消ToolStripMenuItem.Text = "取消";
            this.取消ToolStripMenuItem.Click += new System.EventHandler(this.取消ToolStripMenuItem_Click);
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.选择标准图ToolStripMenuItem,
            this.框选图形范围ToolStripMenuItem});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // 选择标准图ToolStripMenuItem
            // 
            this.选择标准图ToolStripMenuItem.Name = "选择标准图ToolStripMenuItem";
            this.选择标准图ToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.选择标准图ToolStripMenuItem.Text = "选择标准图...";
            this.选择标准图ToolStripMenuItem.Click += new System.EventHandler(this.选择标准图ToolStripMenuItem_Click);
            // 
            // 框选图形范围ToolStripMenuItem
            // 
            this.框选图形范围ToolStripMenuItem.Name = "框选图形范围ToolStripMenuItem";
            this.框选图形范围ToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.框选图形范围ToolStripMenuItem.Text = "框选图形范围...";
            this.框选图形范围ToolStripMenuItem.Click += new System.EventHandler(this.框选图形范围ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslInfo});
            this.statusStrip1.Location = new System.Drawing.Point(0, 424);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(752, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslInfo
            // 
            this.tsslInfo.Name = "tsslInfo";
            this.tsslInfo.Size = new System.Drawing.Size(13, 17);
            this.tsslInfo.Text = "  ";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.pbStandard);
            this.groupBox1.Location = new System.Drawing.Point(0, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(543, 394);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "标准图";
            // 
            // pbStandard
            // 
            this.pbStandard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbStandard.Location = new System.Drawing.Point(3, 17);
            this.pbStandard.Name = "pbStandard";
            this.pbStandard.Size = new System.Drawing.Size(537, 374);
            this.pbStandard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbStandard.TabIndex = 0;
            this.pbStandard.TabStop = false;
            this.pbStandard.Paint += new System.Windows.Forms.PaintEventHandler(this.pbStandard_Paint);
            this.pbStandard.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbStandard_MouseDown);
            this.pbStandard.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbStandard_MouseMove);
            this.pbStandard.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbStandard_MouseUp);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "图片文件|*.BMP;*.JPG;*.GIF";
            this.openFileDialog1.Title = "选择并打开产品标准图片";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.pbArea);
            this.groupBox2.Location = new System.Drawing.Point(549, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 200);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "局部放大";
            // 
            // pbArea
            // 
            this.pbArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbArea.Location = new System.Drawing.Point(3, 17);
            this.pbArea.Name = "pbArea";
            this.pbArea.Size = new System.Drawing.Size(194, 180);
            this.pbArea.TabIndex = 0;
            this.pbArea.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.thresholdUpDown);
            this.groupBox3.Controls.Add(this.cbCharAt);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.nudMaxInclineDegree);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(549, 234);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 187);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "参数设置";
            // 
            // cbCharAt
            // 
            this.cbCharAt.FormattingEnabled = true;
            this.cbCharAt.Items.AddRange(new object[] {
            "左侧",
            "右侧"});
            this.cbCharAt.Location = new System.Drawing.Point(115, 48);
            this.cbCharAt.Name = "cbCharAt";
            this.cbCharAt.Size = new System.Drawing.Size(59, 20);
            this.cbCharAt.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "条形码数字在";
            // 
            // nudMaxInclineDegree
            // 
            this.nudMaxInclineDegree.Location = new System.Drawing.Point(115, 20);
            this.nudMaxInclineDegree.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.nudMaxInclineDegree.Name = "nudMaxInclineDegree";
            this.nudMaxInclineDegree.Size = new System.Drawing.Size(59, 21);
            this.nudMaxInclineDegree.TabIndex = 1;
            this.nudMaxInclineDegree.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "条形码最大倾斜角";
            // 
            // thresholdUpDown
            // 
            this.thresholdUpDown.Location = new System.Drawing.Point(115, 74);
            this.thresholdUpDown.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.thresholdUpDown.Name = "thresholdUpDown";
            this.thresholdUpDown.Size = new System.Drawing.Size(59, 21);
            this.thresholdUpDown.TabIndex = 4;
            this.thresholdUpDown.Value = new decimal(new int[] {
            180,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "黑白阀值";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // SetTargetObjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 446);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetTargetObjectForm";
            this.Text = "设置标准图";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SetStandardPictureForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbStandard)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbArea)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxInclineDegree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thresholdUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 选择标准图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 框选图形范围ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslInfo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pbStandard;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem 操作ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 取消ToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown nudMaxInclineDegree;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbArea;
        private System.Windows.Forms.ComboBox cbCharAt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown thresholdUpDown;
    }
}