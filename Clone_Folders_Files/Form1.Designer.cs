namespace Clone_Folders_Files
{
    partial class Form1
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
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtFilepath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.buildHere = new System.Windows.Forms.TextBox();
            this.fileExt = new System.Windows.Forms.TextBox();
            this.directoriesOnly = new System.Windows.Forms.CheckBox();
            this.btnBrowse2 = new System.Windows.Forms.Button();
            this.filesOnly = new System.Windows.Forms.CheckBox();
            this.noEmpty = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(205, 85);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(81, 29);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "Clone";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtFilepath
            // 
            this.txtFilepath.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.txtFilepath.Location = new System.Drawing.Point(12, 11);
            this.txtFilepath.Name = "txtFilepath";
            this.txtFilepath.Size = new System.Drawing.Size(169, 20);
            this.txtFilepath.TabIndex = 1;
            this.txtFilepath.Text = "Select the source directory";
            this.txtFilepath.Click += new System.EventHandler(this.txtFilepath_Click);
            this.txtFilepath.TextChanged += new System.EventHandler(this.txtFilepath_TextChanged);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(205, 11);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(78, 19);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // buildHere
            // 
            this.buildHere.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.buildHere.Location = new System.Drawing.Point(12, 37);
            this.buildHere.Name = "buildHere";
            this.buildHere.Size = new System.Drawing.Size(169, 20);
            this.buildHere.TabIndex = 3;
            this.buildHere.Text = "Select the destination directory";
            this.buildHere.Click += new System.EventHandler(this.buildHere_Click);
            this.buildHere.TextChanged += new System.EventHandler(this.buildHere_TextChanged);
            // 
            // fileExt
            // 
            this.fileExt.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.fileExt.Location = new System.Drawing.Point(12, 63);
            this.fileExt.Name = "fileExt";
            this.fileExt.Size = new System.Drawing.Size(56, 20);
            this.fileExt.TabIndex = 4;
            this.fileExt.Text = "File types";
            this.fileExt.Click += new System.EventHandler(this.fileExt_Click);
            this.fileExt.TextChanged += new System.EventHandler(this.fileExt_TextChanged);
            // 
            // directoriesOnly
            // 
            this.directoriesOnly.AutoSize = true;
            this.directoriesOnly.Location = new System.Drawing.Point(12, 92);
            this.directoriesOnly.Name = "directoriesOnly";
            this.directoriesOnly.Size = new System.Drawing.Size(108, 17);
            this.directoriesOnly.TabIndex = 5;
            this.directoriesOnly.Text = "Directories ONLY";
            this.directoriesOnly.UseVisualStyleBackColor = true;
            this.directoriesOnly.CheckedChanged += new System.EventHandler(this.directoriesOnly_CheckedChanged);
            // 
            // btnBrowse2
            // 
            this.btnBrowse2.Location = new System.Drawing.Point(205, 38);
            this.btnBrowse2.Name = "btnBrowse2";
            this.btnBrowse2.Size = new System.Drawing.Size(78, 19);
            this.btnBrowse2.TabIndex = 6;
            this.btnBrowse2.Text = "Browse...";
            this.btnBrowse2.UseVisualStyleBackColor = true;
            this.btnBrowse2.Click += new System.EventHandler(this.btnBrowse2_Click);
            // 
            // filesOnly
            // 
            this.filesOnly.AutoSize = true;
            this.filesOnly.Location = new System.Drawing.Point(120, 92);
            this.filesOnly.Name = "filesOnly";
            this.filesOnly.Size = new System.Drawing.Size(79, 17);
            this.filesOnly.TabIndex = 7;
            this.filesOnly.Text = "Files ONLY";
            this.filesOnly.UseVisualStyleBackColor = true;
            this.filesOnly.CheckedChanged += new System.EventHandler(this.filesOnly_CheckedChanged);
            // 
            // noEmpty
            // 
            this.noEmpty.AutoSize = true;
            this.noEmpty.Location = new System.Drawing.Point(90, 66);
            this.noEmpty.Name = "noEmpty";
            this.noEmpty.Size = new System.Drawing.Size(109, 17);
            this.noEmpty.TabIndex = 8;
            this.noEmpty.Text = "No Empty Folders";
            this.noEmpty.UseVisualStyleBackColor = true;
            this.noEmpty.CheckedChanged += new System.EventHandler(this.noEmpty_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 117);
            this.Controls.Add(this.noEmpty);
            this.Controls.Add(this.filesOnly);
            this.Controls.Add(this.btnBrowse2);
            this.Controls.Add(this.directoriesOnly);
            this.Controls.Add(this.fileExt);
            this.Controls.Add(this.buildHere);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtFilepath);
            this.Controls.Add(this.btnSearch);
            this.Name = "Form1";
            this.Text = "File and Directory Clone";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtFilepath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox buildHere;
        private System.Windows.Forms.TextBox fileExt;
        private System.Windows.Forms.CheckBox directoriesOnly;
        private System.Windows.Forms.Button btnBrowse2;
        private System.Windows.Forms.CheckBox filesOnly;
        private System.Windows.Forms.CheckBox noEmpty;
    }
}

