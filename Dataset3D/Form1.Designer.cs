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
            this.cb_hide = new System.Windows.Forms.CheckBox();
            this.bt_load_world = new System.Windows.Forms.Button();
            this.tb_folder = new System.Windows.Forms.TextBox();
            this.tb_scene = new System.Windows.Forms.TextBox();
            this.cb_move = new System.Windows.Forms.CheckBox();
            this.lb_frame = new System.Windows.Forms.Label();
            this.cb_file_world = new System.Windows.Forms.CheckBox();
            this.nud_cam_speed = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.nud_cam = new System.Windows.Forms.NumericUpDown();
            this.cb_move_traj = new System.Windows.Forms.CheckBox();
            this.tb_mvm = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cb_draw_traj = new System.Windows.Forms.CheckBox();
            this.control3D1 = new OpenTK.Extra.Control3D();
            this.label8 = new System.Windows.Forms.Label();
            this.tb_projm = new System.Windows.Forms.TextBox();
            this.bt_save_traj_ds = new System.Windows.Forms.Button();
            this.nud_tint2 = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.cb_pause_draw = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nud_tint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_max_images)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_ptex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_nobj)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_cam_speed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_cam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_tint2)).BeginInit();
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
            this.rtb_regions.Location = new System.Drawing.Point(584, 32);
            this.rtb_regions.Name = "rtb_regions";
            this.rtb_regions.Size = new System.Drawing.Size(262, 346);
            this.rtb_regions.TabIndex = 1;
            this.rtb_regions.Text = "";
            // 
            // cb_draw_axes
            // 
            this.cb_draw_axes.AutoSize = true;
            this.cb_draw_axes.Location = new System.Drawing.Point(12, 66);
            this.cb_draw_axes.Name = "cb_draw_axes";
            this.cb_draw_axes.Size = new System.Drawing.Size(49, 17);
            this.cb_draw_axes.TabIndex = 2;
            this.cb_draw_axes.Text = "Axes";
            this.cb_draw_axes.UseVisualStyleBackColor = true;
            // 
            // cb_draw_bb
            // 
            this.cb_draw_bb.AutoSize = true;
            this.cb_draw_bb.Location = new System.Drawing.Point(12, 89);
            this.cb_draw_bb.Name = "cb_draw_bb";
            this.cb_draw_bb.Size = new System.Drawing.Size(62, 17);
            this.cb_draw_bb.TabIndex = 3;
            this.cb_draw_bb.Text = "BBoxes";
            this.cb_draw_bb.UseVisualStyleBackColor = true;
            // 
            // cb_save
            // 
            this.cb_save.AutoSize = true;
            this.cb_save.Location = new System.Drawing.Point(12, 136);
            this.cb_save.Name = "cb_save";
            this.cb_save.Size = new System.Drawing.Size(51, 17);
            this.cb_save.TabIndex = 4;
            this.cb_save.Text = "Save";
            this.cb_save.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 427);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Save path:";
            // 
            // cb_pause
            // 
            this.cb_pause.AutoSize = true;
            this.cb_pause.Checked = true;
            this.cb_pause.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_pause.Location = new System.Drawing.Point(12, 113);
            this.cb_pause.Name = "cb_pause";
            this.cb_pause.Size = new System.Drawing.Size(56, 17);
            this.cb_pause.TabIndex = 7;
            this.cb_pause.Text = "Pause";
            this.cb_pause.UseVisualStyleBackColor = true;
            // 
            // nud_tint
            // 
            this.nud_tint.Location = new System.Drawing.Point(10, 175);
            this.nud_tint.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nud_tint.Name = "nud_tint";
            this.nud_tint.Size = new System.Drawing.Size(59, 20);
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
            this.label2.Location = new System.Drawing.Point(7, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Timer Interval";
            // 
            // nud_max_images
            // 
            this.nud_max_images.Location = new System.Drawing.Point(10, 230);
            this.nud_max_images.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nud_max_images.Name = "nud_max_images";
            this.nud_max_images.Size = new System.Drawing.Size(59, 20);
            this.nud_max_images.TabIndex = 10;
            this.nud_max_images.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 213);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
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
            this.nud_ptex.Location = new System.Drawing.Point(10, 289);
            this.nud_ptex.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_ptex.Name = "nud_ptex";
            this.nud_ptex.Size = new System.Drawing.Size(59, 20);
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
            this.label4.Location = new System.Drawing.Point(10, 272);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "P Texture";
            // 
            // rb_simple
            // 
            this.rb_simple.AutoSize = true;
            this.rb_simple.Location = new System.Drawing.Point(643, 383);
            this.rb_simple.Margin = new System.Windows.Forms.Padding(2);
            this.rb_simple.Name = "rb_simple";
            this.rb_simple.Size = new System.Drawing.Size(69, 17);
            this.rb_simple.TabIndex = 14;
            this.rb_simple.Text = "YOLO v3";
            this.rb_simple.UseVisualStyleBackColor = true;
            // 
            // rb_xml
            // 
            this.rb_xml.AutoSize = true;
            this.rb_xml.Checked = true;
            this.rb_xml.Location = new System.Drawing.Point(729, 382);
            this.rb_xml.Margin = new System.Windows.Forms.Padding(2);
            this.rb_xml.Name = "rb_xml";
            this.rb_xml.Size = new System.Drawing.Size(47, 17);
            this.rb_xml.TabIndex = 15;
            this.rb_xml.TabStop = true;
            this.rb_xml.Text = "XML";
            this.rb_xml.UseVisualStyleBackColor = true;
            // 
            // nud_nobj
            // 
            this.nud_nobj.Location = new System.Drawing.Point(10, 338);
            this.nud_nobj.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_nobj.Name = "nud_nobj";
            this.nud_nobj.Size = new System.Drawing.Size(59, 20);
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
            this.label5.Location = new System.Drawing.Point(10, 321);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "N Objects";
            // 
            // cb_depth
            // 
            this.cb_depth.AutoSize = true;
            this.cb_depth.Location = new System.Drawing.Point(13, 378);
            this.cb_depth.Name = "cb_depth";
            this.cb_depth.Size = new System.Drawing.Size(55, 17);
            this.cb_depth.TabIndex = 18;
            this.cb_depth.Text = "Depth";
            this.cb_depth.UseVisualStyleBackColor = true;
            // 
            // cb_segm
            // 
            this.cb_segm.AutoSize = true;
            this.cb_segm.Location = new System.Drawing.Point(13, 396);
            this.cb_segm.Name = "cb_segm";
            this.cb_segm.Size = new System.Drawing.Size(53, 17);
            this.cb_segm.TabIndex = 19;
            this.cb_segm.Text = "Segm";
            this.cb_segm.UseVisualStyleBackColor = true;
            // 
            // bt_open_folder
            // 
            this.bt_open_folder.Location = new System.Drawing.Point(534, 421);
            this.bt_open_folder.Name = "bt_open_folder";
            this.bt_open_folder.Size = new System.Drawing.Size(48, 23);
            this.bt_open_folder.TabIndex = 20;
            this.bt_open_folder.Text = "Open";
            this.bt_open_folder.UseVisualStyleBackColor = true;
            this.bt_open_folder.Click += new System.EventHandler(this.bt_open_folder_Click);
            // 
            // bt_reset
            // 
            this.bt_reset.Location = new System.Drawing.Point(15, 32);
            this.bt_reset.Name = "bt_reset";
            this.bt_reset.Size = new System.Drawing.Size(48, 23);
            this.bt_reset.TabIndex = 21;
            this.bt_reset.Text = "Reset";
            this.bt_reset.UseVisualStyleBackColor = true;
            this.bt_reset.Click += new System.EventHandler(this.bt_reset_Click);
            // 
            // lb_render_time
            // 
            this.lb_render_time.AutoSize = true;
            this.lb_render_time.Location = new System.Drawing.Point(89, 9);
            this.lb_render_time.Name = "lb_render_time";
            this.lb_render_time.Size = new System.Drawing.Size(16, 13);
            this.lb_render_time.TabIndex = 22;
            this.lb_render_time.Text = "...";
            // 
            // cb_hide
            // 
            this.cb_hide.AutoSize = true;
            this.cb_hide.Location = new System.Drawing.Point(534, 8);
            this.cb_hide.Name = "cb_hide";
            this.cb_hide.Size = new System.Drawing.Size(48, 17);
            this.cb_hide.TabIndex = 23;
            this.cb_hide.Text = "Hide";
            this.cb_hide.UseVisualStyleBackColor = true;
            this.cb_hide.CheckedChanged += new System.EventHandler(this.cb_hide_CheckedChanged);
            // 
            // bt_load_world
            // 
            this.bt_load_world.Location = new System.Drawing.Point(9, 455);
            this.bt_load_world.Name = "bt_load_world";
            this.bt_load_world.Size = new System.Drawing.Size(112, 23);
            this.bt_load_world.TabIndex = 24;
            this.bt_load_world.Text = "Load World";
            this.bt_load_world.UseVisualStyleBackColor = true;
            this.bt_load_world.Click += new System.EventHandler(this.bt_load_world_Click);
            // 
            // tb_folder
            // 
            this.tb_folder.Location = new System.Drawing.Point(86, 423);
            this.tb_folder.Margin = new System.Windows.Forms.Padding(2);
            this.tb_folder.Name = "tb_folder";
            this.tb_folder.Size = new System.Drawing.Size(444, 20);
            this.tb_folder.TabIndex = 25;
            this.tb_folder.Text = "C:\\Dataset3D";
            // 
            // tb_scene
            // 
            this.tb_scene.Location = new System.Drawing.Point(127, 457);
            this.tb_scene.Margin = new System.Windows.Forms.Padding(2);
            this.tb_scene.Name = "tb_scene";
            this.tb_scene.Size = new System.Drawing.Size(358, 20);
            this.tb_scene.TabIndex = 26;
            this.tb_scene.Text = "big_forest";
            // 
            // cb_move
            // 
            this.cb_move.AutoSize = true;
            this.cb_move.Location = new System.Drawing.Point(480, 8);
            this.cb_move.Name = "cb_move";
            this.cb_move.Size = new System.Drawing.Size(53, 17);
            this.cb_move.TabIndex = 27;
            this.cb_move.Text = "Move";
            this.cb_move.UseVisualStyleBackColor = true;
            // 
            // lb_frame
            // 
            this.lb_frame.AutoSize = true;
            this.lb_frame.Location = new System.Drawing.Point(23, 9);
            this.lb_frame.Name = "lb_frame";
            this.lb_frame.Size = new System.Drawing.Size(16, 13);
            this.lb_frame.TabIndex = 28;
            this.lb_frame.Text = "...";
            // 
            // cb_file_world
            // 
            this.cb_file_world.AutoSize = true;
            this.cb_file_world.Checked = true;
            this.cb_file_world.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_file_world.Location = new System.Drawing.Point(509, 461);
            this.cb_file_world.Name = "cb_file_world";
            this.cb_file_world.Size = new System.Drawing.Size(73, 17);
            this.cb_file_world.TabIndex = 29;
            this.cb_file_world.Text = "File World";
            this.cb_file_world.UseVisualStyleBackColor = true;
            // 
            // nud_cam_speed
            // 
            this.nud_cam_speed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nud_cam_speed.Location = new System.Drawing.Point(411, 5);
            this.nud_cam_speed.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_cam_speed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nud_cam_speed.Name = "nud_cam_speed";
            this.nud_cam_speed.Size = new System.Drawing.Size(59, 20);
            this.nud_cam_speed.TabIndex = 31;
            this.nud_cam_speed.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_cam_speed.ValueChanged += new System.EventHandler(this.nud_cam_speed_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(340, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 30;
            this.label6.Text = "Cam Speed";
            // 
            // nud_cam
            // 
            this.nud_cam.DecimalPlaces = 1;
            this.nud_cam.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nud_cam.Location = new System.Drawing.Point(629, 455);
            this.nud_cam.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nud_cam.Name = "nud_cam";
            this.nud_cam.Size = new System.Drawing.Size(59, 20);
            this.nud_cam.TabIndex = 32;
            this.nud_cam.ValueChanged += new System.EventHandler(this.nud_cam_ValueChanged);
            // 
            // cb_move_traj
            // 
            this.cb_move_traj.AutoSize = true;
            this.cb_move_traj.Location = new System.Drawing.Point(629, 432);
            this.cb_move_traj.Name = "cb_move_traj";
            this.cb_move_traj.Size = new System.Drawing.Size(74, 17);
            this.cb_move_traj.TabIndex = 33;
            this.cb_move_traj.Text = "Move Traj";
            this.cb_move_traj.UseVisualStyleBackColor = true;
            // 
            // tb_mvm
            // 
            this.tb_mvm.Location = new System.Drawing.Point(88, 490);
            this.tb_mvm.Margin = new System.Windows.Forms.Padding(2);
            this.tb_mvm.Name = "tb_mvm";
            this.tb_mvm.Size = new System.Drawing.Size(758, 20);
            this.tb_mvm.TabIndex = 34;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 494);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 35;
            this.label7.Text = "ModelViewMat";
            // 
            // cb_draw_traj
            // 
            this.cb_draw_traj.AutoSize = true;
            this.cb_draw_traj.Checked = true;
            this.cb_draw_traj.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_draw_traj.Location = new System.Drawing.Point(629, 409);
            this.cb_draw_traj.Name = "cb_draw_traj";
            this.cb_draw_traj.Size = new System.Drawing.Size(72, 17);
            this.cb_draw_traj.TabIndex = 36;
            this.cb_draw_traj.Text = "Draw Traj";
            this.cb_draw_traj.UseVisualStyleBackColor = true;
            // 
            // control3D1
            // 
            this.control3D1.BackColor = System.Drawing.Color.Black;
            this.control3D1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.control3D1.Location = new System.Drawing.Point(86, 32);
            this.control3D1.Margin = new System.Windows.Forms.Padding(0);
            this.control3D1.Name = "control3D1";
            this.control3D1.Size = new System.Drawing.Size(496, 381);
            this.control3D1.TabIndex = 11;
            this.control3D1.VSync = false;
            this.control3D1.Load += new System.EventHandler(this.control3D1_Load);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 518);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 13);
            this.label8.TabIndex = 38;
            this.label8.Text = "ProjectionMat";
            // 
            // tb_projm
            // 
            this.tb_projm.Location = new System.Drawing.Point(88, 514);
            this.tb_projm.Margin = new System.Windows.Forms.Padding(2);
            this.tb_projm.Name = "tb_projm";
            this.tb_projm.Size = new System.Drawing.Size(758, 20);
            this.tb_projm.TabIndex = 37;
            // 
            // bt_save_traj_ds
            // 
            this.bt_save_traj_ds.Location = new System.Drawing.Point(740, 405);
            this.bt_save_traj_ds.Name = "bt_save_traj_ds";
            this.bt_save_traj_ds.Size = new System.Drawing.Size(83, 23);
            this.bt_save_traj_ds.TabIndex = 39;
            this.bt_save_traj_ds.Text = "Save Dataset";
            this.bt_save_traj_ds.UseVisualStyleBackColor = true;
            this.bt_save_traj_ds.Click += new System.EventHandler(this.bt_save_traj_ds_Click);
            // 
            // nud_tint2
            // 
            this.nud_tint2.DecimalPlaces = 1;
            this.nud_tint2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nud_tint2.Location = new System.Drawing.Point(752, 455);
            this.nud_tint2.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nud_tint2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nud_tint2.Name = "nud_tint2";
            this.nud_tint2.Size = new System.Drawing.Size(59, 20);
            this.nud_tint2.TabIndex = 40;
            this.nud_tint2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(748, 433);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 13);
            this.label9.TabIndex = 41;
            this.label9.Text = "Time Interval";
            // 
            // cb_pause_draw
            // 
            this.cb_pause_draw.AutoSize = true;
            this.cb_pause_draw.Location = new System.Drawing.Point(770, 6);
            this.cb_pause_draw.Name = "cb_pause_draw";
            this.cb_pause_draw.Size = new System.Drawing.Size(56, 17);
            this.cb_pause_draw.TabIndex = 42;
            this.cb_pause_draw.Text = "Pause";
            this.cb_pause_draw.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 540);
            this.Controls.Add(this.cb_pause_draw);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.nud_tint2);
            this.Controls.Add(this.bt_save_traj_ds);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tb_projm);
            this.Controls.Add(this.cb_draw_traj);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tb_mvm);
            this.Controls.Add(this.cb_move_traj);
            this.Controls.Add(this.nud_cam);
            this.Controls.Add(this.nud_cam_speed);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cb_file_world);
            this.Controls.Add(this.lb_frame);
            this.Controls.Add(this.cb_move);
            this.Controls.Add(this.tb_scene);
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
            this.Name = "Form1";
            this.Text = "Dataset3D (S. Diane, 2018)";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nud_tint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_max_images)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_ptex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_nobj)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_cam_speed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_cam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_tint2)).EndInit();
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
        private System.Windows.Forms.TextBox tb_scene;
        private System.Windows.Forms.CheckBox cb_move;
        private System.Windows.Forms.Label lb_frame;
        private System.Windows.Forms.CheckBox cb_file_world;
        private System.Windows.Forms.NumericUpDown nud_cam_speed;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nud_cam;
        private System.Windows.Forms.CheckBox cb_move_traj;
        private System.Windows.Forms.TextBox tb_mvm;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox cb_draw_traj;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tb_projm;
        private System.Windows.Forms.Button bt_save_traj_ds;
        private System.Windows.Forms.NumericUpDown nud_tint2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox cb_pause_draw;
    }
}

