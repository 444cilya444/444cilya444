namespace WindowsFormsApp1.Forms
{
    partial class DobArchive
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DobArchive));
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.yt_Button2 = new WindowsFormsApp1.yt_Button();
            this.multiColumnComboBox1 = new WindowsFormsApp1.MultiColumnComboBox();
            this.stackPanel1 = new WindowsFormsApp1.Model.StackPanel();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.yt_Button1 = new WindowsFormsApp1.yt_Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.stackPanel1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(145, 64);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(95, 20);
            this.dateTimePicker1.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(164)))), ((int)(((byte)(180)))));
            this.panel1.Controls.Add(this.pictureBox3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(333, 32);
            this.panel1.TabIndex = 72;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(164)))), ((int)(((byte)(180)))));
            this.pictureBox3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox3.Image = global::WindowsFormsApp1.Properties.Resources.close;
            this.pictureBox3.Location = new System.Drawing.Point(301, 0);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(32, 32);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 48;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(164)))), ((int)(((byte)(180)))));
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(4, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 24);
            this.label4.TabIndex = 47;
            this.label4.Text = "Добавить";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(230)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(32, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(261, 20);
            this.label1.TabIndex = 49;
            this.label1.Text = "Дата/Причина снятия с учета";
            // 
            // yt_Button2
            // 
            this.yt_Button2.BackColor = System.Drawing.Color.Snow;
            this.yt_Button2.BackColorAdditional = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.yt_Button2.BackColorGradientEnabled = true;
            this.yt_Button2.BackColorGradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.yt_Button2.BorderColor = System.Drawing.Color.Black;
            this.yt_Button2.BorderColorEnabled = true;
            this.yt_Button2.BorderColorOnHover = System.Drawing.Color.Black;
            this.yt_Button2.BorderColorOnHoverEnabled = true;
            this.yt_Button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.yt_Button2.Font = new System.Drawing.Font("Candara", 12F, System.Drawing.FontStyle.Bold);
            this.yt_Button2.ForeColor = System.Drawing.Color.Black;
            this.yt_Button2.Location = new System.Drawing.Point(86, 115);
            this.yt_Button2.Name = "yt_Button2";
            this.yt_Button2.RippleColor = System.Drawing.Color.Black;
            this.yt_Button2.Rounding = 35;
            this.yt_Button2.RoundingEnable = true;
            this.yt_Button2.Size = new System.Drawing.Size(153, 25);
            this.yt_Button2.TabIndex = 76;
            this.yt_Button2.Text = "Добавить в архив";
            this.yt_Button2.TextHover = "";
            this.yt_Button2.UseDownPressEffectOnClick = false;
            this.yt_Button2.UseDrawRectangle = false;
            this.yt_Button2.UseRippleEffect = false;
            this.yt_Button2.UseZoomEffectOnHover = false;
            this.yt_Button2.Click += new System.EventHandler(this.button1_Click);
            // 
            // multiColumnComboBox1
            // 
            this.multiColumnComboBox1.BackColor = System.Drawing.SystemColors.Window;
            this.multiColumnComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.multiColumnComboBox1.Font = new System.Drawing.Font("Calibri", 12F);
            this.multiColumnComboBox1.FormattingEnabled = true;
            this.multiColumnComboBox1.Items.AddRange(new object[] {
            "Выпустился",
            "Забрал документы",
            "Отчислился"});
            this.multiColumnComboBox1.Location = new System.Drawing.Point(85, 64);
            this.multiColumnComboBox1.Name = "multiColumnComboBox1";
            this.multiColumnComboBox1.Size = new System.Drawing.Size(126, 28);
            this.multiColumnComboBox1.TabIndex = 73;
            this.multiColumnComboBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.multiColumnComboBox1_KeyPress);
            // 
            // stackPanel1
            // 
            this.stackPanel1.Controls.Add(this.tabPage1);
            this.stackPanel1.Controls.Add(this.tabPage2);
            this.stackPanel1.Location = new System.Drawing.Point(1, 32);
            this.stackPanel1.Name = "stackPanel1";
            this.stackPanel1.SelectedIndex = 0;
            this.stackPanel1.Size = new System.Drawing.Size(333, 161);
            this.stackPanel1.TabIndex = 77;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(230)))));
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.yt_Button2);
            this.tabPage1.Controls.Add(this.multiColumnComboBox1);
            this.tabPage1.Controls.Add(this.dateTimePicker1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(325, 135);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(230)))));
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.yt_Button1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(325, 135);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Calibri", 12F);
            this.textBox1.Location = new System.Drawing.Point(101, 64);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(123, 27);
            this.textBox1.TabIndex = 81;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(230)))));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(87, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 20);
            this.label2.TabIndex = 78;
            this.label2.Text = "Добавить группу";
            // 
            // yt_Button1
            // 
            this.yt_Button1.BackColor = System.Drawing.Color.Snow;
            this.yt_Button1.BackColorAdditional = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.yt_Button1.BackColorGradientEnabled = true;
            this.yt_Button1.BackColorGradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.yt_Button1.BorderColor = System.Drawing.Color.Black;
            this.yt_Button1.BorderColorEnabled = true;
            this.yt_Button1.BorderColorOnHover = System.Drawing.Color.Black;
            this.yt_Button1.BorderColorOnHoverEnabled = true;
            this.yt_Button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.yt_Button1.Font = new System.Drawing.Font("Candara", 12F, System.Drawing.FontStyle.Bold);
            this.yt_Button1.ForeColor = System.Drawing.Color.Black;
            this.yt_Button1.Location = new System.Drawing.Point(89, 115);
            this.yt_Button1.Name = "yt_Button1";
            this.yt_Button1.RippleColor = System.Drawing.Color.Black;
            this.yt_Button1.Rounding = 35;
            this.yt_Button1.RoundingEnable = true;
            this.yt_Button1.Size = new System.Drawing.Size(146, 25);
            this.yt_Button1.TabIndex = 80;
            this.yt_Button1.Text = "Добавить группу";
            this.yt_Button1.TextHover = "";
            this.yt_Button1.UseDownPressEffectOnClick = false;
            this.yt_Button1.UseDrawRectangle = false;
            this.yt_Button1.UseRippleEffect = false;
            this.yt_Button1.UseZoomEffectOnHover = false;
            this.yt_Button1.Click += new System.EventHandler(this.yt_Button1_Click);
            // 
            // DobArchive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(230)))));
            this.ClientSize = new System.Drawing.Size(335, 195);
            this.Controls.Add(this.stackPanel1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DobArchive";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Архив";
            this.VisibleChanged += new System.EventHandler(this.DobArchive_VisibleChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DobArchive_Paint);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.stackPanel1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private MultiColumnComboBox multiColumnComboBox1;
        private yt_Button yt_Button2;
        private System.Windows.Forms.Label label1;
        private Model.StackPanel stackPanel1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private yt_Button yt_Button1;
    }
}