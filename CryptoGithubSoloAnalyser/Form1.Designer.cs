namespace CryptoGithubSoloAnalyser
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
            this.chkIsSingle = new System.Windows.Forms.CheckBox();
            this.cmdRunAnalysis = new System.Windows.Forms.Button();
            this.txtCoinID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSymbol = new System.Windows.Forms.TextBox();
            this.txtGtRoot = new System.Windows.Forms.TextBox();
            this.cmdTest11 = new System.Windows.Forms.Button();
            this.cmdGetRoots = new System.Windows.Forms.Button();
            this.chkCleanRoots = new System.Windows.Forms.CheckBox();
            this.cmdGetRootTest = new System.Windows.Forms.Button();
            this.chkSmallSet = new System.Windows.Forms.CheckBox();
            this.cmdRunAll = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLeadPrj = new System.Windows.Forms.TextBox();
            this.chkSmall = new System.Windows.Forms.CheckBox();
            this.lblDbConn = new System.Windows.Forms.Label();
            this.chkClean = new System.Windows.Forms.CheckBox();
            this.cmdRepeatCommit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chkIsSingle
            // 
            this.chkIsSingle.AutoSize = true;
            this.chkIsSingle.BackColor = System.Drawing.SystemColors.Info;
            this.chkIsSingle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIsSingle.Location = new System.Drawing.Point(289, 61);
            this.chkIsSingle.Name = "chkIsSingle";
            this.chkIsSingle.Size = new System.Drawing.Size(124, 20);
            this.chkIsSingle.TabIndex = 0;
            this.chkIsSingle.Text = "SINGLE TOKEN";
            this.chkIsSingle.UseVisualStyleBackColor = false;
            // 
            // cmdRunAnalysis
            // 
            this.cmdRunAnalysis.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRunAnalysis.Location = new System.Drawing.Point(289, 100);
            this.cmdRunAnalysis.Name = "cmdRunAnalysis";
            this.cmdRunAnalysis.Size = new System.Drawing.Size(127, 23);
            this.cmdRunAnalysis.TabIndex = 1;
            this.cmdRunAnalysis.Text = "RUN ANALYSYS";
            this.cmdRunAnalysis.UseVisualStyleBackColor = true;
            this.cmdRunAnalysis.Click += new System.EventHandler(this.cmdRunAnalysis_Click);
            // 
            // txtCoinID
            // 
            this.txtCoinID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCoinID.Location = new System.Drawing.Point(135, 62);
            this.txtCoinID.Name = "txtCoinID";
            this.txtCoinID.Size = new System.Drawing.Size(139, 22);
            this.txtCoinID.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 5;
            this.label1.Text = "COIN ID";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 6;
            this.label2.Text = "SYMBOL";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 7;
            this.label3.Text = "GT ROOT";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtSymbol
            // 
            this.txtSymbol.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSymbol.Location = new System.Drawing.Point(135, 100);
            this.txtSymbol.Name = "txtSymbol";
            this.txtSymbol.Size = new System.Drawing.Size(139, 22);
            this.txtSymbol.TabIndex = 8;
            // 
            // txtGtRoot
            // 
            this.txtGtRoot.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGtRoot.Location = new System.Drawing.Point(135, 139);
            this.txtGtRoot.Name = "txtGtRoot";
            this.txtGtRoot.Size = new System.Drawing.Size(139, 22);
            this.txtGtRoot.TabIndex = 9;
            // 
            // cmdTest11
            // 
            this.cmdTest11.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.cmdTest11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTest11.Location = new System.Drawing.Point(12, 424);
            this.cmdTest11.Name = "cmdTest11";
            this.cmdTest11.Size = new System.Drawing.Size(141, 23);
            this.cmdTest11.TabIndex = 10;
            this.cmdTest11.Text = "TEST CONTRIB 11";
            this.cmdTest11.UseVisualStyleBackColor = false;
            this.cmdTest11.Click += new System.EventHandler(this.cmdTest11_Click);
            // 
            // cmdGetRoots
            // 
            this.cmdGetRoots.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetRoots.Location = new System.Drawing.Point(384, 395);
            this.cmdGetRoots.Name = "cmdGetRoots";
            this.cmdGetRoots.Size = new System.Drawing.Size(127, 23);
            this.cmdGetRoots.TabIndex = 11;
            this.cmdGetRoots.Text = "GET ROOTS";
            this.cmdGetRoots.UseVisualStyleBackColor = true;
            this.cmdGetRoots.Click += new System.EventHandler(this.cmdGetRoots_Click);
            // 
            // chkCleanRoots
            // 
            this.chkCleanRoots.AutoSize = true;
            this.chkCleanRoots.BackColor = System.Drawing.SystemColors.Info;
            this.chkCleanRoots.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCleanRoots.Location = new System.Drawing.Point(389, 369);
            this.chkCleanRoots.Name = "chkCleanRoots";
            this.chkCleanRoots.Size = new System.Drawing.Size(122, 20);
            this.chkCleanRoots.TabIndex = 12;
            this.chkCleanRoots.Text = "CLEAN ROOTS";
            this.chkCleanRoots.UseVisualStyleBackColor = false;
            // 
            // cmdGetRootTest
            // 
            this.cmdGetRootTest.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.cmdGetRootTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetRootTest.Location = new System.Drawing.Point(12, 395);
            this.cmdGetRootTest.Name = "cmdGetRootTest";
            this.cmdGetRootTest.Size = new System.Drawing.Size(145, 23);
            this.cmdGetRootTest.TabIndex = 13;
            this.cmdGetRootTest.Text = "GET ROOTS TEST";
            this.cmdGetRootTest.UseVisualStyleBackColor = false;
            this.cmdGetRootTest.Click += new System.EventHandler(this.cmdGetRootTest_Click);
            // 
            // chkSmallSet
            // 
            this.chkSmallSet.BackColor = System.Drawing.SystemColors.Info;
            this.chkSmallSet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSmallSet.Location = new System.Drawing.Point(389, 343);
            this.chkSmallSet.Name = "chkSmallSet";
            this.chkSmallSet.Size = new System.Drawing.Size(122, 20);
            this.chkSmallSet.TabIndex = 14;
            this.chkSmallSet.Text = "SMALL SET";
            this.chkSmallSet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkSmallSet.UseVisualStyleBackColor = false;
            // 
            // cmdRunAll
            // 
            this.cmdRunAll.BackColor = System.Drawing.Color.LightSalmon;
            this.cmdRunAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRunAll.Location = new System.Drawing.Point(230, 230);
            this.cmdRunAll.Name = "cmdRunAll";
            this.cmdRunAll.Size = new System.Drawing.Size(165, 23);
            this.cmdRunAll.TabIndex = 15;
            this.cmdRunAll.Text = "RUN ALL TOKENS";
            this.cmdRunAll.UseVisualStyleBackColor = false;
            this.cmdRunAll.Click += new System.EventHandler(this.cmdRunAll_Click);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 16;
            this.label4.Text = "LEAD PRJ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtLeadPrj
            // 
            this.txtLeadPrj.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLeadPrj.Location = new System.Drawing.Point(135, 176);
            this.txtLeadPrj.Name = "txtLeadPrj";
            this.txtLeadPrj.Size = new System.Drawing.Size(139, 22);
            this.txtLeadPrj.TabIndex = 17;
            // 
            // chkSmall
            // 
            this.chkSmall.BackColor = System.Drawing.SystemColors.Info;
            this.chkSmall.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSmall.Location = new System.Drawing.Point(230, 259);
            this.chkSmall.Name = "chkSmall";
            this.chkSmall.Size = new System.Drawing.Size(165, 20);
            this.chkSmall.TabIndex = 18;
            this.chkSmall.Text = "SMALL SET";
            this.chkSmall.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkSmall.UseVisualStyleBackColor = false;
            // 
            // lblDbConn
            // 
            this.lblDbConn.BackColor = System.Drawing.Color.NavajoWhite;
            this.lblDbConn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDbConn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDbConn.Location = new System.Drawing.Point(12, 18);
            this.lblDbConn.Name = "lblDbConn";
            this.lblDbConn.Size = new System.Drawing.Size(262, 23);
            this.lblDbConn.TabIndex = 19;
            this.lblDbConn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkClean
            // 
            this.chkClean.BackColor = System.Drawing.SystemColors.Info;
            this.chkClean.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkClean.Location = new System.Drawing.Point(230, 285);
            this.chkClean.Name = "chkClean";
            this.chkClean.Size = new System.Drawing.Size(165, 20);
            this.chkClean.TabIndex = 20;
            this.chkClean.Text = "CLEAN DATA";
            this.chkClean.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkClean.UseVisualStyleBackColor = false;
            // 
            // cmdRepeatCommit
            // 
            this.cmdRepeatCommit.BackColor = System.Drawing.Color.LightSalmon;
            this.cmdRepeatCommit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRepeatCommit.Location = new System.Drawing.Point(27, 282);
            this.cmdRepeatCommit.Name = "cmdRepeatCommit";
            this.cmdRepeatCommit.Size = new System.Drawing.Size(165, 23);
            this.cmdRepeatCommit.TabIndex = 21;
            this.cmdRepeatCommit.Text = "REPEAT COMMITS";
            this.cmdRepeatCommit.UseVisualStyleBackColor = false;
            this.cmdRepeatCommit.Click += new System.EventHandler(this.cmdRepeatCommit_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 459);
            this.Controls.Add(this.cmdRepeatCommit);
            this.Controls.Add(this.chkClean);
            this.Controls.Add(this.lblDbConn);
            this.Controls.Add(this.chkSmall);
            this.Controls.Add(this.txtLeadPrj);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmdRunAll);
            this.Controls.Add(this.chkSmallSet);
            this.Controls.Add(this.cmdGetRootTest);
            this.Controls.Add(this.chkCleanRoots);
            this.Controls.Add(this.cmdGetRoots);
            this.Controls.Add(this.cmdTest11);
            this.Controls.Add(this.txtGtRoot);
            this.Controls.Add(this.txtSymbol);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCoinID);
            this.Controls.Add(this.cmdRunAnalysis);
            this.Controls.Add(this.chkIsSingle);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TOKEN GITHUB ANALYSER";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkIsSingle;
        private System.Windows.Forms.Button cmdRunAnalysis;
        private System.Windows.Forms.TextBox txtCoinID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSymbol;
        private System.Windows.Forms.TextBox txtGtRoot;
        private System.Windows.Forms.Button cmdTest11;
        private System.Windows.Forms.Button cmdGetRoots;
        private System.Windows.Forms.CheckBox chkCleanRoots;
        private System.Windows.Forms.Button cmdGetRootTest;
        private System.Windows.Forms.CheckBox chkSmallSet;
        private System.Windows.Forms.Button cmdRunAll;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtLeadPrj;
        private System.Windows.Forms.CheckBox chkSmall;
        private System.Windows.Forms.Label lblDbConn;
        private System.Windows.Forms.CheckBox chkClean;
        private System.Windows.Forms.Button cmdRepeatCommit;
    }
}

