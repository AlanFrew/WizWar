#define DEBUG

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Library;

namespace WizWar1 {

public partial class Form1 : Form {
    //Image foo = Image.FromFile(@"C:\Users\Alan Frew\My Pictures\foo.bmp");
    Image square = Image.FromFile(@"C:\Users\Alan Frew\My Pictures\Arena\square_teal.bmp");
    Image blue_wizard = Image.FromFile(@"C:\Users\Alan Frew\My Pictures\Arena\model_battleaxe.bmp");
    Image red_wizard = Image.FromFile(@"C:\Users\Alan Frew\My Pictures\Arena\model_claws.bmp");
    public Image horiz_door = Image.FromFile(@"C:\Users\Alan Frew\My Pictures\Arena\door.bmp");
    public Image vert_door = Image.FromFile(@"C:\Users\Alan Frew\My Pictures\Arena\vertical_door.bmp");
    Image castingCursor = Image.FromFile(@"C:\Users\Alan Frew\My Pictures\Arena\ico_rod.bmp");
    Image thornbush = Image.FromFile(@"C:\Users\Alan Frew\My Pictures\Arena\thornbush.bmp");
    Image selectedSquare = Image.FromFile(@"C:\Users\Alan Frew\My Pictures\Arena\square_selected.bmp");
    Image selectedVertWall = Image.FromFile(@"C:\Users\Alan Frew\My Pictures\Arena\vert_wall_selected.bmp");
    Image selectedHorizWall = Image.FromFile(@"C:\Users\Alan Frew\My Pictures\Arena\horiz_wall_selected.bmp");
    Image treasure = Image.FromFile(@"C:\Users\Alan Frew\My Pictures\Arena\treasure.bmp");
    public Image horizIllusionWall = Image.FromFile(@"C:\Users\Alan Frew\My Pictures\Arena\horiz_illusion_wall.bmp");
    public Image vertIllusionWall = Image.FromFile(@"C:\Users\Alan Frew\My Pictures\Arena\vert_illusion_wall.bmp");

    private Point[] squares;
    private UIControl myUI;
    public WizardName myWizard;
    private Square OldSelectedSquare = null;
    private double OldWallX = Double.NaN;
    private double OldWallY = Double.NaN;
    private WallSpace selectedWall = null;
    internal WallSpace SelectedWall {
        get {
            return selectedWall;
        }
    }

    internal Form1(UIControl tMyUI)
    {
        InitializeComponent();

        myUI = tMyUI;
        GameState.SetMe(myUI);
        GameState.Form1Reference[(int)myWizard] = this; //must go after SetMe() is called
    }

    private void Form1_Load(object sender, EventArgs e) {
        squares = new Point[GameState.BoardRef.NSquares];
        for (int i = 0; i < GameState.BoardRef.NSquares; ++i) {
            squares[i] = new Point((int)GameState.BoardRef.squaresByIndex[i].X * square.Width,
                                   (int)GameState.BoardRef.squaresByIndex[i].Y * square.Height);
        }
    }

    private void button1_Click(object sender, EventArgs e) {
        MessageBox.Show("Blah");
    }

    protected override void OnPaint(PaintEventArgs e) {
        base.OnPaint(e);
        Graphics dc = e.Graphics;

        DrawSquaresItemsCreations(dc);
        DrawWallsDoors(dc);
        DrawWizards(dc);

        //The selection box is drawn in OnMouseMoved() due to syncronicity issues
        
    }

    private void DrawWizards(Graphics dc) {
        foreach (Wizard wizard in GameState.wizards) {
            Color tColor = Color.FromArgb(255, 255, 255);
            ImageAttributes tImgAtt = new ImageAttributes();
            tImgAtt.SetColorKey(tColor, tColor);
            Rectangle tRect = new Rectangle(squares[GameState.BoardRef.At(wizard.X, wizard.Y).ID], new Size(square.Width, square.Height));

            if (wizard.Name == "Blue Wizard") {
                dc.DrawImage(blue_wizard, tRect, 0, 0, square.Width, square.Height, GraphicsUnit.Pixel, tImgAtt);
            }
            else if (wizard.Name == "Red Wizard") {
                dc.DrawImage(red_wizard, tRect, 0, 0, square.Width, square.Height, GraphicsUnit.Pixel, tImgAtt);
            }
        }
    }

