using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Media;
using System.Windows.Forms;
using Library;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WizWar1 {

	public partial class BoardForm : Form, IListener<NewEffectEvent, Event>, IListener<StateChangeEvent, Event>,
	IListener<ItemRevealEvent, Event>, IListener<CastEvent, Event>, IListener<ItemUseEvent, Event>, IListener<HealthChangeEvent, Event> {
		Image card_border = Image.FromFile(@"Arena\card_border.png");
		Image square = Image.FromFile(@"Arena\square_teal.bmp");
		Image blue_wizard = Image.FromFile(@"Arena\model_battleaxe.bmp");
		Image red_wizard = Image.FromFile(@"Arena\model_claws.bmp");
		public Image horiz_door = Image.FromFile(@"Arena\door.png");
		public Image vert_door = Image.FromFile(@"Arena\vertical_door.bmp");

		Image selectedSquare = Image.FromFile(@"Arena\square_selected.bmp");
		Image selectedVertWall = Image.FromFile(@"Arena\vert_wall_selected.bmp");
		Image selectedHorizWall = Image.FromFile(@"Arena\horiz_wall_selected.bmp");
		Image treasure = Image.FromFile(@"Arena\treasure.bmp");

		public Image horiz_illusion_wall = Image.FromFile(@"Arena\horiz_illusion_wall.bmp");
		public Image vert_illusion_wall = Image.FromFile(@"Arena\vert_illusion_wall.bmp");

		public Bitmap horizIllusionWall;
		public Bitmap vertIllusionWall;

		private SoundPlayer boop1Player = new SoundPlayer(Environment.CurrentDirectory + @"\Arena\boop1.wav");
		private WMPLib.WindowsMediaPlayer boop1alternatePlayer = new WMPLib.WindowsMediaPlayer { URL = Environment.CurrentDirectory + @"/Arena/boop1.wav" };
		private Point[] squares;
		private UIControl myUI;
		private Square oldSelectedSquare;
		private double oldWallX = Double.NaN;
		private double oldWallY = Double.NaN;
		private WallSpace selectedWallRegion;
		internal WallSpace SelectedWall {
			get {
				return selectedWallRegion;
			}
		}

		private int lastX;
		private int lastY;
		private BoardForm formReference;
		private List<ICard> deadCards;
		internal int numberCardsLeft;
		internal bool passedPriority = false;
		private ICard selectedCard;
		private CardPreview cardPreview = new CardPreview();

		internal BoardForm(UIControl tMyUI) {
			InitializeComponent();

			myUI = tMyUI;

			GameState.EventDispatcher.Register<NewEffectEvent>(this);
			GameState.EventDispatcher.Register<StateChangeEvent>(this);
			GameState.EventDispatcher.Register<ItemRevealEvent>(this);
			GameState.EventDispatcher.Register<CastEvent>(this);
			GameState.EventDispatcher.Register<ItemUseEvent>(this);
			GameState.EventDispatcher.Register<HealthChangeEvent>(this);

			deadCards = new List<ICard>();

			CurrentHealthLabel.Text = "Health: 15";

			//GameState.SetMe(myUI);
			//GameState.Form1Reference[(int)MyWizard] = this; //must go after SetMe() is called

			//if (myUI.myWizard.Name.Contains("Blue")) BackColor = Color.CornflowerBlue;
			//if (myUI.myWizard.Name.Contains("Red")) BackColor = Color.DarkRed;
		}

		private void Form1_Load(object sender, EventArgs e) {
			FindSquarePoints();

			Text = myUI.myWizard.ToString();

			horizIllusionWall = new Bitmap(horiz_illusion_wall);
			horizIllusionWall.MakeTransparent(Color.White);

			vertIllusionWall = new Bitmap(vert_illusion_wall);
			vertIllusionWall.MakeTransparent(Color.White);

			RemoveCursorNavigation(Controls);

			if (myUI.myWizard.Name.Contains("Blue")) BackColor = Color.CornflowerBlue;
			if (myUI.myWizard.Name.Contains("Red")) BackColor = Color.DarkRed;

			foreach (Control child in Controls) {
				child.MouseMove += this.OnMouseMove;
			}
		}

		private void FindSquarePoints() {
			squares = new Point[GameState.BoardRef.NSquares];

			for (int i = 0; i < GameState.BoardRef.NSquares; ++i) {
				squares[i] = new Point((int)GameState.BoardRef.SquaresByIndex[i].X * square.Width,
											  (int)GameState.BoardRef.SquaresByIndex[i].Y * square.Height);
			}
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
			foreach (Wizard wizard in GameState.Wizards) {
				Color tColor = Color.FromArgb(255, 255, 255);
				var tImgAtt = new ImageAttributes();
				tImgAtt.SetColorKey(tColor, tColor);
				var tRect = new Rectangle(squares[GameState.BoardRef.At(wizard.X, wizard.Y).ID], new Size(square.Width, square.Height));

				if (wizard.Name == "Blue Wizard") {
					dc.DrawImage(blue_wizard, tRect, 0, 0, square.Width, square.Height, GraphicsUnit.Pixel, tImgAtt);
				}
				else if (wizard.Name == "Red Wizard") {
					dc.DrawImage(red_wizard, tRect, 0, 0, square.Width, square.Height, GraphicsUnit.Pixel, tImgAtt);
				}
			}
		}

		private void DrawWallsDoors(Graphics dc) {
			var wallPen = new Pen(Color.Black, 4);
			int timesToDraw = 1;

			foreach (KeyValuePair<DoublePoint, IWall> w in GameState.BoardRef.Walls) {
				IWall wall = w.Value;

				if (wall.IsHorizontal()) {
					Point bottomLeft = new DoublePoint(wall.FirstNeighbor.X * square.Width, (wall.FirstNeighbor.Y + 1) * square.Height).ToPoint();
					Point bottomRight = new DoublePoint((wall.FirstNeighbor.X + 1) * square.Width, (wall.FirstNeighbor.Y + 1) * square.Height).ToPoint();
					Point topLeft = new DoublePoint(wall.SecondNeighbor.X * square.Width, wall.SecondNeighbor.Y * square.Height).ToPoint();
					Point topRight = new DoublePoint((wall.SecondNeighbor.X + 1) * square.Width, wall.SecondNeighbor.Y * square.Height).ToPoint();

					if (wall.FirstNeighbor.Y > wall.SecondNeighbor.Y) {
						timesToDraw = 2;
					}

					if (wall is TrueWall) {
						dc.DrawLine(wallPen, topLeft, topRight);

						if (timesToDraw == 2) {
							dc.DrawLine(wallPen, bottomLeft, bottomRight);
						}
					}
					else if (wall.MyImage != null) {
						topLeft.Y -= 5;
						dc.DrawImage(wall.MyImage, topLeft);
					}
					else {
						throw new UnreachableException();
					}
				}
				else {
					Point bottomLeft = new DoublePoint(wall.SecondNeighbor.X * square.Width, (wall.FirstNeighbor.Y + 1) * square.Height).ToPoint();
					Point bottomRight = new DoublePoint((wall.FirstNeighbor.X + 1) * square.Width, (wall.FirstNeighbor.Y + 1) * square.Height).ToPoint();
					Point topLeft = new DoublePoint(wall.SecondNeighbor.X * square.Width, wall.SecondNeighbor.Y * square.Height).ToPoint();
					Point topRight = new DoublePoint((wall.FirstNeighbor.X + 1) * square.Width, wall.SecondNeighbor.Y * square.Height).ToPoint();

					if (wall.FirstNeighbor.X > wall.SecondNeighbor.X) {
						timesToDraw = 2;
					}

					if (wall is TrueWall) {
						dc.DrawLine(wallPen, topLeft, bottomLeft);

						if (timesToDraw == 2) {
							dc.DrawLine(wallPen, topRight, bottomRight);
						}
					}
					else if (wall.MyImage != null) {
						topRight.X -= 5;
						dc.DrawImage(wall.MyImage, topLeft);
					}
					else {
						throw new UnreachableException();
					}
				}
			}
		}

		private void DrawSquaresItemsCreations(Graphics dc) {
			foreach (Square s in GameState.BoardRef.SquaresByIndex) {
				dc.DrawImage(square, squares[s.ID]);

				foreach (ICreation c in s.creationsHere) {
					if (c.MyImage != null) {
						dc.DrawImage(c.MyImage, squares[s.ID]);
						break;
					}
				}

				foreach (ILocatable i in s.CarriablesHere) {
					if (i is ITreasure) {
						Color tColor = Color.FromArgb(255, 255, 255);

						var tImgAtt = new ImageAttributes();

						tImgAtt.SetColorKey(tColor, tColor);

						var tRect = new Rectangle(squares[GameState.BoardRef.At(i.X, i.Y).ID], new Size(square.Width, square.Height));

						dc.DrawImage(treasure, tRect, 0, 0, square.Width, square.Height, GraphicsUnit.Pixel, tImgAtt);
						break;
					}
				}
			}
		}

		private void MouseMoved(object sender, MouseEventArgs e) {
#if (!DEBUG)
			SuspendLayout();
#endif

			StateLabel.Text = myUI.State.ToString();

			Graphics dc = CreateGraphics();
			Point mouse = FindMouse();
			Square currentSquare = FindSelectedSquare(mouse.X, mouse.Y);
			switch (LibraryFunctions.FindCase((Math.Abs(mouse.X - SnapX(mouse.X)) < 10 && OnBoard(mouse.X, mouse.Y)), Math.Abs(mouse.Y - SnapY(mouse.Y)) < 10 && OnBoard(mouse.X, mouse.Y))) {
				case LibraryFunctions.Case.None:
					if (currentSquare != oldSelectedSquare) {
						if (oldSelectedSquare != null) {
							InvalidateSquare(oldSelectedSquare);
						}

						InvalidateSelectedNeighbors();

						oldSelectedSquare = currentSquare;

						oldWallX = Double.NaN;
						oldWallY = Double.NaN;

						Square s = FindSelectedSquare(mouse.X, mouse.Y);
						if (s != null) {
							ImageAttributes tImgAtt = MakeWhiteTransparent();
							var tRect = new Rectangle(squares[s.ID], new Size(square.Width, square.Height));
							dc.DrawImage(selectedSquare, tRect, 0, 0, square.Width, square.Height, GraphicsUnit.Pixel, tImgAtt);

							//boop1alternatePlayer.controls.stop();
							//boop1alternatePlayer.controls.play();

							boop1Player.Play();

							selectedWallRegion = null;
							//MessageBox.Show("Tried to draw selected square");
						}
					}
					break;
				case LibraryFunctions.Case.First:
					if (SnapX(mouse.X) != oldWallX && SnapX(mouse.X) != Double.NaN) {
						InvalidateSelectedNeighbors();

						oldWallX = SnapX(mouse.X);

						if (oldWallX < 0) {
							throw new Exception("oldWall X is " + oldWallX);
						}

						oldWallY = (Math.Floor((double)mouse.Y / square.Height) + 0.5) * square.Height;
						if (oldWallY < 0) {
							oldWallY = Double.NaN;
						}

						if (currentSquare != null) {
							InvalidateSquare(currentSquare);
							Update(); //actually does something
						}
						if (oldSelectedSquare != null) {
							InvalidateSquare(oldSelectedSquare);
							Update();
						}

						double tester1 = SnapX(mouse.X) - selectedVertWall.Width / 2;
						double tester2 = Math.Floor((double)mouse.Y / square.Height) * square.Height;
						if (Math.Floor(mouse.X / (double)square.Width) <= GameState.BoardRef.Width) {
							selectedWallRegion = new WallSpace(SnapX(mouse.X) / square.Width - 0.5, Math.Floor((double)mouse.Y / square.Height));

							var tRect = new Rectangle((int)tester1, (int)tester2, selectedVertWall.Width, selectedVertWall.Height);
							dc.DrawImage(selectedVertWall, tRect, 0, 0, selectedVertWall.Width, selectedVertWall.Height, GraphicsUnit.Pixel, MakeWhiteTransparent());

						}
						oldSelectedSquare = null;
					}
					break;
				case LibraryFunctions.Case.Second:
					if (SnapY(mouse.Y) != oldWallY && SnapY(mouse.Y) != Double.NaN) {
						InvalidateSelectedNeighbors();

						oldWallX = (Math.Floor((double)mouse.X / square.Width) + 0.5) * square.Width;
						if (oldWallX < 0) {
							oldWallX = Double.NaN;
						}

						oldWallY = SnapY(mouse.Y);

						if (currentSquare != null) {
							InvalidateSquare(currentSquare);
							Update(); //actually does something
						}
						if (oldSelectedSquare != null) {
							InvalidateSquare(oldSelectedSquare);
							Update();
						}

						double tester1 = Math.Floor((double)mouse.X / square.Width) * square.Width;
						double tester2 = SnapY(mouse.Y) - selectedHorizWall.Height / 2;
						if (Math.Floor(mouse.Y / (double)square.Height) <= GameState.BoardRef.Height) {
							var tRect = new Rectangle((int)tester1, (int)tester2, selectedHorizWall.Width, selectedHorizWall.Height);
							dc.DrawImage(selectedHorizWall, tRect, 0, 0, selectedHorizWall.Width, selectedHorizWall.Height, GraphicsUnit.Pixel, MakeWhiteTransparent());
						}
						oldSelectedSquare = null;
					}
					break;
				case LibraryFunctions.Case.Both:
					selectedWallRegion = null;

					if (oldSelectedSquare != null) {
						InvalidateSquare(oldSelectedSquare);
						oldSelectedSquare = null;
					}

					if (currentSquare != null) {
						InvalidateSquare(currentSquare);
					}

					InvalidateSelectedNeighbors();

					oldWallX = Double.NaN;
					oldWallY = Double.NaN;

					break;
				default:
					throw new UnreachableException();
			}

			toolTip1.Hide(HandOCards);

			cardPreview1.Visible = false;

#if (!DEBUG)
			ResumeLayout(false);
			//PeformLayout();
#endif
		}

		private static ImageAttributes MakeWhiteTransparent() {
			Color tColor = Color.FromArgb(255, 255, 255);
			var tImgAtt = new ImageAttributes();
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
			if (oldWallX / square.Width == Math.Round(oldWallX / square.Width)) { //vertical wall
				if (oldWallX == 0 || oldWallX / square.Width >= GameState.BoardRef.Width) {
					doShotgunRedraw = true;
				}
				else {
					InvalidateSquare(GameState.BoardRef.AtNoWrap(Math.Floor(oldWallX / square.Width - 1), Math.Floor(oldWallY / square.Height)));
					InvalidateSquare(GameState.BoardRef.AtNoWrap(Math.Floor(oldWallX / square.Width), Math.Floor(oldWallY / square.Height)));
					//Update();
				}
			}

			if (oldWallY / square.Height == Math.Round(oldWallY / square.Height)) { //horizontal wall
				if (oldWallY == 0 || oldWallY / square.Height >= GameState.BoardRef.Height || oldWallY == Double.NaN) {
					doShotgunRedraw = true;
				}
				else {
					//double tester1 = Math.Floor(OldWallX / square.Width);
					//double tester2 = (OldWallY - 1) / square.Height;
					InvalidateSquare(GameState.BoardRef.AtNoWrap(Math.Floor(oldWallX / square.Width), oldWallY / square.Height - 1));
					InvalidateSquare(GameState.BoardRef.AtNoWrap(Math.Floor(oldWallX / square.Width), oldWallY / square.Height));
				}
			}
			Update(); //actually necessary

			if (doShotgunRedraw) {
				var shotgunRedraw = new Rectangle((int)oldWallX - square.Width / 2, (int)oldWallY - square.Height / 2, square.Width, square.Height);
				Invalidate(shotgunRedraw);
				Update();
			}
		}

		private void InvalidateSquare(Square tSquare) {
			Invalidate(new Rectangle((int)tSquare.X * square.Width, (int)tSquare.Y * square.Height, square.Width, square.Height));
		}

		private void OnKeyDown(object sender, KeyEventArgs e) {
			if (myUI.myWizard != GameState.ActivePlayer) {
				return;
			}

			if (myUI.State == UIState.Normal) {
				Square oldLocation = GameState.ActivePlayer.Location;
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

				if (GameState.ActivePlayer.Location != oldLocation) {
					foreach (UIControl UI in GameState.UiReference) {
						UI.myBoard.InvalidateSquare(oldLocation);
						UI.myBoard.InvalidateSquare(GameState.ActivePlayer.Location);
					}
				}
			}
		}

		protected override void OnMouseDown(MouseEventArgs e) {
			Square s = FindSelectedSquare(e.X, e.Y);
			if (s == null) {
				return;
			}

			if (myUI.State == UIState.CastingSpell) {
				CastAtSelectedSquare(s);
			}
			else if (myUI.State == UIState.UsingItem) {
				if (GameState.InitialUltimatum(Event.New<ItemUseEvent>(true, new ItemUseEvent(myUI.CurrentAimable as IItem))) == Redirect.Proceed) {
					if (TestLoSButton.ContainsMouse(new Point(e.X, e.Y)) == false) {
						maskedTextBox1.Clear();
						maskedTextBox2.Clear();
					}

					if ((myUI.CurrentAimable.IsValidTargetType(TargetTypes.Wall) || myUI.CurrentAimable.IsValidTargetType(TargetTypes.WallSpace)) && selectedWallRegion != null) {
						if (GameState.BoardRef.TestLoSToWall(s.X - GameState.ActivePlayer.X, s.Y - GameState.ActivePlayer.Y, selectedWallRegion) == false) {
							MessageBox.Show("No line of sight to wall");
						}
						else {
							MessageBox.Show("Yes line of sight to wall");
							TargetWall(e);
						}
					}
					else if (GameState.BoardRef.TestLoSNew(s.X - GameState.ActivePlayer.X, s.Y - GameState.ActivePlayer.Y, myUI) == false) {
						MessageBox.Show("No line of sight");
					}
					else if (myUI.CurrentAimable != null) {
						myUI.CurrentAimable.ShotDirection = LibraryFunctions.VectorToAngle(s.X - GameState.ActivePlayer.X, GameState.ActivePlayer.Y - s.Y);

						//this function call does all the work
						TargetSquare(s);
					}
				}
			}
			else if (myUI.State == UIState.Querying) {
				bool wizardFound = false;
				foreach (Wizard w in GameState.Wizards) {
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
			else if (myUI.State == UIState.QueryingSquare || myUI.State == UIState.Normal) {

				myUI.mySquareInfo = new SquareInfoPane(myUI, s) { StartPosition = FormStartPosition.Manual, Location = new Point(350, 100) };

				myUI.mySquareInfo.BringToFront();
				myUI.mySquareInfo.RefreshAll();
				myUI.mySquareInfo.Show();

				if (myUI.State == UIState.QueryingSquare) {
					myUI.State = UIState.Previous;
				}
			}
			else if (myUI.State == UIState.TestingLoS) {
				Graphics dc = CreateGraphics();
				Invalidate();
				Update();

				Pen LoSPen;
				//Trace.Assert(this.Left != 0 && this.Top != 0, );
				//Trace.Assert(false, "cursor at " + tSquare.Y + " ID of " + tSquare.ID);
				LoSPen = GameState.BoardRef.TestLoSNew(s.X - GameState.ActivePlayer.X, s.Y - GameState.ActivePlayer.Y, myUI) ? new Pen(Color.Green, 8) : new Pen(Color.Red, 8);

				Point a = new DoublePoint((GameState.ActivePlayer.X + 0.5) * square.Width, (GameState.ActivePlayer.Y + 0.5) * square.Height).ToPoint();
				Point b = new DoublePoint((s.X + 0.5) * square.Width, (s.Y + 0.5) * square.Height).ToPoint();
				dc.DrawLine(LoSPen, a, b);
			}
			else if (myUI.State == UIState.Locked) {
#if DEBUG
            if (selectedWall != null) {
                var newWall = new TrueWall {X = selectedWall.X, Y = selectedWall.Y};
                if (f == null)
                {
                    f = new StreamWriter("map3.txt");
                    f.WriteLine("Walls:");
                }
                GameState.BoardRef.AddWall(newWall);
                
                f.WriteLine(newWall.FirstNeighbor.X + " " + newWall.FirstNeighbor.Y + " " + newWall.SecondNeighbor.X + " " + newWall.SecondNeighbor.Y, 0, 1);
                f.Flush();
            }
#endif
			}
		}

		private void ManualLoSTarget_Click(object sender, EventArgs e) {
			try {
				if (GameState.BoardRef.TestLoSNew(Double.Parse(maskedTextBox1.Text), Double.Parse(maskedTextBox2.Text), myUI) == false) {
					MessageBox.Show("No line of sight");
					return;
				}

				if (myUI.CurrentAimable != null) {
					myUI.CurrentAimable.ShotDirection = LibraryFunctions.VectorToAngle(Double.Parse(maskedTextBox1.Text), Double.Parse(maskedTextBox1.Text));
				}
			}
			catch (FormatException) {
				MessageBox.Show("No line of sight");
			}
		}

		private Point FindMouse() {
			var tPoint = new Point(Cursor.Position.X, Cursor.Position.Y);
			tPoint = PointToClient(tPoint);

			return tPoint;
		}

		[Obsolete]
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
			double squaresOver = Math.Floor(mouseX / square.Width);
			//MessageBox.Show("squaresOver is " + squaresOver);
			double squaresDown = Math.Floor(mouseY / square.Height);
			//MessageBox.Show("SquaresDown is " + squaresDown);
			Square result = GameState.BoardRef.AtNoWrap(squaresOver, squaresDown);
			if (result != null) {
				//MessageBox.Show("Square ID is: " + result.ID);
				return result;
			}

			return null;
		}

		private double SnapX(double rawX) {
			double squaresOver = Math.Round(rawX / square.Width);

			if (squaresOver <= GameState.BoardRef.Width) {
				return squaresOver * square.Width;
			}

			return Double.NaN;
		}

		private double SnapY(double rawY) {
			double squaresDown = Math.Round(rawY / square.Height);

			if (squaresDown <= GameState.BoardRef.Height) {
				return squaresDown * square.Height;
			}

			return Double.NaN;
		}

		private DoublePoint SnapXY(Point rawCoordinates) {
			return new DoublePoint(FindSquareUnderMouse(rawCoordinates).X, FindSquareUnderMouse(rawCoordinates).Y);
		}

		private void RemoveCursorNavigation(Control.ControlCollection controls) {
			foreach (Control ctrl in controls) {
				if (ctrl is ListBox || ctrl is TreeView) {
					continue;
				}

				ctrl.PreviewKeyDown += MainWin_PreviewKeyDown;
				
				RemoveCursorNavigation(ctrl.Controls);
			}

		}

		public void MainWin_PreviewKeyDown(Object sender, PreviewKeyDownEventArgs e) {
			switch (e.KeyCode) {
				case Keys.Up:
				case Keys.Down:
				case Keys.Left:
				case Keys.Right:
					this.OnKeyDown(new KeyEventArgs(e.KeyData));
					break;
			}
		}

		//returns on invalid target; calls MyControl.TargetValidated if a wall is targeted, otherwise opens the targeting window
		//needs to be edited to consider WallSpace targets too
		internal void TargetWall(MouseEventArgs tE) {
			var gameTarget = (ITarget)GameState.BoardRef.LookForWall(selectedWallRegion.X, selectedWallRegion.Y) ?? selectedWallRegion;

			if (gameTarget != null && myUI.CurrentAimable.IsValidTargetType(gameTarget.ActiveTargetType)) {
				//MessageBox.Show("You are on wallspace " + OnWallSpace.X + " " + OnWallSpace.Y);
				var a = new TargetingEvent(gameTarget, myUI.myWizard);
				if (a.GetFlowControl() == Redirect.Proceed) {
					TargetValidated(gameTarget, gameTarget.ActiveTargetType);
				}

				return;
			}
		}

		private void TargetSquare(Square s) {
			if (s == null) {
				return;
			}

			if (myUI.CurrentAimable == null) {
				return;
			}

			var validTargets = s.LocatablesHere.Where(l => myUI.CurrentAimable.IsValidTarget(l));

			if (validTargets.Count() == 1) {
				TargetValidated(validTargets.First(), validTargets.First().ActiveTargetType);
			}
			else {
				var targetWindow1 = new TargetChooser(myUI, s, myUI.CurrentAimable) { StartPosition = FormStartPosition.Manual, Location = new Point(100, 100) };
				targetWindow1.Show();
				targetWindow1.ValidateTarget();
			}
		}

		private void TestLoSButton_Click(object sender, EventArgs e) {
			if (myUI.State != UIState.TestingLoS) {
				myUI.State = UIState.TestingLoS;
				maskedTextBox1.Enabled = true;
				maskedTextBox2.Enabled = true;
			}
			else {
				myUI.State = UIState.Previous;
				maskedTextBox1.Enabled = false;
				maskedTextBox2.Enabled = false;
			}
		}

		private void TestLosButton_Enter(object sender, EventArgs e) {
			Focus();
		}

		private void LoSCoordButton_Click(object sender, EventArgs e) {
			Graphics dc = CreateGraphics();
			Invalidate();
			Update();

			Pen LoSPen;
			//Trace.Assert(this.Left != 0 && this.Top != 0, );
			//Trace.Assert(false, "cursor at " + tSquare.Y + " ID of " + tSquare.ID);
			try {
				double testX = Double.Parse(maskedTextBox1.Text);
				double testY = Double.Parse(maskedTextBox2.Text);

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
			// ReSharper disable UnusedVariable.Compiler
			int i = 0;
			// ReSharper restore UnusedVariable.Compiler
		}

		private void ContinueButton_Click(object sender, EventArgs e) {
			ContinueButton.Enabled = false;

			if (myUI.State == UIState.CastQuery) {
				passedPriority = true;

				bool everyonePassed = true;
				foreach (UIControl uic in GameState.UiReference) {
					if (uic.myBoard.passedPriority == false) {
						everyonePassed = false;
						break;
					}
				}

				if (everyonePassed) {
					GameState.RunTheStack();
					foreach (Wizard w in GameState.Wizards) {
						w.myUI.myBoard.StackOSpellsRefresh();
						//w.myUI.myControl.ListOItemsRefresh();
						w.myUI.myBoard.passedPriority = false;
						if (GameState.TheStack.Count != 0) {
							myUI.State = UIState.CastQuery;
							ContinueButton.Enabled = true;
						}
						else {
							if (w.myUI.PreviousState == UIState.Locked) {
								w.myUI.State = UIState.Locked;
							}
							else {
								w.myUI.State = UIState.Normal;
							}
						}
					}
					GameState.RedrawAll();
					GameState.PriorityHolder = GameState.ActivePlayer.myUI;
				}
				else if (GameState.PriorityHolder == myUI) {
					GameState.PriorityHolder = GameState.NextPriorityHolder;
				}
			}
			else if (myUI.State == UIState.TurnComplete) {
				GameState.TurnCycle();
				foreach (Wizard w in GameState.Wizards) {
					w.myUI.myBoard.passedPriority = false;
				}
				GameState.PriorityHolder = GameState.ActivePlayer.myUI;
			}
			else if (myUI.State == UIState.CastingSpell || myUI.State == UIState.UsingItem) {
				TargetValidated(null, TargetTypes.None);
			}
		}

		internal void TargetValidated(ITarget tTarget, TargetTypes tTargetType) {
			if (myUI.CurrentAimable is ISpell) {
				TargetSpellValidated(tTarget, tTargetType);
			}
			else {
				TargetItemValidated(tTarget, tTargetType);
			}
		}

		internal void TargetSpellValidated(ITarget tTarget, TargetTypes tTargetType) {
			var spell = myUI.CurrentAimable as ISpell;
			spell.Caster = myUI.myWizard;
			spell.Target = tTarget;

			if (spell.ActiveSpellType == SpellType.Attack) {
				if (myUI.AttackedThisTurn == true) {
					CancelButton_Click(this, new EventArgs());
					return;
				}
				else {
					myUI.AttackedThisTurn = true;
				}
			}

			myUI.myWizard.HandRemove(spell.OriginalCard);

			deadCards.Add(spell.OriginalCard);

			foreach (ICard c in deadCards) {
				GameState.Discard.Add(c.RecursiveCopy());
			}

			deadCards.Clear();

			GameState.NewSpell(spell);
		}

		internal void TargetItemValidated(ITarget tTarget, TargetTypes tTargetType) {
			var item = myUI.CurrentAimable as IItem;
			item.Target = tTarget;

			if (item.Target is Wizard) {
				if (myUI.AttackedThisTurn) {
					CancelButton_Click(this, new EventArgs());
					return;
				}
				else {
					myUI.AttackedThisTurn = true;
				}
			}

			foreach (ICard c in deadCards) {
				GameState.Discard.Add(c.RecursiveCopy());
			}
			deadCards.Clear();

			GameState.NewSpell(item.UseItem());
		}

		private void CancelButton_Click(object sender, EventArgs e) {
			if (myUI.State == UIState.CastingSpell) {
				myUI.State = UIState.Normal;
			}

			myUI.myWizard.HandAddRange(deadCards);
			foreach (ICard c in deadCards) {
				GameState.Discard.Add(c);
			}
			deadCards.Clear();
			HandOCardsRefresh();
		}

		public void HandOCardsRefresh() {
			HandOCards.Items.Clear();

			foreach (ICard card in myUI.myWizard.Hand) {
				HandOCards.Items.Add(card);
			}

			CastButton.Enabled = false;

			HandCountLabel.Text = "Cards in Hand (" + HandOCards.Items.Count + ")";
		}

		private void CounteractButton_Click(object sender, EventArgs e) {
			if (StackOSpells.SelectedNode == null || myUI.State != UIState.CastQuery || HandOCards.SelectedItem == null) {
				return;
			}

			var spellToCast = ((Card<Cardable>)HandOCards.SelectedItem).WrappedCard as ISpell;

			if (spellToCast.IsValidCastingType(SpellType.Counteraction) == false) {
				return;
			}

			spellToCast.Caster = myUI.myWizard;

			var spellToTarget = StackOSpells.SelectedNode.Tag as ITarget;

			if (spellToCast.IsValidTargetParent(spellToTarget)) {
				myUI.CurrentAimable = spellToCast;
				myUI.CurrentAimable.Target = spellToTarget;

				deadCards.Add((ICard)HandOCards.SelectedItem);
				myUI.myWizard.HandRemove(HandOCards.SelectedItem as ICard);
				HandOCardsRefresh();

				TargetValidated(spellToTarget, TargetTypes.Spell); //this second parameter is never used!
			}
			else {
				spellToCast.Caster = null;
			}
		}

		private void EndTurnButton_Click(object sender, EventArgs e) {
			if (!myUI.myWizard.SettleHand()) {
				InstructionText.Text = "Discard cards until you are under your maximum hand size";
				return;
			}

			EndTurnButton.Enabled = false;		//TODO: Consider deleting this line altogether
			GameState.TurnCycle();
		}

		private void UseItemButton_Click(object sender, EventArgs e) {
			Object tempref = Inventory.SelectedItem;

			//begin error checking
			if (myUI.State != UIState.Normal || tempref == null || tempref is Number || tempref is Trap) {
				return;
			}

			if (myUI.AttackedThisTurn == true && (tempref as Item).IsOnlyValidTargetTypeForItem(TargetTypes.Wizard)) {
				MessageBox.Show("You already attacked this turn");
				return;
			}
			//end error checking

			var checkMyType = (tempref as IItem);

			myUI.CurrentAimable = checkMyType;

			myUI.State = UIState.UsingItem;

			if (myUI.CurrentAimable.ValidTargetTypes.Contains(TargetTypes.None)) {
				ContinueButton.Enabled = true;
			}

			if (checkMyType is INumberable) {
				NumberButton.Enabled = true;
				numberCardsLeft++;
			}
		}

		private void ShowInfoButton_Click(object sender, EventArgs e) {
			if (myUI.myInfo == null) {
				myUI.myInfo = new InfoPane(myUI);
				myUI.myInfo.StartPosition = FormStartPosition.Manual;
				myUI.myInfo.Location = new Point(myUI.myBoard.Location.X + 300, myUI.myBoard.Location.Y + 400);
				myUI.myInfo.Show();
			}
		}

		private void DiscardButton_Click(object sender, EventArgs e) {
			if (HandOCards.SelectedItem == null) {
				return;
			}

			myUI.State = UIState.Discarding;
			myUI.myWizard.TakeCard(HandOCards.SelectedItem as ICard);

			if (myUI.myWizard.CurrentHandSize <= myUI.myWizard.MaxHandSize) {
				EndTurnButton.Enabled = true;

				if (myUI.myWizard.CurrentHandSize < myUI.myWizard.MaxHandSize && myUI.myWizard.CardsDrawn < 2) {
					DrawButton.Enabled = true;

					myUI.myWizard.CardsDiscardedByChoice++;
				}

				if (myUI.myWizard.CardsDiscardedByChoice >= 2) {
					myUI.State = UIState.Drawing;
				}
			}

			HandOCardsRefresh();
		}

		private void DrawButton_Click(object sender, EventArgs e) {
			if (myUI.myWizard.CurrentHandSize < myUI.myWizard.MaxHandSize && myUI.myWizard.CardsDrawn < 2) {
				GameState.Deck.DealCards(myUI.myWizard, 1);
				myUI.State = UIState.Drawing;
			}

			if (myUI.myWizard.CurrentHandSize >= myUI.myWizard.MaxHandSize || myUI.myWizard.CardsDrawn >= 2) {
				DrawButton.Enabled = false;
			}

			HandOCardsRefresh();
		}

		private void NumberButton_Click(object sender, EventArgs e) {
			if (((Card<Cardable>)HandOCards.SelectedItem).WrappedCard is Number && myUI.State == UIState.CastingSpell) {
				if (myUI.CurrentAimable is INumberable && numberCardsLeft > 0) {
					(myUI.CurrentAimable as INumberable).CardValue = (((Card<Cardable>)HandOCards.SelectedItem).WrappedCard as Number).Value;

					numberCardsLeft--;
					
					if (numberCardsLeft < 1) {
						NumberButton.Enabled = false;
					}
				}
				
				deadCards.Add((ICard)HandOCards.SelectedItem);
				
				myUI.myWizard.HandRemove(HandOCards.SelectedItem as ICard);
				
				HandOCardsRefresh();
			}
		}

		private void DropItemButton_Click(object sender, EventArgs e) {
			myUI.myWizard.dropItem(Inventory.SelectedItem as IItem);
		}

		public void OnEvent(StateChangeEvent tEvent) {
			if (tEvent.IsAttempt || tEvent.EventTarget != myUI.myWizard) {
				return;
			}

			InstructionText.Text = string.Empty;

			//don't compare things to the "Previous" state literally!
			UIState testNewState;
			if (tEvent.NewState == UIState.Previous) {
				testNewState = myUI.PreviousState;
			}
			else {
				testNewState = tEvent.NewState;
			}

			//start by disabling buttons, then enable the ones used in the curent state
			EndTurnButton.Enabled = false;
			CastButton.Enabled = false;
			UseItemButton.Enabled = false;
			DrawButton.Enabled = false;
			DiscardButton.Enabled = false;
			DropItemButton.Enabled = false;
			ContinueButton.Enabled = false;
			CancelButton.Enabled = false;
			NumberButton.Enabled = false;
			DrawButton.Enabled = false;
			ContinueButton.BackColor = Color.LightYellow;

			//there are some duplicates here; it's organized this way for logical simplicity
			if (myUI.myWizard == GameState.ActivePlayer) {
				if (tEvent.OldState == UIState.CastingSpell) {
					numberCardsLeft = 0;
				}

				//enable new buttons
				if (testNewState == UIState.Normal) {
					DiscardButton.Enabled = true;
					EndTurnButton.Enabled = true;
				}

				if (testNewState == UIState.Drawing) {
					EndTurnButton.Enabled = true;

					if (myUI.myWizard.CardsDrawn < 2) {
						DrawButton.Enabled = true;
					}
				}

				if (testNewState == UIState.CastingSpell) {
					CancelButton.Enabled = true;
				}

				if (testNewState == UIState.TurnComplete) {
					EndTurnButton.Enabled = true;
				}

				if (testNewState == UIState.Normal || testNewState == UIState.Discarding) {
					if (myUI.myWizard.CurrentHandSize < myUI.myWizard.MaxHandSize) {
						DrawButton.Enabled = true;
					}

					DiscardButton.Enabled = true;
				}
			}

			if (testNewState == UIState.CastQuery) {
				ContinueButton.BackColor = Color.LightSalmon;

				ContinueTimer.Value = 100;

				Task.Factory.StartNew(UpdateContinueTimer);
			}
			else {
				ContinueTimer.Value = 0;
			}
		}

		private void UpdateContinueTimer() {
			var startTime = System.DateTime.Now;

			while (ContinueTimer.Value > 0) {
			
				ContinueTimer.Invoke(new Action(UpdateContinueTimerInner));

				Thread.Sleep(100);
			}

			ContinueTimer.Invoke(new Action(ClickContinueInner));
		}

		private void ClickContinueInner() {
			ContinueButton_Click(null, null);
		}

		private void UpdateContinueTimerInner() {
			ContinueTimer.Increment(-1);
		}

		public void OnEvent(ItemRevealEvent tEvent) {
			if (tEvent.EventTarget == myUI.myWizard) {
				ListOItemsRefresh();
			}
		}

		public void OnEvent(CastEvent tEvent) {
			passedPriority = false;
			ContinueButton.Enabled = true;
		}

		public void OnEvent(ItemUseEvent tEvent) {
			passedPriority = false;
			ContinueButton.Enabled = true;
		}

		public void OnEvent(NewEffectEvent tEvent) {
			if (tEvent.IsAttempt == false) {
				if (tEvent.myEffect.target == myUI.myWizard) {
					MessageBox.Show("New effects have entered the stack that target you.");
					RefreshAll();
				}
			}
		}

		public void OnEvent(HealthChangeEvent tEvent) {
			if (tEvent.EventTarget == myUI.myWizard) {
				CurrentHealthLabel.Text = "Health: " + (myUI.myWizard.hit_points + tEvent.Amount);
			}
		}

		public void RefreshAll() {
			HandOCardsRefresh();

			StackOSpellsRefresh();

			ListOItemsRefresh();
		}

		public void ListOItemsRefresh() {
			Inventory.Items.Clear();
			foreach (ICarriable i in myUI.myWizard.Inventory) {
				Inventory.Items.Add(i);
			}
			UseItemButton.Enabled = false;
		}

		public void StackOSpellsRefresh() {
			//the stack is organized by spell, but individual effects can be targeted. Hence the tree structure
			StackOSpells.Nodes.Clear();
			foreach (IStackable s in GameState.TheStack) {
				var tn = new TreeNode();
				tn.Tag = s;
				tn.Text = s.ToString();
				StackOSpells.Nodes.Add(tn);

				if (!(s is ISpellOrItemUsage)) {
					return;
				}

				foreach (Effect e in (s as ISpellOrItemUsage).EffectsWaiting) {
					var child = new TreeNode();
					child.Tag = e;
					child.Text = e.ToString();
					tn.Nodes.Add(child);
				}
			}

			CounteractButton.Enabled = false;

			ContinueButton.Enabled = StackOSpells.Nodes.Count > 0;
		}

		private void HandOCards_SelectedIndexChanged(object sender, EventArgs e) {
			if (myUI.State == UIState.CastQuery) {
				ContinueTimer.Value = 100;
			}

			CastButton.Enabled = false;

			if (HandOCards.SelectedItem != null) {
				if (myUI.myWizard == GameState.ActivePlayer) {
					if (myUI.State == UIState.Normal) {

						dynamic lorem = HandOCards.SelectedItem;
						if (lorem.WrappedCard is Spell) {
							if ((lorem.WrappedCard as Spell).IsOnlyValidCastingType(SpellType.Counteraction)) {
								CastButton.Enabled = GameState.TheStack.Count != 0;
							}
							else {
								CastButton.Enabled = true;
							}
						}
					}
				}
			}
		}

		private void OnMouseMove(object sender, MouseEventArgs e) {
			if (e.X == lastX && e.Y == lastY) {
				return;
			}
			else {
				lastX = e.X;
				lastY = e.Y;
			}

			if (sender == HandOCards) {
				var listBox = (ListBox)sender;

				var point = new Point(e.X, e.Y);

				int hoverIndex = listBox.IndexFromPoint(point);

				if (hoverIndex >= 0
				&& hoverIndex < listBox.Items.Count
				&& point.Y >= 0
				&& point.Y <= listBox.ClientRectangle.Height) {
					selectedCard = (listBox.Items[hoverIndex] as ICard);


					if (selectedCard.WrappedCard is ISpell) {
						var castingTypes = String.Join("/", (selectedCard.WrappedCard as ISpell).ValidCastingTypes.Select(spellType => spellType.ToString()).ToArray());

						toolTip1.Show(castingTypes, listBox, e.X + 20, e.Y + 10);		//this is necessary to card preview to work
					}
					else {
						toolTip1.Show(selectedCard.WrappedCard.GetType().ToString().Replace("WizWar1.", string.Empty), listBox, e.X + 20, e.Y + 10);
					}

					cardPreview1.Location = new Point(265, 40);		//relative to board

					cardPreview1.CardText.Text = selectedCard.Description;
					cardPreview1.CardTitle.Text = selectedCard.Name;

					cardPreview1.Visible = true;

					HandOCards.Focus();
				}
			}
			else {
				toolTip1.Hide(HandOCards);

				cardPreview1.Visible = false;
			}
		}

		private void CastButton_Click(object sender, EventArgs e) {
			var tempref = ((dynamic)HandOCards.SelectedItem).WrappedCard;

			//begin error checking
			if (myUI.State != UIState.Normal || !(tempref is ISpell)) {
				return;
			}

			var checkMyType = (tempref as ISpell);

			if (myUI.State != UIState.CastQuery && checkMyType.IsOnlyValidCastingType(SpellType.Counteraction)) {
				return;
			}

			if (myUI.AttackedThisTurn == true && checkMyType.IsOnlyValidCastingType(SpellType.Attack)) {
				return;
			}
			//end error checking

			myUI.CurrentAimable = checkMyType;

			myUI.State = UIState.CastingSpell;

			if (myUI.CurrentAimable.ValidTargetTypes.Contains(TargetTypes.None)) {
				ContinueButton.Enabled = true;
			}

			if (checkMyType is INumberable) {
				NumberButton.Enabled = true;
				
				numberCardsLeft++;
			}
		}

		private void CastAtSelectedSquare(Square s) {
			if (GameState.InitialUltimatum(Event.New<CastEvent>(true, new CastEvent(myUI.CurrentAimable as ISpell))) == Redirect.Proceed) {
				//if (TestLoSButton.ContainsMouse(new Point(e.X, e.Y)) == false) {
				//	maskedTextBox1.Clear();
				//	maskedTextBox2.Clear();
				//}

				if ((myUI.CurrentAimable.IsValidTargetType(TargetTypes.Wall) || myUI.CurrentAimable.IsValidTargetType(TargetTypes.WallSpace)) && selectedWallRegion != null) {
					if (GameState.BoardRef.TestLoSToWall(s.X - GameState.ActivePlayer.X, s.Y - GameState.ActivePlayer.Y, selectedWallRegion) == false) {
						MessageBox.Show("No line of sight to wall");
					}
					else {
						MessageBox.Show("Yes line of sight to wall");
						TargetWall(null);
					}
				}
				else if (GameState.BoardRef.TestLoSNew(s.X - GameState.ActivePlayer.X, s.Y - GameState.ActivePlayer.Y, myUI) == false) {
					MessageBox.Show("No line of sight");
				}
				else if (myUI.CurrentAimable != null) {
					myUI.CurrentAimable.ShotDirection = LibraryFunctions.VectorToAngle(s.X - GameState.ActivePlayer.X, GameState.ActivePlayer.Y - s.Y);

					//this function call does all the work
					TargetSquare(s);
				}
			}
		}

		internal void StartCastingFireball(Square targetSquare) {
			HandOCards.SelectedIndex = 0;

			CastButton_Click(new object(), null);

			CastAtSelectedSquare(targetSquare);
		}
	}
}