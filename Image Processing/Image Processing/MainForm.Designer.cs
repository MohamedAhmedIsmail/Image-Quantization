namespace Image_Processing
{
    partial class MainForm
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
            this.pboxResult = new System.Windows.Forms.PictureBox();
            this.pboxSource = new System.Windows.Forms.PictureBox();
            this.btnChooseImage = new System.Windows.Forms.Button();
            this.btnRefine = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pboxResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboxSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // pboxResult
            // 
            this.pboxResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pboxResult.Location = new System.Drawing.Point(474, 12);
            this.pboxResult.Name = "pboxResult";
            this.pboxResult.Size = new System.Drawing.Size(382, 308);
            this.pboxResult.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pboxResult.TabIndex = 0;
            this.pboxResult.TabStop = false;
            // 
            // pboxSource
            // 
            this.pboxSource.BackColor = System.Drawing.Color.White;
            this.pboxSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pboxSource.Location = new System.Drawing.Point(24, 12);
            this.pboxSource.Name = "pboxSource";
            this.pboxSource.Size = new System.Drawing.Size(382, 308);
            this.pboxSource.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pboxSource.TabIndex = 1;
            this.pboxSource.TabStop = false;
            // 
            // btnChooseImage
            // 
            this.btnChooseImage.Location = new System.Drawing.Point(146, 351);
            this.btnChooseImage.Name = "btnChooseImage";
            this.btnChooseImage.Size = new System.Drawing.Size(135, 23);
            this.btnChooseImage.TabIndex = 2;
            this.btnChooseImage.Text = "Choose an image";
            this.btnChooseImage.UseVisualStyleBackColor = true;
            this.btnChooseImage.Click += new System.EventHandler(this.btnChooseImage_Click);
            // 
            // btnRefine
            // 
            this.btnRefine.Location = new System.Drawing.Point(610, 351);
            this.btnRefine.Name = "btnRefine";
            this.btnRefine.Size = new System.Drawing.Size(114, 23);
            this.btnRefine.TabIndex = 3;
            this.btnRefine.Text = "Refined image";
            this.btnRefine.UseVisualStyleBackColor = true;
            this.btnRefine.Click += new System.EventHandler(this.btnRefine_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(610, 380);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 4;
            this.numericUpDown1.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(549, 382);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "K clusters";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(883, 431);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.btnRefine);
            this.Controls.Add(this.btnChooseImage);
            this.Controls.Add(this.pboxSource);
            this.Controls.Add(this.pboxResult);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Image Processing";
            ((System.ComponentModel.ISupportInitialize)(this.pboxResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboxSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pboxResult;
        private System.Windows.Forms.PictureBox pboxSource;
        private System.Windows.Forms.Button btnChooseImage;
        private System.Windows.Forms.Button btnRefine;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label1;
    }
}

