using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WordDisplayer
{

	class ConfigurationUtils
	{
		public const string CONF_FOLDER_NAME = "Configuration";
		public const string IMAGE_FOLDER = "Images";
		public const char COMMENT = '#';
		public const char WORD_IMAGE_SEPERATOR = ',';
		public const string DONT_PARSE = "#DONT_PARSE";

		public static FileInfo[] ReadConfigurationFile(string FilePath)
		{
			string imageFolder = IMAGE_FOLDER;
			List<FileInfo> allInfo = new List<FileInfo>();
			if (File.Exists(FilePath))
			{
				if (imageFolder.LastIndexOf('/') == -1 && imageFolder.LastIndexOf('\\') == -1)
				{
					imageFolder += '/';
				}

				foreach (string line in File.ReadLines(FilePath))
				{
					line.Trim();
					if (line != "" && line[0] != COMMENT) // empty or comment
					{
						FileInfo fileInfo = new FileInfo();
						int indexOfComma = line.LastIndexOf(WORD_IMAGE_SEPERATOR);
						if (indexOfComma != -1) // Do we have an image?
						{
							fileInfo.word = line.Substring(0, indexOfComma);
							fileInfo.imagePath = imageFolder + line.Substring(indexOfComma + 1);
							fileInfo.imagePath = fileInfo.imagePath.Trim();
							fileInfo.imagePath = fileInfo.imagePath.Replace(" ", "");
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

			if (allInfo.Count == 0)
			{
				MessageBox.Show($"{FilePath} is empty...");
			}
			return allInfo.ToArray();
		}

		public static bool CleanFile(string filePath)
		{
			if (!filePath.Contains(".txt"))
			{
				filePath += ".txt";
			}

			if (!File.Exists(filePath))
			{
				MessageBox.Show($"File: {filePath} does not seem to exist.", "ERROR",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}

			string[] allLines = File.ReadAllLines(filePath); // words in line are seperated by \t or ' '

			if (allLines.Length > 0 && allLines[0].Contains(DONT_PARSE))
			{
				MessageBox.Show($"{filePath} has the {DONT_PARSE} flag.", $"{DONT_PARSE}",
					MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			List<string> parsedLines = new List<string>();
			foreach (string line in allLines)
			{
				if (line == "") continue;

				line.Trim();
				string tempString = "";
				for (int j = 0; j < line.Length; j++)
				{
					if (line[j] == '\t' || line[j] == ' ' || j == line.Length - 1)
					{
						if (j == line.Length - 1)
						{
							tempString += line[j];
						}
						// Remove all numbers
						float outFloat;
						if (float.TryParse(tempString, out outFloat))
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
			foreach (string line in parsedLines)
			{
				if (line != string.Empty)
				{
					streamWriter.WriteLine(line);
				}
			}
			streamWriter.Close();

			return true;
		}
	}
}