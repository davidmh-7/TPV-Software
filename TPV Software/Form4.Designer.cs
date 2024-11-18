namespace TPV_Software
{
    partial class Form4
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
            button2 = new Button();
            button1 = new Button();
            listView1 = new ListView();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // button2
            // 
            button2.Location = new Point(699, 407);
            button2.Name = "button2";
            button2.Size = new Size(89, 31);
            button2.TabIndex = 31;
            button2.Text = "Cancelar";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(604, 407);
            button1.Name = "button1";
            button1.Size = new Size(89, 31);
            button1.TabIndex = 32;
            button1.Text = "Imprimir";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // listView1
            // 
            listView1.BackColor = SystemColors.ControlLightLight;
            listView1.BorderStyle = BorderStyle.FixedSingle;
            listView1.Location = new Point(12, 26);
            listView1.Name = "listView1";
            listView1.Size = new Size(764, 217);
            listView1.TabIndex = 34;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(23, 271);
            label1.Name = "label1";
            label1.Size = new Size(99, 15);
            label1.TabIndex = 35;
            label1.Text = "Resumen pedido:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(426, 271);
            label2.Name = "label2";
            label2.Size = new Size(70, 15);
            label2.TabIndex = 36;
            label2.Text = "Precio total:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(502, 271);
            label3.Name = "label3";
            label3.Size = new Size(40, 15);
            label3.TabIndex = 37;
            label3.Text = "37,34€";
            // 
            // Form4
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(listView1);
            Controls.Add(button1);
            Controls.Add(button2);
            Name = "Form4";
            Text = "Form4";
            Load += Form4_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button2;
        private Button button1;
        private ListView listView1;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}