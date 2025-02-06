namespace hilal_Haberci
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Uretim = new Button();
            haberBox = new TextBox();
            anahtarBox = new TextBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // Uretim
            // 
            Uretim.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
            Uretim.Location = new Point(745, 165);
            Uretim.Name = "Uretim";
            Uretim.Size = new Size(251, 60);
            Uretim.TabIndex = 0;
            Uretim.Text = "Haber Üret";
            Uretim.UseVisualStyleBackColor = true;
            Uretim.Click += Uretim_Click;
            // 
            // haberBox
            // 
            haberBox.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            haberBox.Location = new Point(20, 17);
            haberBox.Multiline = true;
            haberBox.Name = "haberBox";
            haberBox.ScrollBars = ScrollBars.Vertical;
            haberBox.Size = new Size(719, 587);
            haberBox.TabIndex = 1;
            haberBox.Text = "Hilal Haber tarafından \r\nÜretilen Haber İçeriği";
            haberBox.TextChanged += haberBox_TextChanged;
            // 
            // anahtarBox
            // 
            anahtarBox.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            anahtarBox.Location = new Point(745, 42);
            anahtarBox.Multiline = true;
            anahtarBox.Name = "anahtarBox";
            anahtarBox.ScrollBars = ScrollBars.Both;
            anahtarBox.Size = new Size(251, 117);
            anahtarBox.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label1.Location = new Point(745, 17);
            label1.Name = "label1";
            label1.Size = new Size(170, 25);
            label1.TabIndex = 4;
            label1.Text = "Anahtar Kelimeler";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1004, 616);
            Controls.Add(label1);
            Controls.Add(anahtarBox);
            Controls.Add(haberBox);
            Controls.Add(Uretim);
            Name = "Form1";
            Text = "Hilal The Haber Maker";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Uretim;
        private TextBox haberBox;
        private TextBox anahtarBox;
        private Label label1;
    }
}
