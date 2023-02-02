﻿
namespace OthelloWinFormGame
{
    partial class Othello_GameSetting
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
            this.buttonPlayCPU = new System.Windows.Forms.Button();
            this.buttonPVP = new System.Windows.Forms.Button();
            this.buttonBoardSize = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonPlayCPU
            // 
            this.buttonPlayCPU.Location = new System.Drawing.Point(12, 57);
            this.buttonPlayCPU.Name = "buttonPlayCPU";
            this.buttonPlayCPU.Size = new System.Drawing.Size(97, 35);
            this.buttonPlayCPU.TabIndex = 0;
            this.buttonPlayCPU.Text = "Play against the computer";
            this.buttonPlayCPU.UseVisualStyleBackColor = true;
            this.buttonPlayCPU.Click += new System.EventHandler(this.buttonPlayCPU_Click);
            // 
            // buttonPVP
            // 
            this.buttonPVP.Location = new System.Drawing.Point(115, 57);
            this.buttonPVP.Name = "buttonPVP";
            this.buttonPVP.Size = new System.Drawing.Size(97, 35);
            this.buttonPVP.TabIndex = 1;
            this.buttonPVP.Text = "Play against your friend";
            this.buttonPVP.UseVisualStyleBackColor = true;
            this.buttonPVP.Click += new System.EventHandler(this.buttonPVP_Click);
            // 
            // buttonBoardSize
            // 
            this.buttonBoardSize.Location = new System.Drawing.Point(12, 12);
            this.buttonBoardSize.Name = "buttonBoardSize";
            this.buttonBoardSize.Size = new System.Drawing.Size(200, 35);
            this.buttonBoardSize.TabIndex = 2;
            this.buttonBoardSize.Text = "Board Size: 6x6 (click to increase)";
            this.buttonBoardSize.UseVisualStyleBackColor = true;
            this.buttonBoardSize.Click += new System.EventHandler(this.buttonBoardSize_Click);
            // 
            // Othello_GameSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(223, 101);
            this.Controls.Add(this.buttonBoardSize);
            this.Controls.Add(this.buttonPVP);
            this.Controls.Add(this.buttonPlayCPU);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Othello_GameSetting";
            this.RightToLeftLayout = true;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Othello-Game Setting";
            this.Load += new System.EventHandler(this.Othello_GameSetting_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonPlayCPU;
        private System.Windows.Forms.Button buttonPVP;
        private System.Windows.Forms.Button buttonBoardSize;
    }
}