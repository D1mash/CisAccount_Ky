namespace Учет_цистерн
{
    partial class StationForm
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
            this.btn_refsh_station_form = new System.Windows.Forms.Button();
            this.btn_dlt_station_form = new System.Windows.Forms.Button();
            this.btn_upd_station_form = new System.Windows.Forms.Button();
            this.btn_add_station_form = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_refsh_station_form
            // 
            this.btn_refsh_station_form.Location = new System.Drawing.Point(255, 19);
            this.btn_refsh_station_form.Name = "btn_refsh_station_form";
            this.btn_refsh_station_form.Size = new System.Drawing.Size(75, 23);
            this.btn_refsh_station_form.TabIndex = 7;
            this.btn_refsh_station_form.Text = "Обновить";
            this.btn_refsh_station_form.UseVisualStyleBackColor = true;
            this.btn_refsh_station_form.Click += new System.EventHandler(this.btn_refsh_station_form_Click_1);
            // 
            // btn_dlt_station_form
            // 
            this.btn_dlt_station_form.Location = new System.Drawing.Point(174, 19);
            this.btn_dlt_station_form.Name = "btn_dlt_station_form";
            this.btn_dlt_station_form.Size = new System.Drawing.Size(75, 23);
            this.btn_dlt_station_form.TabIndex = 6;
            this.btn_dlt_station_form.Text = "Удалить";
            this.btn_dlt_station_form.UseVisualStyleBackColor = true;
            this.btn_dlt_station_form.Click += new System.EventHandler(this.btn_dlt_station_form_Click_1);
            // 
            // btn_upd_station_form
            // 
            this.btn_upd_station_form.Location = new System.Drawing.Point(93, 19);
            this.btn_upd_station_form.Name = "btn_upd_station_form";
            this.btn_upd_station_form.Size = new System.Drawing.Size(75, 23);
            this.btn_upd_station_form.TabIndex = 5;
            this.btn_upd_station_form.Text = "Изменить";
            this.btn_upd_station_form.UseVisualStyleBackColor = true;
            this.btn_upd_station_form.Click += new System.EventHandler(this.btn_upd_station_form_Click);
            // 
            // btn_add_station_form
            // 
            this.btn_add_station_form.Location = new System.Drawing.Point(12, 19);
            this.btn_add_station_form.Name = "btn_add_station_form";
            this.btn_add_station_form.Size = new System.Drawing.Size(75, 23);
            this.btn_add_station_form.TabIndex = 4;
            this.btn_add_station_form.Text = "Добавить";
            this.btn_add_station_form.UseVisualStyleBackColor = true;
            this.btn_add_station_form.Click += new System.EventHandler(this.btn_add_station_form_Click_1);
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.btn_refsh_station_form);
            this.groupBox1.Controls.Add(this.btn_add_station_form);
            this.groupBox1.Controls.Add(this.btn_dlt_station_form);
            this.groupBox1.Controls.Add(this.btn_upd_station_form);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1370, 61);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gridControl1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 61);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1370, 688);
            this.groupBox2.TabIndex = 9;
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
            this.gridColumn4});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsLayout.Columns.AddNewColumns = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gridView1_RowCellClick);
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
            this.gridColumn2.Caption = "Наименование";
            this.gridColumn2.FieldName = "Name";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "Name", "Кол-во: {0}", "0")});
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "5-значный код";
            this.gridColumn3.FieldName = "Code";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "6-значный код";
            this.gridColumn4.FieldName = "Code6";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            // 
            // StationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1370, 749);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "StationForm";
            this.Text = "StationForm";
            this.Load += new System.EventHandler(this.StationForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_refsh_station_form;
        private System.Windows.Forms.Button btn_dlt_station_form;
        private System.Windows.Forms.Button btn_upd_station_form;
        private System.Windows.Forms.Button btn_add_station_form;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
    }
}