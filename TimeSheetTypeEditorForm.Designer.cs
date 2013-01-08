namespace TimeSheetControl
{
    partial class TimeSheetTypeEditorForm
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
            System.Windows.Forms.Label codeLabel;
            System.Windows.Forms.Label idLabel;
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.catalogComboBox = new System.Windows.Forms.ComboBox();
            this.codeTextBox = new System.Windows.Forms.TextBox();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.timeSheetTypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnUpdate = new System.Windows.Forms.Button();
            catalogLabel = new System.Windows.Forms.Label();
            codeLabel = new System.Windows.Forms.Label();
            idLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timeSheetTypeBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(idLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.idTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(codeLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.codeTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(catalogLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.catalogComboBox, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(205, 74);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // catalogLabel
            // 
            catalogLabel.AutoSize = true;
            catalogLabel.Location = new System.Drawing.Point(3, 48);
            catalogLabel.Name = "catalogLabel";
            catalogLabel.Size = new System.Drawing.Size(46, 13);
            catalogLabel.TabIndex = 0;
            catalogLabel.Text = "Catalog:";
            // 
            // catalogComboBox
            // 
            this.catalogComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.timeSheetTypeBindingSource, "Catalog", true));
            this.catalogComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.catalogComboBox.FormattingEnabled = true;
            this.catalogComboBox.Location = new System.Drawing.Point(78, 51);
            this.catalogComboBox.Name = "catalogComboBox";
            this.catalogComboBox.Size = new System.Drawing.Size(124, 21);
            this.catalogComboBox.TabIndex = 1;
            // 
            // codeLabel
            // 
            codeLabel.AutoSize = true;
            codeLabel.Location = new System.Drawing.Point(3, 24);
            codeLabel.Name = "codeLabel";
            codeLabel.Size = new System.Drawing.Size(35, 13);
            codeLabel.TabIndex = 2;
            codeLabel.Text = "Code:";
            // 
            // codeTextBox
            // 
            this.codeTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.timeSheetTypeBindingSource, "Code", true));
            this.codeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeTextBox.Location = new System.Drawing.Point(78, 27);
            this.codeTextBox.Name = "codeTextBox";
            this.codeTextBox.Size = new System.Drawing.Size(124, 20);
            this.codeTextBox.TabIndex = 3;
            // 
            // idLabel
            // 
            idLabel.AutoSize = true;
            idLabel.Location = new System.Drawing.Point(3, 0);
            idLabel.Name = "idLabel";
            idLabel.Size = new System.Drawing.Size(19, 13);
            idLabel.TabIndex = 4;
            idLabel.Text = "Id:";
            // 
            // idTextBox
            // 
            this.idTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.timeSheetTypeBindingSource, "Id", true));
            this.idTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.idTextBox.Location = new System.Drawing.Point(78, 3);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(124, 20);
            this.idTextBox.TabIndex = 5;
            // 
            // timeSheetTypeBindingSource
            // 
            this.timeSheetTypeBindingSource.DataSource = typeof(TimeSheetControl.TimeSheetType);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnUpdate.Location = new System.Drawing.Point(3, 77);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(205, 23);
            this.btnUpdate.TabIndex = 5;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            // 
            // TimeSheetTypeEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(211, 103);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnUpdate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TimeSheetTypeEditorForm";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "TimeSheetTypeEditorForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timeSheetTypeBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.BindingSource timeSheetTypeBindingSource;
        private System.Windows.Forms.TextBox codeTextBox;
        private System.Windows.Forms.ComboBox catalogComboBox;
        private System.Windows.Forms.Button btnUpdate;
    }
}