namespace Учет_цистерн
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.contextMenuStrip_Product = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1_Product = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2_Station = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3_Brigade = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4_Owner = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Service = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Carriage = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new TradeWright.UI.Forms.TabControlExtra();
            this.contextMenuStrip_Report = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_Product.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.contextMenuStrip_Report.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip_Product
            // 
            this.contextMenuStrip_Product.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1_Product,
            this.toolStripMenuItem2_Station,
            this.toolStripMenuItem3_Brigade,
            this.toolStripMenuItem4_Owner,
            this.toolStripMenuItem_Service,
            this.toolStripMenuItem_Carriage});
            this.contextMenuStrip_Product.Name = "contextMenuStrip1";
            this.contextMenuStrip_Product.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            resources.ApplyResources(this.contextMenuStrip_Product, "contextMenuStrip_Product");
            // 
            // toolStripMenuItem1_Product
            // 
            this.toolStripMenuItem1_Product.CheckOnClick = true;
            resources.ApplyResources(this.toolStripMenuItem1_Product, "toolStripMenuItem1_Product");
            this.toolStripMenuItem1_Product.Name = "toolStripMenuItem1_Product";
            this.toolStripMenuItem1_Product.Click += new System.EventHandler(this.ToolStripMenuItem1_Product_Click);
            // 
            // toolStripMenuItem2_Station
            // 
            this.toolStripMenuItem2_Station.CheckOnClick = true;
            resources.ApplyResources(this.toolStripMenuItem2_Station, "toolStripMenuItem2_Station");
            this.toolStripMenuItem2_Station.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolStripMenuItem2_Station.Name = "toolStripMenuItem2_Station";
            this.toolStripMenuItem2_Station.Click += new System.EventHandler(this.toolStripMenuItem2_Station_Click);
            // 
            // toolStripMenuItem3_Brigade
            // 
            resources.ApplyResources(this.toolStripMenuItem3_Brigade, "toolStripMenuItem3_Brigade");
            this.toolStripMenuItem3_Brigade.Name = "toolStripMenuItem3_Brigade";
            this.toolStripMenuItem3_Brigade.Click += new System.EventHandler(this.ToolStripMenuItem3_Brigade_Click);
            // 
            // toolStripMenuItem4_Owner
            // 
            resources.ApplyResources(this.toolStripMenuItem4_Owner, "toolStripMenuItem4_Owner");
            this.toolStripMenuItem4_Owner.Name = "toolStripMenuItem4_Owner";
            this.toolStripMenuItem4_Owner.Click += new System.EventHandler(this.toolStripMenuItem4_Owner_Click);
            // 
            // toolStripMenuItem_Service
            // 
            resources.ApplyResources(this.toolStripMenuItem_Service, "toolStripMenuItem_Service");
            this.toolStripMenuItem_Service.Name = "toolStripMenuItem_Service";
            this.toolStripMenuItem_Service.Click += new System.EventHandler(this.toolStripMenuItem_Service_Click);
            // 
            // toolStripMenuItem_Carriage
            // 
            resources.ApplyResources(this.toolStripMenuItem_Carriage, "toolStripMenuItem_Carriage");
            this.toolStripMenuItem_Carriage.Name = "toolStripMenuItem_Carriage";
            this.toolStripMenuItem_Carriage.Click += new System.EventHandler(this.ToolStripMenuItem_Carriage_Click);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button6);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // button4
            // 
            resources.ApplyResources(this.button4, "button4");
            this.button4.Cursor = System.Windows.Forms.Cursors.Default;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.Name = "button4";
            this.button4.UseMnemonic = false;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.Cursor = System.Windows.Forms.Cursors.Default;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.Name = "button1";
            this.button1.UseMnemonic = false;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Cursor = System.Windows.Forms.Cursors.Default;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.Name = "button2";
            this.button2.UseMnemonic = false;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            resources.ApplyResources(this.button3, "button3");
            this.button3.Cursor = System.Windows.Forms.Cursors.Default;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.Name = "button3";
            this.button3.UseMnemonic = false;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // button6
            // 
            resources.ApplyResources(this.button6, "button6");
            this.button6.Cursor = System.Windows.Forms.Cursors.Default;
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.Name = "button6";
            this.button6.UseMnemonic = false;
            this.button6.UseVisualStyleBackColor = false;
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.tabControl1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.AllowDrop = true;
            // 
            // 
            // 
            this.tabControl1.DisplayStyleProvider.BlendStyle = TradeWright.UI.Forms.BlendStyle.Normal;
            this.tabControl1.DisplayStyleProvider.BorderColorDisabled = System.Drawing.SystemColors.ControlLight;
            this.tabControl1.DisplayStyleProvider.BorderColorFocused = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(157)))), ((int)(((byte)(185)))));
            this.tabControl1.DisplayStyleProvider.BorderColorHighlighted = System.Drawing.SystemColors.ControlDark;
            this.tabControl1.DisplayStyleProvider.BorderColorSelected = System.Drawing.SystemColors.ControlDark;
            this.tabControl1.DisplayStyleProvider.BorderColorUnselected = System.Drawing.SystemColors.ControlDark;
            this.tabControl1.DisplayStyleProvider.CloserButtonFillColorFocused = System.Drawing.Color.Empty;
            this.tabControl1.DisplayStyleProvider.CloserButtonFillColorFocusedActive = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tabControl1.DisplayStyleProvider.CloserButtonFillColorHighlighted = System.Drawing.Color.Empty;
            this.tabControl1.DisplayStyleProvider.CloserButtonFillColorHighlightedActive = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tabControl1.DisplayStyleProvider.CloserButtonFillColorSelected = System.Drawing.Color.Empty;
            this.tabControl1.DisplayStyleProvider.CloserButtonFillColorSelectedActive = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tabControl1.DisplayStyleProvider.CloserButtonFillColorUnselected = System.Drawing.Color.Empty;
            this.tabControl1.DisplayStyleProvider.CloserButtonOutlineColorFocused = System.Drawing.Color.Empty;
            this.tabControl1.DisplayStyleProvider.CloserButtonOutlineColorFocusedActive = System.Drawing.Color.Red;
            this.tabControl1.DisplayStyleProvider.CloserButtonOutlineColorHighlighted = System.Drawing.Color.Empty;
            this.tabControl1.DisplayStyleProvider.CloserButtonOutlineColorHighlightedActive = System.Drawing.Color.Red;
            this.tabControl1.DisplayStyleProvider.CloserButtonOutlineColorSelected = System.Drawing.Color.Empty;
            this.tabControl1.DisplayStyleProvider.CloserButtonOutlineColorSelectedActive = System.Drawing.Color.Red;
            this.tabControl1.DisplayStyleProvider.CloserButtonOutlineColorUnselected = System.Drawing.Color.Empty;
            this.tabControl1.DisplayStyleProvider.CloserColorFocused = System.Drawing.Color.Black;
            this.tabControl1.DisplayStyleProvider.CloserColorFocusedActive = System.Drawing.Color.White;
            this.tabControl1.DisplayStyleProvider.CloserColorHighlighted = System.Drawing.Color.Black;
            this.tabControl1.DisplayStyleProvider.CloserColorHighlightedActive = System.Drawing.Color.White;
            this.tabControl1.DisplayStyleProvider.CloserColorSelected = System.Drawing.Color.Black;
            this.tabControl1.DisplayStyleProvider.CloserColorSelectedActive = System.Drawing.Color.White;
            this.tabControl1.DisplayStyleProvider.CloserColorUnselected = System.Drawing.Color.Empty;
            this.tabControl1.DisplayStyleProvider.FocusTrack = false;
            this.tabControl1.DisplayStyleProvider.HotTrack = true;
            this.tabControl1.DisplayStyleProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tabControl1.DisplayStyleProvider.Opacity = 1F;
            this.tabControl1.DisplayStyleProvider.Overlap = 0;
            this.tabControl1.DisplayStyleProvider.Padding = new System.Drawing.Point(6, 3);
            this.tabControl1.DisplayStyleProvider.PageBackgroundColorDisabled = System.Drawing.SystemColors.Control;
            this.tabControl1.DisplayStyleProvider.PageBackgroundColorFocused = System.Drawing.SystemColors.ControlLight;
            this.tabControl1.DisplayStyleProvider.PageBackgroundColorHighlighted = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(244)))), ((int)(((byte)(252)))));
            this.tabControl1.DisplayStyleProvider.PageBackgroundColorSelected = System.Drawing.SystemColors.ControlLightLight;
            this.tabControl1.DisplayStyleProvider.PageBackgroundColorUnselected = System.Drawing.SystemColors.Control;
            this.tabControl1.DisplayStyleProvider.Radius = 2;
            this.tabControl1.DisplayStyleProvider.SelectedTabIsLarger = true;
            this.tabControl1.DisplayStyleProvider.ShowTabCloser = true;
            this.tabControl1.DisplayStyleProvider.TabColorDisabled1 = System.Drawing.SystemColors.Control;
            this.tabControl1.DisplayStyleProvider.TabColorDisabled2 = System.Drawing.SystemColors.Control;
            this.tabControl1.DisplayStyleProvider.TabColorFocused1 = System.Drawing.SystemColors.ControlLight;
            this.tabControl1.DisplayStyleProvider.TabColorFocused2 = System.Drawing.SystemColors.ControlLight;
            this.tabControl1.DisplayStyleProvider.TabColorHighLighted1 = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(244)))), ((int)(((byte)(252)))));
            this.tabControl1.DisplayStyleProvider.TabColorHighLighted2 = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(237)))), ((int)(((byte)(252)))));
            this.tabControl1.DisplayStyleProvider.TabColorSelected1 = System.Drawing.SystemColors.ControlLightLight;
            this.tabControl1.DisplayStyleProvider.TabColorSelected2 = System.Drawing.SystemColors.ControlLightLight;
            this.tabControl1.DisplayStyleProvider.TabColorUnSelected1 = System.Drawing.SystemColors.Control;
            this.tabControl1.DisplayStyleProvider.TabColorUnSelected2 = System.Drawing.SystemColors.Control;
            this.tabControl1.DisplayStyleProvider.TabPageMargin = new System.Windows.Forms.Padding(1);
            this.tabControl1.DisplayStyleProvider.TextColorDisabled = System.Drawing.SystemColors.ControlDark;
            this.tabControl1.DisplayStyleProvider.TextColorFocused = System.Drawing.SystemColors.ControlText;
            this.tabControl1.DisplayStyleProvider.TextColorHighlighted = System.Drawing.SystemColors.ControlText;
            this.tabControl1.DisplayStyleProvider.TextColorSelected = System.Drawing.SystemColors.ControlText;
            this.tabControl1.DisplayStyleProvider.TextColorUnselected = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.HotTrack = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.DragOver += new System.Windows.Forms.DragEventHandler(this.tabControl1_DragOver);
            this.tabControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseDown);
            this.tabControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseMove);
            // 
            // contextMenuStrip_Report
            // 
            this.contextMenuStrip_Report.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStrip_Report.Name = "contextMenuStrip_Report";
            this.contextMenuStrip_Report.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            resources.ApplyResources(this.contextMenuStrip_Report, "contextMenuStrip_Report");
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            this.toolStripMenuItem1.Click += new System.EventHandler(this.ToolStripMenuItem1_Click);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.contextMenuStrip_Product.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.contextMenuStrip_Report.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Product;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1_Product;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2_Station;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3_Brigade;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4_Owner;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Service;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Carriage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.GroupBox groupBox2;
        private TradeWright.UI.Forms.TabControlExtra tabControl1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Report;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}

