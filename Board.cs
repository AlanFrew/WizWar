﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Library;
using System.Windows.Forms;
using WizWar1.Properties;

namespace WizWar1
{

class Board {
    //Apparently coordinate system starts at the center of the top left square, so that wizards are on integers and walls are x.5
    //public class NeighborPair {
    //    public NeighborPair(Square tFirst, Square tSecond) {
    //        first = tFirst;
    //        second = tSecond;
    //    }

    //    public NeighborPair() {
    //    }

    //    public Square first;
    //    public Square second;
    //}

    private Square[][] board;
    public Square[] SquaresByIndex;
    public Dictionary<DoublePoint, IWall> Walls = new Dictionary<DoublePoint, IWall>();

    private int columns;
    public int Width {
        get {
            return columns;
        }
    }

    private int rows;
    public int Height {
        get {
            return rows;
        }
    }

    public int NSquares { get; set; }

    int index = 0;
    public Board(int tPlayers) {
        switch (tPlayers) {
        case 2:
            columns = 5;
            rows = 10;
            NSquares = columns * rows;
            SquaresByIndex = new Square[NSquares];
            board = new Square[columns][];
            for (int i = 0; i < columns; ++i) {
                board[i] = new Square[rows];
                for (int j = 0; j < rows; ++j)
                {
                    var temp = new Square(i, j) {ID = index};
                    board[i][j] = temp;
                    SquaresByIndex[index] = temp;
                    index++;
                }
            }
        break;
        }
    }

    public Square At(int tX, int tY) {
        return board[LibraryFunctions.IndexFixer(tX, columns)][LibraryFunctions.IndexFixer(tY, rows)];
    }

    public Square At(double tX, double tY) {
        return At((int)tX, (int)tY);
    }

    public Square AtNoWrap(double tX, double tY) {
        try {
            return board[(int)tX][(int)tY];
        }
        catch (IndexOutOfRangeException) {
            return null;
        }
    }

    public IWall LookForWall(double tX, double tY) {
        try {
            return Walls[new DoublePoint(tX, tY)];
        }
        catch (KeyNotFoundException) {
        }

        return null;
    }

    public IWall LookForWall(Square tFirst, Square tSecond) {
        foreach (Direction d in Enum.GetValues(typeof(Direction))) {
            if (tFirst.GetNeighbor(d) == tSecond) {
                //if (tFirst.X * tFirst.Y == 0 || tSecond.X - tFirst.X + tSecond.Y - tFirst.Y == -1)  {
                if ((tFirst.X == 0 && tSecond.X > tFirst.X + 1) || 
                    (tFirst.Y == 0 && tSecond.Y > tFirst.Y + 1) || 
                    tSecond.X - tFirst.X + tSecond.Y - tFirst.Y == -1) {

                    LibraryFunctions.Swap(ref tFirst, ref tSecond);
                }

                double xCoordinate = tFirst.X;
                double yCoordinate = tFirst.Y;

                if (tFirst.X == tSecond.X) {
                    yCoordinate += 0.5;
                }
                else {
                    xCoordinate += 0.5;
                }

                try {
                    return Walls[new DoublePoint(xCoordinate, yCoordinate)];
                }
                catch (KeyNotFoundException) {
                    return null;
                }
            }
        }

        return null;
    }

    public void AddWall(IWall tWall) {
        Walls.Add(new DoublePoint(tWall.X, tWall.Y), tWall);

        if (tWall.IsVertical()) {
            tWall.FirstNeighbor = At(tWall.X - 0.5, tWall.Y);
            tWall.SecondNeighbor = At(tWall.X + 0.5, tWall.Y);
        }
        else {
            tWall.FirstNeighbor = At(tWall.X, tWall.Y - 0.5);
            tWall.SecondNeighbor = At(tWall.X, tWall.Y + 0.5);
        }

        tWall.ArrangeNeighbors();
    }

    public void RemoveWall(IWall tWall) {
        try {
            Walls.Remove(new DoublePoint(tWall.X, tWall.Y));
        }
        catch (KeyNotFoundException) {
        }
    }

