/*
 * Created by SharpDevelop.
 * User: HoangITK
 * Date: 12/29/2012
 * Time: 12:01 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace TimeSheetDemo
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{            
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            TimeSheetControl.TimeSheetCatalogColor timeSheetCatalogColor10 = new TimeSheetControl.TimeSheetCatalogColor();
            TimeSheetControl.TimeSheetCatalogColor timeSheetCatalogColor11 = new TimeSheetControl.TimeSheetCatalogColor();
            TimeSheetControl.TimeSheetCatalogColor timeSheetCatalogColor12 = new TimeSheetControl.TimeSheetCatalogColor();
            TimeSheetControl.TimeSheetCatalogColor timeSheetCatalogColor13 = new TimeSheetControl.TimeSheetCatalogColor();
            TimeSheetControl.TimeSheetCatalogColor timeSheetCatalogColor14 = new TimeSheetControl.TimeSheetCatalogColor();
            TimeSheetControl.TimeSheetCatalogColor timeSheetCatalogColor15 = new TimeSheetControl.TimeSheetCatalogColor();
            TimeSheetControl.TimeSheetCatalogColor timeSheetCatalogColor16 = new TimeSheetControl.TimeSheetCatalogColor();
            TimeSheetControl.TimeSheetCatalogColor timeSheetCatalogColor17 = new TimeSheetControl.TimeSheetCatalogColor();
            TimeSheetControl.TimeSheetCatalogColor timeSheetCatalogColor18 = new TimeSheetControl.TimeSheetCatalogColor();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            TimeSheetControl.TimeSheetStatusColor timeSheetStatusColor8 = new TimeSheetControl.TimeSheetStatusColor();
            TimeSheetControl.TimeSheetStatusColor timeSheetStatusColor9 = new TimeSheetControl.TimeSheetStatusColor();
            TimeSheetControl.TimeSheetStatusColor timeSheetStatusColor10 = new TimeSheetControl.TimeSheetStatusColor();
            TimeSheetControl.TimeSheetStatusColor timeSheetStatusColor11 = new TimeSheetControl.TimeSheetStatusColor();
            TimeSheetControl.TimeSheetStatusColor timeSheetStatusColor12 = new TimeSheetControl.TimeSheetStatusColor();
            TimeSheetControl.TimeSheetStatusColor timeSheetStatusColor13 = new TimeSheetControl.TimeSheetStatusColor();
            TimeSheetControl.TimeSheetStatusColor timeSheetStatusColor14 = new TimeSheetControl.TimeSheetStatusColor();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tsGridView = new TimeSheetControl.TimeSheetGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            ((System.ComponentModel.ISupportInitialize)(this.tsGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "applicant.ico");
            this.imageList1.Images.SetKeyName(1, "applicant-denied.ico");
            this.imageList1.Images.SetKeyName(2, "applicant-good.ico");
            this.imageList1.Images.SetKeyName(3, "applicant-keepinview.ico");
            this.imageList1.Images.SetKeyName(4, "Candidate.ico");
            this.imageList1.Images.SetKeyName(5, "Casual-Activation.ico");
            this.imageList1.Images.SetKeyName(6, "Casual-Future.ico");
            this.imageList1.Images.SetKeyName(7, "Casual-Probation.ico");
            this.imageList1.Images.SetKeyName(8, "Casual-Resignation.ico");
            this.imageList1.Images.SetKeyName(9, "Casual-Training.ico");
            this.imageList1.Images.SetKeyName(10, "Contractor-Activation.ico");
            this.imageList1.Images.SetKeyName(11, "Contractor-Future.ico");
            this.imageList1.Images.SetKeyName(12, "Contractor-Probation.ico");
            this.imageList1.Images.SetKeyName(13, "Contractor-Resignation.ico");
            this.imageList1.Images.SetKeyName(14, "Contractor-Training.ico");
            this.imageList1.Images.SetKeyName(15, "Pemanent-Activation.ico");
            this.imageList1.Images.SetKeyName(16, "Pemanent-Future.ico");
            this.imageList1.Images.SetKeyName(17, "Pemanent-Probation.ico");
            this.imageList1.Images.SetKeyName(18, "Pemanent-Resignation.ico");
            this.imageList1.Images.SetKeyName(19, "Pemanent-Training.ico");
            this.imageList1.Images.SetKeyName(20, "propation-unpaid.ico");
            this.imageList1.Images.SetKeyName(21, "resignee.ico");
            this.imageList1.Images.SetKeyName(22, "staff-casual.ico");
            this.imageList1.Images.SetKeyName(23, "staff-fulltime.ico");
            this.imageList1.Images.SetKeyName(24, "staff-parttime.ico");
            this.imageList1.Images.SetKeyName(25, "staff-propation.ico");
            this.imageList1.Images.SetKeyName(26, "staff-seasonal.ico");
            this.imageList1.Images.SetKeyName(27, "staff-training.ico");
            this.imageList1.Images.SetKeyName(28, "staff-waitforresign.ico");
            // 
            // tsGridView
            // 
            this.tsGridView.AllowUserToAddRows = false;
            timeSheetCatalogColor10.Catalog = TimeSheetControl.TimeSheetCatalog.None;
            timeSheetCatalogColor10.Color = System.Drawing.Color.Empty;
            timeSheetCatalogColor11.Catalog = TimeSheetControl.TimeSheetCatalog.WorkingDay;
            timeSheetCatalogColor11.Color = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(43)))), ((int)(((byte)(78)))));
            timeSheetCatalogColor12.Catalog = TimeSheetControl.TimeSheetCatalog.Holiday;
            timeSheetCatalogColor12.Color = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            timeSheetCatalogColor13.Catalog = TimeSheetControl.TimeSheetCatalog.WeekendOff;
            timeSheetCatalogColor13.Color = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(125)))), ((int)(((byte)(125)))));
            timeSheetCatalogColor14.Catalog = TimeSheetControl.TimeSheetCatalog.WeekendOffHalf;
            timeSheetCatalogColor14.Color = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(227)))), ((int)(((byte)(227)))));
            timeSheetCatalogColor15.Catalog = TimeSheetControl.TimeSheetCatalog.Leave;
            timeSheetCatalogColor15.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(249)))), ((int)(((byte)(189)))));
            timeSheetCatalogColor16.Catalog = TimeSheetControl.TimeSheetCatalog.BusinessTrip;
            timeSheetCatalogColor16.Color = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(143)))), ((int)(((byte)(151)))));
            timeSheetCatalogColor17.Catalog = TimeSheetControl.TimeSheetCatalog.Overtime;
            timeSheetCatalogColor17.Color = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(134)))), ((int)(((byte)(134)))));
            timeSheetCatalogColor18.Catalog = TimeSheetControl.TimeSheetCatalog.Shift;
            timeSheetCatalogColor18.Color = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            new TimeSheetControl.GenericCollectionBase<TimeSheetControl.TimeSheetCatalogColor>().Add(timeSheetCatalogColor10);
            new TimeSheetControl.GenericCollectionBase<TimeSheetControl.TimeSheetCatalogColor>().Add(timeSheetCatalogColor11);
            new TimeSheetControl.GenericCollectionBase<TimeSheetControl.TimeSheetCatalogColor>().Add(timeSheetCatalogColor12);
            new TimeSheetControl.GenericCollectionBase<TimeSheetControl.TimeSheetCatalogColor>().Add(timeSheetCatalogColor13);
            new TimeSheetControl.GenericCollectionBase<TimeSheetControl.TimeSheetCatalogColor>().Add(timeSheetCatalogColor14);
            new TimeSheetControl.GenericCollectionBase<TimeSheetControl.TimeSheetCatalogColor>().Add(timeSheetCatalogColor15);
            new TimeSheetControl.GenericCollectionBase<TimeSheetControl.TimeSheetCatalogColor>().Add(timeSheetCatalogColor16);
            new TimeSheetControl.GenericCollectionBase<TimeSheetControl.TimeSheetCatalogColor>().Add(timeSheetCatalogColor17);
            new TimeSheetControl.GenericCollectionBase<TimeSheetControl.TimeSheetCatalogColor>().Add(timeSheetCatalogColor18);
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tsGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.tsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Fuchsia;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tsGridView.DefaultCellStyle = dataGridViewCellStyle4;
            this.tsGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsGridView.FromDate = new System.DateTime(2013, 1, 15, 16, 59, 11, 809);
            this.tsGridView.HeaderDateFormat = "ddd, dd/MM/yyyy";
            this.tsGridView.Location = new System.Drawing.Point(0, 0);
            this.tsGridView.Name = "tsGridView";
            this.tsGridView.PositionShowToolTip = System.Drawing.ContentAlignment.TopRight;
            this.tsGridView.RowTemplate.Height = 25;
            this.tsGridView.Size = new System.Drawing.Size(449, 421);
            timeSheetStatusColor8.Color = System.Drawing.Color.Empty;
            timeSheetStatusColor8.Status = TimeSheetControl.TimeSheetStatus.None;
            timeSheetStatusColor9.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            timeSheetStatusColor9.Status = TimeSheetControl.TimeSheetStatus.InvalidTS;
            timeSheetStatusColor10.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            timeSheetStatusColor10.Status = TimeSheetControl.TimeSheetStatus.ValidTS;
            timeSheetStatusColor11.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            timeSheetStatusColor11.Status = TimeSheetControl.TimeSheetStatus.UnApprovedOT;
            timeSheetStatusColor12.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            timeSheetStatusColor12.Status = TimeSheetControl.TimeSheetStatus.ApprovedOT;
            timeSheetStatusColor13.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            timeSheetStatusColor13.Status = TimeSheetControl.TimeSheetStatus.ApprovedLeave;
            timeSheetStatusColor14.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            timeSheetStatusColor14.Status = TimeSheetControl.TimeSheetStatus.Locked;
            new TimeSheetControl.GenericCollectionBase<TimeSheetControl.TimeSheetStatusColor>().Add(timeSheetStatusColor8);
            new TimeSheetControl.GenericCollectionBase<TimeSheetControl.TimeSheetStatusColor>().Add(timeSheetStatusColor9);
            new TimeSheetControl.GenericCollectionBase<TimeSheetControl.TimeSheetStatusColor>().Add(timeSheetStatusColor10);
            new TimeSheetControl.GenericCollectionBase<TimeSheetControl.TimeSheetStatusColor>().Add(timeSheetStatusColor11);
            new TimeSheetControl.GenericCollectionBase<TimeSheetControl.TimeSheetStatusColor>().Add(timeSheetStatusColor12);
            new TimeSheetControl.GenericCollectionBase<TimeSheetControl.TimeSheetStatusColor>().Add(timeSheetStatusColor13);
            new TimeSheetControl.GenericCollectionBase<TimeSheetControl.TimeSheetStatusColor>().Add(timeSheetStatusColor14);
            this.tsGridView.TabIndex = 3;
            this.tsGridView.ToDate = new System.DateTime(2013, 1, 15, 16, 59, 11, 809);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.propertyGrid1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tsGridView);
            this.splitContainer1.Size = new System.Drawing.Size(614, 421);
            this.splitContainer1.SplitterDistance = 161;
            this.splitContainer1.TabIndex = 4;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(161, 421);
            this.propertyGrid1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 421);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainForm";
            this.Text = "TimeSheetDemo";
            ((System.ComponentModel.ISupportInitialize)(this.tsGridView)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.ImageList imageList1;
        private TimeSheetControl.TimeSheetGridView tsGridView;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
	}
}
