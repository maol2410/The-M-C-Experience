
namespace mss
{
    partial class FormProfile
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
            this.searchForMovie = new System.Windows.Forms.Button();
            this.SearchForSeries = new System.Windows.Forms.Button();
            this.updateSettings = new System.Windows.Forms.Button();
            this.ProfileControlPanel = new System.Windows.Forms.TableLayoutPanel();
            this.userNameBox = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvWatchList = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvSeriesWatchList = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.previousWatchMedia = new System.Windows.Forms.Button();
            this.ReturnToCustomer = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.ProfileControlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWatchList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSeriesWatchList)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchForMovie
            // 
            this.searchForMovie.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchForMovie.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchForMovie.Location = new System.Drawing.Point(3, 3);
            this.searchForMovie.Name = "searchForMovie";
            this.searchForMovie.Size = new System.Drawing.Size(156, 177);
            this.searchForMovie.TabIndex = 0;
            this.searchForMovie.Text = "Search For Movie";
            this.searchForMovie.UseVisualStyleBackColor = true;
            this.searchForMovie.Click += new System.EventHandler(this.searchForMovie_Click);
            // 
            // SearchForSeries
            // 
            this.SearchForSeries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SearchForSeries.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchForSeries.Location = new System.Drawing.Point(3, 186);
            this.SearchForSeries.Name = "SearchForSeries";
            this.SearchForSeries.Size = new System.Drawing.Size(156, 177);
            this.SearchForSeries.TabIndex = 3;
            this.SearchForSeries.Text = "Search for series";
            this.SearchForSeries.UseVisualStyleBackColor = true;
            this.SearchForSeries.Click += new System.EventHandler(this.searchForSeries_Click);
            // 
            // updateSettings
            // 
            this.updateSettings.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateSettings.Location = new System.Drawing.Point(15, 84);
            this.updateSettings.Name = "updateSettings";
            this.updateSettings.Size = new System.Drawing.Size(222, 54);
            this.updateSettings.TabIndex = 4;
            this.updateSettings.Text = "Update Settings";
            this.updateSettings.UseVisualStyleBackColor = true;
            this.updateSettings.Click += new System.EventHandler(this.updateSettings_Click);
            // 
            // ProfileControlPanel
            // 
            this.ProfileControlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ProfileControlPanel.ColumnCount = 1;
            this.ProfileControlPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ProfileControlPanel.Controls.Add(this.searchForMovie, 0, 0);
            this.ProfileControlPanel.Controls.Add(this.SearchForSeries, 0, 1);
            this.ProfileControlPanel.Location = new System.Drawing.Point(825, 114);
            this.ProfileControlPanel.Name = "ProfileControlPanel";
            this.ProfileControlPanel.RowCount = 2;
            this.ProfileControlPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.ProfileControlPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.ProfileControlPanel.Size = new System.Drawing.Size(162, 366);
            this.ProfileControlPanel.TabIndex = 6;
            // 
            // userNameBox
            // 
            this.userNameBox.Location = new System.Drawing.Point(15, 43);
            this.userNameBox.Name = "userNameBox";
            this.userNameBox.Size = new System.Drawing.Size(178, 20);
            this.userNameBox.TabIndex = 6;
            this.userNameBox.Text = "Mateo :)";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Child ",
            "Adolescente",
            "Adult"});
            this.comboBox1.Location = new System.Drawing.Point(217, 45);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 7;
            this.comboBox1.Text = "Child";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(213, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 19);
            this.label1.TabIndex = 8;
            this.label1.Text = "Profile Type:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(680, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 19);
            this.label2.TabIndex = 9;
            this.label2.Text = "Latest Media:";
            // 
            // dgvWatchList
            // 
            this.dgvWatchList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvWatchList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWatchList.Location = new System.Drawing.Point(346, 114);
            this.dgvWatchList.Name = "dgvWatchList";
            this.dgvWatchList.Size = new System.Drawing.Size(464, 150);
            this.dgvWatchList.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(343, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 19);
            this.label3.TabIndex = 11;
            this.label3.Text = "WatchList:";
            // 
            // dgvSeriesWatchList
            // 
            this.dgvSeriesWatchList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSeriesWatchList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSeriesWatchList.Location = new System.Drawing.Point(346, 331);
            this.dgvSeriesWatchList.Name = "dgvSeriesWatchList";
            this.dgvSeriesWatchList.Size = new System.Drawing.Size(464, 150);
            this.dgvSeriesWatchList.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(11, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 19);
            this.label4.TabIndex = 13;
            this.label4.Text = "User Name:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(343, 309);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(142, 19);
            this.label5.TabIndex = 14;
            this.label5.Text = "Series WatchList:";
            // 
            // previousWatchMedia
            // 
            this.previousWatchMedia.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.previousWatchMedia.Location = new System.Drawing.Point(678, 31);
            this.previousWatchMedia.Name = "previousWatchMedia";
            this.previousWatchMedia.Size = new System.Drawing.Size(300, 50);
            this.previousWatchMedia.TabIndex = 15;
            this.previousWatchMedia.Text = "Movie";
            this.previousWatchMedia.UseVisualStyleBackColor = true;
            this.previousWatchMedia.Click += new System.EventHandler(this.previousWatchMedia_Click);
            // 
            // ReturnToCustomer
            // 
            this.ReturnToCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ReturnToCustomer.Location = new System.Drawing.Point(8, 491);
            this.ReturnToCustomer.Name = "ReturnToCustomer";
            this.ReturnToCustomer.Size = new System.Drawing.Size(111, 50);
            this.ReturnToCustomer.TabIndex = 16;
            this.ReturnToCustomer.Text = "Return To Customer";
            this.ReturnToCustomer.UseVisualStyleBackColor = true;
            this.ReturnToCustomer.Click += new System.EventHandler(this.ReturnToCustomer_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.19929F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.70463F));
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.button1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.button2, 2, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 238);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(323, 104);
            this.tableLayoutPanel1.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 52);
            this.label6.TabIndex = 0;
            this.label6.Text = "Rate Media:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label8.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 85);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 19);
            this.label8.TabIndex = 2;
            this.label8.Text = "Rate Serie:";
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(110, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(71, 20);
            this.textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox2.Location = new System.Drawing.Point(110, 81);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(71, 20);
            this.textBox2.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Top;
            this.button1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(187, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(133, 46);
            this.button1.TabIndex = 5;
            this.button1.Text = "Rate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click_1);
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(187, 55);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(133, 46);
            this.button2.TabIndex = 6;
            this.button2.Text = "Rate";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click_1);
            // 
            // FormProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(990, 553);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.ReturnToCustomer);
            this.Controls.Add(this.updateSettings);
            this.Controls.Add(this.previousWatchMedia);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dgvSeriesWatchList);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgvWatchList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.userNameBox);
            this.Controls.Add(this.ProfileControlPanel);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Name = "FormProfile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Profile";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ProfileControlPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWatchList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSeriesWatchList)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button searchForMovie;
        private System.Windows.Forms.Button SearchForSeries;
        private System.Windows.Forms.Button updateSettings;
        private System.Windows.Forms.TableLayoutPanel ProfileControlPanel;
        private System.Windows.Forms.TextBox userNameBox;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvWatchList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvSeriesWatchList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button previousWatchMedia;
        private System.Windows.Forms.Button ReturnToCustomer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

