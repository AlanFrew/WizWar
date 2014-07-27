using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WizWar1 {

class MapGenerator {
    public void floob() {
        FileInfo myFile = new FileInfo("map1.txt");
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
        sw.Close();
    }

}
}
