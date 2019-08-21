namespace Учет_цистерн
{
    partial class AddNewStation_StationForm
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
            this.label_Add_Name_StationForm = new System.Windows.Forms.Label();
            this.label_Add_Code_StationForm = new System.Windows.Forms.Label();
            this.label_Add_Code6_StationForm = new System.Windows.Forms.Label();
            this.textBox_Add_Name_StationForm = new System.Windows.Forms.TextBox();
            this.textBox_Add_Code_StationForm = new System.Windows.Forms.TextBox();
            this.textBox_Add_Code6_StationForm = new System.Windows.Forms.TextBox();
            this.button_Add_OK_StationForm = new System.Windows.Forms.Button();
            this.button_Add_Cancel_StationForm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_Add_Name_StationForm
            // 
            this.label_Add_Name_StationForm.AutoSize = true;
            this.label_Add_Name_StationForm.Location = new System.Drawing.Point(12, 35);
            this.label_Add_Name_StationForm.Name = "label_Add_Name_StationForm";
            this.label_Add_Name_StationForm.Size = new System.Drawing.Size(83, 13);
            this.label_Add_Name_StationForm.TabIndex = 0;
            this.label_Add_Name_StationForm.Text = "Наименование";
            // 
            // label_Add_Code_StationForm
            // 
            this.label_Add_Code_StationForm.AutoSize = true;
            this.label_Add_Code_StationForm.Location = new System.Drawing.Point(12, 61);
            this.label_Add_Code_StationForm.Name = "label_Add_Code_StationForm";
            this.label_Add_Code_StationForm.Size = new System.Drawing.Size(26, 13);
            this.label_Add_Code_StationForm.TabIndex = 1;
            this.label_Add_Code_StationForm.Text = "Код";
            // 
            // label_Add_Code6_StationForm
            // 
            this.label_Add_Code6_StationForm.AutoSize = true;
            this.label_Add_Code6_StationForm.Location = new System.Drawing.Point(12, 87);
            this.label_Add_Code6_StationForm.Name = "label_Add_Code6_StationForm";
            this.label_Add_Code6_StationForm.Size = new System.Drawing.Size(35, 13);
            this.label_Add_Code6_StationForm.TabIndex = 2;
            this.label_Add_Code6_StationForm.Text = "Код 6";
            // 
            // textBox_Add_Name_StationForm
            // 
            this.textBox_Add_Name_StationForm.Location = new System.Drawing.Point(125, 32);
            this.textBox_Add_Name_StationForm.Name = "textBox_Add_Name_StationForm";
            this.textBox_Add_Name_StationForm.Size = new System.Drawing.Size(236, 20);
            this.textBox_Add_Name_StationForm.TabIndex = 3;
            // 
            // textBox_Add_Code_StationForm
            // 
            this.textBox_Add_Code_StationForm.Location = new System.Drawing.Point(125, 58);
            this.textBox_Add_Code_StationForm.Name = "textBox_Add_Code_StationForm";
            this.textBox_Add_Code_StationForm.Size = new System.Drawing.Size(236, 20);
            this.textBox_Add_Code_StationForm.TabIndex = 4;
            // 
            // textBox_Add_Code6_StationForm
            // 
            this.textBox_Add_Code6_StationForm.Location = new System.Drawing.Point(125, 84);
            this.textBox_Add_Code6_StationForm.Name = "textBox_Add_Code6_StationForm";
            this.textBox_Add_Code6_StationForm.Size = new System.Drawing.Size(236, 20);
            this.textBox_Add_Code6_StationForm.TabIndex = 5;
            // 
            // button_Add_OK_StationForm
            // 
            this.button_Add_OK_StationForm.Location = new System.Drawing.Point(205, 140);
            this.button_Add_OK_StationForm.Name = "button_Add_OK_StationForm";
            this.button_Add_OK_StationForm.Size = new System.Drawing.Size(75, 23);
            this.button_Add_OK_StationForm.TabIndex = 6;
            this.button_Add_OK_StationForm.Text = "ОК";
            this.button_Add_OK_StationForm.UseVisualStyleBackColor = true;
            this.button_Add_OK_StationForm.Click += new System.EventHandler(this.button_OK_StationForm_Click);
            // 
            // button_Add_Cancel_StationForm
            // 
            this.button_Add_Cancel_StationForm.Location = new System.Drawing.Point(286, 140);
            this.button_Add_Cancel_StationForm.Name = "button_Add_Cancel_StationForm";
            this.button_Add_Cancel_StationForm.Size = new System.Drawing.Size(75, 23);
            this.button_Add_Cancel_StationForm.TabIndex = 7;
            this.button_Add_Cancel_StationForm.Text = "Отмена";
            this.button_Add_Cancel_StationForm.UseVisualStyleBackColor = true;
            this.button_Add_Cancel_StationForm.Click += new System.EventHandler(this.button_Add_Cancel_StationForm_Click);
            // 
            // AddNewStation_StationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 190);
            this.Controls.Add(this.button_Add_Cancel_StationForm);
            this.Controls.Add(this.button_Add_OK_StationForm);
            this.Controls.Add(this.textBox_Add_Code6_StationForm);
            this.Controls.Add(this.textBox_Add_Code_StationForm);
            this.Controls.Add(this.textBox_Add_Name_StationForm);
            this.Controls.Add(this.label_Add_Code6_StationForm);
            this.Controls.Add(this.label_Add_Code_StationForm);
            this.Controls.Add(this.label_Add_Name_StationForm);
            this.Name = "AddNewStation_StationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавление в \"Станции\"";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label_Add_Name_StationForm;
        public System.Windows.Forms.Label label_Add_Code_StationForm;
        public System.Windows.Forms.Label label_Add_Code6_StationForm;
        public System.Windows.Forms.TextBox textBox_Add_Name_StationForm;
        public System.Windows.Forms.TextBox textBox_Add_Code_StationForm;
        public System.Windows.Forms.TextBox textBox_Add_Code6_StationForm;
        public System.Windows.Forms.Button button_Add_OK_StationForm;
        public System.Windows.Forms.Button button_Add_Cancel_StationForm;
    }
}