    public void ReadMapFile() {
        var m = MapReadMode.Doors;
        Wizard owner = null;
        var sr = new StreamReader(Settings.Default.MapFile);

        while(sr.EndOfStream == false) {
            String wholeLine = sr.ReadLine();
            if (wholeLine.Contains("Walls:")) {
                m = MapReadMode.Walls;
                continue;
            }
            else if (wholeLine.Contains("Doors:")) {
                m = MapReadMode.Doors;
                continue;
            }
            else if (wholeLine.Equals("")) {
                continue;
            }
            else if (wholeLine.Contains("Treasures:")) {
                m = MapReadMode.Treasures;
                continue;
            }
            else if (wholeLine.Contains("Bases:")) {
                m = MapReadMode.Bases;
                continue;
            }

            if (m == MapReadMode.Walls || m == MapReadMode.Doors) {
                string[] wallPoints = wholeLine.Split(' ');
                int x1;
                Int32.TryParse(wallPoints[0], out x1);
                int y1;
                Int32.TryParse(wallPoints[1], out y1);
                int x2;
                Int32.TryParse(wallPoints[2], out x2);
                int y2;
                Int32.TryParse(wallPoints[3], out y2);
                IWall wall;

                if (m == MapReadMode.Walls) {
                    wall = new TrueWall();
                }
                else {
                    wall = new Door();
                }

                wall.FirstNeighbor = board[x1][y1];
                wall.SecondNeighbor = board[x2][y2];
                wall.ArrangeNeighbors();

                if (wall.FirstNeighbor.X == wall.SecondNeighbor.X) {
                    wall.X = wall.FirstNeighbor.X;
                }
                else {
                    wall.X = wall.FirstNeighbor.X + 0.5;
                }
            
                if (wall.FirstNeighbor.Y == wall.SecondNeighbor.Y) {
                    wall.Y = wall.FirstNeighbor.Y;
                }
                else {
                    wall.Y = wall.FirstNeighbor.Y + 0.5;
                }

                Walls.Add(new DoublePoint(wall.X, wall.Y), wall);
            }
            else if (m == MapReadMode.Treasures) {
                if (wholeLine == "Blue") {
                    owner = GameState.Wizards[0];
                }
                else if (wholeLine == "Red") {
                    owner = GameState.Wizards[1];
                }
                else if (wholeLine == "Green") {
                    owner = GameState.Wizards[2];
                }
                else if (wholeLine == "Yellow") {
                    owner = GameState.Wizards[3];
                }
                else {
                    var treasurePoints = new String[2];
                    treasurePoints = wholeLine.Split(' ');
                    int x;
                    int y;
                    Int32.TryParse(treasurePoints[0], out x);
                    Int32.TryParse(treasurePoints[1], out y);

                    var tempTreasure = new Treasure(owner);
                    tempTreasure.Location = GameState.BoardRef.At(x, y);
                    tempTreasure.Location.AddLocatable(tempTreasure);
                }
            }
            else if (m == MapReadMode.Bases) {
                if (wholeLine == "Blue") {
                    owner = GameState.Wizards[0];
                }
                else if (wholeLine == "Red") {
                    owner = GameState.Wizards[1];
                }
                else if (wholeLine == "Green") {
                    owner = GameState.Wizards[2];
                }
                else if (wholeLine == "Yellow") {
                    owner = GameState.Wizards[3];
                }
            }
            else {
                string[] basePoints = wholeLine.Split(' ');
                int x;
                int y;
                Int32.TryParse(basePoints[0], out x);
                Int32.TryParse(basePoints[1], out y);

                owner.HomeSquare = GameState.BoardRef.At(x, y);
            }
        }
        sr.Close();
    }

    //private class WallComparer : IComparer<IWall> {
    //    public int Compare(IWall wall1, IWall wall2) {
    //        wall1.ArrangeNeighbors();
    //        wall2.ArrangeNeighbors();
    //        return (int)(wall1.FirstNeighbor.Y - wall2.FirstNeighbor.Y);
    //    }
    //}

    //arguments are in expanded coordinates
    //this function tests whether a specific line of sight ends on the target
    public bool TestDirectLoS(double x1, double y1, double x2, double y2) {
        double ychange = (y2 - y1);
        double xchange  = (x2 - x1);
        double slope = ychange / xchange;
        bool xpositive = (xchange > 0);
        bool ypositive = (ychange > 0);
        double testx;
        double testy;

        for (testx = x1; testx < x2; testx++) {
            if (xpositive) {
                double testXIntercept = slope * (testx - x1) + y1;

                //Console.WriteLine("checking key " + Math.Ceiling(testx) + " " + Math.Ceiling(testx + 1) + " " + Math.Floor(testXIntercept) + " " + Math.Ceiling(testXIntercept));
                //if (vertical_walls.ContainsKey("" + Math.Ceiling(testx) + " " + Math.Ceiling(testx + 1) + " " + Math.Floor(testXIntercept) + " " + Math.Ceiling(testXIntercept))) {
                //    //don't forget to test IsPassable
                //    return false;
                //}

                if (Walls.ContainsKey(new DoublePoint(testx + 0.5, testXIntercept))) {
                    return false;
                }
            }
        }

        for (testy = y1; testy < y2; testy++) {
            if (ypositive) {
                double testYIntercept = (testy - y1) / slope + x1;

                if (Walls.ContainsKey(new DoublePoint(testYIntercept + 0.5, testy))) {
                    return false;
                }
            }
        }

        return true;
    }

