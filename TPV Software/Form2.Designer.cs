namespace TPV_Software
{
    partial class Form2
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
            label3 = new Label();
            button3 = new Button();
            button1 = new Button();
            button2 = new Button();
            button4 = new Button();
            button5 = new Button();
            label4 = new Label();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.FlatStyle = FlatStyle.System;
            label3.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(210, 53);
            label3.Name = "label3";
            label3.Size = new Size(219, 37);
            label3.TabIndex = 6;
            label3.Text = "Elija una opción";
            // 
            // button3
            // 
            button3.BackColor = Color.Black;
            button3.BackgroundImageLayout = ImageLayout.Center;
            button3.FlatAppearance.BorderColor = SystemColors.ControlLight;
            button3.FlatAppearance.BorderSize = 0;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button3.ForeColor = SystemColors.ControlLightLight;
            button3.Location = new Point(338, 160);
            button3.Name = "button3";
            button3.Size = new Size(106, 86);
            button3.TabIndex = 9;
            button3.Text = "Gestion Stock";
            button3.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            button1.BackColor = Color.Black;
            button1.BackgroundImageLayout = ImageLayout.Center;
            button1.FlatAppearance.BorderColor = SystemColors.ControlLight;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = SystemColors.ControlLightLight;
            button1.Location = new Point(176, 160);
            button1.Name = "button1";
            button1.Size = new Size(106, 86);
            button1.TabIndex = 10;
            button1.Text = "Gestion Usuarios";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(125, 274);
            button2.Name = "button2";
            button2.Size = new Size(100, 31);
            button2.TabIndex = 11;
            button2.Text = "Vista Usuario";
            button2.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(260, 274);
            button4.Name = "button4";
            button4.Size = new Size(100, 31);
            button4.TabIndex = 12;
            button4.Text = "Ir a tras";
            button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Location = new Point(379, 274);
            button5.Name = "button5";
            button5.Size = new Size(100, 31);
            button5.TabIndex = 13;
            button5.Text = "Cerrar app";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = SystemColors.ControlLightLight;
            label4.Font = new Font("Segoe UI", 8F);
            label4.ForeColor = SystemColors.ControlDarkDark;
            label4.Location = new Point(193, 107);
            label4.Name = "label4";
            label4.Size = new Size(251, 13);
            label4.TabIndex = 14;
            label4.Text = "Enter your information to login in your account";
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(584, 361);
            Controls.Add(label4);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(button3);
            Controls.Add(label3);
            Name = "Form2";
            Text = "Form2";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label3;
        private Button button3;
        private Button button1;
        private Button button2;
        private Button button4;
        private Button button5;
        private Label label4;
    }
}