    private void DrawWallsDoors(Graphics dc) {
        Pen wallPen = new Pen(Color.Black, 4);
        int TimesToDraw = 1;

        foreach (KeyValuePair<DoublePoint, IWall> w in GameState.BoardRef.walls) {
            IWall wall = w.Value;
            if (wall.IsHorizontal()) {
                Point a = new DoublePoint(wall.FirstNeighbor.X * square.Width, (wall.FirstNeighbor.Y + 1) * square.Height).ToPoint();
                Point b = new DoublePoint((wall.FirstNeighbor.X + 1) * square.Width, (wall.FirstNeighbor.Y + 1) * square.Height).ToPoint();
                Point c = new DoublePoint(wall.SecondNeighbor.X * square.Width, wall.SecondNeighbor.Y * square.Height).ToPoint();
                Point d = new DoublePoint((wall.SecondNeighbor.X + 1) * square.Width, wall.SecondNeighbor.Y * square.Height).ToPoint();

                if (wall.FirstNeighbor.Y > wall.SecondNeighbor.Y) {
                    TimesToDraw = 2;
                }

                if (wall is TrueWall) {
                    dc.DrawLine(wallPen, a, b);
                    if (TimesToDraw == 2) {
                        dc.DrawLine(wallPen, c, d);
                    }
                }
                else if (wall.MyImage != null) { //fix to include doors!
                    a = new DoublePoint((wall.FirstNeighbor.X) * square.Width, (wall.FirstNeighbor.Y + 1) * square.Height - wall.MyImage.Height / 2).ToPoint();
                    c = new DoublePoint((wall.FirstNeighbor.X) * square.Width, (wall.FirstNeighbor.Y) * square.Height - wall.MyImage.Height / 2).ToPoint();
                    dc.DrawImage(wall.MyImage, a);
                    dc.DrawImage(wall.MyImage, c);
                }
                else {
                    throw new UnreachableException();
                }
            }
            else {
                if (wall is TrueWall) {
                    if (wall.FirstNeighbor.X > wall.SecondNeighbor.X) {
                        Point a = new DoublePoint((wall.FirstNeighbor.X + 1) * square.Width, wall.FirstNeighbor.Y * square.Height).ToPoint();
                        Point b = new DoublePoint((wall.FirstNeighbor.X + 1) * square.Width, (wall.FirstNeighbor.Y + 1) * square.Height).ToPoint();
                        dc.DrawLine(wallPen, a, b);

                        a = new DoublePoint(wall.SecondNeighbor.X * square.Width, wall.SecondNeighbor.Y * square.Height).ToPoint();
                        b = new DoublePoint(wall.SecondNeighbor.X * square.Width, (wall.SecondNeighbor.Y + 1) * square.Height).ToPoint();
                        dc.DrawLine(wallPen, a, b);
                    }
                    else {
                        Point a = new DoublePoint(wall.FirstNeighbor.X * square.Width, wall.FirstNeighbor.Y * square.Height).ToPoint();
                        Point b = new DoublePoint(wall.FirstNeighbor.X * square.Width, (wall.FirstNeighbor.Y + 1) * square.Height).ToPoint();
                        dc.DrawLine(wallPen, a, b);

                    }
                }
                else if (wall is Door) {
                    Point a = new DoublePoint((wall.FirstNeighbor.X + 1) * square.Width, wall.FirstNeighbor.Y * square.Height).ToPoint();
                    dc.DrawImage(vert_door, a);

                }
                else if (wall.MyImage != null) {
                    Point a = new DoublePoint((wall.FirstNeighbor.X + 1) * square.Width - wall.MyImage.Width / 2, wall.FirstNeighbor.Y * square.Height).ToPoint();
                    dc.DrawImage(wall.MyImage, a);
                }
                else {
                    throw new UnreachableException();
                }
            }
        }
    }

