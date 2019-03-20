namespace FT2232HJTAGCSharpNETTestApp
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.OKbutton = new System.Windows.Forms.Button();
            this.DLLVersionLabel = new System.Windows.Forms.Label();
            this.PassFailureStatusStrip = new System.Windows.Forms.StatusStrip();
            this.PassFailStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.PassFailResultsStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ChannelComboBox = new System.Windows.Forms.ComboBox();
            this.ChannelLabel = new System.Windows.Forms.Label();
            this.DeviceNameLabel = new System.Windows.Forms.Label();
            this.MainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.OptionsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CommandSequenceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HiSpeedDeviceTypeGroupBox = new System.Windows.Forms.GroupBox();
            this.FT4232HRadioButton = new System.Windows.Forms.RadioButton();
            this.FT2232HRadioButton = new System.Windows.Forms.RadioButton();
            this.PassFailureStatusStrip.SuspendLayout();
            this.MainMenuStrip.SuspendLayout();
            this.HiSpeedDeviceTypeGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // OKbutton
            // 
            this.OKbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OKbutton.Location = new System.Drawing.Point(555, 230);
            this.OKbutton.Name = "OKbutton";
            this.OKbutton.Size = new System.Drawing.Size(75, 23);
            this.OKbutton.TabIndex = 0;
            this.OKbutton.Text = "Test";
            this.OKbutton.UseVisualStyleBackColor = true;
            this.OKbutton.Click += new System.EventHandler(this.OKbutton_Click);
            // 
            // DLLVersionLabel
            // 
            this.DLLVersionLabel.AutoSize = true;
            this.DLLVersionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DLLVersionLabel.Location = new System.Drawing.Point(33, 37);
            this.DLLVersionLabel.Name = "DLLVersionLabel";
            this.DLLVersionLabel.Size = new System.Drawing.Size(222, 13);
            this.DLLVersionLabel.TabIndex = 1;
            this.DLLVersionLabel.Text = "FT2232/FT4232 JTAG DLL Version = ";
            // 
            // PassFailureStatusStrip
            // 
            this.PassFailureStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PassFailStatusLabel,
            this.PassFailResultsStatusLabel});
            this.PassFailureStatusStrip.Location = new System.Drawing.Point(0, 278);
            this.PassFailureStatusStrip.Name = "PassFailureStatusStrip";
            this.PassFailureStatusStrip.Size = new System.Drawing.Size(665, 22);
            this.PassFailureStatusStrip.TabIndex = 3;
            // 
            // PassFailStatusLabel
            // 
            this.PassFailStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.PassFailStatusLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.PassFailStatusLabel.Name = "PassFailStatusLabel";
            this.PassFailStatusLabel.Size = new System.Drawing.Size(121, 17);
            this.PassFailStatusLabel.Text = "Pass/Failure Status";
            // 
            // PassFailResultsStatusLabel
            // 
            this.PassFailResultsStatusLabel.Name = "PassFailResultsStatusLabel";
            this.PassFailResultsStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // ChannelComboBox
            // 
            this.ChannelComboBox.FormattingEnabled = true;
            this.ChannelComboBox.Location = new System.Drawing.Point(583, 34);
            this.ChannelComboBox.Name = "ChannelComboBox";
            this.ChannelComboBox.Size = new System.Drawing.Size(47, 21);
            this.ChannelComboBox.TabIndex = 4;
            // 
            // ChannelLabel
            // 
            this.ChannelLabel.AutoSize = true;
            this.ChannelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChannelLabel.Location = new System.Drawing.Point(524, 37);
            this.ChannelLabel.Name = "ChannelLabel";
            this.ChannelLabel.Size = new System.Drawing.Size(53, 13);
            this.ChannelLabel.TabIndex = 5;
            this.ChannelLabel.Text = "Channel";
            // 
            // DeviceNameLabel
            // 
            this.DeviceNameLabel.AutoSize = true;
            this.DeviceNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeviceNameLabel.Location = new System.Drawing.Point(33, 74);
            this.DeviceNameLabel.Name = "DeviceNameLabel";
            this.DeviceNameLabel.Size = new System.Drawing.Size(98, 13);
            this.DeviceNameLabel.TabIndex = 6;
            this.DeviceNameLabel.Text = "Device Name = ";
            // 
            // MainMenuStrip
            // 
            this.MainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OptionsMenuItem});
            this.MainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MainMenuStrip.Name = "MainMenuStrip";
            this.MainMenuStrip.Size = new System.Drawing.Size(665, 24);
            this.MainMenuStrip.TabIndex = 8;
            // 
            // OptionsMenuItem
            // 
            this.OptionsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CommandSequenceMenuItem,
            this.toolStripSeparator1,
            this.ExitMenuItem});
            this.OptionsMenuItem.Name = "OptionsMenuItem";
            this.OptionsMenuItem.Size = new System.Drawing.Size(56, 20);
            this.OptionsMenuItem.Text = "Options";
            // 
            // CommandSequenceMenuItem
            // 
            this.CommandSequenceMenuItem.Checked = true;
            this.CommandSequenceMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CommandSequenceMenuItem.Name = "CommandSequenceMenuItem";
            this.CommandSequenceMenuItem.Size = new System.Drawing.Size(171, 22);
            this.CommandSequenceMenuItem.Text = "Cmd Seq";
            this.CommandSequenceMenuItem.Click += new System.EventHandler(this.CommandSequenceMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(168, 6);
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.E)));
            this.ExitMenuItem.Size = new System.Drawing.Size(171, 22);
            this.ExitMenuItem.Text = "Exit";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // HiSpeedDeviceTypeGroupBox
            // 
            this.HiSpeedDeviceTypeGroupBox.Controls.Add(this.FT4232HRadioButton);
            this.HiSpeedDeviceTypeGroupBox.Controls.Add(this.FT2232HRadioButton);
            this.HiSpeedDeviceTypeGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HiSpeedDeviceTypeGroupBox.Location = new System.Drawing.Point(36, 100);
            this.HiSpeedDeviceTypeGroupBox.Name = "HiSpeedDeviceTypeGroupBox";
            this.HiSpeedDeviceTypeGroupBox.Size = new System.Drawing.Size(173, 55);
            this.HiSpeedDeviceTypeGroupBox.TabIndex = 9;
            this.HiSpeedDeviceTypeGroupBox.TabStop = false;
            this.HiSpeedDeviceTypeGroupBox.Text = "Hi Speed Device Type";
            // 
            // FT4232HRadioButton
            // 
            this.FT4232HRadioButton.AutoSize = true;
            this.FT4232HRadioButton.Enabled = false;
            this.FT4232HRadioButton.Location = new System.Drawing.Point(90, 20);
            this.FT4232HRadioButton.Name = "FT4232HRadioButton";
            this.FT4232HRadioButton.Size = new System.Drawing.Size(77, 17);
            this.FT4232HRadioButton.TabIndex = 1;
            this.FT4232HRadioButton.TabStop = true;
            this.FT4232HRadioButton.Text = "FT4232H";
            this.FT4232HRadioButton.UseVisualStyleBackColor = true;
            // 
            // FT2232HRadioButton
            // 
            this.FT2232HRadioButton.AutoSize = true;
            this.FT2232HRadioButton.Enabled = false;
            this.FT2232HRadioButton.Location = new System.Drawing.Point(7, 20);
            this.FT2232HRadioButton.Name = "FT2232HRadioButton";
            this.FT2232HRadioButton.Size = new System.Drawing.Size(77, 17);
            this.FT2232HRadioButton.TabIndex = 0;
            this.FT2232HRadioButton.TabStop = true;
            this.FT2232HRadioButton.Text = "FT2232H";
            this.FT2232HRadioButton.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 300);
            this.Controls.Add(this.HiSpeedDeviceTypeGroupBox);
            this.Controls.Add(this.DeviceNameLabel);
            this.Controls.Add(this.ChannelLabel);
            this.Controls.Add(this.ChannelComboBox);
            this.Controls.Add(this.PassFailureStatusStrip);
            this.Controls.Add(this.MainMenuStrip);
            this.Controls.Add(this.DLLVersionLabel);
            this.Controls.Add(this.OKbutton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main Form";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.PassFailureStatusStrip.ResumeLayout(false);
            this.PassFailureStatusStrip.PerformLayout();
            this.MainMenuStrip.ResumeLayout(false);
            this.MainMenuStrip.PerformLayout();
            this.HiSpeedDeviceTypeGroupBox.ResumeLayout(false);
            this.HiSpeedDeviceTypeGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OKbutton;
        private System.Windows.Forms.Label DLLVersionLabel;
        private System.Windows.Forms.StatusStrip PassFailureStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel PassFailStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel PassFailResultsStatusLabel;
        private System.Windows.Forms.ComboBox ChannelComboBox;
        private System.Windows.Forms.Label ChannelLabel;
        private System.Windows.Forms.Label DeviceNameLabel;
        private System.Windows.Forms.MenuStrip MainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem OptionsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CommandSequenceMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
        private System.Windows.Forms.GroupBox HiSpeedDeviceTypeGroupBox;
        private System.Windows.Forms.RadioButton FT2232HRadioButton;
        private System.Windows.Forms.RadioButton FT4232HRadioButton;

    }
}

