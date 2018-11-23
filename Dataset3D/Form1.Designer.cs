namespace Dataset3D
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.rtb_regions = new System.Windows.Forms.RichTextBox();
            this.cb_draw_axes = new System.Windows.Forms.CheckBox();
            this.cb_draw_bb = new System.Windows.Forms.CheckBox();
            this.cb_save = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rtb_folder = new System.Windows.Forms.RichTextBox();
            this.cb_pause = new System.Windows.Forms.CheckBox();
            this.nud_tint = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.nud_max_images = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nud_ptex = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.control3D1 = new OpenTK.Extra.Control3D();
            this.rb_simple = new System.Windows.Forms.RadioButton();
            this.rb_xml = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.nud_tint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_max_images)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_ptex)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // rtb_regions
            // 
            this.rtb_regions.Location = new System.Drawing.Point(115, 482);
            this.rtb_regions.Margin = new System.Windows.Forms.Padding(4);
            this.rtb_regions.Name = "rtb_regions";
            this.rtb_regions.Size = new System.Drawing.Size(724, 180);
            this.rtb_regions.TabIndex = 1;
            this.rtb_regions.Text = "";
            // 
            // cb_draw_axes
            // 
            this.cb_draw_axes.AutoSize = true;
            this.cb_draw_axes.Location = new System.Drawing.Point(16, 13);
            this.cb_draw_axes.Margin = new System.Windows.Forms.Padding(4);
            this.cb_draw_axes.Name = "cb_draw_axes";
            this.cb_draw_axes.Size = new System.Drawing.Size(60, 21);
            this.cb_draw_axes.TabIndex = 2;
            this.cb_draw_axes.Text = "Axes";
            this.cb_draw_axes.UseVisualStyleBackColor = true;
            // 
            // cb_draw_bb
            // 
            this.cb_draw_bb.AutoSize = true;
            this.cb_draw_bb.Location = new System.Drawing.Point(16, 42);
            this.cb_draw_bb.Margin = new System.Windows.Forms.Padding(4);
            this.cb_draw_bb.Name = "cb_draw_bb";
            this.cb_draw_bb.Size = new System.Drawing.Size(77, 21);
            this.cb_draw_bb.TabIndex = 3;
            this.cb_draw_bb.Text = "BBoxes";
            this.cb_draw_bb.UseVisualStyleBackColor = true;
            // 
            // cb_save
            // 
            this.cb_save.AutoSize = true;
            this.cb_save.Location = new System.Drawing.Point(16, 116);
            this.cb_save.Margin = new System.Windows.Forms.Padding(4);
            this.cb_save.Name = "cb_save";
            this.cb_save.Size = new System.Drawing.Size(62, 21);
            this.cb_save.TabIndex = 4;
            this.cb_save.Text = "Save";
            this.cb_save.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 672);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Save path:";
            // 
            // rtb_folder
            // 
            this.rtb_folder.Location = new System.Drawing.Point(115, 670);
            this.rtb_folder.Margin = new System.Windows.Forms.Padding(4);
            this.rtb_folder.Multiline = false;
            this.rtb_folder.Name = "rtb_folder";
            this.rtb_folder.Size = new System.Drawing.Size(724, 25);
            this.rtb_folder.TabIndex = 6;
            this.rtb_folder.Text = "D:\\Dataset3D";
            // 
            // cb_pause
            // 
            this.cb_pause.AutoSize = true;
            this.cb_pause.Checked = true;
            this.cb_pause.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_pause.Location = new System.Drawing.Point(16, 87);
            this.cb_pause.Margin = new System.Windows.Forms.Padding(4);
            this.cb_pause.Name = "cb_pause";
            this.cb_pause.Size = new System.Drawing.Size(70, 21);
            this.cb_pause.TabIndex = 7;
            this.cb_pause.Text = "Pause";
            this.cb_pause.UseVisualStyleBackColor = true;
            // 
            // nud_tint
            // 
            this.nud_tint.Location = new System.Drawing.Point(13, 184);
            this.nud_tint.Margin = new System.Windows.Forms.Padding(4);
            this.nud_tint.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nud_tint.Name = "nud_tint";
            this.nud_tint.Size = new System.Drawing.Size(79, 22);
            this.nud_tint.TabIndex = 8;
            this.nud_tint.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nud_tint.ValueChanged += new System.EventHandler(this.nud_tint_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 163);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Timer Interval";
            // 
            // nud_max_images
            // 
            this.nud_max_images.Location = new System.Drawing.Point(13, 252);
            this.nud_max_images.Margin = new System.Windows.Forms.Padding(4);
            this.nud_max_images.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nud_max_images.Name = "nud_max_images";
            this.nud_max_images.Size = new System.Drawing.Size(79, 22);
            this.nud_max_images.TabIndex = 10;
            this.nud_max_images.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 231);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Max Images";
            // 
            // nud_ptex
            // 
            this.nud_ptex.DecimalPlaces = 3;
            this.nud_ptex.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nud_ptex.Location = new System.Drawing.Point(13, 325);
            this.nud_ptex.Margin = new System.Windows.Forms.Padding(4);
            this.nud_ptex.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_ptex.Name = "nud_ptex";
            this.nud_ptex.Size = new System.Drawing.Size(79, 22);
            this.nud_ptex.TabIndex = 13;
            this.nud_ptex.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 304);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 17);
            this.label4.TabIndex = 12;
            this.label4.Text = "P Texture";
            // 
            // control3D1
            // 
            this.control3D1.BackColor = System.Drawing.Color.Black;
            this.control3D1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.control3D1.Location = new System.Drawing.Point(115, 9);
            this.control3D1.Margin = new System.Windows.Forms.Padding(0);
            this.control3D1.Name = "control3D1";
            this.control3D1.Size = new System.Drawing.Size(724, 469);
            this.control3D1.TabIndex = 11;
            this.control3D1.VSync = false;
            this.control3D1.Load += new System.EventHandler(this.control3D1_Load);
            // 
            // rb_simple
            // 
            this.rb_simple.AutoSize = true;
            this.rb_simple.Checked = true;
            this.rb_simple.Location = new System.Drawing.Point(17, 487);
            this.rb_simple.Name = "rb_simple";
            this.rb_simple.Size = new System.Drawing.Size(87, 21);
            this.rb_simple.TabIndex = 14;
            this.rb_simple.TabStop = true;
            this.rb_simple.Text = "YOLO v3";
            this.rb_simple.UseVisualStyleBackColor = true;
            // 
            // rb_xml
            // 
            this.rb_xml.AutoSize = true;
            this.rb_xml.Location = new System.Drawing.Point(17, 514);
            this.rb_xml.Name = "rb_xml";
            this.rb_xml.Size = new System.Drawing.Size(57, 21);
            this.rb_xml.TabIndex = 15;
            this.rb_xml.Text = "XML";
            this.rb_xml.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 703);
            this.Controls.Add(this.rb_xml);
            this.Controls.Add(this.rb_simple);
            this.Controls.Add(this.nud_ptex);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.control3D1);
            this.Controls.Add(this.nud_max_images);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nud_tint);
            this.Controls.Add(this.cb_pause);
            this.Controls.Add(this.rtb_folder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_save);
            this.Controls.Add(this.cb_draw_bb);
            this.Controls.Add(this.cb_draw_axes);
            this.Controls.Add(this.rtb_regions);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Dataset3D (S. Diane, 2018)";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nud_tint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_max_images)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_ptex)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.RichTextBox rtb_regions;
        private System.Windows.Forms.CheckBox cb_draw_axes;
        private System.Windows.Forms.CheckBox cb_draw_bb;
        private System.Windows.Forms.CheckBox cb_save;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtb_folder;
        private System.Windows.Forms.CheckBox cb_pause;
        private System.Windows.Forms.NumericUpDown nud_tint;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nud_max_images;
        private System.Windows.Forms.Label label3;
        private OpenTK.Extra.Control3D control3D1;
        private System.Windows.Forms.NumericUpDown nud_ptex;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rb_simple;
        private System.Windows.Forms.RadioButton rb_xml;
    }
}

