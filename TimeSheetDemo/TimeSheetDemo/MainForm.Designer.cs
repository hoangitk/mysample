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
            TimeSheetControl.TimeSheetCatalogColor timeSheetCatalogColor1 = new TimeSheetControl.TimeSheetCatalogColor();
            TimeSheetControl.TimeSheetCatalogColor timeSheetCatalogColor2 = new TimeSheetControl.TimeSheetCatalogColor();
            TimeSheetControl.TimeSheetCatalogColor timeSheetCatalogColor3 = new TimeSheetControl.TimeSheetCatalogColor();
            TimeSheetControl.TimeSheetCatalogColor timeSheetCatalogColor4 = new TimeSheetControl.TimeSheetCatalogColor();
            TimeSheetControl.TimeSheetCatalogColor timeSheetCatalogColor5 = new TimeSheetControl.TimeSheetCatalogColor();
            TimeSheetControl.TimeSheetCatalogColor timeSheetCatalogColor6 = new TimeSheetControl.TimeSheetCatalogColor();
            TimeSheetControl.TimeSheetCatalogColor timeSheetCatalogColor7 = new TimeSheetControl.TimeSheetCatalogColor();
            TimeSheetControl.TimeSheetCatalogColor timeSheetCatalogColor8 = new TimeSheetControl.TimeSheetCatalogColor();
            TimeSheetControl.TimeSheetCatalogColor timeSheetCatalogColor9 = new TimeSheetControl.TimeSheetCatalogColor();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            TimeSheetControl.TimeSheetStatusColor timeSheetStatusColor1 = new TimeSheetControl.TimeSheetStatusColor();
            TimeSheetControl.TimeSheetStatusColor timeSheetStatusColor2 = new TimeSheetControl.TimeSheetStatusColor();
            TimeSheetControl.TimeSheetStatusColor timeSheetStatusColor3 = new TimeSheetControl.TimeSheetStatusColor();
            TimeSheetControl.TimeSheetStatusColor timeSheetStatusColor4 = new TimeSheetControl.TimeSheetStatusColor();
            TimeSheetControl.TimeSheetStatusColor timeSheetStatusColor5 = new TimeSheetControl.TimeSheetStatusColor();
            TimeSheetControl.TimeSheetStatusColor timeSheetStatusColor6 = new TimeSheetControl.TimeSheetStatusColor();
            TimeSheetControl.TimeSheetStatusColor timeSheetStatusColor7 = new TimeSheetControl.TimeSheetStatusColor();
            this.panel1 = new System.Windows.Forms.Panel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tsGridView = new TimeSheetControl.TimeSheetGridView();
            ((System.ComponentModel.ISupportInitialize)(this.tsGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 356);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(614, 65);
            this.panel1.TabIndex = 1;
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
            timeSheetCatalogColor1.Catalog = TimeSheetControl.TimeSheetCatalog.None;
            timeSheetCatalogColor1.Color = System.Drawing.Color.Empty;
            timeSheetCatalogColor2.Catalog = TimeSheetControl.TimeSheetCatalog.WorkingDay;
            timeSheetCatalogColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(43)))), ((int)(((byte)(78)))));
            timeSheetCatalogColor3.Catalog = TimeSheetControl.TimeSheetCatalog.Holiday;
            timeSheetCatalogColor3.Color = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            timeSheetCatalogColor4.Catalog = TimeSheetControl.TimeSheetCatalog.WeekendOff;
            timeSheetCatalogColor4.Color = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(125)))), ((int)(((byte)(125)))));
            timeSheetCatalogColor5.Catalog = TimeSheetControl.TimeSheetCatalog.WeekendOffHalf;
            timeSheetCatalogColor5.Color = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(227)))), ((int)(((byte)(227)))));
            timeSheetCatalogColor6.Catalog = TimeSheetControl.TimeSheetCatalog.Leave;
            timeSheetCatalogColor6.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(249)))), ((int)(((byte)(189)))));
            timeSheetCatalogColor7.Catalog = TimeSheetControl.TimeSheetCatalog.BusinessTrip;
            timeSheetCatalogColor7.Color = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(143)))), ((int)(((byte)(151)))));
            timeSheetCatalogColor8.Catalog = TimeSheetControl.TimeSheetCatalog.Overtime;
            timeSheetCatalogColor8.Color = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(134)))), ((int)(((byte)(134)))));
            timeSheetCatalogColor9.Catalog = TimeSheetControl.TimeSheetCatalog.Shift;
            timeSheetCatalogColor9.Color = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            new TimeSheetControl.GenericCollectionBase<TimeSheetControl.TimeSheetCatalogColor>().Add(timeSheetCatalogColor1);
            new TimeSheetControl.GenericCollectionBase<TimeSheetControl.TimeSheetCatalogColor>().Add(timeSheetCatalogColor2);
            new TimeSheetControl.GenericCollectionBase<TimeSheetControl.TimeSheetCatalogColor>().Add(timeSheetCatalogColor3);
            new TimeSheetControl.GenericCollectionBase<TimeSheetControl.TimeSheetCatalogColor>().Add(timeSheetCatalogColor4);
            new TimeSheetControl.GenericCollectionBase<TimeSheetControl.TimeSheetCatalogColor>().Add(timeSheetCatalogColor5);
            new TimeSheetControl.GenericCollectionBase<TimeSheetControl.TimeSheetCatalogColor>().Add(timeSheetCatalogColor6);
            new TimeSheetControl.GenericCollectionBase<TimeSheetControl.TimeSheetCatalogColor>().Add(timeSheetCatalogColor7);
            new TimeSheetControl.GenericCollectionBase<TimeSheetControl.TimeSheetCatalogColor>().Add(timeSheetCatalogColor8);
            new TimeSheetControl.GenericCollectionBase<TimeSheetControl.TimeSheetCatalogColor>().Add(timeSheetCatalogColor9);
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tsGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.tsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Fuchsia;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tsGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.tsGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsGridView.FromDate = new System.DateTime(2013, 1, 15, 16, 59, 11, 809);
            this.tsGridView.HeaderDateFormat = "ddd, dd/MM/yyyy";
            this.tsGridView.Location = new System.Drawing.Point(0, 0);
            this.tsGridView.Name = "tsGridView";
            this.tsGridView.PositionShowToolTip = System.Drawing.ContentAlignment.TopRight;
            this.tsGridView.RowTemplate.Height = 25;
            this.tsGridView.Size = new System.Drawing.Size(614, 356);
            timeSheetStatusColor1.Color = System.Drawing.Color.Empty;
            timeSheetStatusColor1.Status = TimeSheetControl.TimeSheetStatus.None;
            timeSheetStatusColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            timeSheetStatusColor2.Status = TimeSheetControl.TimeSheetStatus.InvalidTS;
            timeSheetStatusColor3.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            timeSheetStatusColor3.Status = TimeSheetControl.TimeSheetStatus.ValidTS;
            timeSheetStatusColor4.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            timeSheetStatusColor4.Status = TimeSheetControl.TimeSheetStatus.UnApprovedOT;
            timeSheetStatusColor5.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            timeSheetStatusColor5.Status = TimeSheetControl.TimeSheetStatus.ApprovedOT;
            timeSheetStatusColor6.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            timeSheetStatusColor6.Status = TimeSheetControl.TimeSheetStatus.ApprovedLeave;
            timeSheetStatusColor7.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            timeSheetStatusColor7.Status = TimeSheetControl.TimeSheetStatus.Locked;
            new TimeSheetControl.GenericCollectionBase<TimeSheetControl.TimeSheetStatusColor>().Add(timeSheetStatusColor1);
            new TimeSheetControl.GenericCollectionBase<TimeSheetControl.TimeSheetStatusColor>().Add(timeSheetStatusColor2);
            new TimeSheetControl.GenericCollectionBase<TimeSheetControl.TimeSheetStatusColor>().Add(timeSheetStatusColor3);
            new TimeSheetControl.GenericCollectionBase<TimeSheetControl.TimeSheetStatusColor>().Add(timeSheetStatusColor4);
            new TimeSheetControl.GenericCollectionBase<TimeSheetControl.TimeSheetStatusColor>().Add(timeSheetStatusColor5);
            new TimeSheetControl.GenericCollectionBase<TimeSheetControl.TimeSheetStatusColor>().Add(timeSheetStatusColor6);
            new TimeSheetControl.GenericCollectionBase<TimeSheetControl.TimeSheetStatusColor>().Add(timeSheetStatusColor7);
            this.tsGridView.TabIndex = 3;
            this.tsGridView.ToDate = new System.DateTime(2013, 1, 15, 16, 59, 11, 809);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 421);
            this.Controls.Add(this.tsGridView);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Text = "TimeSheetDemo";
            ((System.ComponentModel.ISupportInitialize)(this.tsGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ImageList imageList1;
        private TimeSheetControl.TimeSheetGridView tsGridView;
	}
}
