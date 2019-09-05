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
            this.dataGVOwner = new System.Windows.Forms.DataGridView();
            this.btnOwnerAdd = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGVOwner)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.btnOwnerReffresh);
            this.groupBox1.Controls.Add(this.btnOwnerDelete);
            this.groupBox1.Controls.Add(this.btnOwnerUpdate);
            this.groupBox1.Controls.Add(this.btnOwnerAdd);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(776, 63);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // btnOwnerReffresh
            // 
            this.btnOwnerReffresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnOwnerReffresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
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
            this.btnOwnerDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnOwnerDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
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
            this.btnOwnerUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnOwnerUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOwnerUpdate.Location = new System.Drawing.Point(93, 19);
            this.btnOwnerUpdate.Name = "btnOwnerUpdate";
            this.btnOwnerUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnOwnerUpdate.TabIndex = 0;
            this.btnOwnerUpdate.Text = "Изменить";
            this.btnOwnerUpdate.UseVisualStyleBackColor = true;
            this.btnOwnerUpdate.Click += new System.EventHandler(this.btnOwnerUpdate_Click);
            // 
            // dataGVOwner
            // 
            this.dataGVOwner.AllowUserToAddRows = false;
            this.dataGVOwner.AllowUserToDeleteRows = false;
            this.dataGVOwner.AllowUserToOrderColumns = true;
            this.dataGVOwner.AllowUserToResizeRows = false;
            this.dataGVOwner.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGVOwner.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGVOwner.Location = new System.Drawing.Point(12, 80);
            this.dataGVOwner.Name = "dataGVOwner";
            this.dataGVOwner.ReadOnly = true;
            this.dataGVOwner.Size = new System.Drawing.Size(1246, 367);
            this.dataGVOwner.TabIndex = 3;
            this.dataGVOwner.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGVOwner_CellClick);
            // 
            // btnOwnerAdd
            // 
            this.btnOwnerAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnOwnerAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOwnerAdd.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnOwnerAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOwnerAdd.Location = new System.Drawing.Point(7, 19);
            this.btnOwnerAdd.Name = "btnOwnerAdd";
            this.btnOwnerAdd.Size = new System.Drawing.Size(80, 23);
            this.btnOwnerAdd.TabIndex = 0;
            this.btnOwnerAdd.Text = "Добавить";
            this.btnOwnerAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOwnerAdd.UseMnemonic = false;
            this.btnOwnerAdd.UseVisualStyleBackColor = false;
            this.btnOwnerAdd.Click += new System.EventHandler(this.btnOwnerAdd_Click);
            // 
            // OwnerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1270, 459);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGVOwner);
            this.Name = "OwnerForm";
            this.Text = "OwnerForm";
            this.Load += new System.EventHandler(this.OwnerForm_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGVOwner)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnOwnerReffresh;
        private System.Windows.Forms.Button btnOwnerDelete;
        private System.Windows.Forms.Button btnOwnerUpdate;
        private System.Windows.Forms.Button btnOwnerAdd;
        private System.Windows.Forms.DataGridView dataGVOwner;
    }
}