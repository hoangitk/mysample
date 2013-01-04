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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.timeSheetGridView1 = new TimeSheetControl.TimeSheetGridView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timeSheetGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnAddNew);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 356);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(614, 65);
            this.panel1.TabIndex = 1;
            // 
            // btnAddNew
            // 
            this.btnAddNew.Location = new System.Drawing.Point(12, 21);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(75, 23);
            this.btnAddNew.TabIndex = 0;
            this.btnAddNew.Text = "Add New";
            this.btnAddNew.UseVisualStyleBackColor = true;
            // 
            // timeSheetGridView1
            // 
            this.timeSheetGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.timeSheetGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.timeSheetGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.timeSheetGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.timeSheetGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeSheetGridView1.FromDate = new System.DateTime(2013, 1, 4, 11, 7, 41, 481);
            this.timeSheetGridView1.Location = new System.Drawing.Point(0, 0);
            this.timeSheetGridView1.Name = "timeSheetGridView1";
            this.timeSheetGridView1.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeSheetGridView1.RowTemplate.Height = 25;
            this.timeSheetGridView1.Size = new System.Drawing.Size(614, 356);
            this.timeSheetGridView1.TabIndex = 2;
            this.timeSheetGridView1.ToDate = new System.DateTime(2013, 1, 4, 11, 7, 41, 481);
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 421);
            this.Controls.Add(this.timeSheetGridView1);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Text = "TimeSheetDemo";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.timeSheetGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnAddNew;
        private TimeSheetControl.TimeSheetGridView timeSheetGridView1;
        private System.Windows.Forms.ImageList imageList1;
	}
}
