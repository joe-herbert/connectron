using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connectron
{
	public partial class Settings : Form
	{
		public Settings()
		{
			InitializeComponent();
		}

		public static Color[] colorArr = { Color.Red, Color.Blue, Color.Green, Color.Gold, Color.Violet, Color.Black, Color.Gray, Color.Cyan, Color.Maroon, Color.Lime };
		public static List<Color>[] validColors = { new List<Color>(), new List<Color>(), new List<Color>(), new List<Color>(), new List<Color>(), new List<Color>(), new List<Color>(), new List<Color>(), new List<Color>(), new List<Color>() };
		public static string[] names = { "", "", "", "", "", "", "", "", "", "" };
		public static bool[] cpu = { false, false, false, false, false, false, false, false, false, false };
		public static int widthNo = 6;
		public static int heightNo = 6;
		public static int winLength = 4;
		public static int noPlayers = 2;
		public static int noRounds = 1;
		public static bool corners = true;
		public static bool solitaire = true;
		public static bool bomb = true;
		public static bool overflow = false;
		public static bool alliances = false;
		public static int allianceSender = 0;

		private void LblCol_Click(object sender, EventArgs e)
		{
			Label sndr = (Label)sender;
			int sndrNum = Convert.ToInt32(sndr.Text) - 1;
			DialogResult result = colorDialog1.ShowDialog();
			if (result == DialogResult.OK)
			{
				if (colorArr.Contains(colorDialog1.Color) && colorArr[sndrNum] != colorDialog1.Color)
				{
					MessageBox.Show("This color is already in use by another character");
				}
				else
				{
					Color oldColor = colorArr[sndrNum];
					colorArr[sndrNum] = colorDialog1.Color;
					try
					{
						validColors[sndrNum][0] = colorDialog1.Color;
					}
					catch
					{
						validColors[sndrNum].Add(colorDialog1.Color);
					}
					for (int i = 0; i < 10; i++)
					{
						if (validColors[i].Contains(oldColor))
						{
							validColors[i][validColors[i].IndexOf(oldColor)] = colorDialog1.Color;
						}
					}
					sndr.BackColor = colorDialog1.Color;
				}
			}
		}

		private void BtnGo_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < 10; i++)
			{
				if (validColors[i].Count == 0)
				{
					validColors[i].Add(colorArr[i]);
				}
			}
			if (txtName1.Text == "" || txtName2.Text == "" || (txtName3.Text == "" && txtName3.Visible == true) || (txtName4.Text == "" && txtName4.Visible == true) || (txtName5.Text == "" && txtName5.Visible == true) || (txtName6.Text == "" && txtName6.Visible == true) || (txtName7.Text == "" && txtName7.Visible == true) || (txtName8.Text == "" && txtName8.Visible == true) || (txtName9.Text == "" && txtName9.Visible == true) || (txtName10.Text == "" && txtName10.Visible == true))
			{
				MessageBox.Show("Please enter a name for every player");
			}
			else
			{
				if (!txtName3.Visible)
				{
					names[2] = "";
				}
				if (!txtName4.Visible)
				{
					names[3] = "";
				}
				if (!txtName5.Visible)
				{
					names[4] = "";
				}
				if (!txtName6.Visible)
				{
					names[5] = "";
				}
				if (!txtName7.Visible)
				{
					names[6] = "";
				}
				if (!txtName8.Visible)
				{
					names[7] = "";
				}
				if (!txtName9.Visible)
				{
					names[8] = "";
				}
				if (!txtName10.Visible)
				{
					names[9] = "";
				}
				Game game = new Game();
				game.Show();
				game.LoadGrid();
			}
		}

		private void NudGrid_ValueChanged(object sender, EventArgs e)
		{
			if (nudGridHeight.Value > nudGridWidth.Value)
			{
				nudWinningLength.Maximum = nudGridHeight.Value;
			}
			else
			{
				nudWinningLength.Maximum = nudGridWidth.Value;
			}
			widthNo = Convert.ToInt32(nudGridWidth.Value);
			heightNo = Convert.ToInt32(nudGridHeight.Value);
			if (nudGridHeight.Value > 7)
			{
				chkBxOverflow.Enabled = true;
			}
			else
			{
				chkBxOverflow.Enabled = false;
				chkBxOverflow.Checked = false;
				overflow = false;
			}
		}

		private void NudNoPlayers_ValueChanged(object sender, EventArgs e)
		{
			noPlayers = Convert.ToInt32(nudNoPlayers.Value);
			switch (noPlayers)
			{
				case 0:
					chkCPU1.Checked = true;
					cpu[0] = true;
					chkCPU2.Checked = true;
					cpu[1] = true;
					if (txtName1.Text == "")
					{
						txtName1.Text = "CPU 1";
						names[0] = "CPU 1";
					}
					if (txtName2.Text == "" || txtName2.Text == "CPU")
					{
						txtName2.Text = "CPU 2";
						names[1] = "CPU 2";
					}
					lblCol3.Visible = false;
					txtName3.Visible = false;
					chkCPU3.Visible = false;
					lblCol4.Visible = false;
					txtName4.Visible = false;
					chkCPU4.Visible = false;
					lblCol5.Visible = false;
					txtName5.Visible = false;
					chkCPU5.Visible = false;
					lblCol6.Visible = false;
					txtName6.Visible = false;
					chkCPU6.Visible = false;
					lblCol7.Visible = false;
					txtName7.Visible = false;
					chkCPU7.Visible = false;
					lblCol8.Visible = false;
					txtName8.Visible = false;
					chkCPU8.Visible = false;
					lblCol9.Visible = false;
					txtName9.Visible = false;
					chkCPU9.Visible = false;
					lblCol10.Visible = false;
					txtName10.Visible = false;
					chkCPU10.Visible = false;
					if (alliances)
					{
						btnAdd1.Visible = true;
						btnAdd2.Visible = true;
						btnAdd3.Visible = false;
						btnAdd4.Visible = false;
						btnAdd5.Visible = false;
						btnAdd6.Visible = false;
						btnAdd7.Visible = false;
						btnAdd8.Visible = false;
						btnAdd9.Visible = false;
						btnAdd10.Visible = false;
					}
					else
					{
						btnAdd1.Visible = false;
						btnAdd2.Visible = false;
						btnAdd3.Visible = false;
						btnAdd4.Visible = false;
						btnAdd5.Visible = false;
						btnAdd6.Visible = false;
						btnAdd7.Visible = false;
						btnAdd8.Visible = false;
						btnAdd9.Visible = false;
						btnAdd10.Visible = false;
					}
					chkBxBomb.Checked = false;
					chkBxBomb.Enabled = false;
					bomb = false;
					break;
				case 1:
					if (cpu[0])
					{
						names[0] = "";
						chkCPU1.Checked = false;
						cpu[0] = false;
					}
					chkCPU2.Checked = true;
					cpu[1] = true;
					if (txtName2.Text == "")
					{
						txtName2.Text = "CPU";
						names[1] = "CPU";
					}
					lblCol3.Visible = false;
					txtName3.Visible = false;
					chkCPU3.Visible = false;
					lblCol4.Visible = false;
					txtName4.Visible = false;
					chkCPU4.Visible = false;
					lblCol5.Visible = false;
					txtName5.Visible = false;
					chkCPU5.Visible = false;
					lblCol6.Visible = false;
					txtName6.Visible = false;
					chkCPU6.Visible = false;
					lblCol7.Visible = false;
					txtName7.Visible = false;
					chkCPU7.Visible = false;
					lblCol8.Visible = false;
					txtName8.Visible = false;
					chkCPU8.Visible = false;
					lblCol9.Visible = false;
					txtName9.Visible = false;
					chkCPU9.Visible = false;
					lblCol10.Visible = false;
					txtName10.Visible = false;
					chkCPU10.Visible = false;
					if (alliances)
					{
						btnAdd1.Visible = true;
						btnAdd2.Visible = true;
						btnAdd3.Visible = false;
						btnAdd4.Visible = false;
						btnAdd5.Visible = false;
						btnAdd6.Visible = false;
						btnAdd7.Visible = false;
						btnAdd8.Visible = false;
						btnAdd9.Visible = false;
						btnAdd10.Visible = false;
					}
					else
					{
						btnAdd1.Visible = false;
						btnAdd2.Visible = false;
						btnAdd3.Visible = false;
						btnAdd4.Visible = false;
						btnAdd5.Visible = false;
						btnAdd6.Visible = false;
						btnAdd7.Visible = false;
						btnAdd8.Visible = false;
						btnAdd9.Visible = false;
						btnAdd10.Visible = false;
					}
					chkBxBomb.Enabled = true;
					break;
				case 2:
					if (cpu[0])
					{
						names[0] = "";
						chkCPU1.Checked = false;
						cpu[0] = false;
					}
					if (cpu[1])
					{
						names[1] = "";
						chkCPU2.Checked = false;
						cpu[1] = false;
					}
					lblCol3.Visible = false;
					txtName3.Visible = false;
					chkCPU3.Visible = false;
					lblCol4.Visible = false;
					txtName4.Visible = false;
					chkCPU4.Visible = false;
					lblCol5.Visible = false;
					txtName5.Visible = false;
					chkCPU5.Visible = false;
					lblCol6.Visible = false;
					txtName6.Visible = false;
					chkCPU6.Visible = false;
					lblCol7.Visible = false;
					txtName7.Visible = false;
					chkCPU7.Visible = false;
					lblCol8.Visible = false;
					txtName8.Visible = false;
					chkCPU8.Visible = false;
					lblCol9.Visible = false;
					txtName9.Visible = false;
					chkCPU9.Visible = false;
					lblCol10.Visible = false;
					txtName10.Visible = false;
					chkCPU10.Visible = false;
					if (alliances)
					{
						btnAdd1.Visible = true;
						btnAdd2.Visible = true;
						btnAdd3.Visible = false;
						btnAdd4.Visible = false;
						btnAdd5.Visible = false;
						btnAdd6.Visible = false;
						btnAdd7.Visible = false;
						btnAdd8.Visible = false;
						btnAdd9.Visible = false;
						btnAdd10.Visible = false;
					}
					else
					{
						btnAdd1.Visible = false;
						btnAdd2.Visible = false;
						btnAdd3.Visible = false;
						btnAdd4.Visible = false;
						btnAdd5.Visible = false;
						btnAdd6.Visible = false;
						btnAdd7.Visible = false;
						btnAdd8.Visible = false;
						btnAdd9.Visible = false;
						btnAdd10.Visible = false;
					}
					break;
				case 3:
					lblCol3.Visible = true;
					txtName3.Visible = true;
					chkCPU3.Visible = true;
					lblCol4.Visible = false;
					txtName4.Visible = false;
					chkCPU4.Visible = false;
					lblCol5.Visible = false;
					txtName5.Visible = false;
					chkCPU5.Visible = false;
					lblCol6.Visible = false;
					txtName6.Visible = false;
					chkCPU6.Visible = false;
					lblCol7.Visible = false;
					txtName7.Visible = false;
					chkCPU7.Visible = false;
					lblCol8.Visible = false;
					txtName8.Visible = false;
					chkCPU8.Visible = false;
					lblCol9.Visible = false;
					txtName9.Visible = false;
					chkCPU9.Visible = false;
					lblCol10.Visible = false;
					txtName10.Visible = false;
					chkCPU10.Visible = false;
					if (alliances)
					{
						btnAdd1.Visible = true;
						btnAdd2.Visible = true;
						btnAdd3.Visible = true;
						btnAdd4.Visible = false;
						btnAdd5.Visible = false;
						btnAdd6.Visible = false;
						btnAdd7.Visible = false;
						btnAdd8.Visible = false;
						btnAdd9.Visible = false;
						btnAdd10.Visible = false;
					}
					else
					{
						btnAdd1.Visible = false;
						btnAdd2.Visible = false;
						btnAdd3.Visible = false;
						btnAdd4.Visible = false;
						btnAdd5.Visible = false;
						btnAdd6.Visible = false;
						btnAdd7.Visible = false;
						btnAdd8.Visible = false;
						btnAdd9.Visible = false;
						btnAdd10.Visible = false;
					}
					break;
				case 4:
					lblCol3.Visible = true;
					txtName3.Visible = true;
					chkCPU3.Visible = true;
					lblCol4.Visible = true;
					txtName4.Visible = true;
					chkCPU4.Visible = true;
					lblCol5.Visible = false;
					txtName5.Visible = false;
					chkCPU5.Visible = false;
					lblCol6.Visible = false;
					txtName6.Visible = false;
					chkCPU6.Visible = false;
					lblCol7.Visible = false;
					txtName7.Visible = false;
					chkCPU7.Visible = false;
					lblCol8.Visible = false;
					txtName8.Visible = false;
					chkCPU8.Visible = false;
					lblCol9.Visible = false;
					txtName9.Visible = false;
					chkCPU9.Visible = false;
					lblCol10.Visible = false;
					txtName10.Visible = false;
					chkCPU10.Visible = false;
					if (alliances)
					{
						btnAdd1.Visible = true;
						btnAdd2.Visible = true;
						btnAdd3.Visible = true;
						btnAdd4.Visible = true;
						btnAdd5.Visible = false;
						btnAdd6.Visible = false;
						btnAdd7.Visible = false;
						btnAdd8.Visible = false;
						btnAdd9.Visible = false;
						btnAdd10.Visible = false;
					}
					else
					{
						btnAdd1.Visible = false;
						btnAdd2.Visible = false;
						btnAdd3.Visible = false;
						btnAdd4.Visible = false;
						btnAdd5.Visible = false;
						btnAdd6.Visible = false;
						btnAdd7.Visible = false;
						btnAdd8.Visible = false;
						btnAdd9.Visible = false;
						btnAdd10.Visible = false;
					}
					break;
				case 5:
					lblCol3.Visible = true;
					txtName3.Visible = true;
					chkCPU3.Visible = true;
					lblCol4.Visible = true;
					txtName4.Visible = true;
					chkCPU4.Visible = true;
					lblCol5.Visible = true;
					txtName5.Visible = true;
					chkCPU5.Visible = true;
					lblCol6.Visible = false;
					txtName6.Visible = false;
					chkCPU6.Visible = false;
					lblCol7.Visible = false;
					txtName7.Visible = false;
					chkCPU7.Visible = false;
					lblCol8.Visible = false;
					txtName8.Visible = false;
					chkCPU8.Visible = false;
					lblCol9.Visible = false;
					txtName9.Visible = false;
					chkCPU9.Visible = false;
					lblCol10.Visible = false;
					txtName10.Visible = false;
					chkCPU10.Visible = false;
					if (alliances)
					{
						btnAdd1.Visible = true;
						btnAdd2.Visible = true;
						btnAdd3.Visible = true;
						btnAdd4.Visible = true;
						btnAdd5.Visible = true;
						btnAdd6.Visible = false;
						btnAdd7.Visible = false;
						btnAdd8.Visible = false;
						btnAdd9.Visible = false;
						btnAdd10.Visible = false;
					}
					else
					{
						btnAdd1.Visible = false;
						btnAdd2.Visible = false;
						btnAdd3.Visible = false;
						btnAdd4.Visible = false;
						btnAdd5.Visible = false;
						btnAdd6.Visible = false;
						btnAdd7.Visible = false;
						btnAdd8.Visible = false;
						btnAdd9.Visible = false;
						btnAdd10.Visible = false;
					}
					break;
				case 6:
					lblCol3.Visible = true;
					txtName3.Visible = true;
					chkCPU3.Visible = true;
					lblCol4.Visible = true;
					txtName4.Visible = true;
					chkCPU4.Visible = true;
					lblCol5.Visible = true;
					txtName5.Visible = true;
					chkCPU5.Visible = true;
					lblCol6.Visible = true;
					txtName6.Visible = true;
					chkCPU6.Visible = true;
					lblCol7.Visible = false;
					txtName7.Visible = false;
					chkCPU7.Visible = false;
					lblCol8.Visible = false;
					txtName8.Visible = false;
					chkCPU8.Visible = false;
					lblCol9.Visible = false;
					txtName9.Visible = false;
					chkCPU9.Visible = false;
					lblCol10.Visible = false;
					txtName10.Visible = false;
					chkCPU10.Visible = false;
					if (alliances)
					{
						btnAdd1.Visible = true;
						btnAdd2.Visible = true;
						btnAdd3.Visible = true;
						btnAdd4.Visible = true;
						btnAdd5.Visible = true;
						btnAdd6.Visible = true;
						btnAdd7.Visible = false;
						btnAdd8.Visible = false;
						btnAdd9.Visible = false;
						btnAdd10.Visible = false;
					}
					else
					{
						btnAdd1.Visible = false;
						btnAdd2.Visible = false;
						btnAdd3.Visible = false;
						btnAdd4.Visible = false;
						btnAdd5.Visible = false;
						btnAdd6.Visible = false;
						btnAdd7.Visible = false;
						btnAdd8.Visible = false;
						btnAdd9.Visible = false;
						btnAdd10.Visible = false;
					}
					break;
				case 7:
					lblCol3.Visible = true;
					txtName3.Visible = true;
					chkCPU3.Visible = true;
					lblCol4.Visible = true;
					txtName4.Visible = true;
					chkCPU4.Visible = true;
					lblCol5.Visible = true;
					txtName5.Visible = true;
					chkCPU5.Visible = true;
					lblCol6.Visible = true;
					txtName6.Visible = true;
					chkCPU6.Visible = true;
					lblCol7.Visible = true;
					txtName7.Visible = true;
					chkCPU7.Visible = true;
					lblCol8.Visible = false;
					txtName8.Visible = false;
					chkCPU8.Visible = false;
					lblCol9.Visible = false;
					txtName9.Visible = false;
					chkCPU9.Visible = false;
					lblCol10.Visible = false;
					txtName10.Visible = false;
					chkCPU10.Visible = false;
					if (alliances)
					{
						btnAdd1.Visible = true;
						btnAdd2.Visible = true;
						btnAdd3.Visible = true;
						btnAdd4.Visible = true;
						btnAdd5.Visible = true;
						btnAdd6.Visible = true;
						btnAdd7.Visible = true;
						btnAdd8.Visible = false;
						btnAdd9.Visible = false;
						btnAdd10.Visible = false;
					}
					else
					{
						btnAdd1.Visible = false;
						btnAdd2.Visible = false;
						btnAdd3.Visible = false;
						btnAdd4.Visible = false;
						btnAdd5.Visible = false;
						btnAdd6.Visible = false;
						btnAdd7.Visible = false;
						btnAdd8.Visible = false;
						btnAdd9.Visible = false;
						btnAdd10.Visible = false;
					}
					break;
				case 8:
					lblCol3.Visible = true;
					txtName3.Visible = true;
					chkCPU3.Visible = true;
					lblCol4.Visible = true;
					txtName4.Visible = true;
					chkCPU4.Visible = true;
					lblCol5.Visible = true;
					txtName5.Visible = true;
					chkCPU5.Visible = true;
					lblCol6.Visible = true;
					txtName6.Visible = true;
					chkCPU6.Visible = true;
					lblCol7.Visible = true;
					txtName7.Visible = true;
					chkCPU7.Visible = true;
					lblCol8.Visible = true;
					txtName8.Visible = true;
					chkCPU8.Visible = true;
					lblCol9.Visible = false;
					txtName9.Visible = false;
					chkCPU9.Visible = false;
					lblCol10.Visible = false;
					txtName10.Visible = false;
					chkCPU10.Visible = false;
					if (alliances)
					{
						btnAdd1.Visible = true;
						btnAdd2.Visible = true;
						btnAdd3.Visible = true;
						btnAdd4.Visible = true;
						btnAdd5.Visible = true;
						btnAdd6.Visible = true;
						btnAdd7.Visible = true;
						btnAdd8.Visible = true;
						btnAdd9.Visible = false;
						btnAdd10.Visible = false;
					}
					else
					{
						btnAdd1.Visible = false;
						btnAdd2.Visible = false;
						btnAdd3.Visible = false;
						btnAdd4.Visible = false;
						btnAdd5.Visible = false;
						btnAdd6.Visible = false;
						btnAdd7.Visible = false;
						btnAdd8.Visible = false;
						btnAdd9.Visible = false;
						btnAdd10.Visible = false;
					}
					break;
				case 9:
					lblCol3.Visible = true;
					txtName3.Visible = true;
					chkCPU3.Visible = true;
					lblCol4.Visible = true;
					txtName4.Visible = true;
					chkCPU4.Visible = true;
					lblCol5.Visible = true;
					txtName5.Visible = true;
					chkCPU5.Visible = true;
					lblCol6.Visible = true;
					txtName6.Visible = true;
					chkCPU6.Visible = true;
					lblCol7.Visible = true;
					txtName7.Visible = true;
					chkCPU7.Visible = true;
					lblCol8.Visible = true;
					txtName8.Visible = true;
					chkCPU8.Visible = true;
					lblCol9.Visible = true;
					txtName9.Visible = true;
					chkCPU9.Visible = true;
					lblCol10.Visible = false;
					txtName10.Visible = false;
					chkCPU10.Visible = false;
					if (alliances)
					{
						btnAdd1.Visible = true;
						btnAdd2.Visible = true;
						btnAdd3.Visible = true;
						btnAdd4.Visible = true;
						btnAdd5.Visible = true;
						btnAdd6.Visible = true;
						btnAdd7.Visible = true;
						btnAdd8.Visible = true;
						btnAdd9.Visible = true;
						btnAdd10.Visible = false;
					}
					else
					{
						btnAdd1.Visible = false;
						btnAdd2.Visible = false;
						btnAdd3.Visible = false;
						btnAdd4.Visible = false;
						btnAdd5.Visible = false;
						btnAdd6.Visible = false;
						btnAdd7.Visible = false;
						btnAdd8.Visible = false;
						btnAdd9.Visible = false;
						btnAdd10.Visible = false;
					}
					break;
				case 10:
					lblCol3.Visible = true;
					txtName3.Visible = true;
					chkCPU3.Visible = true;
					lblCol4.Visible = true;
					txtName4.Visible = true;
					chkCPU4.Visible = true;
					lblCol5.Visible = true;
					txtName5.Visible = true;
					chkCPU5.Visible = true;
					lblCol6.Visible = true;
					txtName6.Visible = true;
					chkCPU6.Visible = true;
					lblCol7.Visible = true;
					txtName7.Visible = true;
					chkCPU7.Visible = true;
					lblCol8.Visible = true;
					txtName8.Visible = true;
					chkCPU8.Visible = true;
					lblCol9.Visible = true;
					txtName9.Visible = true;
					chkCPU9.Visible = true;
					lblCol10.Visible = true;
					txtName10.Visible = true;
					chkCPU10.Visible = true;
					if (alliances)
					{
						btnAdd1.Visible = true;
						btnAdd2.Visible = true;
						btnAdd3.Visible = true;
						btnAdd4.Visible = true;
						btnAdd5.Visible = true;
						btnAdd6.Visible = true;
						btnAdd7.Visible = true;
						btnAdd8.Visible = true;
						btnAdd9.Visible = true;
						btnAdd10.Visible = true;
					}
					else
					{
						btnAdd1.Visible = false;
						btnAdd2.Visible = false;
						btnAdd3.Visible = false;
						btnAdd4.Visible = false;
						btnAdd5.Visible = false;
						btnAdd6.Visible = false;
						btnAdd7.Visible = false;
						btnAdd8.Visible = false;
						btnAdd9.Visible = false;
						btnAdd10.Visible = false;
					}
					break;
			}
		}

		private void NudWinningLength_ValueChanged(object sender, EventArgs e)
		{
			winLength = Convert.ToInt32(nudWinningLength.Value);
		}

		private void NudNoRounds_ValueChanged(object sender, EventArgs e)
		{
			noRounds = Convert.ToInt32(nudNoRounds.Value);
		}

		private void TxtName_TextChanged(object sender, EventArgs e)
		{
			TextBox sndr = (TextBox)sender;
			int sndrNum = Convert.ToInt32(sndr.Name.Replace("txtName", ""));
			names[sndrNum - 1] = sndr.Text;
		}

		private void LblTitle_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Made By Joe Herbert \r\rJan 2018\r" + Width + ", " + Height);
		}

		private void ChkBxCorners_CheckedChanged(object sender, EventArgs e)
		{
			corners = chkBxCorners.Checked;
		}

		private void ChkBxSolitaire_CheckedChanged(object sender, EventArgs e)
		{
			solitaire = chkBxSolitaire.Checked;
		}

		private void ChkBxBomb_CheckedChanged(object sender, EventArgs e)
		{
			bomb = chkBxBomb.Checked;
		}

		private void ChkBxOverflow_CheckedChanged(object sender, EventArgs e)
		{
			overflow = chkBxOverflow.Checked;
		}

		private void ChkBxAlliances_CheckedChanged(object sender, EventArgs e)
		{
			alliances = chkBxAlliances.Checked;
			if (alliances)
			{
				lblAlliances.Visible = true;
			}
			else
			{
				lblAlliances.Visible = false;
			}
			NudNoPlayers_ValueChanged(sender, e);
			foreach (List<Color> lst in validColors)
			{
				lst.Clear();
			}
			for (int i = 0; i < 10; i++)
			{
				validColors[i].Add(colorArr[i]);
			}
		}

		private void BtnAddAlliance_Click(object sender, EventArgs e)
		{
			Button sndr = (Button)sender;
			allianceSender = Convert.ToInt32(sndr.Name.Replace("btnAdd", "")) - 1;
			Alliances allianceForm = new Alliances();
			allianceForm.Show();
		}

		private void ChkCPU_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox sndr = (CheckBox)sender;
			int sndrNum = Convert.ToInt32(sndr.Name.Replace("chkCPU", "")) - 1;
			cpu[sndrNum] = sndr.Checked;

			switch (sndrNum)
			{
				case 0:
					if (txtName1.Text == "")
					{
						txtName1.Text = "CPU 1";
					}
					else if (txtName1.Text == "CPU 1")
					{
						txtName1.Text = "";
					}
					break;
				case 1:
					if (txtName2.Text == "")
					{
						txtName2.Text = "CPU 2";
					}
					else if (txtName2.Text == "CPU 2")
					{
						txtName2.Text = "";
					}
					break;
				case 2:
					if (txtName3.Text == "")
					{
						txtName3.Text = "CPU 3";
					}
					else if (txtName3.Text == "CPU 3")
					{
						txtName3.Text = "";
					}
					break;
				case 3:
					if (txtName4.Text == "")
					{
						txtName4.Text = "CPU 4";
					}
					else if (txtName4.Text == "CPU 4")
					{
						txtName4.Text = "";
					}
					break;
				case 4:
					if (txtName5.Text == "")
					{
						txtName5.Text = "CPU 5";
					}
					else if (txtName5.Text == "CPU 5")
					{
						txtName5.Text = "";
					}
					break;
				case 5:
					if (txtName6.Text == "")
					{
						txtName6.Text = "CPU 6";
					}
					else if (txtName6.Text == "CPU 6")
					{
						txtName6.Text = "";
					}
					break;
				case 6:
					if (txtName7.Text == "")
					{
						txtName7.Text = "CPU 7";
					}
					else if (txtName7.Text == "CPU 7")
					{
						txtName7.Text = "";
					}
					break;
				case 7:
					if (txtName8.Text == "")
					{
						txtName8.Text = "CPU 8";
					}
					else if (txtName8.Text == "CPU 8")
					{
						txtName8.Text = "";
					}
					break;
				case 8:
					if (txtName9.Text == "")
					{
						txtName9.Text = "CPU 9";
					}
					else if (txtName9.Text == "CPU 9")
					{
						txtName9.Text = "";
					}
					break;
				case 9:
					if (txtName10.Text == "")
					{
						txtName10.Text = "CPU 10";
					}
					else if (txtName10.Text == "CPU 10")
					{
						txtName10.Text = "";
					}
					break;
			}

		}
	}
}