    private void DrawSquaresItemsCreations(Graphics dc) {
        foreach (Square s in GameState.BoardRef.squaresByIndex) {
            dc.DrawImage(square, squares[s.ID]);
            foreach (ICreation c in s.creationsHere) {
                if (c is ThornbushCreation) {
                    dc.DrawImage(thornbush, squares[s.ID]);
                    break;
                }
            }

            foreach (IItem i in s.ItemsHere) {
                if (i is ITreasure) {
                    Color tColor = Color.FromArgb(255, 255, 255);
                    ImageAttributes tImgAtt = new ImageAttributes();
                    tImgAtt.SetColorKey(tColor, tColor);
                    Rectangle tRect = new Rectangle(squares[GameState.BoardRef.At(i.X, i.Y).ID], new Size(square.Width, square.Height));
                    dc.DrawImage(treasure, tRect, 0, 0, square.Width, square.Height, GraphicsUnit.Pixel, tImgAtt);
                    break;
                }
            }
        }
    }

    private void MouseMoved(object sender, EventArgs e) {
#if (!DEBUG)
        this.SuspendLayout();
#endif
        StateLabel.Text = myUI.State.ToString();

        Graphics dc = this.CreateGraphics();
        Point mouse = FindMouse();
        Square currentSquare = this.FindSelectedSquare(mouse.X, mouse.Y);
        switch (LibraryFunctions.FindCase((Math.Abs(mouse.X - this.SnapX(mouse.X)) < 10 && OnBoard(mouse.X, mouse.Y)), Math.Abs(mouse.Y - this.SnapY(mouse.Y)) < 10 && OnBoard(mouse.X, mouse.Y))) {
            case LibraryFunctions.Case.None:
                if (currentSquare != OldSelectedSquare) {
                    if (OldSelectedSquare != null) {
                        InvalidateSquare(OldSelectedSquare);
                    }

                    InvalidateSelectedNeighbors();

                    OldSelectedSquare = currentSquare;
                    
                    OldWallX = Double.NaN;
                    OldWallY = Double.NaN;

                    Square s = FindSelectedSquare(mouse.X, mouse.Y);
                    if (s != null) {
                        ImageAttributes tImgAtt = MakeWhiteTransparent();
                        Rectangle tRect = new Rectangle(squares[s.ID], new Size(square.Width, square.Height));
                        dc.DrawImage(selectedSquare, tRect, 0, 0, square.Width, square.Height, GraphicsUnit.Pixel, tImgAtt);
                        selectedWall = null;
                        //MessageBox.Show("Tried to draw selected square");
                    }
                }
                break;
            case LibraryFunctions.Case.First:
                if (this.SnapX(mouse.X) != OldWallX && this.SnapX(mouse.X) != Double.NaN) {
                    InvalidateSelectedNeighbors();

                    OldWallX = this.SnapX(mouse.X);
                    OldWallY = (Math.Floor((double)mouse.Y / square.Height) + 0.5) * square.Height;
                    if (OldWallY < 0) {
                        OldWallY = Double.NaN;
                    }

                    if (currentSquare != null) {
                        this.InvalidateSquare(currentSquare);
                        Update(); //actually does something
                    }
                    if (OldSelectedSquare != null) {
                        InvalidateSquare(OldSelectedSquare);
                        Update();
                    }

                    double tester1 = this.SnapX(mouse.X) - selectedVertWall.Width / 2;
                    double tester2 = Math.Floor((double)mouse.Y / square.Height) * square.Height;
                    if (Math.Floor(mouse.X / (double)square.Width) <= GameState.BoardRef.Width) {
                        dc.DrawImage(selectedVertWall, new Point((int)(tester1), (int)(tester2)));
                        selectedWall = new WallSpace(SnapX(mouse.X) / square.Width - 0.5, Math.Floor((double)mouse.Y / square.Height));
                    }
                    OldSelectedSquare = null;
                }
                break;
            case LibraryFunctions.Case.Second:
                if (this.SnapY(mouse.Y) != OldWallY && this.SnapY(mouse.Y) != Double.NaN) {
                    InvalidateSelectedNeighbors();

                    OldWallX = (Math.Floor((double)mouse.X / square.Width) + 0.5) * square.Width;
                    if (OldWallX < 0) {
                       OldWallX = Double.NaN;
                    }

                    OldWallY = this.SnapY(mouse.Y);

                    if (currentSquare != null) {
                        this.InvalidateSquare(currentSquare);
                        Update(); //actually does something
                    }
                    if (OldSelectedSquare != null) {
                        InvalidateSquare(OldSelectedSquare);
                        Update();
                    }

                    double tester1 = Math.Floor((double)mouse.X / square.Width) * square.Width;
                    double tester2 = this.SnapY(mouse.Y) - selectedHorizWall.Height / 2;
                    if (Math.Floor(mouse.Y / (double)square.Height) <= GameState.BoardRef.Height) {
                        dc.DrawImage(selectedHorizWall, new Point((int)(tester1), (int)(tester2)));
                        selectedWall = new WallSpace(Math.Floor((double)mouse.X / square.Width), SnapY(mouse.Y) / square.Height - 0.5);
                    }
                    OldSelectedSquare = null;
                }
                break;
            case LibraryFunctions.Case.Both:
                selectedWall = null;

                if (OldSelectedSquare != null) {
                    InvalidateSquare(OldSelectedSquare);
                    OldSelectedSquare = null;
                }
                
                if (currentSquare != null) {
                    InvalidateSquare(currentSquare);
                    currentSquare = null;
                }

                InvalidateSelectedNeighbors();

                OldWallX = Double.NaN;
                OldWallY = Double.NaN;

                break;
            default:
                throw new UnreachableException();
        }
#if (!DEBUG)
        ResumeLayout(false);
        PeformLayout();
#endif
    }