    public bool TestLoSNew(double xChange, double yChange, UIControl casterUI) {
        var testString = new StringBuilder();
        double yStart = GameState.ActivePlayer.Y;
        double xStart = GameState.ActivePlayer.X;
        double xCurrent = xStart;
        double yCurrent = yStart;
        Square testSquare;

        if (xChange != 0) {
            xCurrent += xChange / (2 * Math.Abs(xChange));
        }
        
        if (yChange != 0) {
            yCurrent += yChange / (2 * Math.Abs(yChange));
        }

        while (Math.Abs(xCurrent - xStart) < Math.Abs(xChange)) {
            if (yChange != 0) {
                yCurrent = yStart + yChange / xChange * (xCurrent - xStart);
                //yCurrent = Library.IndexFixer(yCurrent, this.rows);
            }

            //this structure not duplicated for other axis
            testString.Append("Xtester: " + xCurrent + ' ' + yCurrent + '\n');
            IWall w = LookForWall(xCurrent, Math.Round(yCurrent));
            if (w != null) {
                //if (Math.Abs(xCurrent - xStart + xChange) <= 1 && casterUI.SpellToCast.IsValidSpellTargetType(TargetTypes.Wall)) {
                if (w.X == casterUI.myBoard.SelectedWall.X && w.Y == casterUI.myBoard.SelectedWall.Y) {
                    return true;
                }
                else {
                    MessageBox.Show(testString.ToString());
                    return false;
                }
            }

            //check for obstructions
            if (xChange > 0) {
                testSquare = GameState.BoardRef.At(Math.Floor(xCurrent), Math.Round(yCurrent));
            }
            else if (xChange < 0) {
                testSquare = GameState.BoardRef.At(Math.Ceiling(xCurrent), Math.Round(yCurrent));
            }
            else {
                testSquare = GameState.BoardRef.At(xCurrent, Math.Round(yCurrent));
            }

            foreach (Creation c in testSquare.creationsHere) {
                if (c is Obstruction) {
                    return false;
                }
            }

            xCurrent += xChange / Math.Abs(xChange);
            //xCurrent = Library.IndexFixer(xCurrent, this.columns);
        }
        //check for obstructions one last time
        //don't check for obstructions in last square! Handle in targetchooser??
        //if (xChange > 0) {
        //    testSquare = GameState.BoardRef.At(Math.Floor(xCurrent), Math.Round(yCurrent));
        //}
        //else if (xChange < 0) {
        //    testSquare = GameState.BoardRef.At(Math.Ceiling(xCurrent), Math.Round(yCurrent));
        //}
        //else {
        //    testSquare = GameState.BoardRef.At(xCurrent, Math.Round(yCurrent));
        //}

        //foreach (Creation c in testSquare.creationsHere) {
        //    if (c is Obstruction) {
        //        return false;
        //    }
        //}

        //repeat for other axis

        xCurrent = xStart;
        if (xChange != 0) {
            xCurrent += xChange / (2 * Math.Abs(xChange));
        }
        yCurrent = yStart;
        if (yChange != 0) {
            yCurrent += yChange / (2 * Math.Abs(yChange));
        }

        while (Math.Abs(yCurrent - yStart) < Math.Abs(yChange)) {
            if (xChange != 0) {
                xCurrent = xStart + xChange / yChange * (yCurrent - yStart);
                //xCurrent = Library.IndexFixer(xCurrent, this.columns);
            }

            testString.Append("Ytester: " + xCurrent + ' ' + yCurrent + '\n');
            if (LookForWall(Math.Round(xCurrent), yCurrent) != null) {
                MessageBox.Show(testString.ToString());
                return false;
            }

            //check for obstructions
            if (yChange > 0) {
                testSquare = GameState.BoardRef.At(Math.Round(xCurrent), Math.Floor(yCurrent));
            }
            else if (yChange < 0) {
                testSquare = GameState.BoardRef.At(Math.Round(xCurrent), Math.Ceiling(yCurrent));
            }
            else {
                testSquare = GameState.BoardRef.At(Math.Round(xCurrent), yCurrent);
            }

            foreach (Creation c in testSquare.creationsHere) {
                if (c is Obstruction) {
                    return false;
                }
            }

            yCurrent += yChange / Math.Abs(yChange);
            //yCurrent = Library.IndexFixer(yCurrent, this.rows);
        }
        //check for obstructions in final square...or not (handle in targetchooser?)
        //if (yChange > 0) {
        //    testSquare = GameState.BoardRef.At(Math.Round(xCurrent), Math.Floor(yCurrent));
        //}
        //else if (xChange < 0) {
        //    testSquare = GameState.BoardRef.At(Math.Round(xCurrent), Math.Ceiling(yCurrent));
        //}
        //else {
        //    testSquare = GameState.BoardRef.At(Math.Round(xCurrent), yCurrent);
        //}

        //foreach (Creation c in testSquare.creationsHere) {
        //    if (c is Obstruction) {
        //        return false;
        //    }
        //}
        //MessageBox.Show(testString.ToString());
        return true;
    }

