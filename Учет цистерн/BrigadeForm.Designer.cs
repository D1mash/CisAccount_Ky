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
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGVBrigade)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnBrigadeReffresh);
            this.groupBox1.Controls.Add(this.btnBrigadeDelete);
            this.groupBox1.Controls.Add(this.btnBrigadeUpdate);
            this.groupBox1.Controls.Add(this.btnBrigadeAdd);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(776, 55);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnBrigadeReffresh
            // 
            this.btnBrigadeReffresh.Location = new System.Drawing.Point(250, 20);
            this.btnBrigadeReffresh.Name = "btnBrigadeReffresh";
            this.btnBrigadeReffresh.Size = new System.Drawing.Size(75, 23);
            this.btnBrigadeReffresh.TabIndex = 0;
            this.btnBrigadeReffresh.Text = "Обновить";
            this.btnBrigadeReffresh.UseVisualStyleBackColor = true;
            this.btnBrigadeReffresh.Click += new System.EventHandler(this.BtnBrigadeReffresh_Click);
            // 
            // btnBrigadeDelete
            // 
            this.btnBrigadeDelete.Location = new System.Drawing.Point(169, 19);
            this.btnBrigadeDelete.Name = "btnBrigadeDelete";
            this.btnBrigadeDelete.Size = new System.Drawing.Size(75, 23);
            this.btnBrigadeDelete.TabIndex = 0;
            this.btnBrigadeDelete.Text = "Удалить";
            this.btnBrigadeDelete.UseVisualStyleBackColor = true;
            this.btnBrigadeDelete.Click += new System.EventHandler(this.BtnBrigadeDelete_Click);
            // 
            // btnBrigadeUpdate
            // 
            this.btnBrigadeUpdate.Location = new System.Drawing.Point(88, 20);
            this.btnBrigadeUpdate.Name = "btnBrigadeUpdate";
            this.btnBrigadeUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnBrigadeUpdate.TabIndex = 0;
            this.btnBrigadeUpdate.Text = "Изменить";
            this.btnBrigadeUpdate.UseVisualStyleBackColor = true;
            this.btnBrigadeUpdate.Click += new System.EventHandler(this.BtnBrigadeUpdate_Click);
            // 
            // btnBrigadeAdd
            // 
            this.btnBrigadeAdd.Location = new System.Drawing.Point(7, 20);
            this.btnBrigadeAdd.Name = "btnBrigadeAdd";
            this.btnBrigadeAdd.Size = new System.Drawing.Size(75, 23);
            this.btnBrigadeAdd.TabIndex = 0;
            this.btnBrigadeAdd.Text = "Добавить";
            this.btnBrigadeAdd.UseVisualStyleBackColor = true;
            this.btnBrigadeAdd.Click += new System.EventHandler(this.BtnBrigadeAdd_Click);
            // 
            // dataGVBrigade
            // 
            this.dataGVBrigade.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGVBrigade.Location = new System.Drawing.Point(12, 73);
            this.dataGVBrigade.Name = "dataGVBrigade";
            this.dataGVBrigade.Size = new System.Drawing.Size(1246, 374);
            this.dataGVBrigade.TabIndex = 1;
            this.dataGVBrigade.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGVBrigade_CellClick);
            // 
            // BrigadeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1270, 450);
            this.Controls.Add(this.dataGVBrigade);
            this.Controls.Add(this.groupBox1);
            this.Name = "BrigadeForm";
            this.Text = "FormBrigade";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGVBrigade)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBrigadeReffresh;
        private System.Windows.Forms.Button btnBrigadeDelete;
        private System.Windows.Forms.Button btnBrigadeUpdate;
        private System.Windows.Forms.Button btnBrigadeAdd;
        private System.Windows.Forms.DataGridView dataGVBrigade;
    }
}