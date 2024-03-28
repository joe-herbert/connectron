using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Connectron
{
	public partial class Game : Form
	{
		public Game()
		{
			InitializeComponent();
		}

		//define variables
		int[] scores = { 0, 0, -1, -1, -1, -1, -1, -1, -1, -1 };
		int[] roundBombUsed = { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
		List<Label> lblColors = new List<Label>();
		List<Label> lblNames = new List<Label>();
		List<List<Color>> lstMoves = new List<List<Color>>();
		List<List<Color>> currBoard = new List<List<Color>>();
		List<Color> winningColors = new List<Color>();
		int movesPlayed = 0;
		int noPlayers = 2;
		bool allCPU = false;
		Color currColor;
		int currPlayer = 0;
		int currRound = 0;
		bool bombCounter = false;

		private void Game_Load(object sender, EventArgs e)
		{
			MinimumSize = new Size(740, 550);
			Game_SizeChanged(sender, e);

			//if the user entered less than 2 players set noPlayers to 2 as there will always be at least 2 players
			noPlayers = Settings.noPlayers;
			if (noPlayers < 2)
			{
				noPlayers = 2;
			}
			//reset all scores
			for (int i = 0; i < noPlayers; i++)
			{
				scores[i] = 0;
			}

			//check if all players are computers
			allCPU = true;
			for (int i = 0; i < noPlayers; i++)
			{
				if (!Settings.cpu[i])
				{
					allCPU = false;
					break;
				}
			}

			LoadGrid();

			//set board to lightgray to show it's disabled
			foreach (DataGridViewRow row in tblGrid.Rows)
			{
				foreach (DataGridViewCell cell in row.Cells)
				{
					if (cell.Style.BackColor == Color.Empty)
					{
						cell.Style.BackColor = Color.LightGray;
					}
				}
			}

			//send a message on how to change alliances
			if (Settings.alliances)
			{
				MessageBox.Show("Note: You can change/add alliances at the start of each round (while the board is still empty) by clicking on the name of the person you want to change the alliances for.");
			}
		}

		private void Game_SizeChanged(object sender, EventArgs e)
		{
			//make the board as big a square as possible
			int newHeight = (this.Height - 59);
			int newWidth = (this.Width - 340);
			if ((newHeight) < (newWidth))
			{
				pnlGrid.Size = new Size(newHeight, newHeight);
			}
			else
			{
				pnlGrid.Size = new Size(newWidth, newWidth);
			}
			LoadGrid();
		}

		public void LoadGrid()
		{
			//set up board with all cells the right size
			tblGrid.Width = pnlGrid.Width - 2;
			tblGrid.Height = pnlGrid.Height - 2;
			tblGrid.ColumnCount = Settings.widthNo;
			tblGrid.RowCount = Settings.heightNo;
			int cellWidth = (int)Math.Floor((double)(pnlGrid.Width - 2) / Settings.widthNo);
			int cellHeight = (int)Math.Floor((double)(pnlGrid.Height - 2) / Settings.heightNo);
			foreach (DataGridViewColumn col in tblGrid.Columns)
			{
				col.Width = cellWidth;
			}
			foreach (DataGridViewRow row in tblGrid.Rows)
			{
				row.Height = cellHeight;
			}

			//show bomb and undo buttons if necessary
			if (Settings.bomb && !allCPU)
			{
				btnBomb.Visible = true;
			}
			else
			{
				btnBomb.Visible = false;
			}
			if (allCPU)
			{
				btnUndo.Visible = false;
			}
		}

		//don't show a selection
		private void TblGrid_SelectionChanged(object sender, EventArgs e)
		{
			tblGrid.ClearSelection();
		}

		private void LblName_Click(object sender, EventArgs e)
		{
			//allow the user to edit alliances if it's at the right time
			if (!btnUndo.Enabled && Settings.alliances)
			{
				Label sndr = (Label)sender;
				Settings.allianceSender = Convert.ToInt32(sndr.Name.Replace("lblName", ""));
				Alliances allianceForm = new Alliances();
				allianceForm.Show();
			}
		}

		private void BtnStart_Click(object sender, EventArgs e)
		{
			//clear variables
			currRound = 0;
			lblColors.Clear();
			lblNames.Clear();
			pnlScores.Controls.Clear();
			lstMoves.Clear();

			//reset noPlayers in case anything's changed
			noPlayers = Settings.noPlayers;
			if (noPlayers < 2)
			{
				noPlayers = 2;
			}


			//foreach character reset bomb info and scores and add name and color to panel on RHS
			for (int i = 0; i < 10; i++)
			{
				roundBombUsed[i] = -1;
				if (Settings.names[i] != "")
				{
					scores[i] = 0;
					Label lblColor = new Label
					{
						BackColor = Settings.colorArr[i],
						Text = "",
						AutoSize = false,
						Size = new Size(40, 40),
						Location = new Point(0, (lblColors.Count) * 42)
					};
					lblColors.Add(lblColor);
					Label lblName = new Label
					{
						Text = Settings.names[i] + ": 0",
						TextAlign = ContentAlignment.MiddleCenter,
						AutoSize = false,
						Size = new Size(pnlScores.Width - 42, 40),
						Location = new Point(42, (lblNames.Count) * 42),
						Name = "lblName" + i
					};
					lblName.Click += new EventHandler(LblName_Click);
					lblNames.Add(lblName);
					pnlScores.Controls.Add(lblColors[lblColors.Count - 1]);
					pnlScores.Controls.Add(lblNames[lblNames.Count - 1]);
				}
			}

			//if they're all computers start playing on background thread, else start on main thread
			if (allCPU)
			{
				BackgroundWorker backgroundWorker = new BackgroundWorker();
				backgroundWorker.DoWork += new DoWorkEventHandler(BackgroundWorker_DoWork);
				backgroundWorker.RunWorkerAsync("");
			}
			else
			{
				StartRound();
			}
		}

		private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			StartRound();
		}

		private void StartRound()
		{
			//set everything to first player
			currColor = Settings.colorArr[0];
			currPlayer = 0;
			if (!allCPU)
			{
				lblNames[currPlayer].Font = new Font(lblNames[currPlayer].Font, FontStyle.Regular);
				lblNames[currPlayer].Font = new Font(lblNames[currPlayer].Font, FontStyle.Bold);
			}
			movesPlayed = 0;
			//update labels to show correct info and correct fonts
			foreach (Label lbl in lblColors)
			{
				if (lbl.InvokeRequired)
				{
					lbl.BeginInvoke((MethodInvoker)delegate () { lbl.Update(); });
				}
				else
				{
					lbl.Update();
				}
			}
			foreach (Label lbl in lblNames)
			{
				if (lbl.InvokeRequired)
				{
					lbl.BeginInvoke((MethodInvoker)delegate () { lbl.Update(); });
				}
				else
				{
					lbl.Update();
				}
			}

			//clear list of moves and list of current board and reset them to empty
			lstMoves.Clear();
			currBoard.Clear();
			List<Color> lstColors = new List<Color>();
			for (int r = 0; r < Settings.heightNo; r++)
			{
				currBoard.Add(new List<Color>());
				for (int c = 0; c < Settings.widthNo; c++)
				{
					currBoard[r].Add(Color.Empty);
					lstColors.Add(Color.Empty);
				}
			}
			UpdateTblGrid();
			lstMoves.Add(lstColors);

			//reset roundBombUsed array
			for (int i = 0; i < 10; i++)
			{
				roundBombUsed[i] = -1;
			}

			//enable/disable controls for during game play
			if (tblGrid.InvokeRequired)
			{
				tblGrid.BeginInvoke((MethodInvoker)delegate () { tblGrid.Enabled = true; });
			}
			else
			{
				tblGrid.Enabled = true;
			}
			if (btnStart.InvokeRequired)
			{
				btnStart.BeginInvoke((MethodInvoker)delegate () { btnStart.Enabled = false; });
			}
			else
			{
				btnStart.Enabled = false;
			}
			if (btnEnd.InvokeRequired)
			{
				btnEnd.BeginInvoke((MethodInvoker)delegate () { btnEnd.Enabled = true; });
			}
			else
			{
				btnEnd.Enabled = true;
			}
			if (btnUndo.InvokeRequired)
			{
				btnUndo.BeginInvoke((MethodInvoker)delegate () { btnUndo.Enabled = false; });
			}
			else
			{
				btnUndo.Enabled = false;
			}
			if (btnBomb.InvokeRequired)
			{
				btnBomb.BeginInvoke((MethodInvoker)delegate () { btnBomb.Enabled = true; });
			}
			else
			{
				btnBomb.Enabled = true;
			}

			//if all players are computers start playing
			if (allCPU)
			{
				PlayCPU();
			}
		}

		private void PlayCPU()
		{
			bool played = false;
			bool won = true;
			//get lowest cells (aka cells where a counter could be played)
			List<Cell> lowestCells = new List<Cell>();
			for (int r = 0; r < Settings.heightNo; r++)
			{
				for (int c = 0; c < Settings.widthNo; c++)
				{

					try
					{
						if (currBoard[r + 1][c] != Color.Empty)
						{
							Cell cell = new Cell
							{
								BackColor = currBoard[r][c],
								RowIndex = r,
								ColumnIndex = c
							};
							lowestCells.Add(cell);
						}
					}
					catch
					{
						Cell cell = new Cell
						{
							BackColor = currBoard[r][c],
							RowIndex = r,
							ColumnIndex = c
						};
						lowestCells.Add(cell);
					}
				}
			}
			//foreach of the lowest cells
			foreach (Cell cell in lowestCells)
			{
				if (cell.BackColor == Color.Empty)
				{

					//find if the current player would win by playing here
					won = PlayCounter(cell.ColumnIndex, false);
					Undo(false);
					//if they would win, play it with animations and allowing win
					if (won)
					{
						played = true;
						PlayCounter(cell.ColumnIndex, true);
						break;
					}
					//backup current player info
					Color backupColor = currColor;
					int backupPlayerNum = currPlayer;
					//foreach player
					for (int i = 0; i < noPlayers; i++)
					{
						//if not the current player
						if (backupPlayerNum != i)
						{
							//find if this player would win by playing here
							currColor = Settings.colorArr[i];
							currPlayer = i;
							won = PlayCounter(cell.ColumnIndex, false);
							Undo(false);
							//reset current player info
							currColor = backupColor;
							currPlayer = backupPlayerNum;
							//if player i would have won, play here with the current player to block
							if (won)
							{
								played = true;
								PlayCounter(cell.ColumnIndex, true);
								break;
							}
						}
					}
					if (won)
					{
						break;
					}
				}
			}
			//if a counter hasn't been played, play in a random position
			if (!played)
			{
				Random rnd = new Random();
				int col = 0;
				//generate a random column to play in, provided the column isn't full
				do
				{
					col = rnd.Next(0, Settings.widthNo);
				} while (currBoard[0][col] != Color.Empty);
				PlayCounter(col, true);
			}
			try
			{
				lstMoves.RemoveAt(lstMoves.Count - 2);
			}
			catch { }
		}

		private void TblGrid_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			//when cell is clicked play the counter
			PlayCounter(e.ColumnIndex, true);
		}

		private bool PlayCounter(int col, bool allowWin)
		{
			bool placeCounter = false;
			//work out which column it needs to go in and if the counter can be played in that column
			if (Settings.overflow)
			{
				if (currBoard[1][col] == Color.Empty)
				{
					placeCounter = true;
				}
				else if (Settings.overflow)
				{
					try
					{
						if (currBoard[1][col - 1] == Color.Empty)
						{
							col--;
							placeCounter = true;
						}
					}
					catch { }
					if (!placeCounter)
					{
						try
						{
							if (currBoard[1][col + 1] == Color.Empty)
							{
								col++;
								placeCounter = true;
							}
						}
						catch { }
					}
					if (!placeCounter && currBoard[0][col] == Color.Empty)
					{
						placeCounter = true;
					}
				}
			}
			else
			{
				if (currBoard[0][col] == Color.Empty)
				{
					placeCounter = true;
				}
			}
			
			//if the counter can be played
			if (placeCounter)
			{
				//play counter at top of board
				currBoard[0][col] = currColor;
				//get row it finished on after moving down
				int row = MoveCountersDownInt(col, allowWin);
				bool won = true;
				//if they're playing a bomb
				if (bombCounter)
				{
					//set all surrounding cells to black to show they've been destroyed
					//everything's in try catch statements in case the bomb counter was played against an edge, in which case some cells would be out of bounds of the currBoard list because they don't exist
					try
					{
						currBoard[row - 1][col - 1] = Color.Black;
					}
					catch { }
					try
					{
						currBoard[row][col - 1] = Color.Black;
					}
					catch { }
					try
					{
						currBoard[row + 1][col - 1] = Color.Black;
					}
					catch { }
					try
					{
						currBoard[row + 1][col] = Color.Black;
					}
					catch { }
					try
					{
						currBoard[row + 1][col + 1] = Color.Black;
					}
					catch { }
					try
					{
						currBoard[row][col + 1] = Color.Black;
					}
					catch { }
					try
					{
						currBoard[row - 1][col + 1] = Color.Black;
					}
					catch { }
					try
					{
						currBoard[row - 1][col] = Color.Black;
					}
					catch { }
					try
					{
						currBoard[row][col] = Color.Black;
					}
					catch { }
					UpdateTblGrid();
					//wait half a second before clearing the cells
					System.Threading.Thread.Sleep(500);

					//clear all cells destroyed by the bomb
					try
					{
						currBoard[row - 1][col - 1] = Color.Empty;
					}
					catch { }
					try
					{
						currBoard[row][col - 1] = Color.Empty;
					}
					catch { }
					try
					{
						currBoard[row + 1][col - 1] = Color.Empty;
					}
					catch { }
					try
					{
						currBoard[row + 1][col] = Color.Empty;
					}
					catch { }
					try
					{
						currBoard[row + 1][col + 1] = Color.Empty;
					}
					catch { }
					try
					{
						currBoard[row][col + 1] = Color.Empty;
					}
					catch { }
					try
					{
						currBoard[row - 1][col + 1] = Color.Empty;
					}
					catch { }
					try
					{
						currBoard[row - 1][col] = Color.Empty;
					}
					catch { }
					try
					{
						currBoard[row][col] = Color.Empty;
					}
					catch { }
					UpdateTblGrid();


					//move all counters to bottom
					MoveCountersDown(col - 1);
					MoveCountersDown(col);
					MoveCountersDown(col + 1);
					UpdateTblGrid();
					
					//mark bomb as used for this player
					btnBomb.BackColor = Color.LightGray;
					bombCounter = false;
					roundBombUsed[currPlayer] = lstMoves.Count - 1;

					//check each cell that has been destroyed for wins if it is not empty
					for (int c = 0; c < Settings.noPlayers; c++)
					{
						won = false;
						try
						{
							if (currBoard[row - 1][col - 1] != Color.Empty)
							{
								won = CheckWon(row - 1, col - 1, Settings.colorArr[c], true, allowWin, 612);
							}
						} catch { }
						if (!won)
						{
							try
							{
								if (currBoard[row][col - 1] != Color.Empty)
								{
									won = CheckWon(row, col - 1, Settings.colorArr[c], true, allowWin, 621);
								}
							} catch { }
							if (!won)
							{
								try
								{
									if (currBoard[row + 1][col - 1] != Color.Empty)
									{
										won = CheckWon(row + 1, col - 1, Settings.colorArr[c], true, allowWin, 630);
									}
								} catch { }
								if (!won)
								{
									try
									{
										if (currBoard[row + 1][col + 1] != Color.Empty)
										{
											won = CheckWon(row + 1, col + 1, Settings.colorArr[c], true, allowWin, 639);
										}
									} catch { }
									if (!won)
									{
										try
										{
											if (currBoard[row][col + 1] != Color.Empty)
											{
												won = CheckWon(row, col + 1, Settings.colorArr[c], true, allowWin, 648);
											}
										} catch { }
										if (!won)
										{
											try
											{
												if (currBoard[row - 1][col + 1] != Color.Empty)
												{
													won = CheckWon(row - 1, col + 1, Settings.colorArr[c], true, allowWin, 657);
												}
											} catch { }
										}
									}
								}
							}
						}
					}

				}
				else
				{
					//check for solitaires and check for wins
					if (Settings.solitaire)
					{
						won = CheckSolitaires(row, col, allowWin);
					}
					else
					{
						won = CheckWon(row, col, currColor, false, allowWin, 679);
					}
				}
				//save board
				List<Color> lstColors = new List<Color>();
				foreach (List<Color> lstRow in currBoard)
				{
					foreach (Color cell in lstRow)
					{
						lstColors.Add(cell);
					}
				}
				lstMoves.Add(lstColors);
				//if the user won disable the bomb button
				if (won)
				{
					if (btnBomb.InvokeRequired)
					{
						btnBomb.BeginInvoke((MethodInvoker)delegate () { btnBomb.Enabled = false; });
					}
					else
					{
						btnBomb.Enabled = false;
					}
				}
				else
				{
					//enable undo button
					if (btnUndo.InvokeRequired)
					{
						btnUndo.BeginInvoke((MethodInvoker)delegate () { btnUndo.Enabled = true; });
					}
					else
					{
						btnUndo.Enabled = true;
					}
					//if they can win and this function is not just being used by the computer to check if they would win
					if (allowWin)
					{
						//move has been played
						MovePlayed();
					}
				}
				return won;
			}
			return false;
		}

		private void MoveCountersDown(int col)
		{
			//move all counters in a column down without animations
			for (int r = Settings.heightNo - 1; r >= 0; r--)
			{
				try
				{
					if (currBoard[r][col] != Color.Empty)
					{
						for (int c = Settings.heightNo - 1; c >= 0; c--)
						{
							if (currBoard[c][col] == Color.Empty)
							{
								if (c > r)
								{
									currBoard[c][col] = currBoard[r][col];
									currBoard[r][col] = Color.Empty;
									break;
								}
							}
						}
					}
				}
				catch { }
			}
		}

		private int MoveCountersDownInt(int col, bool animate)
		{
			//move top counter down as far as it can go
			int row = 0;
			try
			{
				//while the cell below is empty keep moving it down with a 100 ms wait between each movement
				while (currBoard[row + 1][col] == Color.Empty)
				{
					//show and wait only if animate is on
					if (animate)
					{
						UpdateTblGrid();
						System.Threading.Thread.Sleep(100);
					}
					currBoard[row + 1][col] = currBoard[row][col];
					currBoard[row][col] = Color.Empty;
					row++;
				}
			}
			catch { }
			if (animate)
			{
				UpdateTblGrid();
			}
			//return the row it ended up on
			return row;
		}

		private bool CheckWon(int cellR, int cellC, Color color, bool checkVertical, bool allowWin, int sndr)
		{
			bool won = true;
			//check horizontally
			won = CheckHorizontally(cellR, cellC, color);
			if (won)
			{
				if (allowWin)
				{
					//get winners names
					List<int> winners = new List<int>();
					string winnersNames = "";
					foreach (Color player in winningColors)
					{
						winnersNames += Settings.names[Array.IndexOf(Settings.colorArr, player)] + ", ";
						winners.Add(Array.IndexOf(Settings.colorArr, player));
					}
					winnersNames = winnersNames.Remove(winnersNames.Length - 2);
					winnersNames = ReplaceLastOccurrence(winnersNames, ", ", " and ");
					if (Settings.noPlayers != 0)
					{
						//tell the user who won
						MessageBox.Show(winnersNames + " won horizontally!");
					}
					WonRound(winners);
				}
			}
			else
			{
				//check vertically
				won = CheckVertically(cellR, cellC, color, checkVertical);
				if (won)
				{
					if (allowWin)
					{
						//get winners names
						List<int> winners = new List<int>();
						string winnersNames = "";
						foreach (Color player in winningColors)
						{
							winnersNames += Settings.names[Array.IndexOf(Settings.colorArr, player)] + ", ";
							winners.Add(Array.IndexOf(Settings.colorArr, player));
						}
						winnersNames = winnersNames.Remove(winnersNames.Length - 2);
						winnersNames = ReplaceLastOccurrence(winnersNames, ", ", " and ");
						if (Settings.noPlayers != 0)
						{
							//tell the user who won
							MessageBox.Show(winnersNames + " won vertically!");
						}
						WonRound(winners);
					}
				}
				else
				{
					//check diagonally top left to bottom right
					won = CheckDiagonallyTLBR(cellR, cellC, color);
					if (won)
					{
						if (allowWin)
						{
							//get winners names
							List<int> winners = new List<int>();
							string winnersNames = "";
							foreach (Color player in winningColors)
							{
								winnersNames += Settings.names[Array.IndexOf(Settings.colorArr, player)] + ", ";
								winners.Add(Array.IndexOf(Settings.colorArr, player));
							}
							winnersNames = winnersNames.Remove(winnersNames.Length - 2);
							winnersNames = ReplaceLastOccurrence(winnersNames, ", ", " and ");
							if (Settings.noPlayers != 0)
							{
								//tell the user who won
								MessageBox.Show(winnersNames + " won diagonally top left to bottom right!");
							}
							WonRound(winners);
						}
					}
					else
					{
						//check diagonally top right to bottom left
						won = CheckDiagonallyTRBL(cellR, cellC, color);
						if (won)
						{
							if (allowWin)
							{
								//get winners names
								List<int> winners = new List<int>();
								string winnersNames = "";
								foreach (Color player in winningColors)
								{
									winnersNames += Settings.names[Array.IndexOf(Settings.colorArr, player)] + ", ";
									winners.Add(Array.IndexOf(Settings.colorArr, player));
								}
								winnersNames = winnersNames.Remove(winnersNames.Length - 2);
								winnersNames = ReplaceLastOccurrence(winnersNames, ", ", " and ");
								if (Settings.noPlayers != 0) 
								{
									//tell the user who won
									MessageBox.Show(winnersNames + " won diagonally top right to bottom left!");
								}
								WonRound(winners);
							}
						}
					}
				}
			}
			return won;
		}

		private bool CheckHorizontally(int cellR, int cellC, Color color)
		{
			try
			{
				//if cells on both sides are wrong color return false
				if (!Settings.validColors[Array.IndexOf(Settings.colorArr, color)].Contains(currBoard[cellR][cellC - 1]) && !Settings.validColors[Array.IndexOf(Settings.colorArr, color)].Contains(currBoard[cellR][cellC + 1]))
				{
					return false;
				}
			}
			catch { }
			winningColors.Clear();
			bool won = true;
			int earliestCol = cellC;
			//find the furthest left column which is part of a correct color line with this cell
			for (int c = 0; c < Settings.winLength; c++)
			{
				try
				{
					if (!Settings.validColors[Array.IndexOf(Settings.colorArr, color)].Contains(currBoard[cellR][cellC - c]))
					{
						earliestCol = (cellC - c) + 1;
						break;
					}
					if (c == (Settings.winLength - 1))
					{
						earliestCol = (cellC - c);
						break;
					}
				}
				catch
				{
					earliestCol = (cellC - c) + 1;
					break;
				}
			}
			int winLength = Settings.winLength;
			//work right until either they've won or you reach a color that isn't valid
			for (int c = 0; c < winLength; c++)
			{
				try
				{
					//if invalid color
					if (!Settings.validColors[Array.IndexOf(Settings.colorArr, color)].Contains(currBoard[cellR][earliestCol + c]))
					{
						won = false;
						break;
					}
					else
					{
						//add to winning colors if not already in there
						if (!winningColors.Contains(currBoard[cellR][earliestCol + c]))
						{
							winningColors.Add(currBoard[cellR][earliestCol + c]);
						}
						if (Settings.corners)
						{
							//if in the corner decrease the winlength by the necessary amount
							if ((cellR == 0 && (earliestCol + c) == 0) || (cellR == 0 && (earliestCol + c) == (Settings.widthNo - 1)) || (cellR == (Settings.heightNo - 1) && (earliestCol + c) == 0) || (cellR == (Settings.heightNo - 1) && (earliestCol + c) == (Settings.widthNo - 1)))
							{
								if (Settings.winLength >= 7)
								{
									winLength -= 2;
								}
								else
								{
									winLength--;
								}
							}
						}
					}
				}
				catch
				{
					if (earliestCol != -1)
					{
						won = false;
						break;
					}
				}
			}
			return won;
		}

		private bool CheckVertically(int cellR, int cellC, Color color, bool checkVertical)
		{
			try
			{
				//if cells on both sides are wrong color return false
				if (!Settings.validColors[Array.IndexOf(Settings.colorArr, color)].Contains(currBoard[cellR - 1][cellC]) && !Settings.validColors[Array.IndexOf(Settings.colorArr, color)].Contains(currBoard[cellR + 1][cellC]))
				{
					return false;
				}
			}
			catch { }
			winningColors.Clear();
			bool won = true;
			int col = cellC;
			int earliestR = cellR;
			if (checkVertical)
			{
				//find the highest row which is part of a correct color line with this cell
				for (int c = 0; c < Settings.winLength; c++)
				{
					try
					{
						if (!Settings.validColors[Array.IndexOf(Settings.colorArr, color)].Contains(currBoard[cellR - c][cellC]))
						{
							earliestR = (cellR - c) + 1;
							break;
						}
						if (c == (Settings.winLength - 1))
						{
							earliestR = (cellR - c);
							break;
						}
					}
					catch
					{
						earliestR = (cellR - c) + 1;
						break;
					}
				}
			}
			int winLength = Settings.winLength;
			//work down until either they've won or you reach a color that isn't valid
			for (int c = 0; c < winLength; c++)
			{
				try
				{
					//if invalid color
					if (!Settings.validColors[Array.IndexOf(Settings.colorArr, color)].Contains(currBoard[earliestR + c][col]))
					{
						won = false;
						break;
					}
					else
					{
						//add to winning colors if not already in there
						if (!winningColors.Contains(currBoard[earliestR + c][col]))
						{
							winningColors.Add(currBoard[earliestR + c][col]);
						}
						if (Settings.corners)
						{
							//if in the corner decrease the winlength by the necessary amount
							if (((earliestR + c) == 0 && col == 0) || ((earliestR + c) == 0 && col == (Settings.widthNo - 1)) || ((earliestR + c) == (Settings.heightNo - 1) && col == 0) || ((earliestR + c) == (Settings.heightNo - 1) && col == (Settings.widthNo - 1)))
							{
								if (Settings.winLength >= 7)
								{
									winLength -= 2;
								}
								else
								{
									winLength--;
								}
							}
						}
					}
				}
				catch
				{
					won = false;
					break;
				}
			}
			return won;
		}

		private bool CheckDiagonallyTLBR(int cellR, int cellC, Color color)
		{
			try
			{
				//if cells on both sides are wrong color return false
				if (!Settings.validColors[Array.IndexOf(Settings.colorArr, color)].Contains(currBoard[cellR - 1][cellC - 1]) && !Settings.validColors[Array.IndexOf(Settings.colorArr, color)].Contains(currBoard[cellR + 1][cellC + 1]))
				{
					return false;
				}
			}
			catch { }
			winningColors.Clear();
			bool won = true;
			int earliestC = 0;
			int earliestR = 0;
			//find the furthest top left cell which is part of a correct color line with this cell
			for (int c = 0; c < Settings.winLength; c++)
			{
				try
				{
					if (!Settings.validColors[Array.IndexOf(Settings.colorArr, color)].Contains(currBoard[cellR - c][cellC - c]))
					{
						earliestC = (cellC - c) + 1;
						earliestR = (cellR - c) + 1;
						break;
					}
					if (c == (Settings.winLength - 1))
					{
						earliestC = (cellC - c);
						earliestR = (cellR - c);
						break;
					}
				}
				catch
				{
					earliestC = (cellC - c) + 1;
					earliestR = (cellR - c) + 1;
					break;
				}
			}
			int winLength = Settings.winLength;
			//work right and down until either they've won or you reach a color that isn't valid
			for (int c = 0; c < winLength; c++)
			{
				try
				{
					//if invalid color
					if (!Settings.validColors[Array.IndexOf(Settings.colorArr, color)].Contains(currBoard[earliestR + c][earliestC + c]))
					{
						won = false;
						break;
					}
					else
					{
						//add to winning colors if not already in there
						if (!winningColors.Contains(currBoard[earliestR + c][earliestC + c]))
						{
							winningColors.Add(currBoard[earliestR + c][earliestC + c]);
						}
						if (Settings.corners)
						{
							//if in the corner decrease the winlength by the necessary amount
							if (((earliestR + c) == 0 && (earliestC + c) == 0) || ((earliestR + c) == 0 && (earliestC + c) == (Settings.widthNo - 1)) || ((earliestR + c) == (Settings.heightNo - 1) && (earliestC + c) == 0) || ((earliestR + c) == (Settings.heightNo - 1) && (earliestC + c) == (Settings.widthNo - 1)))
							{
								if (Settings.winLength >= 7)
								{
									winLength -= 2;
								}
								else
								{
									winLength--;
								}
							}
						}
					}
				}
				catch
				{
					if (earliestC != -1)
					{
						won = false;
						break;
					}
				}
			}
			return won;
		}

		private bool CheckDiagonallyTRBL(int cellR, int cellC, Color color)
		{
			try
			{
				//if cells on both sides are wrong color return false
				if (!Settings.validColors[Array.IndexOf(Settings.colorArr, color)].Contains(currBoard[cellR - 1][cellC + 1]) && !Settings.validColors[Array.IndexOf(Settings.colorArr, color)].Contains(currBoard[cellR + 1][cellC - 1]))
				{
					return false;
				}
			}
			catch { }
			winningColors.Clear();
			bool won = true;
			int earliestC = 0;
			int earliestR = 0;
			//find the furthest bottom right cell which is part of a correct color line with this cell
			for (int c = 0; c < Settings.winLength; c++)
			{
				try
				{
					if (!Settings.validColors[Array.IndexOf(Settings.colorArr, color)].Contains(currBoard[cellR - c][cellC + c]))
					{
						earliestC = (cellC + c) - 1;
						earliestR = (cellR - c) + 1;
						break;
					}
					if (c == (Settings.winLength - 1))
					{
						earliestC = (cellC + c);
						earliestR = (cellR - c);
						break;
					}
				}
				catch
				{
					earliestC = (cellC + c) - 1;
					earliestR = (cellR - c) + 1;
					break;
				}
			}
			if (earliestC == -1)
			{
				earliestC = 0;
			}
			int winLength = Settings.winLength;
			//work left and down until either they've won or you reach a color that isn't valid
			for (int c = 0; c < winLength; c++)
			{
				try
				{
					//if invalid color
					if (!Settings.validColors[Array.IndexOf(Settings.colorArr, color)].Contains(currBoard[earliestR + c][earliestC - c]))
					{
						won = false;
						break;
					}
					else
					{
						//add to winning colors if not already in there
						if (!winningColors.Contains(currBoard[earliestR + c][earliestC - c]))
						{
							winningColors.Add(currBoard[earliestR + c][earliestC - c]);
						}
						if (Settings.corners)
						{
							//if in the corner decrease the winlength by the necessary amount
							if (((earliestR + c) == 0 && (earliestC - c) == 0) || ((earliestR + c) == 0 && (earliestC - c) == (Settings.widthNo - 1)) || ((earliestR + c) == (Settings.heightNo - 1) && (earliestC - c) == 0) || ((earliestR + c) == (Settings.heightNo - 1) && (earliestC - c) == (Settings.widthNo - 1)))
							{
								if (Settings.winLength >= 7)
								{
									winLength -= 2;
								}
								else
								{
									winLength--;
								}
							}
						}
					}
				}
				catch
				{
					if (earliestC != -1)
					{
						won = false;
						break;
					}
				}
			}
			return won;
		}

		private bool CheckSolitaires(int row, int col, bool allowWin)
		{
			bool won = true;
			bool sol = false;
			try
			{
				//if cell is a solitaire
				if (CheckCellIsSolitaire(row + 1, col))
				{
					sol = true;
					if (allowWin)
					{
						currBoard[row + 1][col] = Color.Black;
						UpdateTblGrid();
						System.Threading.Thread.Sleep(500);
						currBoard[row + 1][col] = Color.Empty;
						UpdateTblGrid();
					}
					else
					{
						currBoard[row + 1][col] = Color.Empty;
					}
					//move all counters in col column to the bottom
					MoveCountersDown(col);
					//check wins
					if (currBoard[row + 1][col] != Color.Empty)
					{
						for (int r = 0; r < Settings.heightNo; r++)
						{
							if (currBoard[r][col] != Color.Empty)
							{
								won = CheckWon(r, col, currBoard[r][col], true, allowWin, 1246);
							}
						}
					}
					else
					{
						won = false;
					}
				}
			}
			catch { }
			try
			{
				//if cell is a solitaire
				if (CheckCellIsSolitaire(row + 1, col - 1))
				{
					sol = true;
					if (allowWin)
					{
						currBoard[row + 1][col - 1] = Color.Black;
						UpdateTblGrid();
						System.Threading.Thread.Sleep(500);
						currBoard[row + 1][col - 1] = Color.Empty;
						UpdateTblGrid();
					}
					else
					{
						currBoard[row + 1][col - 1] = Color.Empty;
					}
					//move all counters in col - 1 column to the bottom
					MoveCountersDown(col - 1);
					//check wins
					if (currBoard[row + 1][col - 1] != Color.Empty)
					{
						for (int r = 0; r < Settings.heightNo; r++)
						{
							if (currBoard[r][col - 1] != Color.Empty)
							{
								won = CheckWon(r, col - 1, currBoard[r][col - 1], true, allowWin, 1279);
							}
						}
					}
					else
					{
						won = false;
					}
				}
			}
			catch { }
			try
			{
				//if cell is a solitaire
				if (CheckCellIsSolitaire(row + 1, col + 1))
				{
					sol = true;
					if (allowWin)
					{
						currBoard[row + 1][col + 1] = Color.Black;
						UpdateTblGrid();
						System.Threading.Thread.Sleep(500);
						currBoard[row + 1][col + 1] = Color.Empty;
						UpdateTblGrid();
					}
					else
					{
						currBoard[row + 1][col + 1] = Color.Empty;
					}
					//move all counters in col + 1 column to the bottom
					MoveCountersDown(col + 1);
					//check wins
					if (currBoard[row + 1][col + 1] != Color.Empty)
					{
						for (int r = 0; r < Settings.heightNo; r++)
						{
							if (currBoard[r][col + 1] != Color.Empty)
							{
								won = CheckWon(r, col + 1, currBoard[r][col + 1], true, allowWin, 1312);
							}
						}
					}
					else
					{
						won = false;
					}
				}
			}
			catch { }
			//check if won if haven't already checked
			if (!sol)
			{
				won = CheckWon(row, col, currColor, false, allowWin, 1325);
			}
			return won;
		}

		private bool CheckCellIsSolitaire(int cellR, int cellC)
		{
			//check if this cell is a solitaire
			Color baseColor;
			int basePlayer = 0;
			try
			{
				baseColor = currBoard[cellR - 1][cellC];
			}
			catch
			{
				baseColor = currBoard[cellR + 1][cellC];
			}
			//if any adjacent cell isn't a valid color then return false
			if (baseColor == Color.Empty || baseColor == currBoard[cellR][cellC])
			{
				return false;
			}
			else
			{
				basePlayer = Array.IndexOf(Settings.colorArr, baseColor);
				try
				{
					if (!Settings.validColors[basePlayer].Contains(currBoard[cellR - 1][cellC - 1]))
						return false;
				}
				catch { }
				try
				{
					if (!Settings.validColors[basePlayer].Contains(currBoard[cellR - 1][cellC + 1]))
						return false;
				}
				catch { }
				try
				{
					if (!Settings.validColors[basePlayer].Contains(currBoard[cellR][cellC - 1]))
						return false;
				}
				catch { }
				try
				{
					if (!Settings.validColors[basePlayer].Contains(currBoard[cellR][cellC + 1]))
						return false;
				}
				catch { }
				try
				{
					if (!Settings.validColors[basePlayer].Contains(currBoard[cellR + 1][cellC - 1]))
						return false;
				}
				catch { }
				try
				{
					if (!Settings.validColors[basePlayer].Contains(currBoard[cellR + 1][cellC]))
						return false;
				}
				catch { }
				try
				{
					if (!Settings.validColors[basePlayer].Contains(currBoard[cellR + 1][cellC + 1]))
						return false;
				}
				catch { }
				return true;
			}
		}

		private void MovePlayed()
		{
			//set next color
			UpdateTblGrid();
			movesPlayed++;
			bool full = false;
			//if played more or same amount of moves as spaces
			if (movesPlayed >= (Settings.widthNo * Settings.heightNo))
			{
				full = true;
				//check if board is full
				foreach (List<Color> lstRow in currBoard)
				{
					foreach (Color cell in lstRow)
					{
						if (cell == Color.Empty)
						{
							full = false;
							break;
						}
					}
					if (!full)
					{
						break;
					}
				}
			}
			//if full tell user it was a draw
			if (full)
			{
				if (Settings.noPlayers != 0)
				{
					MessageBox.Show("It was a draw!");
				}
				WonRound(new List<int>());
			}
			else
			{
				//reset label to regular font
				if (!allCPU)
				{
					lblNames[currPlayer].Font = new Font(lblNames[currPlayer].Font, FontStyle.Regular);
					if (lblNames[currPlayer].InvokeRequired)
					{
						lblNames[currPlayer].BeginInvoke((MethodInvoker)delegate () { lblNames[currPlayer].Update(); });
					}
					else
					{
						lblNames[currPlayer].Update();
					}
				}
				//move onto next player
				currPlayer++;
				if (currPlayer == noPlayers)
				{
					currPlayer = 0;
				}
				currColor = Settings.colorArr[currPlayer];
				//enable/disable bomb for next round
				if (roundBombUsed[currPlayer] == -1)
				{
					if (btnBomb.InvokeRequired)
					{
						btnBomb.BeginInvoke((MethodInvoker)delegate () { btnBomb.Enabled = true; });
					}
					else
					{
						btnBomb.Enabled = true;
					}
				}
				else
				{
					if (btnBomb.InvokeRequired)
					{
						btnBomb.BeginInvoke((MethodInvoker)delegate () { btnBomb.Enabled = false; });
					}
					else
					{
						btnBomb.Enabled = false;
					}
				}
				//if computer's turn
				if (Settings.cpu[currPlayer])
				{
					//update label to bold font
					if (!allCPU)
					{
						lblNames[currPlayer].Font = new Font(lblNames[currPlayer].Font, FontStyle.Bold);
						if (lblNames[currPlayer].InvokeRequired)
						{
							lblNames[currPlayer].BeginInvoke((MethodInvoker)delegate () { lblNames[currPlayer].Update(); });
						}
						else
						{
							lblNames[currPlayer].Update();
						}
					}
					//play computer
					PlayCPU();
				}
				else
				{
					//update label to bold font
					if (!allCPU)
					{
						lblNames[currPlayer].Font = new Font(lblNames[currPlayer].Font, FontStyle.Bold);
					}
				}
			}
		}

		private void WonRound(List<int> winners)
		{
			//increase scores of winners
			foreach (int winner in winners)
			{
				scores[winner] += (noPlayers / winners.Count);

				if (lblNames[winner].InvokeRequired)
				{
					lblNames[winner].BeginInvoke((MethodInvoker)delegate () { lblNames[winner].Text = Settings.names[winner] + ": " + scores[winner]; });
				}
				else
				{
					lblNames[winner].Text = Settings.names[winner] + ": " + scores[winner];
				}
			}
			currRound++;
			//if finished all rounds
			if (currRound == Settings.noRounds)
			{
				End();

				//work out overall winners
				int highest = 0;
				List<int> finalWinners = new List<int>();
				for (int i = 0; i < 10; i++)
				{
					if (scores[i] > highest)
					{
						highest = scores[i];
						finalWinners.Clear();
						finalWinners.Add(i);
					}
					else if (scores[i] == highest)
					{
						finalWinners.Add(i);
					}
				}
				//tell user the winners
				if (finalWinners.Count == 1)
				{
					MessageBox.Show("The winner was " + Settings.names[finalWinners[0]] + " with " + highest + " points!");
				}
				else
				{
					string winnersNames = "";
					foreach (int wnr in finalWinners)
					{
						winnersNames += Settings.names[wnr] + ", ";
					}
					winnersNames = winnersNames.Remove(winnersNames.Length - 2);
					winnersNames = ReplaceLastOccurrence(winnersNames, ", ", " and ");
					MessageBox.Show("The winners were " + winnersNames + " with " + highest + " points!");
				}
			}
			else
			{
				//if not finished all rounds start the next round
				StartRound();
			}
		}

		private void BtnEnd_Click(object sender, EventArgs e)
		{
			End();
		}

		private void End()
		{
			//disable/enable controls as necessary
			if (tblGrid.InvokeRequired)
			{
				tblGrid.BeginInvoke((MethodInvoker)delegate () { tblGrid.Enabled = false; });
			}
			else
			{
				tblGrid.Enabled = false;
			}

			if (btnStart.InvokeRequired)
			{
				btnStart.BeginInvoke((MethodInvoker)delegate () { btnStart.Enabled = true; });
			}
			else
			{
				btnStart.Enabled = true;
			}

			if (btnEnd.InvokeRequired)
			{
				btnEnd.BeginInvoke((MethodInvoker)delegate () { btnEnd.Enabled = false; });
			}
			else
			{
				btnEnd.Enabled = false;
			}

			if (btnUndo.InvokeRequired)
			{
				btnUndo.BeginInvoke((MethodInvoker)delegate () { btnUndo.Enabled = false; });
			}
			else
			{
				btnUndo.Enabled = false;
			}

			if (btnBomb.InvokeRequired)
			{
				btnBomb.BeginInvoke((MethodInvoker)delegate () { btnBomb.Enabled = false; });
			}
			else
			{
				btnBomb.Enabled = false;
			}
			if (!allCPU)
			{
				lblNames[currPlayer].Font = new Font(lblNames[currPlayer].Font, FontStyle.Regular);
			}

			//color board to lightgray to show disabled
			for (int r = 0; r < Settings.heightNo; r++)
			{
				for (int c = 0; c < Settings.widthNo; c++)
				{
					if (currBoard[r][c] == Color.Empty)
					{
						currBoard[r][c] = Color.LightGray;
					}
				}
			}
			UpdateTblGrid();
			lstMoves.Clear();
		}

		private void BtnBomb_Click(object sender, EventArgs e)
		{
			//turn bomb on/off for this round
			if (btnBomb.BackColor == Color.LightGray)
			{
				btnBomb.BackColor = Color.Gray;
				bombCounter = true;
			}
			else
			{
				btnBomb.BackColor = Color.LightGray;
				bombCounter = false;
			}
		}

		private void BtnUndo_Click(object sender, EventArgs e)
		{
			Undo(true);
		}

		private void Undo(bool changePlayer)
		{
			//remove last move
			lstMoves.RemoveAt(lstMoves.Count - 1);

			List<Color> lstColors = lstMoves[lstMoves.Count - 1];
			int count = 0;

			//set board to previous board
			for (int r = 0; r < Settings.heightNo; r++)
			{
				for (int c = 0; c < Settings.widthNo; c++)
				{
					currBoard[r][c] = lstColors[count];
					count++;
				}
			}
			UpdateTblGrid();

			//go back onto the previous player if necessary
			if (changePlayer)
			{
				movesPlayed--;
				//set prev player info
				if (!allCPU)
				{
					lblNames[currPlayer].Font = new Font(lblNames[currPlayer].Font, FontStyle.Regular);
				}
				currPlayer--;
				if (currPlayer == -1)
				{
					currPlayer = Settings.noPlayers - 1;
				}
				if (!allCPU)
				{
					lblNames[currPlayer].Font = new Font(lblNames[currPlayer].Font, FontStyle.Bold);
				}
				currColor = Settings.colorArr[currPlayer];
				if (lstMoves.Count - 1 == roundBombUsed[currPlayer])
				{
					roundBombUsed[currPlayer] = -1;
					roundBombUsed[currPlayer] = -1;
				}
				if (roundBombUsed[currPlayer] == -1)
				{
					btnBomb.Enabled = true;
				}
				else
				{
					btnBomb.Enabled = false;
				}
			}

			//disable undo button if there's no previous moves else enable it
			if (lstMoves.Count == 1)
			{
				if (btnUndo.InvokeRequired)
				{
					btnUndo.BeginInvoke((MethodInvoker)delegate () { btnUndo.Enabled = false; });
				}
				else
				{
					btnUndo.Enabled = false;
				}
			}
			else
			{
				if (btnUndo.InvokeRequired)
				{
					btnUndo.BeginInvoke((MethodInvoker)delegate () { btnUndo.Enabled = true; });
				}
				else
				{
					btnUndo.Enabled = true;
				}
			}
		}

		private void UpdateTblGrid()
		{
			//put currBoard onto tblGrid for user to see
			for (int r = 0; r < Settings.heightNo; r++)
			{
				for (int c = 0; c < Settings.widthNo; c++)
				{
					tblGrid.Rows[r].Cells[c].Style.BackColor = currBoard[r][c];
				}
			}
			if (tblGrid.InvokeRequired)
			{
				tblGrid.BeginInvoke((MethodInvoker)delegate () { tblGrid.Refresh(); });
			}
			else
			{
				tblGrid.Refresh();
			}
		}

		//function to replace the last occurence of a string within a string
		private static string ReplaceLastOccurrence(string source, string find, string replace)
		{
			int place = source.LastIndexOf(find);
			if (place == -1)
			{
				return source;
			}
			string result = source.Remove(place, find.Length).Insert(place, replace);
			return result;
		}

		//Cell used to store the lowest cells in PlayCPU()
		private class Cell
		{
			public Color BackColor = Color.Empty;
			public int RowIndex = 0;
			public int ColumnIndex = 0;
		}
	}
}