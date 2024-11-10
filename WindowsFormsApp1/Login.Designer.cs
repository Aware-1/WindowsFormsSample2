namespace WindowsFormsApp1
{
    partial class Login
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
            this.LblPass = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.PassTxtBox = new System.Windows.Forms.TextBox();
            this.UserTxtBox = new System.Windows.Forms.TextBox();
            this.LoginBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LblPass
            // 
            this.LblPass.AutoSize = true;
            this.LblPass.Location = new System.Drawing.Point(203, 69);
            this.LblPass.Name = "LblPass";
            this.LblPass.Size = new System.Drawing.Size(47, 13);
            this.LblPass.TabIndex = 0;
            this.LblPass.Text = "رمز ورود";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(192, 34);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(58, 13);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "نام کاربری";
            // 
            // PassTxtBox
            // 
            this.PassTxtBox.Location = new System.Drawing.Point(25, 66);
            this.PassTxtBox.Name = "PassTxtBox";
            this.PassTxtBox.Size = new System.Drawing.Size(144, 20);
            this.PassTxtBox.TabIndex = 2;
            // 
            // UserTxtBox
            // 
            this.UserTxtBox.Location = new System.Drawing.Point(25, 31);
            this.UserTxtBox.Name = "UserTxtBox";
            this.UserTxtBox.Size = new System.Drawing.Size(144, 20);
            this.UserTxtBox.TabIndex = 3;
            // 
            // LoginBtn
            // 
            this.LoginBtn.Location = new System.Drawing.Point(89, 107);
            this.LoginBtn.Name = "LoginBtn";
            this.LoginBtn.Size = new System.Drawing.Size(99, 31);
            this.LoginBtn.TabIndex = 4;
            this.LoginBtn.Text = "ورود";
            this.LoginBtn.UseVisualStyleBackColor = true;
            this.LoginBtn.Click += new System.EventHandler(this.LoginBtn_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 161);
            this.Controls.Add(this.LoginBtn);
            this.Controls.Add(this.UserTxtBox);
            this.Controls.Add(this.PassTxtBox);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.LblPass);
            this.Name = "Login";
            this.Text = "ورود";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LblPass;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox PassTxtBox;
        private System.Windows.Forms.TextBox UserTxtBox;
        private System.Windows.Forms.Button LoginBtn;
    }
}