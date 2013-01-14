﻿/*
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tsGridView = new TimeSheetControl.TimeSheetGridView();
            this.gridContextMenu = new TimeSheetControl.MyContextMenu(this.components);
            this.cmbStyle = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tsGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmbStyle);
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
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tsGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.tsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tsGridView.ContextMenuStrip = this.gridContextMenu;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tsGridView.DefaultCellStyle = dataGridViewCellStyle5;
            this.tsGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsGridView.FromDate = new System.DateTime(2013, 1, 4, 11, 7, 41, 481);
            this.tsGridView.HeaderDateFormat = "ddd, dd/MM/yyyy";
            this.tsGridView.Location = new System.Drawing.Point(0, 0);
            this.tsGridView.Name = "tsGridView";
            this.tsGridView.PositionShowToolTip = System.Drawing.ContentAlignment.TopRight;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tsGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.tsGridView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsGridView.RowTemplate.Height = 25;
            this.tsGridView.Size = new System.Drawing.Size(614, 356);
            this.tsGridView.TabIndex = 2;
            this.tsGridView.ToDate = new System.DateTime(2013, 1, 4, 11, 7, 41, 481);
            // 
            // gridContextMenu
            // 
            this.gridContextMenu.Name = "gridContextMenu";
            this.gridContextMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // cmbStyle
            // 
            this.cmbStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStyle.FormattingEnabled = true;
            this.cmbStyle.Location = new System.Drawing.Point(12, 23);
            this.cmbStyle.Name = "cmbStyle";
            this.cmbStyle.Size = new System.Drawing.Size(135, 21);
            this.cmbStyle.TabIndex = 2;
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
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tsGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Panel panel1;
        private TimeSheetControl.TimeSheetGridView tsGridView;
        private System.Windows.Forms.ImageList imageList1;
        private TimeSheetControl.MyContextMenu gridContextMenu;
        private System.Windows.Forms.ComboBox cmbStyle;
	}
}