    private static ImageAttributes MakeWhiteTransparent() {
        Color tColor = Color.FromArgb(255, 255, 255);
        ImageAttributes tImgAtt = new ImageAttributes();
        tImgAtt.SetColorKey(tColor, tColor);
        return tImgAtt;
    }

    private bool OnBoard(double tX, double tY) {
        if (SnapX(tX) / square.Width < GameState.BoardRef.Width && 
        SnapY(tY) / square.Height < GameState.BoardRef.Height) {
            return true;
        }
        
        if ((SnapX(tX) / square.Width == GameState.BoardRef.Width) && Math.Abs(tX - SnapX(tX)) < 10 && SnapY(tY) / square.Height < GameState.BoardRef.Height) {
            return true;
        }

        if ((SnapY(tY) / square.Height == GameState.BoardRef.Height) && Math.Abs(tY - SnapY(tY)) < 10 && SnapX(tX) / square.Width < GameState.BoardRef.Width) {
            return true;
        }

        return false;
    }

    private void InvalidateSelectedNeighbors() {
        bool doShotgunRedraw = false;
        if (OldWallX / square.Width == Math.Round(OldWallX / square.Width)) { //vertical wall
            if (OldWallX == 0 || OldWallX / square.Width >= GameState.BoardRef.Width) {
                doShotgunRedraw = true;
            }
            else {
                InvalidateSquare(GameState.BoardRef.AtNoWrap(Math.Floor(OldWallX / square.Width - 1), Math.Floor(OldWallY / square.Height)));
                InvalidateSquare(GameState.BoardRef.AtNoWrap(Math.Floor(OldWallX / square.Width), Math.Floor(OldWallY / square.Height)));
                //Update();
            }
        }

        if (OldWallY / square.Height == Math.Round(OldWallY / square.Height)) { //horizontal wall
            if (OldWallY == 0 || OldWallY / square.Height >= GameState.BoardRef.Height) {
                doShotgunRedraw = true;
            }
            else {
                double tester1 = Math.Floor(OldWallX / square.Width);
                double tester2 = (OldWallY - 1) / square.Height;
                InvalidateSquare(GameState.BoardRef.AtNoWrap(Math.Floor(OldWallX / square.Width), OldWallY / square.Height - 1));
                InvalidateSquare(GameState.BoardRef.AtNoWrap(Math.Floor(OldWallX / square.Width), OldWallY / square.Height));
            }
        }
        Update(); //actually necessary

        if (doShotgunRedraw == true) {
            Rectangle shotgunRedraw = new Rectangle((int)OldWallX - square.Width / 2, (int)OldWallY - square.Height / 2, square.Width, square.Height);
            Invalidate(shotgunRedraw);
            Update();
        }
    }

