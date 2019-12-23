namespace Учет_цистерн
{
    partial class OwnerForm
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
            this.btnOwnerReffresh = new System.Windows.Forms.Button();
            this.btnOwnerDelete = new System.Windows.Forms.Button();
            this.btnOwnerUpdate = new System.Windows.Forms.Button();
            this.btnOwnerAdd = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnOwnerReffresh);
            this.groupBox1.Controls.Add(this.btnOwnerDelete);
            this.groupBox1.Controls.Add(this.btnOwnerUpdate);
            this.groupBox1.Controls.Add(this.btnOwnerAdd);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1370, 50);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // btnOwnerReffresh
            // 
            this.btnOwnerReffresh.Location = new System.Drawing.Point(255, 19);
            this.btnOwnerReffresh.Name = "btnOwnerReffresh";
            this.btnOwnerReffresh.Size = new System.Drawing.Size(75, 23);
            this.btnOwnerReffresh.TabIndex = 0;
            this.btnOwnerReffresh.Text = "Обновить";
            this.btnOwnerReffresh.UseVisualStyleBackColor = true;
            this.btnOwnerReffresh.Click += new System.EventHandler(this.btnOwnerReffresh_Click);
            // 
            // btnOwnerDelete
            // 
            this.btnOwnerDelete.Location = new System.Drawing.Point(174, 19);
            this.btnOwnerDelete.Name = "btnOwnerDelete";
            this.btnOwnerDelete.Size = new System.Drawing.Size(75, 23);
            this.btnOwnerDelete.TabIndex = 0;
            this.btnOwnerDelete.Text = "Удалить";
            this.btnOwnerDelete.UseVisualStyleBackColor = true;
            this.btnOwnerDelete.Click += new System.EventHandler(this.btnOwnerDelete_Click);
            // 
            // btnOwnerUpdate
            // 
            this.btnOwnerUpdate.Location = new System.Drawing.Point(93, 19);
            this.btnOwnerUpdate.Name = "btnOwnerUpdate";
            this.btnOwnerUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnOwnerUpdate.TabIndex = 0;
            this.btnOwnerUpdate.Text = "Изменить";
            this.btnOwnerUpdate.UseVisualStyleBackColor = true;
            this.btnOwnerUpdate.Click += new System.EventHandler(this.btnOwnerUpdate_Click);
            // 
            // btnOwnerAdd
            // 
            this.btnOwnerAdd.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnOwnerAdd.Location = new System.Drawing.Point(12, 19);
            this.btnOwnerAdd.Name = "btnOwnerAdd";
            this.btnOwnerAdd.Size = new System.Drawing.Size(75, 23);
            this.btnOwnerAdd.TabIndex = 0;
            this.btnOwnerAdd.Text = "Добавить";
            this.btnOwnerAdd.UseVisualStyleBackColor = true;
            this.btnOwnerAdd.Click += new System.EventHandler(this.btnOwnerAdd_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gridControl1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 50);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1370, 699);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(3, 16);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1364, 680);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
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
            this.gridColumn3.Caption = "Полное наименование";
            this.gridColumn3.FieldName = "FullName";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // OwnerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1370, 749);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "OwnerForm";
            this.Text = "OwnerForm";
            this.Load += new System.EventHandler(this.OwnerForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnOwnerReffresh;
        private System.Windows.Forms.Button btnOwnerDelete;
        private System.Windows.Forms.Button btnOwnerUpdate;
        private System.Windows.Forms.Button btnOwnerAdd;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
    }
}