namespace Учет_цистерн
{
    partial class BrigadeForm
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
            this.btnBrigadeReffresh = new System.Windows.Forms.Button();
            this.btnBrigadeDelete = new System.Windows.Forms.Button();
            this.btnBrigadeUpdate = new System.Windows.Forms.Button();
            this.btnBrigadeAdd = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.btnBrigadeReffresh);
            this.groupBox1.Controls.Add(this.btnBrigadeDelete);
            this.groupBox1.Controls.Add(this.btnBrigadeUpdate);
            this.groupBox1.Controls.Add(this.btnBrigadeAdd);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1370, 61);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnBrigadeReffresh
            // 
            this.btnBrigadeReffresh.Location = new System.Drawing.Point(255, 19);
            this.btnBrigadeReffresh.Name = "btnBrigadeReffresh";
            this.btnBrigadeReffresh.Size = new System.Drawing.Size(75, 23);
            this.btnBrigadeReffresh.TabIndex = 0;
            this.btnBrigadeReffresh.Text = "Обновить";
            this.btnBrigadeReffresh.UseVisualStyleBackColor = true;
            this.btnBrigadeReffresh.Click += new System.EventHandler(this.BtnBrigadeReffresh_Click);
            // 
            // btnBrigadeDelete
            // 
            this.btnBrigadeDelete.Location = new System.Drawing.Point(174, 19);
            this.btnBrigadeDelete.Name = "btnBrigadeDelete";
            this.btnBrigadeDelete.Size = new System.Drawing.Size(75, 23);
            this.btnBrigadeDelete.TabIndex = 0;
            this.btnBrigadeDelete.Text = "Удалить";
            this.btnBrigadeDelete.UseVisualStyleBackColor = true;
            this.btnBrigadeDelete.Click += new System.EventHandler(this.BtnBrigadeDelete_Click);
            // 
            // btnBrigadeUpdate
            // 
            this.btnBrigadeUpdate.Location = new System.Drawing.Point(93, 19);
            this.btnBrigadeUpdate.Name = "btnBrigadeUpdate";
            this.btnBrigadeUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnBrigadeUpdate.TabIndex = 0;
            this.btnBrigadeUpdate.Text = "Изменить";
            this.btnBrigadeUpdate.UseVisualStyleBackColor = true;
            this.btnBrigadeUpdate.Click += new System.EventHandler(this.BtnBrigadeUpdate_Click);
            // 
            // btnBrigadeAdd
            // 
            this.btnBrigadeAdd.Location = new System.Drawing.Point(12, 19);
            this.btnBrigadeAdd.Name = "btnBrigadeAdd";
            this.btnBrigadeAdd.Size = new System.Drawing.Size(75, 23);
            this.btnBrigadeAdd.TabIndex = 0;
            this.btnBrigadeAdd.Text = "Добавить";
            this.btnBrigadeAdd.UseVisualStyleBackColor = true;
            this.btnBrigadeAdd.Click += new System.EventHandler(this.BtnBrigadeAdd_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gridControl1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 61);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1370, 688);
            this.groupBox2.TabIndex = 2;
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
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gridView1_RowCellClick);
            this.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView1_RowStyle);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "ID";
            this.gridColumn1.FieldName = "ID";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Имя";
            this.gridColumn2.FieldName = "Name";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "Name", "Кол-во:{0}", "0")});
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Фамилия";
            this.gridColumn3.FieldName = "Surname";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Отчество";
            this.gridColumn4.FieldName = "Lastname";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "ФИО";
            this.gridColumn5.FieldName = "FIO";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            // 
            // BrigadeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.ClientSize = new System.Drawing.Size(1370, 749);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "BrigadeForm";
            this.Text = "FormBrigade";
            this.Load += new System.EventHandler(this.BrigadeForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBrigadeReffresh;
        private System.Windows.Forms.Button btnBrigadeDelete;
        private System.Windows.Forms.Button btnBrigadeUpdate;
        private System.Windows.Forms.Button btnBrigadeAdd;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
    }
}