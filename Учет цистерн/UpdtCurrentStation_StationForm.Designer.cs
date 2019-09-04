namespace Учет_цистерн
{
    partial class UpdtCurrentStation_StationForm
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
            this.button_Updt_Cancel_StationForm = new System.Windows.Forms.Button();
            this.button_Updt_OK_StationForm = new System.Windows.Forms.Button();
            this.textBox_Updt_Code6_StationForm = new System.Windows.Forms.TextBox();
            this.textBox_Updt_Code_StationForm = new System.Windows.Forms.TextBox();
            this.textBox_Updt_Name_StationForm = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button_Updt_Cancel_StationForm
            // 
            this.button_Updt_Cancel_StationForm.Location = new System.Drawing.Point(286, 138);
            this.button_Updt_Cancel_StationForm.Name = "button_Updt_Cancel_StationForm";
            this.button_Updt_Cancel_StationForm.Size = new System.Drawing.Size(75, 23);
            this.button_Updt_Cancel_StationForm.TabIndex = 15;
            this.button_Updt_Cancel_StationForm.Text = "Отмена";
            this.button_Updt_Cancel_StationForm.UseVisualStyleBackColor = true;
            this.button_Updt_Cancel_StationForm.Click += new System.EventHandler(this.button_Updt_Cancel_StationForm_Click);
            // 
            // button_Updt_OK_StationForm
            // 
            this.button_Updt_OK_StationForm.Location = new System.Drawing.Point(205, 138);
            this.button_Updt_OK_StationForm.Name = "button_Updt_OK_StationForm";
            this.button_Updt_OK_StationForm.Size = new System.Drawing.Size(75, 23);
            this.button_Updt_OK_StationForm.TabIndex = 14;
            this.button_Updt_OK_StationForm.Text = "ОК";
            this.button_Updt_OK_StationForm.UseVisualStyleBackColor = true;
            this.button_Updt_OK_StationForm.Click += new System.EventHandler(this.button_Updt_OK_StationForm_Click);
            // 
            // textBox_Updt_Code6_StationForm
            // 
            this.textBox_Updt_Code6_StationForm.Location = new System.Drawing.Point(125, 82);
            this.textBox_Updt_Code6_StationForm.Name = "textBox_Updt_Code6_StationForm";
            this.textBox_Updt_Code6_StationForm.Size = new System.Drawing.Size(236, 20);
            this.textBox_Updt_Code6_StationForm.TabIndex = 13;
            // 
            // textBox_Updt_Code_StationForm
            // 
            this.textBox_Updt_Code_StationForm.Location = new System.Drawing.Point(125, 56);
            this.textBox_Updt_Code_StationForm.Name = "textBox_Updt_Code_StationForm";
            this.textBox_Updt_Code_StationForm.Size = new System.Drawing.Size(236, 20);
            this.textBox_Updt_Code_StationForm.TabIndex = 12;
            // 
            // textBox_Updt_Name_StationForm
            // 
            this.textBox_Updt_Name_StationForm.Location = new System.Drawing.Point(125, 30);
            this.textBox_Updt_Name_StationForm.Name = "textBox_Updt_Name_StationForm";
            this.textBox_Updt_Name_StationForm.Size = new System.Drawing.Size(236, 20);
            this.textBox_Updt_Name_StationForm.TabIndex = 11;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 32);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(102, 17);
            this.checkBox1.TabIndex = 16;
            this.checkBox1.Text = "Наименование";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckStateChanged += new System.EventHandler(this.checkBox1_CheckStateChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(12, 58);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(45, 17);
            this.checkBox2.TabIndex = 17;
            this.checkBox2.Text = "Код";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckStateChanged += new System.EventHandler(this.checkBox2_CheckStateChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(12, 84);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(54, 17);
            this.checkBox3.TabIndex = 18;
            this.checkBox3.Text = "Код 6";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckStateChanged += new System.EventHandler(this.checkBox3_CheckStateChanged);
            // 
            // UpdtCurrentStation_StationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 183);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button_Updt_Cancel_StationForm);
            this.Controls.Add(this.button_Updt_OK_StationForm);
            this.Controls.Add(this.textBox_Updt_Code6_StationForm);
            this.Controls.Add(this.textBox_Updt_Code_StationForm);
            this.Controls.Add(this.textBox_Updt_Name_StationForm);
            this.Name = "UpdtCurrentStation_StationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактирование в \"Станции\"";
            this.Load += new System.EventHandler(this.UpdtCurrentStation_StationForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button button_Updt_Cancel_StationForm;
        public System.Windows.Forms.Button button_Updt_OK_StationForm;
        public System.Windows.Forms.TextBox textBox_Updt_Code6_StationForm;
        public System.Windows.Forms.TextBox textBox_Updt_Code_StationForm;
        public System.Windows.Forms.TextBox textBox_Updt_Name_StationForm;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
    }
}