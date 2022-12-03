using System.Windows.Forms;
using System;
using System.IO;
using System.Collections.Generic;

namespace WordDisplayer
{
	public struct FileInfo
	{
		public string word;
		public string imagePath;
	}

	public partial class MainForm : Form
	{
		FileInfo[] _InfoInCurrentFile = { };

		Random _RandomGenerator = new Random();
		int _LastNumberPicked = -1;

		public MainForm()
		{
			InitializeComponent();

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
			ChangeLabel(_InfoInCurrentFile.Length == 0 ? $"No words in file{button.Text}..." : null );
		}

		private void ChangeLabel(string forceLabel = null)
		{
			if ( forceLabel != null )
			{
				this.WordText.Text = forceLabel;
				return;
			}
			int randomNumber = _RandomGenerator.Next(_InfoInCurrentFile.Length);
			
			for (int i = 0; randomNumber == _LastNumberPicked; i++ )
			{
				if (i == 100) break;
				randomNumber = _RandomGenerator.Next(_InfoInCurrentFile.Length);
			}
			_LastNumberPicked = randomNumber;
			

			if (_InfoInCurrentFile.Length > 0 )
			{
				this.WordText.Text = _InfoInCurrentFile[randomNumber].word;
				ChangeImage(_InfoInCurrentFile[randomNumber].imagePath);
			}
			else
			{
				this.WordText.Text = "No words in this file.";
				ImageBox.Visible = false;
			}
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
			ChangeLabel();
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
	}

	class ConfigurationUtils
	{
		public const string CONF_FOLDER_NAME = "Configuration";
		public const string IMAGE_FOLDER = "Images";
		public const char COMMENT = '#';
		public const char WORD_IMAGE_SEPERATOR = ',';
		public const string DONT_PARSE = "#DONT_PARSE";

		public static FileInfo[] ReadConfigurationFile( string FilePath )
		{
			string imageFolder = IMAGE_FOLDER;
			List<FileInfo> allInfo = new List<FileInfo>();
			if (File.Exists(FilePath))
			{
				if(imageFolder.LastIndexOf('/') == -1 && imageFolder.LastIndexOf('\\') == -1 )
				{
					imageFolder += '/';
				}

				foreach (string line in File.ReadLines(FilePath))
				{
					line.Trim();
					if ( line != "" && line[0] != COMMENT ) // empty or comment
					{
						FileInfo fileInfo = new FileInfo();
						int indexOfComma = line.LastIndexOf(WORD_IMAGE_SEPERATOR);
						if( indexOfComma != -1 ) // Do we have an image?
						{
							fileInfo.word = line.Substring(0, indexOfComma);
							fileInfo.imagePath = imageFolder + line.Substring(indexOfComma + 1);
							fileInfo.imagePath = fileInfo.imagePath.Trim();
							fileInfo.imagePath = fileInfo.imagePath.Replace( " ", "" );
						}
						else
						{
							fileInfo.word = line;
							fileInfo.imagePath = "";
						}
						allInfo.Add(fileInfo);
					}
				}
			}

			if (allInfo.Count == 0 )
			{
				MessageBox.Show( $"{FilePath} is empty..." );
			}
			return allInfo.ToArray();
		}

		public static bool CleanFile( string filePath )
		{
			if(!filePath.Contains( ".txt" ) )
			{
				filePath += ".txt";
			}

			if(!File.Exists(filePath))
			{
				MessageBox.Show($"File: {filePath} does not seem to exist.", "ERROR",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}

			string[] allLines = File.ReadAllLines(filePath); // words in line are seperated by \t or ' '

			if (allLines.Length > 0 && allLines[0].Contains(DONT_PARSE))
			{
				MessageBox.Show($"{filePath} has the {DONT_PARSE} flag.", $"{DONT_PARSE}", 
					MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}

			List<string> parsedLines = new List<string>();
			foreach(string line in allLines)
			{
				if (line == "") continue;

				line.Trim();
				string tempString = "";
				for( int j = 0; j < line.Length; j++ )
				{
					if( line[j] == '\t' || line[j] == ' ' || j == line.Length - 1 )
					{
						if( j == line.Length - 1)
						{
							tempString += line[j];
						}
						// Remove all numbers
						float outFloat;
						if(float.TryParse( tempString, out outFloat ) )
						{
							tempString = "";
							continue;
						}
						parsedLines.Add(tempString);
						tempString = "";
					}
					else
					{
						tempString += line[j];
					}
				}
			}

			// Clear file
			File.WriteAllText(filePath, string.Empty);
			// Write to file with new parsed lines;
			StreamWriter streamWriter = File.AppendText(filePath);
			foreach( string line in parsedLines )
			{
				if( line != string.Empty )
				{
					streamWriter.WriteLine(line);
				}
			}
			streamWriter.Close();

			return true;
		}
	}
}
