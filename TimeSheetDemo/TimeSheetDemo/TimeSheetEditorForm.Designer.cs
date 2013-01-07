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
            this.catalogComboBox = new System.Windows.Forms.ComboBox();
            this.dayDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.statusComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.leaveItemsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.shiftItemsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnUpdate = new System.Windows.Forms.Button();
            this.shiftItemsDataGridView = new System.Windows.Forms.DataGridView();
            this.leaveItemsDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeSheetDayBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            catalogLabel = new System.Windows.Forms.Label();
            dayLabel = new System.Windows.Forms.Label();
            statusLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leaveItemsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.shiftItemsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.shiftItemsDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leaveItemsDataGridView)).BeginInit();
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
            // catalogComboBox
            // 
            this.catalogComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.timeSheetDayBindingSource, "Catalog", true));
            this.catalogComboBox.FormattingEnabled = true;
            this.catalogComboBox.Location = new System.Drawing.Point(66, 19);
            this.catalogComboBox.Name = "catalogComboBox";
            this.catalogComboBox.Size = new System.Drawing.Size(200, 21);
            this.catalogComboBox.TabIndex = 2;
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
            // dayDateTimePicker
            // 
            this.dayDateTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.timeSheetDayBindingSource, "Day", true));
            this.dayDateTimePicker.Location = new System.Drawing.Point(66, 46);
            this.dayDateTimePicker.Name = "dayDateTimePicker";
            this.dayDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.dayDateTimePicker.TabIndex = 4;
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
            // leaveItemsBindingSource
            // 
            this.leaveItemsBindingSource.DataMember = "LeaveItems";
            this.leaveItemsBindingSource.DataSource = this.timeSheetDayBindingSource;
            // 
            // shiftItemsBindingSource
            // 
            this.shiftItemsBindingSource.DataMember = "ShiftItems";
            this.shiftItemsBindingSource.DataSource = this.timeSheetDayBindingSource;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(415, 360);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 9;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            // 
            // shiftItemsDataGridView
            // 
            this.shiftItemsDataGridView.AutoGenerateColumns = false;
            this.shiftItemsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.shiftItemsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.shiftItemsDataGridView.DataSource = this.shiftItemsBindingSource;
            this.shiftItemsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shiftItemsDataGridView.Location = new System.Drawing.Point(3, 16);
            this.shiftItemsDataGridView.Name = "shiftItemsDataGridView";
            this.shiftItemsDataGridView.Size = new System.Drawing.Size(501, 103);
            this.shiftItemsDataGridView.TabIndex = 0;
            // 
            // leaveItemsDataGridView
            // 
            this.leaveItemsDataGridView.AutoGenerateColumns = false;
            this.leaveItemsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.leaveItemsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8});
            this.leaveItemsDataGridView.DataSource = this.leaveItemsBindingSource;
            this.leaveItemsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leaveItemsDataGridView.Location = new System.Drawing.Point(3, 16);
            this.leaveItemsDataGridView.Name = "leaveItemsDataGridView";
            this.leaveItemsDataGridView.Size = new System.Drawing.Size(501, 96);
            this.leaveItemsDataGridView.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "FromTime";
            this.dataGridViewTextBoxColumn5.HeaderText = "FromTime";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "ToTime";
            this.dataGridViewTextBoxColumn6.HeaderText = "ToTime";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "TimeSheetType";
            this.dataGridViewTextBoxColumn7.HeaderText = "TimeSheetType";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "Status";
            this.dataGridViewTextBoxColumn8.HeaderText = "Status";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // timeSheetDayBindingSource
            // 
            this.timeSheetDayBindingSource.DataSource = typeof(TimeSheetControl.TimeSheetDay);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "FromTime";
            this.dataGridViewTextBoxColumn1.HeaderText = "FromTime";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "ToTime";
            this.dataGridViewTextBoxColumn2.HeaderText = "ToTime";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "TimeSheetType";
            this.dataGridViewTextBoxColumn3.HeaderText = "TimeSheetType";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Status";
            this.dataGridViewTextBoxColumn4.HeaderText = "Status";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // TimeSheetEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 395);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "TimeSheetEditorForm";
            this.Text = "Time Sheet Editor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.leaveItemsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.shiftItemsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.shiftItemsDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leaveItemsDataGridView)).EndInit();
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
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridView shiftItemsDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;


    }
}