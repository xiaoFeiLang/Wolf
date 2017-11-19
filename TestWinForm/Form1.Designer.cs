namespace TestWinForm
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.CreateTestDB = new System.Windows.Forms.Button();
            this.InsertData = new System.Windows.Forms.Button();
            this.SelectData = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // CreateTestDB
            // 
            this.CreateTestDB.Location = new System.Drawing.Point(43, 18);
            this.CreateTestDB.Name = "CreateTestDB";
            this.CreateTestDB.Size = new System.Drawing.Size(75, 23);
            this.CreateTestDB.TabIndex = 0;
            this.CreateTestDB.Text = "Create";
            this.CreateTestDB.UseVisualStyleBackColor = true;
            this.CreateTestDB.Click += new System.EventHandler(this.CreateTestDB_Click);
            // 
            // InsertData
            // 
            this.InsertData.Location = new System.Drawing.Point(185, 18);
            this.InsertData.Name = "InsertData";
            this.InsertData.Size = new System.Drawing.Size(75, 23);
            this.InsertData.TabIndex = 1;
            this.InsertData.Text = "Insert";
            this.InsertData.UseVisualStyleBackColor = true;
            this.InsertData.Click += new System.EventHandler(this.InsertData_Click);
            // 
            // SelectData
            // 
            this.SelectData.Location = new System.Drawing.Point(315, 18);
            this.SelectData.Name = "SelectData";
            this.SelectData.Size = new System.Drawing.Size(75, 23);
            this.SelectData.TabIndex = 2;
            this.SelectData.Text = "Select";
            this.SelectData.UseVisualStyleBackColor = true;
            this.SelectData.Click += new System.EventHandler(this.SelectData_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(31, 74);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(370, 271);
            this.dataGridView1.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 362);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.SelectData);
            this.Controls.Add(this.InsertData);
            this.Controls.Add(this.CreateTestDB);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CreateTestDB;
        private System.Windows.Forms.Button InsertData;
        private System.Windows.Forms.Button SelectData;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}