    public bool TestLoSToWall(double xChange, double yChange, WallSpace targetWall) {
        var testString = new StringBuilder();
        double yStart = GameState.ActivePlayer.Y;
        double xStart = GameState.ActivePlayer.X;
        double xCurrent = xStart;
        double yCurrent = yStart;
        Square testSquare;

        if (xChange != 0) {
            xCurrent += xChange / (2 * Math.Abs(xChange));
        }

        if (yChange != 0) {
            yCurrent += yChange / (2 * Math.Abs(yChange));
        }

        while (Math.Abs(xCurrent - xStart) < Math.Abs(xChange)) {
            if (yChange != 0) {
                yCurrent = yStart + yChange / xChange * (xCurrent - xStart);
                //yCurrent = Library.IndexFixer(yCurrent, this.rows);
            }

            //see if there is a wall at the current testing location, and whether it's the target
            testString.Append("Xtester: " + xCurrent + ' ' + yCurrent + '\n');
            IWall w = LookForWall(xCurrent, Math.Round(yCurrent));
            if (w != null) {
                if (w.X == targetWall.X && w.Y == targetWall.Y) {
                    return true;
                }
                else {
                    MessageBox.Show(testString.ToString());
                    return false;
                }
            }

            //check for obstructions
            if (xChange > 0) {
                testSquare = GameState.BoardRef.At(Math.Floor(xCurrent), Math.Round(yCurrent));
            }
            else if (xChange < 0) {
                testSquare = GameState.BoardRef.At(Math.Ceiling(xCurrent), Math.Round(yCurrent));
            }
            else {
                testSquare = GameState.BoardRef.At(xCurrent, Math.Round(yCurrent));
            }

            foreach (Creation c in testSquare.creationsHere) {
                if (c is Obstruction) {
                    return false;
                }
            }

            xCurrent += xChange / Math.Abs(xChange);
            //xCurrent = Library.IndexFixer(xCurrent, this.columns);
        }

        //repeat for other axis

        xCurrent = xStart;
        if (xChange != 0) {
            xCurrent += xChange / (2 * Math.Abs(xChange));
        }
        yCurrent = yStart;
        if (yChange != 0) {
            yCurrent += yChange / (2 * Math.Abs(yChange));
        }

        while (Math.Abs(yCurrent - yStart) < Math.Abs(yChange)) {
            if (xChange != 0) {
                xCurrent = xStart + xChange / yChange * (yCurrent - yStart);
                //xCurrent = Library.IndexFixer(xCurrent, this.columns);
            }

            testString.Append("Ytester: " + xCurrent + ' ' + yCurrent + '\n');
            IWall w = LookForWall(xCurrent, Math.Round(yCurrent));
            if (w != null) {
                if (w.X == targetWall.X && w.Y == targetWall.Y) {
                    return true;
                }
                else {
                    MessageBox.Show(testString.ToString());
                    return false;
                }
            }

            //check for obstructions
            if (yChange > 0) {
                testSquare = GameState.BoardRef.At(Math.Round(xCurrent), Math.Floor(yCurrent));
            }
            else if (yChange < 0) {
                testSquare = GameState.BoardRef.At(Math.Round(xCurrent), Math.Ceiling(yCurrent));
            }
            else {
                testSquare = GameState.BoardRef.At(Math.Round(xCurrent), yCurrent);
            }

            foreach (Creation c in testSquare.creationsHere) {
                if (c is Obstruction) {
                    return false;
                }
            }

            yCurrent += yChange / Math.Abs(yChange);
            //yCurrent = Library.IndexFixer(yCurrent, this.rows);
        }
        return true;
    }

    public bool IsAdjacent(ILocatable first, ILocatable second) {
        if (first is Square && second is Square) {
            if (Enum.GetValues(typeof (Direction)).Cast<Direction>().Any(d => (first as Square).GetNeighbor(d) == second)) {
                return true;
            }
        }

        if (first is Square && second is IWall) {
            var temp = first;
            first = second;
            second = temp;
        }

        if (first is IWall) {
            if (second is Square) {
                return (first as IWall).FirstNeighbor == second || (first as IWall).SecondNeighbor == second;
            }
        }

        return false;
    }
}
}
