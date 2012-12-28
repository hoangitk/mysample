namespace TimeSheetControl
{
    partial class TimeSheetView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tsGrid = new TimeSheetControl.TimeSheetGrid();
            this.tsHeader = new TimeSheetControl.TimeSheetGridHeader();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tsGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tsHeader)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tsGrid);
            this.splitContainer1.Panel2.Controls.Add(this.tsHeader);
            this.splitContainer1.Size = new System.Drawing.Size(577, 473);
            this.splitContainer1.SplitterDistance = 122;
            this.splitContainer1.TabIndex = 1;
            // 
            // tsGrid
            // 
            this.tsGrid.AutoScroll = true;
            this.tsGrid.AutoScrollMinSize = new System.Drawing.Size(48, 0);
            this.tsGrid.CellHeight = 0;
            this.tsGrid.CellWidth = 48;
            this.tsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsGrid.FromDate = new System.DateTime(((long)(0)));
            this.tsGrid.ItemCount = 0;
            this.tsGrid.LineColor = System.Drawing.Color.BlueViolet;
            this.tsGrid.Location = new System.Drawing.Point(0, 23);
            this.tsGrid.Name = "tsGrid";
            this.tsGrid.Size = new System.Drawing.Size(451, 450);
            this.tsGrid.TabIndex = 1;
            this.tsGrid.Text = "timeSheetGrid1";
            this.tsGrid.ToDate = new System.DateTime(((long)(0)));
            // 
            // tsHeader
            // 
            this.tsHeader.AutoScrollMinSize = new System.Drawing.Size(48, 40);
            this.tsHeader.CellWidth = 48;
            this.tsHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.tsHeader.FromDate = new System.DateTime(((long)(0)));
            this.tsHeader.HeaderColor = System.Drawing.Color.LightGreen;
            this.tsHeader.HeaderHeight = 40;
            this.tsHeader.LineColor = System.Drawing.Color.Blue;
            this.tsHeader.Location = new System.Drawing.Point(0, 0);
            this.tsHeader.Name = "tsHeader";
            this.tsHeader.Size = new System.Drawing.Size(451, 23);
            this.tsHeader.TabIndex = 0;
            this.tsHeader.Text = "timeSheetGridHeader1";
            this.tsHeader.ToDate = new System.DateTime(((long)(0)));
            // 
            // TimeSheetView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "TimeSheetView";
            this.Size = new System.Drawing.Size(577, 473);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tsGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tsHeader)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private TimeSheetGridHeader tsHeader;
        private TimeSheetGrid tsGrid;

    }
}
