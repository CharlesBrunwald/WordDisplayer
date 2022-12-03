
namespace WordDisplayer
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
			this.NextButton = new System.Windows.Forms.Button();
			this.WordText = new System.Windows.Forms.Label();
			this.ConfigurationFileList = new System.Windows.Forms.FlowLayoutPanel();
			this.ImageBox = new System.Windows.Forms.PictureBox();
			this.ParseButton = new System.Windows.Forms.Button();
			this.ParseSearchBox = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.ImageBox)).BeginInit();
			this.SuspendLayout();
			// 
			// NextButton
			// 
			this.NextButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.NextButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.NextButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.NextButton.Location = new System.Drawing.Point(386, 569);
			this.NextButton.Margin = new System.Windows.Forms.Padding(4);
			this.NextButton.Name = "NextButton";
			this.NextButton.Size = new System.Drawing.Size(613, 62);
			this.NextButton.TabIndex = 0;
			this.NextButton.Text = "Next";
			this.NextButton.UseVisualStyleBackColor = true;
			this.NextButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NextButton_MouseClick);
			// 
			// WordText
			// 
			this.WordText.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.WordText.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.WordText.Location = new System.Drawing.Point(284, 47);
			this.WordText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.WordText.Name = "WordText";
			this.WordText.Size = new System.Drawing.Size(800, 129);
			this.WordText.TabIndex = 1;
			this.WordText.Text = "WORD HERE";
			this.WordText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ConfigurationFileList
			// 
			this.ConfigurationFileList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ConfigurationFileList.AutoScroll = true;
			this.ConfigurationFileList.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.ConfigurationFileList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ConfigurationFileList.Location = new System.Drawing.Point(16, 260);
			this.ConfigurationFileList.Margin = new System.Windows.Forms.Padding(4);
			this.ConfigurationFileList.Name = "ConfigurationFileList";
			this.ConfigurationFileList.Size = new System.Drawing.Size(276, 370);
			this.ConfigurationFileList.TabIndex = 3;
			// 
			// ImageBox
			// 
			this.ImageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
			this.ImageBox.InitialImage = null;
			this.ImageBox.Location = new System.Drawing.Point(386, 237);
			this.ImageBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.ImageBox.Name = "ImageBox";
			this.ImageBox.Size = new System.Drawing.Size(613, 299);
			this.ImageBox.TabIndex = 4;
			this.ImageBox.TabStop = false;
			// 
			// ParseButton
			// 
			this.ParseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ParseButton.BackColor = System.Drawing.Color.Red;
			this.ParseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ParseButton.Location = new System.Drawing.Point(1129, 47);
			this.ParseButton.Name = "ParseButton";
			this.ParseButton.Size = new System.Drawing.Size(142, 61);
			this.ParseButton.TabIndex = 5;
			this.ParseButton.Text = "Parse File";
			this.ParseButton.UseVisualStyleBackColor = false;
			this.ParseButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ParseButton_MouseClick);
			// 
			// ParseSearchBox
			// 
			this.ParseSearchBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ParseSearchBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ParseSearchBox.Location = new System.Drawing.Point(1091, 11);
			this.ParseSearchBox.Name = "ParseSearchBox";
			this.ParseSearchBox.Size = new System.Drawing.Size(221, 30);
			this.ParseSearchBox.TabIndex = 6;
			this.ParseSearchBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ParseSearchBox_KeyDown);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.ClientSize = new System.Drawing.Size(1331, 665);
			this.Controls.Add(this.ParseSearchBox);
			this.Controls.Add(this.ParseButton);
			this.Controls.Add(this.ImageBox);
			this.Controls.Add(this.ConfigurationFileList);
			this.Controls.Add(this.WordText);
			this.Controls.Add(this.NextButton);
			this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.Name = "MainForm";
			this.Text = "Word Picker";
			((System.ComponentModel.ISupportInitialize)(this.ImageBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button NextButton;
		private System.Windows.Forms.Label WordText;
		private System.Windows.Forms.FlowLayoutPanel ConfigurationFileList;
		private System.Windows.Forms.PictureBox ImageBox;
		private System.Windows.Forms.Button ParseButton;
		private System.Windows.Forms.TextBox ParseSearchBox;
	}
}

