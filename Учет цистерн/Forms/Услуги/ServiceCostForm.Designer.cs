namespace Учет_цистерн
{
    partial class ServiceCostForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Btn_Refresh = new System.Windows.Forms.Button();
            this.Btn_Delete = new System.Windows.Forms.Button();
            this.Btn_Updt = new System.Windows.Forms.Button();
            this.Btn_Add = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.Btn_Refresh);
            this.groupBox1.Controls.Add(this.Btn_Delete);
            this.groupBox1.Controls.Add(this.Btn_Updt);
            this.groupBox1.Controls.Add(this.Btn_Add);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1370, 61);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // Btn_Refresh
            // 
            this.Btn_Refresh.Location = new System.Drawing.Point(255, 19);
            this.Btn_Refresh.Name = "Btn_Refresh";
            this.Btn_Refresh.Size = new System.Drawing.Size(75, 23);
            this.Btn_Refresh.TabIndex = 3;
            this.Btn_Refresh.Text = "Обновить";
            this.Btn_Refresh.UseVisualStyleBackColor = true;
            this.Btn_Refresh.Click += new System.EventHandler(this.Btn_Refresh_Click);
            // 
            // Btn_Delete
            // 
            this.Btn_Delete.Location = new System.Drawing.Point(174, 19);
            this.Btn_Delete.Name = "Btn_Delete";
            this.Btn_Delete.Size = new System.Drawing.Size(75, 23);
            this.Btn_Delete.TabIndex = 2;
            this.Btn_Delete.Text = "Удалить";
            this.Btn_Delete.UseVisualStyleBackColor = true;
            this.Btn_Delete.Click += new System.EventHandler(this.Btn_Delete_Click);
            // 
            // Btn_Updt
            // 
            this.Btn_Updt.Location = new System.Drawing.Point(93, 19);
            this.Btn_Updt.Name = "Btn_Updt";
            this.Btn_Updt.Size = new System.Drawing.Size(75, 23);
            this.Btn_Updt.TabIndex = 1;
            this.Btn_Updt.Text = "Изменить";
            this.Btn_Updt.UseVisualStyleBackColor = true;
            this.Btn_Updt.Click += new System.EventHandler(this.Btn_Updt_Click);
            // 
            // Btn_Add
            // 
            this.Btn_Add.Location = new System.Drawing.Point(12, 19);
            this.Btn_Add.Name = "Btn_Add";
            this.Btn_Add.Size = new System.Drawing.Size(75, 23);
            this.Btn_Add.TabIndex = 0;
            this.Btn_Add.Text = "Добавить";
            this.Btn_Add.UseVisualStyleBackColor = true;
            this.Btn_Add.Click += new System.EventHandler(this.Btn_Add_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gridControl1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 61);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1370, 688);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(3, 16);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1364, 669);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsLayout.Columns.AddNewColumns = false;
            this.gridView1.OptionsMenu.EnableColumnMenu = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gridView1_RowCellClick);
            this.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView1_RowStyle);
            // 
            // ServiceCostForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1370, 749);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ServiceCostForm";
            this.Text = "ServiceCostForm";
            this.Load += new System.EventHandler(this.ServiceCostForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button Btn_Refresh;
        private System.Windows.Forms.Button Btn_Delete;
        private System.Windows.Forms.Button Btn_Updt;
        private System.Windows.Forms.Button Btn_Add;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}