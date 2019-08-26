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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_refsh_station_form = new System.Windows.Forms.Button();
            this.btn_dlt_station_form = new System.Windows.Forms.Button();
            this.btn_upd_station_form = new System.Windows.Forms.Button();
            this.btn_add_station_form = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView_Station_Form = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Station_Form)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_refsh_station_form);
            this.panel1.Controls.Add(this.btn_dlt_station_form);
            this.panel1.Controls.Add(this.btn_upd_station_form);
            this.panel1.Controls.Add(this.btn_add_station_form);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1370, 63);
            this.panel1.TabIndex = 1;
            // 
            // btn_refsh_station_form
            // 
            this.btn_refsh_station_form.Location = new System.Drawing.Point(264, 21);
            this.btn_refsh_station_form.Name = "btn_refsh_station_form";
            this.btn_refsh_station_form.Size = new System.Drawing.Size(75, 23);
            this.btn_refsh_station_form.TabIndex = 3;
            this.btn_refsh_station_form.Text = "Обновить";
            this.btn_refsh_station_form.UseVisualStyleBackColor = true;
            this.btn_refsh_station_form.Click += new System.EventHandler(this.btn_refsh_station_form_Click);
            // 
            // btn_dlt_station_form
            // 
            this.btn_dlt_station_form.Location = new System.Drawing.Point(183, 21);
            this.btn_dlt_station_form.Name = "btn_dlt_station_form";
            this.btn_dlt_station_form.Size = new System.Drawing.Size(75, 23);
            this.btn_dlt_station_form.TabIndex = 2;
            this.btn_dlt_station_form.Text = "Удалить";
            this.btn_dlt_station_form.UseVisualStyleBackColor = true;
            this.btn_dlt_station_form.Click += new System.EventHandler(this.btn_dlt_station_form_Click);
            // 
            // btn_upd_station_form
            // 
            this.btn_upd_station_form.Location = new System.Drawing.Point(102, 21);
            this.btn_upd_station_form.Name = "btn_upd_station_form";
            this.btn_upd_station_form.Size = new System.Drawing.Size(75, 23);
            this.btn_upd_station_form.TabIndex = 1;
            this.btn_upd_station_form.Text = "Изменить";
            this.btn_upd_station_form.UseVisualStyleBackColor = true;
            this.btn_upd_station_form.Click += new System.EventHandler(this.btn_upd_station_form_Click);
            // 
            // btn_add_station_form
            // 
            this.btn_add_station_form.Location = new System.Drawing.Point(21, 21);
            this.btn_add_station_form.Name = "btn_add_station_form";
            this.btn_add_station_form.Size = new System.Drawing.Size(75, 23);
            this.btn_add_station_form.TabIndex = 0;
            this.btn_add_station_form.Text = "Добавить";
            this.btn_add_station_form.UseVisualStyleBackColor = true;
            this.btn_add_station_form.Click += new System.EventHandler(this.btn_add_station_form_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridView_Station_Form);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 63);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1370, 686);
            this.panel2.TabIndex = 2;
            // 
            // dataGridView_Station_Form
            // 
            this.dataGridView_Station_Form.AllowUserToAddRows = false;
            this.dataGridView_Station_Form.AllowUserToDeleteRows = false;
            this.dataGridView_Station_Form.AllowUserToResizeRows = false;
            this.dataGridView_Station_Form.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_Station_Form.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Station_Form.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Station_Form.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_Station_Form.Name = "dataGridView_Station_Form";
            this.dataGridView_Station_Form.ReadOnly = true;
            this.dataGridView_Station_Form.Size = new System.Drawing.Size(1370, 686);
            this.dataGridView_Station_Form.TabIndex = 0;
            this.dataGridView_Station_Form.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_Station_Form_CellClick);
            // 
            // StationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 749);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "StationForm";
            this.Text = "StationForm";
            this.Load += new System.EventHandler(this.StationForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Station_Form)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_add_station_form;
        private System.Windows.Forms.Button btn_refsh_station_form;
        private System.Windows.Forms.Button btn_dlt_station_form;
        private System.Windows.Forms.Button btn_upd_station_form;
        private System.Windows.Forms.DataGridView dataGridView_Station_Form;
    }
}