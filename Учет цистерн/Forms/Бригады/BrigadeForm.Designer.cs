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
            this.dataGVBrigade = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGVBrigade)).BeginInit();
            this.groupBox2.SuspendLayout();
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
            // dataGVBrigade
            // 
            this.dataGVBrigade.AllowUserToAddRows = false;
            this.dataGVBrigade.AllowUserToDeleteRows = false;
            this.dataGVBrigade.AllowUserToOrderColumns = true;
            this.dataGVBrigade.AllowUserToResizeRows = false;
            this.dataGVBrigade.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGVBrigade.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGVBrigade.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGVBrigade.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGVBrigade.Location = new System.Drawing.Point(3, 16);
            this.dataGVBrigade.Name = "dataGVBrigade";
            this.dataGVBrigade.ReadOnly = true;
            this.dataGVBrigade.Size = new System.Drawing.Size(1364, 669);
            this.dataGVBrigade.TabIndex = 1;
            this.dataGVBrigade.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGVBrigade_CellClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGVBrigade);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 61);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1370, 688);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // BrigadeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1370, 749);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "BrigadeForm";
            this.Text = "FormBrigade";
            this.Load += new System.EventHandler(this.BrigadeForm_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGVBrigade)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBrigadeReffresh;
        private System.Windows.Forms.Button btnBrigadeDelete;
        private System.Windows.Forms.Button btnBrigadeUpdate;
        private System.Windows.Forms.Button btnBrigadeAdd;
        private System.Windows.Forms.DataGridView dataGVBrigade;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}