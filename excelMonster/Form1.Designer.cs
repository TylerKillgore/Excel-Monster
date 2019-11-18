using Microsoft.VisualBasic.FileIO;
using System;
using System.Data;
using System.Windows.Forms;

namespace excelMonster
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Label label1;
        private TextBox tbCSVPath;
        private Button btBrowse;
        private Button btUpload;
        private DataGridView dgCSVData;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.tbCSVPath = new System.Windows.Forms.TextBox();
            this.btBrowse = new System.Windows.Forms.Button();
            this.btUpload = new System.Windows.Forms.Button();
            this.dgCSVData = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgCSVData)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "File path:";
            // 
            // tbCSVPath
            // 
            this.tbCSVPath.Location = new System.Drawing.Point(76, 50);
            this.tbCSVPath.Name = "tbCSVPath";
            this.tbCSVPath.Size = new System.Drawing.Size(345, 20);
            this.tbCSVPath.TabIndex = 1;
            // 
            // btBrowse
            // 
            this.btBrowse.Location = new System.Drawing.Point(437, 48);
            this.btBrowse.Name = "btBrowse";
            this.btBrowse.Size = new System.Drawing.Size(101, 23);
            this.btBrowse.TabIndex = 2;
            this.btBrowse.Text = "Browse";
            this.btBrowse.UseVisualStyleBackColor = true;
            this.btBrowse.Click += new System.EventHandler(this.btBrowse_Click);
            // 
            // btUpload
            // 
            this.btUpload.Location = new System.Drawing.Point(76, 76);
            this.btUpload.Name = "btUpload";
            this.btUpload.Size = new System.Drawing.Size(101, 23);
            this.btUpload.TabIndex = 3;
            this.btUpload.Text = "Upload CSV";
            this.btUpload.UseVisualStyleBackColor = true;
            this.btUpload.Click += new System.EventHandler(this.btUpload_Click);
            // 
            // dgCSVData
            // 
            this.dgCSVData.AllowUserToAddRows = false;
            this.dgCSVData.AllowUserToDeleteRows = false;
            this.dgCSVData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCSVData.Location = new System.Drawing.Point(26, 129);
            this.dgCSVData.Name = "dgCSVData";
            this.dgCSVData.ReadOnly = true;
            this.dgCSVData.Size = new System.Drawing.Size(512, 129);
            this.dgCSVData.TabIndex = 4;
            // 
            // CSVImporter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 268);
            this.Controls.Add(this.dgCSVData);
            this.Controls.Add(this.btUpload);
            this.Controls.Add(this.btBrowse);
            this.Controls.Add(this.tbCSVPath);
            this.Controls.Add(this.label1);
            this.Name = "CSVImporter";
            this.Text = "CSVImporter";
            ((System.ComponentModel.ISupportInitialize)(this.dgCSVData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void btUpload_Click(object sender, EventArgs e)
        {
            try
            {
                dgCSVData.DataSource = GetDataTableFromCSVFile(tbCSVPath.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Import CSV File", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            // To list only csv files, we need to add this filter
            openFileDialog.Filter = "|*.csv";
            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                tbCSVPath.Text = openFileDialog.FileName;
            }
        }
        private static DataTable GetDataTableFromCSVFile(string csvfilePath)
        {
            DataTable csvData = new DataTable();
            using (TextFieldParser csvReader = new TextFieldParser(csvfilePath))
            {
                csvReader.SetDelimiters(new string[] { "," });
                csvReader.HasFieldsEnclosedInQuotes = true;

                //Read columns from CSV file, remove this line if columns not exits  
                string[] colFields = csvReader.ReadFields();

                foreach (string column in colFields)
                {
                    DataColumn datecolumn = new DataColumn(column);
                    datecolumn.AllowDBNull = true;
                    csvData.Columns.Add(datecolumn);
                }

                while (!csvReader.EndOfData)
                {
                    string[] fieldData = csvReader.ReadFields();
                    //Making empty value as null
                    for (int i = 0; i < fieldData.Length; i++)
                    {
                        if (fieldData[i] == "")
                        {
                            fieldData[i] = null;
                        }
                    }
                    csvData.Rows.Add(fieldData);
                }
            }
            return csvData;
        }
        #endregion
    }
}

