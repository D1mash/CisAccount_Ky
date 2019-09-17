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
            this.dataGridView_Station_Form = new System.Windows.Forms.DataGridView();
            this.btn_refsh_station_form = new System.Windows.Forms.Button();
            this.btn_dlt_station_form = new System.Windows.Forms.Button();
            this.btn_upd_station_form = new System.Windows.Forms.Button();
            this.btn_add_station_form = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Station_Form)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView_Station_Form
            // 
            this.dataGridView_Station_Form.AllowUserToAddRows = false;
            this.dataGridView_Station_Form.AllowUserToDeleteRows = false;
            this.dataGridView_Station_Form.AllowUserToResizeRows = false;
            this.dataGridView_Station_Form.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_Station_Form.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView_Station_Form.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Station_Form.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Station_Form.Location = new System.Drawing.Point(3, 16);
            this.dataGridView_Station_Form.Name = "dataGridView_Station_Form";
            this.dataGridView_Station_Form.ReadOnly = true;
            this.dataGridView_Station_Form.Size = new System.Drawing.Size(1364, 669);
            this.dataGridView_Station_Form.TabIndex = 3;
            this.dataGridView_Station_Form.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Station_Form_CellClick_1);
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
            this.groupBox2.Controls.Add(this.dataGridView_Station_Form);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 61);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1370, 688);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Station_Form)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_Station_Form;
        private System.Windows.Forms.Button btn_refsh_station_form;
        private System.Windows.Forms.Button btn_dlt_station_form;
        private System.Windows.Forms.Button btn_upd_station_form;
        private System.Windows.Forms.Button btn_add_station_form;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}