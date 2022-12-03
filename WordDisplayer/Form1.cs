using System.Windows.Forms;
using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;

namespace WordDisplayer
{
	public struct FileInfo
	{
		public string word;
		public string imagePath;
	}

	public struct ColorConfig
	{
		public ColorConfig(Control _control, Color _light, Color _dark)
		{
			control = _control;
			light = _light;
			dark = _dark;
		}
		public Control control;
		public System.Drawing.Color light;
		public System.Drawing.Color dark;
	}

	public partial class MainForm : Form
	{
		FileInfo[] _InfoInCurrentFile = { };

		Random _RandomGenerator = new Random();
		int _LastNumberPicked = -1;

		List<ColorConfig> ColorConfigs = null;

		public MainForm()
		{
			InitializeComponent();

			// Color theme configs
			ColorConfigs = new List<ColorConfig>();
			ColorConfigs.Add(new ColorConfig(this, SystemColors.ActiveCaption, SystemColors.ControlDarkDark));
			ColorConfigs.Add(new ColorConfig(this.NextButton, SystemColors.ActiveBorder, SystemColors.ControlDarkDark));
			ColorConfigs.Add(new ColorConfig(this.ConfigurationFileList, SystemColors.ActiveCaption, SystemColors.ControlDarkDark));
			ColorConfigs.Add(new ColorConfig(this.ParseSearchBox, SystemColors.Window, SystemColors.ControlDark));
			for (int i = 0; i < ColorConfigs.Count; i++)
			{
				ColorConfigs[i].control.BackColor = ColorConfigs[i].dark;
			}

			//this.FormBorderStyle = FormBorderStyle.FixedSingle;
			//this.MaximizeBox = false;

			ImageBox.SizeMode = PictureBoxSizeMode.StretchImage;

			if(!Directory.Exists( ConfigurationUtils.IMAGE_FOLDER ) )
			{
				Directory.CreateDirectory(ConfigurationUtils.IMAGE_FOLDER);
			}
			if ( !Directory.Exists( ConfigurationUtils.CONF_FOLDER_NAME ) )
			{
				Directory.CreateDirectory(ConfigurationUtils.CONF_FOLDER_NAME);
				MessageBox.Show( $"Directory: {ConfigurationUtils.CONF_FOLDER_NAME} did not exist. " +
					"It has been created.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
				ChangeLabel( $"No files in directory: {ConfigurationUtils.CONF_FOLDER_NAME}" );
				return;
			}
			string[] allFiles = Directory.GetFiles(ConfigurationUtils.CONF_FOLDER_NAME);
			for(int i = 0; i < allFiles.Length; i++)
			{
				Button button = new Button();
				button.Text = allFiles[i];
				button.AutoSize = true;
				button.Click += FileButton_Click;
				ConfigurationFileList.Controls.Add(button);
			}

			ChangeLabel( allFiles.Length == 0 ? 
				$"No files in directory: {ConfigurationUtils.CONF_FOLDER_NAME}" : "Pick a file to load.");
		}

		private void FileButton_Click(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			_InfoInCurrentFile = ConfigurationUtils.ReadConfigurationFile( button.Text );
			_LastNumberPicked = -1;
			ChangeLabel();
		}

		private void ChangeLabel(string forceLabel = null)
		{
			if ( forceLabel != null )
			{
				this.WordText.Text = forceLabel;
				return;
			}
			if( _InfoInCurrentFile.Length == 0 )
			{
				this.WordText.Text = "No words in this file.";
				ImageBox.Visible = false;
				return;
			}

			int randomNumber = _RandomGenerator.Next(_InfoInCurrentFile.Length);
			
			for (int i = 0; randomNumber == _LastNumberPicked; i++ )
			{
				if (i == 100) break;
				randomNumber = _RandomGenerator.Next(_InfoInCurrentFile.Length);
			}
			_LastNumberPicked = randomNumber;
			
			this.WordText.Text = _InfoInCurrentFile[randomNumber].word;
			ChangeImage(_InfoInCurrentFile[randomNumber].imagePath);
		}

		private void ChangeImage( string imagePath )
		{
			if( imagePath != "" && File.Exists(imagePath) )
			{
				ImageBox.Visible = true;
				ImageBox.Image = System.Drawing.Image.FromFile(imagePath);
			}
			else
			{
				ImageBox.Visible = false;
			}
		}

		private void NextButton_MouseClick(object sender, MouseEventArgs e)
		{
			if(ConfigurationFileList.Controls.Count > 0 )
			{
				ChangeLabel();
			}
		}

		private void ParseButton_MouseClick(object sender, MouseEventArgs e)
		{
			string filePath = ConfigurationUtils.CONF_FOLDER_NAME + '/' +  ParseSearchBox.Text;
			if( ConfigurationUtils.CleanFile(filePath) )
			{
				MessageBox.Show($"{filePath} has been succefully parsed.", "Success" );
			}
		}

		private void ParseSearchBox_KeyDown(object sender, KeyEventArgs e)
		{
			if( e.KeyCode == Keys.Enter )
			{
				ParseButton_MouseClick(null, null);
			}
		}

		private void ThemeToggleButon_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if ( checkBox.CheckState == CheckState.Checked )
			{
				for (int i = 0; i < ColorConfigs.Count; i++)
				{
					ColorConfigs[i].control.BackColor = ColorConfigs[i].light;
				}
			}
			else if( checkBox.CheckState == CheckState.Unchecked )
			{
				for (int i = 0; i < ColorConfigs.Count; i++)
				{
					ColorConfigs[i].control.BackColor = ColorConfigs[i].dark;
				}
			}
		}
	}
}
