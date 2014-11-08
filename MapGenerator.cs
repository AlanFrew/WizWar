using System.IO;

namespace WizWar1 {
	class MapGenerator {
		public void ParseMapFile() {
			var myFile = new FileInfo("map2.txt");
			StreamWriter sw = myFile.CreateText();

			sw.WriteLine("Walls:");
			//horizontal walls
			for (int i = 0; i < 5; ++i) {
				sw.WriteLine(i.ToString() + ' ' + "9" + ' ' + i + ' ' + "0");
			}

			//vertical walls
			for (int j = 0; j < 10; ++j) {
				sw.WriteLine("4" + ' ' + j + ' ' + "0" + ' ' + j);
			}

			//treasure
			sw.WriteLine("Treasures:");
			sw.WriteLine("Blue");
			sw.WriteLine("2 2");
			sw.WriteLine("2 7");
			sw.Close();
		}
	}
}