    private void InvalidateSquare(Square tSquare) {
        this.Invalidate(new Rectangle((int)tSquare.X * square.Width, (int)tSquare.Y * square.Height, square.Width, square.Height));
    }

    private void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
        if (myUI.myWizard != GameState.ActivePlayer) {
            return;
        }

        if (myUI.State == UIState.Normal) {
            Square oldLocation = GameState.ActivePlayer.ContainingSquare;
            if (e.KeyData == Keys.Left) {
                GameState.ActivePlayer.MoveOne(Direction.West);
            }
            else if (e.KeyData == Keys.Right) {
                GameState.ActivePlayer.MoveOne(Direction.East);
            }
            else if (e.KeyData == Keys.Down) {
                GameState.ActivePlayer.MoveOne(Direction.South);
            }
            else if (e.KeyData == Keys.Up) {
                GameState.ActivePlayer.MoveOne(Direction.North);
            }
            else if (e.KeyData == Keys.A) {
                MessageBox.Show("Help!");
            }

            if (GameState.ActivePlayer.ContainingSquare != oldLocation) {
                foreach (UIControl UI in GameState.UIReference) {
                    UI.myForm.InvalidateSquare(oldLocation);
                    UI.myForm.InvalidateSquare(GameState.ActivePlayer.ContainingSquare);
                }
            } 
        }
    }

    protected override void OnMouseDown(MouseEventArgs e) {
        Square s = FindSelectedSquare(e.X, e.Y);
        if (s == null) {
            return;
        }

        //if (selectedWall == null) {
        //    MessageBox.Show("null");
        //}
        //else {
        //    MessageBox.Show(selectedWall.ToString());
        //}

        if (myUI.State == UIState.CastingSpell || myUI.State == UIState.FindingTarget) {
            if (GameState.InitialUltimatum(Event.New<CastEvent>(true, new CastEvent(myUI.SpellToCast))) == Redirect.Proceed) {
                if (TestLoSButton.ContainsMouse(new Point(e.X, e.Y)) == false) {
                    maskedTextBox1.Clear();
                    maskedTextBox2.Clear();
                }

                if (myUI.SpellToCast.IsValidSpellTargetType(TargetTypes.Wall) && selectedWall != null) {
                    if (GameState.BoardRef.TestLoSToWall(s.X - GameState.ActivePlayer.X, s.Y - GameState.ActivePlayer.Y, selectedWall) == false) {
                        MessageBox.Show("No line of sight to wall");
                    }
                    else {
                        MessageBox.Show("Yes line of sight to wall");
                        TargetWallOrSquare(e);
                    }
                }
                else if (GameState.BoardRef.TestLoSNew(s.X - GameState.ActivePlayer.X, s.Y - GameState.ActivePlayer.Y, myUI) == false) {
                    MessageBox.Show("No line of sight");
                }
                else {
                    if (myUI.SpellToCast != null) {
                        myUI.SpellToCast.ShotDirection = LibraryFunctions.VectorToAngle(s.X - GameState.ActivePlayer.X, s.Y - GameState.ActivePlayer.Y);
                    }
                    else if (myUI.ItemToUse != null) {
                        myUI.ItemToUse.ShotDirection = LibraryFunctions.VectorToAngle(s.X - GameState.ActivePlayer.X, s.Y - GameState.ActivePlayer.Y);
                    }

                    //this function call does all the work
                    TargetWallOrSquare(e); 
                }
            }
        }
        else if (myUI.State == UIState.Querying) {
            bool wizardFound = false;
            foreach (Wizard w in GameState.wizards) {
                if (w.X == s.X && w.Y == s.Y) {
                    myUI.myInfo.QueryWizard(w);
                    wizardFound = true;
                    break;
                }
            }

            if (wizardFound == false) {
                myUI.myInfo.QuerySquare(s); //no idea if I put this back together properly
            }
        }
        else if (myUI.State == UIState.QueryingSquare) {
            
            SquareInfoPane mySquareInfo = myUI.mySquareInfo;
            if (mySquareInfo == null) {
                mySquareInfo = new SquareInfoPane(myUI, s);
                mySquareInfo.StartPosition = FormStartPosition.Manual;
                mySquareInfo.Location = new Point(350, 100);
            }
            else {
                mySquareInfo.QuerySquare = s;
                mySquareInfo.BringToFront();
                mySquareInfo.RefreshAll();
                mySquareInfo.Show();
            }
            myUI.State = UIState.Previous;
        }
        else if (myUI.State == UIState.TestingLoS) {
            Graphics dc = this.CreateGraphics();
            this.Invalidate();
            this.Update();

            Pen LoSPen;
            //Trace.Assert(this.Left != 0 && this.Top != 0, );
            //Trace.Assert(false, "cursor at " + tSquare.Y + " ID of " + tSquare.ID);
            if (GameState.BoardRef.TestLoSNew(s.X - GameState.ActivePlayer.X, s.Y - GameState.ActivePlayer.Y, myUI) == true) {
                LoSPen = new Pen(Color.Green, 8);
            }
            else {
                LoSPen = new Pen(Color.Red, 8);
            }


            Point a = new DoublePoint((GameState.ActivePlayer.X + 0.5) * square.Width, (GameState.ActivePlayer.Y + 0.5) * square.Height).ToPoint();
            Square clickedSquare = FindSelectedSquare(e.X, e.Y);
            Point b = new DoublePoint((clickedSquare.X + 0.5) * square.Width, (clickedSquare.Y + 0.5) * square.Height).ToPoint();
            dc.DrawLine(LoSPen, a, b);
        }

    }

