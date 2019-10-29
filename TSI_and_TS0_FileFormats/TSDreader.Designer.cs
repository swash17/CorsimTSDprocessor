namespace TSI_and_TS0_FileFormats
{
    partial class TSDreader
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
            this.treeView_Messages = new System.Windows.Forms.TreeView();
            this.menuStrip_top = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openForDisplayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openForWriteTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToShowVehicleTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToTrackAVehicleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_top.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView_Messages
            // 
            this.treeView_Messages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_Messages.HotTracking = true;
            this.treeView_Messages.LineColor = System.Drawing.Color.DarkGreen;
            this.treeView_Messages.Location = new System.Drawing.Point(0, 24);
            this.treeView_Messages.Name = "treeView_Messages";
            this.treeView_Messages.Size = new System.Drawing.Size(542, 537);
            this.treeView_Messages.TabIndex = 2;
            // 
            // menuStrip_top
            // 
            this.menuStrip_top.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip_top.Location = new System.Drawing.Point(0, 0);
            this.menuStrip_top.Name = "menuStrip_top";
            this.menuStrip_top.Size = new System.Drawing.Size(542, 24);
            this.menuStrip_top.TabIndex = 3;
            this.menuStrip_top.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openForDisplayToolStripMenuItem,
            this.openForWriteTestToolStripMenuItem,
            this.openToShowVehicleTypesToolStripMenuItem,
            this.openToTrackAVehicleToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openForDisplayToolStripMenuItem
            // 
            this.openForDisplayToolStripMenuItem.Name = "openForDisplayToolStripMenuItem";
            this.openForDisplayToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.openForDisplayToolStripMenuItem.Text = "Open For Display";
            this.openForDisplayToolStripMenuItem.Click += new System.EventHandler(this.openForDisplayToolStripMenuItem_Click);
            // 
            // openForWriteTestToolStripMenuItem
            // 
            this.openForWriteTestToolStripMenuItem.Name = "openForWriteTestToolStripMenuItem";
            this.openForWriteTestToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.openForWriteTestToolStripMenuItem.Text = "Open For Write Test";
            this.openForWriteTestToolStripMenuItem.Click += new System.EventHandler(this.openForWriteTestToolStripMenuItem_Click);
            // 
            // openToShowVehicleTypesToolStripMenuItem
            // 
            this.openToShowVehicleTypesToolStripMenuItem.Name = "openToShowVehicleTypesToolStripMenuItem";
            this.openToShowVehicleTypesToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.openToShowVehicleTypesToolStripMenuItem.Text = "Open To Show Vehicle Types";
            this.openToShowVehicleTypesToolStripMenuItem.Click += new System.EventHandler(this.openToShowVehicleTypesToolStripMenuItem_Click);
            // 
            // openToTrackAVehicleToolStripMenuItem
            // 
            this.openToTrackAVehicleToolStripMenuItem.Name = "openToTrackAVehicleToolStripMenuItem";
            this.openToTrackAVehicleToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.openToTrackAVehicleToolStripMenuItem.Text = "Open to Track a Vehicle";
            this.openToTrackAVehicleToolStripMenuItem.Click += new System.EventHandler(this.openToTrackAVehicleToolStripMenuItem_Click);
            // 
            // TSDreader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 561);
            this.Controls.Add(this.treeView_Messages);
            this.Controls.Add(this.menuStrip_top);
            this.MainMenuStrip = this.menuStrip_top;
            this.Name = "TSDreader";
            this.Text = "TS0 Reader (and other items)";
            this.menuStrip_top.ResumeLayout(false);
            this.menuStrip_top.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView_Messages;
        private System.Windows.Forms.MenuStrip menuStrip_top;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openForDisplayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openForWriteTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToShowVehicleTypesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToTrackAVehicleToolStripMenuItem;
    }
}

