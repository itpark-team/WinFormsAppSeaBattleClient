﻿namespace WinFormsAppSeaBattleClient.Forms
{
    partial class FormGame
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
            this.buttonConnectToServer = new System.Windows.Forms.Button();
            this.buttonStartGame = new System.Windows.Forms.Button();
            this.pictureBoxMyField = new System.Windows.Forms.PictureBox();
            this.pictureBoxShootField = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMyField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxShootField)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonConnectToServer
            // 
            this.buttonConnectToServer.Location = new System.Drawing.Point(12, 12);
            this.buttonConnectToServer.Name = "buttonConnectToServer";
            this.buttonConnectToServer.Size = new System.Drawing.Size(179, 27);
            this.buttonConnectToServer.TabIndex = 0;
            this.buttonConnectToServer.Text = "Подключиться к серверу";
            this.buttonConnectToServer.UseVisualStyleBackColor = true;
            this.buttonConnectToServer.Click += new System.EventHandler(this.buttonConnectToServer_Click);
            // 
            // buttonStartGame
            // 
            this.buttonStartGame.Location = new System.Drawing.Point(197, 14);
            this.buttonStartGame.Name = "buttonStartGame";
            this.buttonStartGame.Size = new System.Drawing.Size(179, 25);
            this.buttonStartGame.TabIndex = 1;
            this.buttonStartGame.Text = "Начать игру";
            this.buttonStartGame.UseVisualStyleBackColor = true;
            this.buttonStartGame.Click += new System.EventHandler(this.buttonStartGame_Click);
            // 
            // pictureBoxMyField
            // 
            this.pictureBoxMyField.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBoxMyField.Location = new System.Drawing.Point(12, 63);
            this.pictureBoxMyField.Name = "pictureBoxMyField";
            this.pictureBoxMyField.Size = new System.Drawing.Size(250, 250);
            this.pictureBoxMyField.TabIndex = 2;
            this.pictureBoxMyField.TabStop = false;
            // 
            // pictureBoxShootField
            // 
            this.pictureBoxShootField.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBoxShootField.Location = new System.Drawing.Point(268, 63);
            this.pictureBoxShootField.Name = "pictureBoxShootField";
            this.pictureBoxShootField.Size = new System.Drawing.Size(250, 250);
            this.pictureBoxShootField.TabIndex = 3;
            this.pictureBoxShootField.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Поле моё";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(268, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Поле другого игрока";
            // 
            // FormGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBoxShootField);
            this.Controls.Add(this.pictureBoxMyField);
            this.Controls.Add(this.buttonStartGame);
            this.Controls.Add(this.buttonConnectToServer);
            this.Name = "FormGame";
            this.Text = "FormGame";
            this.Load += new System.EventHandler(this.FormGame_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMyField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxShootField)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button buttonConnectToServer;
        private Button buttonStartGame;
        private PictureBox pictureBoxMyField;
        private PictureBox pictureBoxShootField;
        private Label label1;
        private Label label2;
    }
}