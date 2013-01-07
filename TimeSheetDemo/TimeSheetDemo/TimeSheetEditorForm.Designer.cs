namespace TimeSheetDemo
{
    partial class TimeSheetEditorForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label catalogLabel;
            System.Windows.Forms.Label dayLabel;
            System.Windows.Forms.Label statusLabel;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.catalogComboBox = new System.Windows.Forms.ComboBox();
            this.dayDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.statusComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.leaveItemsDataGridView = new System.Windows.Forms.DataGridView();
            this.leaveItemsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.shiftItemsDataGridView = new System.Windows.Forms.DataGridView();
            this.shiftItemsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.timeSheetDayBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.shiftFromTimeColumn = new TimeSheetControl.DataGridViewCalendarColumn();
            this.shiftToTimeColumn = new TimeSheetControl.DataGridViewCalendarColumn();
            this.shiftTimeSheetTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shiftStatusColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.leaveFromTimeColumn = new TimeSheetControl.DataGridViewCalendarColumn();
            this.leaveToTimeColumn = new TimeSheetControl.DataGridViewCalendarColumn();
            this.leaveTimeSheetTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.leaveStatusColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            catalogLabel = new System.Windows.Forms.Label();
            dayLabel = new System.Windows.Forms.Label();
            statusLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leaveItemsDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leaveItemsBindingSource)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.shiftItemsDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.shiftItemsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeSheetDayBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // catalogLabel
            // 
            catalogLabel.AutoSize = true;
            catalogLabel.Location = new System.Drawing.Point(14, 22);
            catalogLabel.Name = "catalogLabel";
            catalogLabel.Size = new System.Drawing.Size(46, 13);
            catalogLabel.TabIndex = 1;
            catalogLabel.Text = "Catalog:";
            // 
            // dayLabel
            // 
            dayLabel.AutoSize = true;
            dayLabel.Location = new System.Drawing.Point(14, 50);
            dayLabel.Name = "dayLabel";
            dayLabel.Size = new System.Drawing.Size(29, 13);
            dayLabel.TabIndex = 3;
            dayLabel.Text = "Day:";
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Location = new System.Drawing.Point(14, 75);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new System.Drawing.Size(40, 13);
            statusLabel.TabIndex = 5;
            statusLabel.Text = "Status:";
            // 
            // catalogComboBox
            // 
            this.catalogComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.timeSheetDayBindingSource, "Catalog", true));
            this.catalogComboBox.FormattingEnabled = true;
            this.catalogComboBox.Location = new System.Drawing.Point(66, 19);
            this.catalogComboBox.Name = "catalogComboBox";
            this.catalogComboBox.Size = new System.Drawing.Size(200, 21);
            this.catalogComboBox.TabIndex = 2;
            // 
            // dayDateTimePicker
            // 
            this.dayDateTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.timeSheetDayBindingSource, "Day", true));
            this.dayDateTimePicker.Location = new System.Drawing.Point(66, 46);
            this.dayDateTimePicker.Name = "dayDateTimePicker";
            this.dayDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.dayDateTimePicker.TabIndex = 4;
            // 
            // statusComboBox
            // 
            this.statusComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.timeSheetDayBindingSource, "Status", true));
            this.statusComboBox.FormattingEnabled = true;
            this.statusComboBox.Location = new System.Drawing.Point(66, 72);
            this.statusComboBox.Name = "statusComboBox";
            this.statusComboBox.Size = new System.Drawing.Size(200, 21);
            this.statusComboBox.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.catalogComboBox);
            this.groupBox1.Controls.Add(this.statusComboBox);
            this.groupBox1.Controls.Add(statusLabel);
            this.groupBox1.Controls.Add(catalogLabel);
            this.groupBox1.Controls.Add(this.dayDateTimePicker);
            this.groupBox1.Controls.Add(dayLabel);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(507, 107);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Time Sheet Day";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.leaveItemsDataGridView);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 229);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(507, 115);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Leaves";
            // 
            // leaveItemsDataGridView
            // 
            this.leaveItemsDataGridView.AllowUserToAddRows = false;
            this.leaveItemsDataGridView.AllowUserToDeleteRows = false;
            this.leaveItemsDataGridView.AutoGenerateColumns = false;
            this.leaveItemsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.leaveItemsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.leaveFromTimeColumn,
            this.leaveToTimeColumn,
            this.leaveTimeSheetTypeColumn,
            this.leaveStatusColumn});
            this.leaveItemsDataGridView.DataSource = this.leaveItemsBindingSource;
            this.leaveItemsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leaveItemsDataGridView.Location = new System.Drawing.Point(3, 16);
            this.leaveItemsDataGridView.Name = "leaveItemsDataGridView";
            this.leaveItemsDataGridView.Size = new System.Drawing.Size(501, 96);
            this.leaveItemsDataGridView.TabIndex = 0;
            // 
            // leaveItemsBindingSource
            // 
            this.leaveItemsBindingSource.DataMember = "LeaveItems";
            this.leaveItemsBindingSource.DataSource = this.timeSheetDayBindingSource;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.shiftItemsDataGridView);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 107);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(507, 122);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Shifts";
            // 
            // shiftItemsDataGridView
            // 
            this.shiftItemsDataGridView.AllowUserToAddRows = false;
            this.shiftItemsDataGridView.AllowUserToDeleteRows = false;
            this.shiftItemsDataGridView.AutoGenerateColumns = false;
            this.shiftItemsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.shiftItemsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.shiftFromTimeColumn,
            this.shiftToTimeColumn,
            this.shiftTimeSheetTypeColumn,
            this.shiftStatusColumn});
            this.shiftItemsDataGridView.DataSource = this.shiftItemsBindingSource;
            this.shiftItemsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shiftItemsDataGridView.Location = new System.Drawing.Point(3, 16);
            this.shiftItemsDataGridView.Name = "shiftItemsDataGridView";
            this.shiftItemsDataGridView.Size = new System.Drawing.Size(501, 103);
            this.shiftItemsDataGridView.TabIndex = 0;
            // 
            // shiftItemsBindingSource
            // 
            this.shiftItemsBindingSource.DataMember = "ShiftItems";
            this.shiftItemsBindingSource.DataSource = this.timeSheetDayBindingSource;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(348, 360);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 9;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(429, 360);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // timeSheetDayBindingSource
            // 
            this.timeSheetDayBindingSource.DataSource = typeof(TimeSheetControl.TimeSheetDay);
            // 
            // shiftFromTimeColumn
            // 
            this.shiftFromTimeColumn.DataPropertyName = "FromTime";
            dataGridViewCellStyle3.Format = "HH:mm";
            dataGridViewCellStyle3.NullValue = null;
            this.shiftFromTimeColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.shiftFromTimeColumn.HeaderText = "FromTime";
            this.shiftFromTimeColumn.Name = "shiftFromTimeColumn";
            this.shiftFromTimeColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.shiftFromTimeColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // shiftToTimeColumn
            // 
            this.shiftToTimeColumn.DataPropertyName = "ToTime";
            dataGridViewCellStyle4.Format = "HH:mm";
            dataGridViewCellStyle4.NullValue = null;
            this.shiftToTimeColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.shiftToTimeColumn.HeaderText = "ToTime";
            this.shiftToTimeColumn.Name = "shiftToTimeColumn";
            this.shiftToTimeColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.shiftToTimeColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // shiftTimeSheetTypeColumn
            // 
            this.shiftTimeSheetTypeColumn.DataPropertyName = "TimeSheetType";
            this.shiftTimeSheetTypeColumn.HeaderText = "TimeSheetType";
            this.shiftTimeSheetTypeColumn.Name = "shiftTimeSheetTypeColumn";
            // 
            // shiftStatusColumn
            // 
            this.shiftStatusColumn.DataPropertyName = "Status";
            this.shiftStatusColumn.HeaderText = "Status";
            this.shiftStatusColumn.Name = "shiftStatusColumn";
            this.shiftStatusColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.shiftStatusColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // leaveFromTimeColumn
            // 
            this.leaveFromTimeColumn.DataPropertyName = "FromTime";
            dataGridViewCellStyle1.Format = "HH:mm";
            dataGridViewCellStyle1.NullValue = null;
            this.leaveFromTimeColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.leaveFromTimeColumn.HeaderText = "FromTime";
            this.leaveFromTimeColumn.Name = "leaveFromTimeColumn";
            this.leaveFromTimeColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.leaveFromTimeColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // leaveToTimeColumn
            // 
            this.leaveToTimeColumn.DataPropertyName = "ToTime";
            dataGridViewCellStyle2.Format = "HH:mm";
            dataGridViewCellStyle2.NullValue = null;
            this.leaveToTimeColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.leaveToTimeColumn.HeaderText = "ToTime";
            this.leaveToTimeColumn.Name = "leaveToTimeColumn";
            this.leaveToTimeColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.leaveToTimeColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // leaveTimeSheetTypeColumn
            // 
            this.leaveTimeSheetTypeColumn.DataPropertyName = "TimeSheetType";
            this.leaveTimeSheetTypeColumn.HeaderText = "TimeSheetType";
            this.leaveTimeSheetTypeColumn.Name = "leaveTimeSheetTypeColumn";
            // 
            // leaveStatusColumn
            // 
            this.leaveStatusColumn.DataPropertyName = "Status";
            this.leaveStatusColumn.HeaderText = "Status";
            this.leaveStatusColumn.Name = "leaveStatusColumn";
            this.leaveStatusColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.leaveStatusColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // TimeSheetEditorForm
            // 
            this.AcceptButton = this.btnUpdate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 395);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "TimeSheetEditorForm";
            this.Text = "Time Sheet Editor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.leaveItemsDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leaveItemsBindingSource)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.shiftItemsDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.shiftItemsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeSheetDayBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource timeSheetDayBindingSource;
        private System.Windows.Forms.ComboBox catalogComboBox;
        private System.Windows.Forms.DateTimePicker dayDateTimePicker;
        private System.Windows.Forms.ComboBox statusComboBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.BindingSource shiftItemsBindingSource;
        private System.Windows.Forms.BindingSource leaveItemsBindingSource;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.DataGridView leaveItemsDataGridView;
        private System.Windows.Forms.DataGridView shiftItemsDataGridView;
        private System.Windows.Forms.Button btnCancel;
        private TimeSheetControl.DataGridViewCalendarColumn leaveFromTimeColumn;
        private TimeSheetControl.DataGridViewCalendarColumn leaveToTimeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn leaveTimeSheetTypeColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn leaveStatusColumn;
        private TimeSheetControl.DataGridViewCalendarColumn shiftFromTimeColumn;
        private TimeSheetControl.DataGridViewCalendarColumn shiftToTimeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn shiftTimeSheetTypeColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn shiftStatusColumn;


    }
}