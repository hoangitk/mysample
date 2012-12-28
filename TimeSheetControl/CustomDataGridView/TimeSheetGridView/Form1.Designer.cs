namespace TimeSheetControl
{
    partial class Form1
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
            this.timeSheetView1 = new TimeSheetControl.TimeSheetView();
            this.SuspendLayout();
            // 
            // timeSheetView1
            // 
            this.timeSheetView1.CellHeight = 40;
            this.timeSheetView1.CellWidth = 80;
            this.timeSheetView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeSheetView1.FromDate = new System.DateTime(2012, 12, 28, 18, 28, 27, 0);
            this.timeSheetView1.HeaderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.timeSheetView1.HeaderHeight = 50;
            this.timeSheetView1.ItemCount = 30;
            this.timeSheetView1.LineColor = System.Drawing.Color.Blue;
            this.timeSheetView1.Location = new System.Drawing.Point(0, 0);
            this.timeSheetView1.Name = "timeSheetView1";
            this.timeSheetView1.Size = new System.Drawing.Size(682, 475);
            this.timeSheetView1.TabIndex = 0;
            this.timeSheetView1.ToDate = new System.DateTime(2013, 1, 19, 0, 0, 0, 0);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 475);
            this.Controls.Add(this.timeSheetView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private TimeSheetView timeSheetView1;





    }
}

