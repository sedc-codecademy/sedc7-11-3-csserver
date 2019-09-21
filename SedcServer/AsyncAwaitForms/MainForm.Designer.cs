namespace AsyncAwaitForms
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
            this.btnGetGallery = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnGetGallery
            // 
            this.btnGetGallery.Location = new System.Drawing.Point(794, 22);
            this.btnGetGallery.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnGetGallery.Name = "btnGetGallery";
            this.btnGetGallery.Size = new System.Drawing.Size(138, 42);
            this.btnGetGallery.TabIndex = 0;
            this.btnGetGallery.Text = "Get Gallery";
            this.btnGetGallery.UseVisualStyleBackColor = true;
            this.btnGetGallery.Click += new System.EventHandler(this.btnGetGallery_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1146, 690);
            this.Controls.Add(this.btnGetGallery);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "MainForm";
            this.Text = "Off Net Gallery Downloader";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGetGallery;
    }
}