    private void ManualLoSTarget_Click(object sender, EventArgs e) {
        try {
            if (GameState.BoardRef.TestLoSNew(Double.Parse(maskedTextBox1.Text), Double.Parse(maskedTextBox2.Text), myUI) == false) {
                MessageBox.Show("No line of sight");
                return;
            }

            if (myUI.SpellToCast != null) {
                myUI.SpellToCast.ShotDirection = LibraryFunctions.VectorToAngle(Double.Parse(maskedTextBox1.Text), Double.Parse(maskedTextBox1.Text));
            }
            else if (myUI.ItemToUse != null) {
                myUI.ItemToUse.ShotDirection = LibraryFunctions.VectorToAngle(Double.Parse(maskedTextBox1.Text), Double.Parse(maskedTextBox2.Text));
            }
        }
        catch (FormatException) {
            MessageBox.Show("No line of sight");
            return;
        }
        return;
    }

    private Point FindMouse() {
        Point tPoint = new Point(Cursor.Position.X, Cursor.Position.Y);
        tPoint = this.PointToClient(tPoint);

        return tPoint;
    }

    //deprecated
    private Square FindSquareUnderMouse(Point mouseCoordinates) {
        return FindSelectedSquare(mouseCoordinates.X, mouseCoordinates.Y);
    }

    internal Square FindSelectedSquare() {
        return FindSelectedSquare(FindMouse());
    }

    internal Square FindSelectedSquare(Point tMouse) {
        return FindSelectedSquare(tMouse.X, tMouse.Y);
    }

    internal Square FindSelectedSquare(double mouseX, double mouseY) {
        double squaresOver = Math.Floor(mouseX / (double)square.Width);
        //MessageBox.Show("squaresOver is " + squaresOver);
        double squaresDown = Math.Floor(mouseY / (double)square.Height);
        //MessageBox.Show("SquaresDown is " + squaresDown);
        Square result = GameState.BoardRef.AtNoWrap(squaresOver, squaresDown);
        if (result != null) {
            //MessageBox.Show("Square ID is: " + result.ID);
            return result;
        }
        
        return null;
    }

    private double SnapX(double rawX) {
        double squaresOver = Math.Round(rawX / (double)square.Width);

        if (squaresOver <= GameState.BoardRef.Width) {
            return squaresOver * square.Width;
        }

        return Double.NaN;
    }

