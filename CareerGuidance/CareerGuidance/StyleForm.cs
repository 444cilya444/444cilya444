using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class StyleForm : Form
{
    [Browsable(true)]
    public bool ISVisibleButtonMax { get; set; }

    public StyleForm()
    {
        InitializeComponent();

    }
    //Свойства
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StyleForm));
        this.PHeader = new System.Windows.Forms.Panel();
        this.lbHeader = new System.Windows.Forms.Label();
        this.btnMin = new System.Windows.Forms.Button();
        this.btnMax = new System.Windows.Forms.Button();
        this.button1 = new System.Windows.Forms.Button();
        this.panel2 = new System.Windows.Forms.Panel();
        this.panel3 = new System.Windows.Forms.Panel();
        this.panel4 = new System.Windows.Forms.Panel();
        this.panel5 = new System.Windows.Forms.Panel();
        this.PHeader.SuspendLayout();
        this.SuspendLayout();
        // 
        // PHeader
        // 
        this.PHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
        this.PHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

        this.PHeader.Controls.Add(this.lbHeader);
        this.PHeader.Controls.Add(this.btnMin);
        this.PHeader.Controls.Add(this.btnMax);
        this.PHeader.Controls.Add(this.button1);
        this.PHeader.Cursor = System.Windows.Forms.Cursors.SizeAll;
        this.PHeader.Dock = System.Windows.Forms.DockStyle.Top;
        this.PHeader.Location = new System.Drawing.Point(2, 2);
        this.PHeader.Name = "PHeader";
        this.PHeader.Size = new System.Drawing.Size(1188, 26);
        this.PHeader.TabIndex = 0;
        this.PHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PHeader_MouseDown);
        this.PHeader.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PHeader_MouseMove);
        this.PHeader.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PHeader_MouseUp);

        // 
        // lbHeader
        // 
        this.lbHeader.AutoSize = true;
        this.lbHeader.Font = new System.Drawing.Font("Segoe Script", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        this.lbHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
        this.lbHeader.Location = new System.Drawing.Point(9, 4);
        this.lbHeader.Name = "lbHeader";
        this.lbHeader.Size = new System.Drawing.Size(100, 20);
        this.lbHeader.TabIndex = 4;
        this.lbHeader.Text = "";
        // 
        // btnMin
        // 
        this.btnMin.Cursor = System.Windows.Forms.Cursors.Hand;
        this.btnMin.Dock = System.Windows.Forms.DockStyle.Right;
        this.btnMin.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
        this.btnMin.FlatAppearance.BorderSize = 0;
        this.btnMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnMin.Image = ((System.Drawing.Image)(resources.GetObject("btnMin.Image")));
        this.btnMin.Location = new System.Drawing.Point(1057, 0);
        this.btnMin.Name = "btnMin";
        this.btnMin.Size = new System.Drawing.Size(43, 24);
        this.btnMin.TabIndex = 1;
        this.btnMin.UseVisualStyleBackColor = true;
        this.btnMin.Click += new System.EventHandler(this.WindowAction_Click);
        // 
        // btnMax
        // 
        this.btnMax.Cursor = System.Windows.Forms.Cursors.Hand;
        this.btnMax.Dock = System.Windows.Forms.DockStyle.Right;
        this.btnMax.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
        this.btnMax.FlatAppearance.BorderSize = 0;
        this.btnMax.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnMax.Image = ((System.Drawing.Image)(resources.GetObject("btnMax.Image")));
        this.btnMax.Location = new System.Drawing.Point(1100, 0);
        this.btnMax.Name = "btnMax";
        this.btnMax.Size = new System.Drawing.Size(43, 24);
        this.btnMax.TabIndex = 2;
        this.btnMax.UseVisualStyleBackColor = true;
        this.btnMax.Click += new System.EventHandler(this.WindowAction_Click);
        // 
        // button1
        // 
        this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
        this.button1.Dock = System.Windows.Forms.DockStyle.Right;
        this.button1.FlatAppearance.BorderSize = 0;
        this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
        this.button1.Location = new System.Drawing.Point(1143, 0);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(43, 24);
        this.button1.TabIndex = 3;
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(this.WindowAction_Click);
        // 
        // panel2
        // 
        this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
        this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
        this.panel2.Location = new System.Drawing.Point(1190, 2);
        this.panel2.Name = "panel2";
        this.panel2.Size = new System.Drawing.Size(2, 631);
        this.panel2.BackColor = Color.FromArgb(255, 128, 0);
        this.panel2.TabIndex = 1;
        // 
        // panel3
        // 
        this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
        this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
        this.panel3.Location = new System.Drawing.Point(0, 2);
        this.panel3.Name = "panel3";
        this.panel3.Size = new System.Drawing.Size(2, 631);
        this.panel3.BackColor = Color.FromArgb(255, 128, 0);
        this.panel3.TabIndex = 2;
        // 
        // panel4
        // 
        this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
        this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.panel4.Location = new System.Drawing.Point(2, 631);
        this.panel4.Name = "panel4";
        this.panel4.Size = new System.Drawing.Size(1188, 2);
        this.panel4.BackColor = Color.FromArgb(255, 128, 0);
        this.panel4.TabIndex = 3;
        // 
        // panel5
        // 
        this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
        this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
        this.panel5.Location = new System.Drawing.Point(0, 0);
        this.panel5.Name = "panel5";
        this.panel5.Size = new System.Drawing.Size(1192, 2);
        this.panel5.BackColor = Color.FromArgb(255, 128, 0);
        this.panel5.TabIndex = 4;
        // 
        // StyleForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.SystemColors.ActiveBorder;
        this.ClientSize = new System.Drawing.Size(1192, 633);
        this.Controls.Add(this.panel4);
        this.Controls.Add(this.PHeader);
        this.Controls.Add(this.panel3);
        this.Controls.Add(this.panel2);
        this.Controls.Add(this.panel5);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        this.Name = "StyleForm";
        this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
        this.PHeader.ResumeLayout(false);
        this.PHeader.PerformLayout();
        this.ResumeLayout(false);

    }

    //Переменные
    private System.Windows.Forms.Panel PHeader;
    public System.Windows.Forms.Button btnMin;
    public System.Windows.Forms.Button btnMax;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.Panel panel4;
    private System.Windows.Forms.Panel panel5;
    public System.Windows.Forms.Label lbHeader;
    private Point mouseOffset;
    private bool isMouseDown = false;
    bool flag = true;

    //Панель с кнопками
    private void WindowAction_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        switch (btn.TabIndex)
        {
            case 1:
                WindowState = FormWindowState.Minimized;
                break;
            case 2:
                if (flag)
                {
                    WindowState = FormWindowState.Maximized;
                    flag = false;
                }
                else
                {
                    WindowState = FormWindowState.Normal;
                    flag = true;
                }
                break;
            case 3:
                Close();
                break;
        }
    }


    //Перемещение формы
    private void PHeader_MouseDown(object sender, MouseEventArgs e)
    {
        int xOffset;
        int yOffset;

        if (e.Button == MouseButtons.Left)
        {
            this.Opacity = 0.5;
            xOffset = -e.X - SystemInformation.FrameBorderSize.Width;
            yOffset = -e.Y - SystemInformation.CaptionHeight -
                SystemInformation.FrameBorderSize.Height;
            mouseOffset = new Point(xOffset, yOffset);
            isMouseDown = true;
        }
    }

    private void Form1_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right)
            this.Opacity = 0.5;
    }

    private void PHeader_MouseUp(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            isMouseDown = false;
            this.Opacity = 1;
        }
    }

    private void PHeader_MouseMove(object sender, MouseEventArgs e)
    {
        if (isMouseDown)
        {
            Point mousePos = Control.MousePosition;
            mousePos.Offset(mouseOffset.X, mouseOffset.Y);
            Location = mousePos;
        }
    }

    //===============================================================
}

