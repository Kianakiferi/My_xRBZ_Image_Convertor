using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System;
using xBRZNet;

namespace ConsoleApp1
{
	class Program
	{
		static void Main(string[] args)
		{
			DirectoryInfo folder = new DirectoryInfo(@"Images");
			string fullPath = folder.FullName;

			Console.WriteLine(fullPath);

			if (!Directory.Exists(fullPath + "/Input"))
			{
				Directory.CreateDirectory(fullPath + "/Input");
			}

			if (!Directory.Exists(fullPath + "/Output"))
			{
				Directory.CreateDirectory(fullPath + "/Output");
			}


			string path = @"Images\Input";
			List<string> fileNameList = new List<string>();
			


			AddFiles(path, fileNameList);
			foreach (string imageItem in fileNameList)
			{
				ConvertImage(imageItem);
			}

			System.Diagnostics.Process.Start("explorer.exe", fullPath + "/Output");
		}

		private static void ConvertImage(string imageItem, int scaleSize = 3)
		{
			string targetFile = imageItem;
			string fileName = Path.GetFileNameWithoutExtension(imageItem);
			string fileExtension = Path.GetExtension(imageItem);
			string fileParentPath = Path.GetDirectoryName(imageItem);
			string OutputfolderPath = fileParentPath.Replace("Input", "Output");

			if (!Directory.Exists(OutputfolderPath))
			{
				Directory.CreateDirectory(OutputfolderPath);
			}

			var inputImage = new Bitmap(targetFile);
			var scaledImage = new xBRZScaler().ScaleImage(inputImage, scaleSize);

			scaledImage.Save(OutputfolderPath + "\\" + fileName + fileExtension, ImageFormat.Png);

		}

		private static void AddFiles(string dir, List<string> fileName )
		{
			DirectoryInfo dirInfos = new DirectoryInfo(dir);
			FileInfo[] fileInfos = dirInfos.GetFiles("*.png");

			foreach (FileInfo fileItme in fileInfos)
			{
				fileName.Add(fileItme.FullName);
				Console.WriteLine(fileItme.FullName);
			}

			DirectoryInfo[] subdirInfos = dirInfos.GetDirectories();
			foreach(DirectoryInfo dirItem in subdirInfos)
			{
				AddFiles(dirItem.FullName, fileName);
			}
		}

	}
}