    private double SnapY(double rawY) {
        double squaresDown = Math.Round(rawY / (double)square.Height);

        if (squaresDown <= GameState.BoardRef.Height) {
            return squaresDown * square.Height;
        }

        return Double.NaN;
    }

    private DoublePoint SnapXY(Point rawCoordinates) {
        return new DoublePoint(FindSquareUnderMouse(rawCoordinates).X, FindSquareUnderMouse(rawCoordinates).Y);
    }

    //returns on invalid target; calls MyControl.TargetValidated if a wall is targeted, otherwise opens the targeting window
    //needs to be edited to consider WallSpace targets too
    internal void TargetWallOrSquare(MouseEventArgs tE) {
        if (myUI.SpellToCast.IsValidSpellTargetType(TargetTypes.Wall)
        && selectedWall != null && GameState.BoardRef.LookForWall(selectedWall.X, selectedWall.Y) != null) {
            //MessageBox.Show("You are on wallspace " + OnWallSpace.X + " " + OnWallSpace.Y);
            TargetingEvent a = new TargetingEvent(selectedWall as IWall);
            if (a.GetFlowControl() == Redirect.Proceed) {
                myUI.myControl.TargetValidated(GameState.BoardRef.LookForWall(selectedWall.X, selectedWall.Y), TargetTypes.WallSpace);
            }
            return;
        }

        Square s = FindSelectedSquare(tE.X, tE.Y);

        if (s != null) {
            TargetChooser targetWindow1 = new TargetChooser(myUI, s, myUI.SpellToCast);
            targetWindow1.StartPosition = FormStartPosition.Manual;
            targetWindow1.Location = new Point(100,100);
            targetWindow1.Show();
            targetWindow1.ValidateTarget();
        }
    }

    private IWall findWallUnderMouse(int mouseX, int mouseY) {
        throw new NotImplementedException();
    }

    private void TestLoSButton_Click(object sender, EventArgs e) {
        if (myUI.State != UIState.TestingLoS) {
            myUI.State = UIState.TestingLoS;
        }
        else {
            myUI.State = UIState.Previous;
        }
    }

    private void TestLosButton_Enter(object sender, EventArgs e) {
        this.Focus();
    }

    private void LoSCoordButton_Click(object sender, EventArgs e) {
        Graphics dc = this.CreateGraphics();
        this.Invalidate();
        this.Update();

        Pen LoSPen;
        //Trace.Assert(this.Left != 0 && this.Top != 0, );
        //Trace.Assert(false, "cursor at " + tSquare.Y + " ID of " + tSquare.ID);
        try {
            double testX = Double.Parse(this.maskedTextBox1.Text);
            double testY = Double.Parse(this.maskedTextBox2.Text);

            if (false) {//not ready to support wraparound LoS graphics
                if (GameState.BoardRef.TestLoSNew(testX - GameState.ActivePlayer.X, testY - GameState.ActivePlayer.Y, myUI) == true) {
                    LoSPen = new Pen(Color.Green, 8);
                }
                else {
                    LoSPen = new Pen(Color.Red, 8);
                }

                Point a = new DoublePoint((GameState.ActivePlayer.X + 0.5) * square.Width, (GameState.ActivePlayer.Y + 0.5) * square.Height).ToPoint();
                Square clickedSquare = GameState.BoardRef.At(testX, testY);
                Point b = new DoublePoint((clickedSquare.X + 0.5) * square.Width, (clickedSquare.Y + 0.5) * square.Height).ToPoint();
                dc.DrawLine(LoSPen, a, b);
            }
            else {
                if (GameState.BoardRef.TestLoSNew(testX - GameState.ActivePlayer.X, testY - GameState.ActivePlayer.Y, myUI) == true) {
                    MessageBox.Show("Yes");
                }
                else {
                    MessageBox.Show("No");
                }
            }
        }
        catch (FormatException) {
        }
    }

    private void BreakPointButton_Click(object sender, EventArgs e) {
        int i = 0;
    }
}
}
