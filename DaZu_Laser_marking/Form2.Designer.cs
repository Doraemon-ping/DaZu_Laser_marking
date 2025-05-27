namespace DaZu_Laser_marking
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.主页ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打标机配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mes配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.测试页ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据导出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.主页ToolStripMenuItem,
            this.打标机配置ToolStripMenuItem,
            this.mes配置ToolStripMenuItem,
            this.测试页ToolStripMenuItem,
            this.数据导出ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1364, 32);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 主页ToolStripMenuItem
            // 
            this.主页ToolStripMenuItem.Name = "主页ToolStripMenuItem";
            this.主页ToolStripMenuItem.Size = new System.Drawing.Size(62, 28);
            this.主页ToolStripMenuItem.Text = "主页";
            this.主页ToolStripMenuItem.Click += new System.EventHandler(this.主页ToolStripMenuItem_Click);
            // 
            // 打标机配置ToolStripMenuItem
            // 
            this.打标机配置ToolStripMenuItem.Name = "打标机配置ToolStripMenuItem";
            this.打标机配置ToolStripMenuItem.Size = new System.Drawing.Size(116, 28);
            this.打标机配置ToolStripMenuItem.Text = "打标机配置";
            this.打标机配置ToolStripMenuItem.Click += new System.EventHandler(this.打标机配置ToolStripMenuItem_Click);
            // 
            // mes配置ToolStripMenuItem
            // 
            this.mes配置ToolStripMenuItem.Name = "mes配置ToolStripMenuItem";
            this.mes配置ToolStripMenuItem.Size = new System.Drawing.Size(97, 28);
            this.mes配置ToolStripMenuItem.Text = "mes配置";
            this.mes配置ToolStripMenuItem.Click += new System.EventHandler(this.mes配置ToolStripMenuItem_Click);
            // 
            // 测试页ToolStripMenuItem
            // 
            this.测试页ToolStripMenuItem.Name = "测试页ToolStripMenuItem";
            this.测试页ToolStripMenuItem.Size = new System.Drawing.Size(80, 28);
            this.测试页ToolStripMenuItem.Text = "测试页";
            this.测试页ToolStripMenuItem.Click += new System.EventHandler(this.测试页ToolStripMenuItem_Click);
            // 
            // 数据导出ToolStripMenuItem
            // 
            this.数据导出ToolStripMenuItem.Name = "数据导出ToolStripMenuItem";
            this.数据导出ToolStripMenuItem.Size = new System.Drawing.Size(98, 28);
            this.数据导出ToolStripMenuItem.Text = "数据导出";
            this.数据导出ToolStripMenuItem.Click += new System.EventHandler(this.数据导出ToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Location = new System.Drawing.Point(12, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1330, 850);
            this.panel1.TabIndex = 2;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1364, 899);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form2";
            this.Text = "转向节系统三部LEO客户码转换";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 主页ToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem 打标机配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mes配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 测试页ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 数据导出ToolStripMenuItem;
    }
}