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
            this.cb_pause = new System.Windows.Forms.CheckBox();
            this.nud_tint = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.nud_max_images = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nud_ptex = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.rb_simple = new System.Windows.Forms.RadioButton();
            this.rb_xml = new System.Windows.Forms.RadioButton();
            this.nud_nobj = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.cb_depth = new System.Windows.Forms.CheckBox();
            this.cb_segm = new System.Windows.Forms.CheckBox();
            this.bt_open_folder = new System.Windows.Forms.Button();
            this.bt_reset = new System.Windows.Forms.Button();
            this.lb_render_time = new System.Windows.Forms.Label();
            this.control3D1 = new OpenTK.Extra.Control3D();
            this.cb_hide = new System.Windows.Forms.CheckBox();
            this.bt_load_world = new System.Windows.Forms.Button();
            this.tb_folder = new System.Windows.Forms.TextBox();
            this.tb_shift = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nud_tint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_max_images)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_ptex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_nobj)).BeginInit();
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
            this.rtb_regions.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rtb_regions.Location = new System.Drawing.Point(779, 39);
            this.rtb_regions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rtb_regions.Name = "rtb_regions";
            this.rtb_regions.Size = new System.Drawing.Size(348, 469);
            this.rtb_regions.TabIndex = 1;
            this.rtb_regions.Text = "";
            // 
            // cb_draw_axes
            // 
            this.cb_draw_axes.AutoSize = true;
            this.cb_draw_axes.Location = new System.Drawing.Point(16, 81);
            this.cb_draw_axes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb_draw_axes.Name = "cb_draw_axes";
            this.cb_draw_axes.Size = new System.Drawing.Size(60, 21);
            this.cb_draw_axes.TabIndex = 2;
            this.cb_draw_axes.Text = "Axes";
            this.cb_draw_axes.UseVisualStyleBackColor = true;
            // 
            // cb_draw_bb
            // 
            this.cb_draw_bb.AutoSize = true;
            this.cb_draw_bb.Location = new System.Drawing.Point(16, 110);
            this.cb_draw_bb.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb_draw_bb.Name = "cb_draw_bb";
            this.cb_draw_bb.Size = new System.Drawing.Size(77, 21);
            this.cb_draw_bb.TabIndex = 3;
            this.cb_draw_bb.Text = "BBoxes";
            this.cb_draw_bb.UseVisualStyleBackColor = true;
            // 
            // cb_save
            // 
            this.cb_save.AutoSize = true;
            this.cb_save.Location = new System.Drawing.Point(16, 167);
            this.cb_save.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb_save.Name = "cb_save";
            this.cb_save.Size = new System.Drawing.Size(62, 21);
            this.cb_save.TabIndex = 4;
            this.cb_save.Text = "Save";
            this.cb_save.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 519);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Save path:";
            // 
            // cb_pause
            // 
            this.cb_pause.AutoSize = true;
            this.cb_pause.Checked = true;
            this.cb_pause.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_pause.Location = new System.Drawing.Point(16, 139);
            this.cb_pause.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb_pause.Name = "cb_pause";
            this.cb_pause.Size = new System.Drawing.Size(70, 21);
            this.cb_pause.TabIndex = 7;
            this.cb_pause.Text = "Pause";
            this.cb_pause.UseVisualStyleBackColor = true;
            // 
            // nud_tint
            // 
            this.nud_tint.Location = new System.Drawing.Point(13, 215);
            this.nud_tint.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.label2.Location = new System.Drawing.Point(9, 193);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Timer Interval";
            // 
            // nud_max_images
            // 
            this.nud_max_images.Location = new System.Drawing.Point(13, 283);
            this.nud_max_images.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.label3.Location = new System.Drawing.Point(13, 262);
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
            this.nud_ptex.Location = new System.Drawing.Point(13, 356);
            this.nud_ptex.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.label4.Location = new System.Drawing.Point(13, 335);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 17);
            this.label4.TabIndex = 12;
            this.label4.Text = "P Texture";
            // 
            // rb_simple
            // 
            this.rb_simple.AutoSize = true;
            this.rb_simple.Location = new System.Drawing.Point(867, 518);
            this.rb_simple.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rb_simple.Name = "rb_simple";
            this.rb_simple.Size = new System.Drawing.Size(87, 21);
            this.rb_simple.TabIndex = 14;
            this.rb_simple.Text = "YOLO v3";
            this.rb_simple.UseVisualStyleBackColor = true;
            // 
            // rb_xml
            // 
            this.rb_xml.AutoSize = true;
            this.rb_xml.Checked = true;
            this.rb_xml.Location = new System.Drawing.Point(981, 517);
            this.rb_xml.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rb_xml.Name = "rb_xml";
            this.rb_xml.Size = new System.Drawing.Size(57, 21);
            this.rb_xml.TabIndex = 15;
            this.rb_xml.TabStop = true;
            this.rb_xml.Text = "XML";
            this.rb_xml.UseVisualStyleBackColor = true;
            // 
            // nud_nobj
            // 
            this.nud_nobj.Location = new System.Drawing.Point(13, 416);
            this.nud_nobj.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nud_nobj.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_nobj.Name = "nud_nobj";
            this.nud_nobj.Size = new System.Drawing.Size(79, 22);
            this.nud_nobj.TabIndex = 17;
            this.nud_nobj.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 395);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 17);
            this.label5.TabIndex = 16;
            this.label5.Text = "N Objects";
            // 
            // cb_depth
            // 
            this.cb_depth.AutoSize = true;
            this.cb_depth.Location = new System.Drawing.Point(17, 465);
            this.cb_depth.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb_depth.Name = "cb_depth";
            this.cb_depth.Size = new System.Drawing.Size(68, 21);
            this.cb_depth.TabIndex = 18;
            this.cb_depth.Text = "Depth";
            this.cb_depth.UseVisualStyleBackColor = true;
            // 
            // cb_segm
            // 
            this.cb_segm.AutoSize = true;
            this.cb_segm.Location = new System.Drawing.Point(17, 487);
            this.cb_segm.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb_segm.Name = "cb_segm";
            this.cb_segm.Size = new System.Drawing.Size(66, 21);
            this.cb_segm.TabIndex = 19;
            this.cb_segm.Text = "Segm";
            this.cb_segm.UseVisualStyleBackColor = true;
            // 
            // bt_open_folder
            // 
            this.bt_open_folder.Location = new System.Drawing.Point(712, 518);
            this.bt_open_folder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bt_open_folder.Name = "bt_open_folder";
            this.bt_open_folder.Size = new System.Drawing.Size(64, 28);
            this.bt_open_folder.TabIndex = 20;
            this.bt_open_folder.Text = "Open";
            this.bt_open_folder.UseVisualStyleBackColor = true;
            this.bt_open_folder.Click += new System.EventHandler(this.bt_open_folder_Click);
            // 
            // bt_reset
            // 
            this.bt_reset.Location = new System.Drawing.Point(20, 39);
            this.bt_reset.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bt_reset.Name = "bt_reset";
            this.bt_reset.Size = new System.Drawing.Size(64, 28);
            this.bt_reset.TabIndex = 21;
            this.bt_reset.Text = "Reset";
            this.bt_reset.UseVisualStyleBackColor = true;
            this.bt_reset.Click += new System.EventHandler(this.bt_reset_Click);
            // 
            // lb_render_time
            // 
            this.lb_render_time.AutoSize = true;
            this.lb_render_time.Location = new System.Drawing.Point(119, 11);
            this.lb_render_time.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_render_time.Name = "lb_render_time";
            this.lb_render_time.Size = new System.Drawing.Size(20, 17);
            this.lb_render_time.TabIndex = 22;
            this.lb_render_time.Text = "...";
            // 
            // control3D1
            // 
            this.control3D1.BackColor = System.Drawing.Color.Black;
            this.control3D1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.control3D1.Location = new System.Drawing.Point(115, 39);
            this.control3D1.Margin = new System.Windows.Forms.Padding(0);
            this.control3D1.Name = "control3D1";
            this.control3D1.Size = new System.Drawing.Size(661, 468);
            this.control3D1.TabIndex = 11;
            this.control3D1.VSync = false;
            this.control3D1.Load += new System.EventHandler(this.control3D1_Load);
            // 
            // cb_hide
            // 
            this.cb_hide.AutoSize = true;
            this.cb_hide.Location = new System.Drawing.Point(712, 10);
            this.cb_hide.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb_hide.Name = "cb_hide";
            this.cb_hide.Size = new System.Drawing.Size(59, 21);
            this.cb_hide.TabIndex = 23;
            this.cb_hide.Text = "Hide";
            this.cb_hide.UseVisualStyleBackColor = true;
            this.cb_hide.CheckedChanged += new System.EventHandler(this.cb_hide_CheckedChanged);
            // 
            // bt_load_world
            // 
            this.bt_load_world.Location = new System.Drawing.Point(12, 560);
            this.bt_load_world.Margin = new System.Windows.Forms.Padding(4);
            this.bt_load_world.Name = "bt_load_world";
            this.bt_load_world.Size = new System.Drawing.Size(150, 28);
            this.bt_load_world.TabIndex = 24;
            this.bt_load_world.Text = "Load World";
            this.bt_load_world.UseVisualStyleBackColor = true;
            this.bt_load_world.Click += new System.EventHandler(this.bt_load_world_Click);
            // 
            // tb_folder
            // 
            this.tb_folder.Location = new System.Drawing.Point(115, 521);
            this.tb_folder.Name = "tb_folder";
            this.tb_folder.Size = new System.Drawing.Size(590, 22);
            this.tb_folder.TabIndex = 25;
            this.tb_folder.Text = "C:\\Dataset3D";
            // 
            // tb_shift
            // 
            this.tb_shift.Location = new System.Drawing.Point(169, 563);
            this.tb_shift.Name = "tb_shift";
            this.tb_shift.Size = new System.Drawing.Size(590, 22);
            this.tb_shift.TabIndex = 26;
            this.tb_shift.Text = "0 0 0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1147, 601);
            this.Controls.Add(this.tb_shift);
            this.Controls.Add(this.tb_folder);
            this.Controls.Add(this.bt_load_world);
            this.Controls.Add(this.cb_hide);
            this.Controls.Add(this.lb_render_time);
            this.Controls.Add(this.bt_reset);
            this.Controls.Add(this.bt_open_folder);
            this.Controls.Add(this.cb_segm);
            this.Controls.Add(this.cb_depth);
            this.Controls.Add(this.nud_nobj);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.rb_xml);
            this.Controls.Add(this.rb_simple);
            this.Controls.Add(this.nud_ptex);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.control3D1);
            this.Controls.Add(this.nud_max_images);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nud_tint);
            this.Controls.Add(this.cb_pause);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_save);
            this.Controls.Add(this.cb_draw_bb);
            this.Controls.Add(this.cb_draw_axes);
            this.Controls.Add(this.rtb_regions);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Dataset3D (S. Diane, 2018)";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nud_tint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_max_images)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_ptex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_nobj)).EndInit();
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
        private System.Windows.Forms.NumericUpDown nud_nobj;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cb_depth;
        private System.Windows.Forms.CheckBox cb_segm;
        private System.Windows.Forms.Button bt_open_folder;
        private System.Windows.Forms.Button bt_reset;
        private System.Windows.Forms.Label lb_render_time;
        private System.Windows.Forms.CheckBox cb_hide;
        private System.Windows.Forms.Button bt_load_world;
        private System.Windows.Forms.TextBox tb_folder;
        private System.Windows.Forms.TextBox tb_shift;
    }
}

