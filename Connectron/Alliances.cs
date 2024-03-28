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
	public partial class Alliances : Form
	{
		public Alliances()
		{
			InitializeComponent();
		}

		public static List<Label> lblColors = new List<Label>();
		public static List<Label> lblNames = new List<Label>();
		public static List<Button> btnRemoves = new List<Button>();

		private void Alliances_Load(object sender, EventArgs e)
		{
			if (Settings.names[Settings.allianceSender] == "")
			{
				lblTitle.Text = "Alliances for Player " + (Settings.allianceSender + 1);
			}
			else
			{
				lblTitle.Text = "Alliances for " + Settings.names[Settings.allianceSender];
			}
			lblColors.Clear();
			lblNames.Clear();
			btnRemoves.Clear();
			pnlAlliances.Controls.Clear();
			foreach (Color color in Settings.validColors[Settings.allianceSender])
			{
				if (color != Settings.colorArr[Settings.allianceSender])
				{
					Label lblColor = new Label
					{
						BackColor = color,
						Text = "",
						AutoSize = false,
						Size = new Size(40, 40),
						Location = new Point(0, (lblColors.Count) * 42)
					};
					lblColors.Add(lblColor);
					Label lblName = new Label
					{
						Text = Settings.names[Array.IndexOf(Settings.colorArr, color)],
						TextAlign = ContentAlignment.MiddleCenter,
						AutoSize = false,
						Size = new Size(pnlAlliances.Width - 159, 40),
						Location = new Point(42, (lblNames.Count) * 42)
					};
					lblNames.Add(lblName);
					Button btnRemove = new Button
					{
						Text = "Remove",
						Size = new Size(85, 40),
						Location = new Point(pnlAlliances.Width - 117, (btnRemoves.Count) * 42),
						Name = "btnRemove" + btnRemoves.Count
					};
					btnRemove.Click += new EventHandler(BtnRemove_Click);
					btnRemoves.Add(btnRemove);
					pnlAlliances.Controls.Add(lblColors[lblColors.Count - 1]);
					pnlAlliances.Controls.Add(lblNames[lblNames.Count - 1]);
					pnlAlliances.Controls.Add(btnRemoves[btnRemoves.Count - 1]);
				}
			}
		}

		private void BtnDone_Click(object sender, EventArgs e)
		{
			try
			{
				Color color = Settings.colorArr[Array.IndexOf(Settings.names, cmbAddAlliance.SelectedItem)];
				Settings.validColors[Settings.allianceSender].Add(color);
				Settings.validColors[Array.IndexOf(Settings.colorArr, color)].Add(Settings.colorArr[Settings.allianceSender]);
				Label lblColor = new Label
				{
					BackColor = color,
					Text = "",
					AutoSize = false,
					Size = new Size(40, 40),
					Location = new Point(0, (lblColors.Count) * 42)
				};
				lblColors.Add(lblColor);
				Label lblName = new Label
				{
					Text = cmbAddAlliance.SelectedItem.ToString(),
					TextAlign = ContentAlignment.MiddleCenter,
					AutoSize = false,
					Size = new Size(pnlAlliances.Width - 159, 40),
					Location = new Point(42, (lblNames.Count) * 42)
				};
				lblNames.Add(lblName);
				Button btnRemove = new Button
				{
					Text = "Remove",
					Size = new Size(85, 40),
					Location = new Point(pnlAlliances.Width - 117, (btnRemoves.Count) * 42),
					Name = "btnRemove" + btnRemoves.Count
				};
				btnRemove.Click += new EventHandler(BtnRemove_Click);
				btnRemoves.Add(btnRemove);
				pnlAlliances.Controls.Add(lblColors[lblColors.Count - 1]);
				pnlAlliances.Controls.Add(lblNames[lblNames.Count - 1]);
				pnlAlliances.Controls.Add(btnRemoves[btnRemoves.Count - 1]);
				btnDone.Visible = false;
				cmbAddAlliance.Visible = false;
			} catch
			{
				MessageBox.Show("You must select a player to form an alliance with");
			}
		}

		private void BtnRemove_Click(object sender, EventArgs e)
		{
			Button sndr = (Button)sender;
			int sndrNum = Convert.ToInt32(sndr.Name.Replace("btnRemove", ""));
			lblNames.RemoveAt(sndrNum);
			lblColors.RemoveAt(sndrNum);
			btnRemoves.RemoveAt(sndrNum);
			int player1 = Settings.allianceSender;
			Color player1Color = Settings.colorArr[Settings.allianceSender];
			Color player2Color = Settings.validColors[Settings.allianceSender][sndrNum];
			int player2 = Array.IndexOf(Settings.colorArr, player2Color);
			//remove player2 from player1
			Settings.validColors[Settings.allianceSender].RemoveAt(sndrNum);
			//remove player1 from player2
			Settings.validColors[player2].RemoveAt(Settings.validColors[player2].IndexOf(player1Color	));
			Alliances_Load(sender, e);
		}

		private void BtnAddAlliance_Click(object sender, EventArgs e)
		{
			cmbAddAlliance.Visible = true;
			btnDone.Visible = true;
			cmbAddAlliance.Items.Clear();
			//foreach player add to cmb
			int noPlayers = Settings.noPlayers;
			if (noPlayers < 2)
			{
				noPlayers = 2;
			}
			for (int i = 0; i < noPlayers; i++)
			{
				if (i != Settings.allianceSender && !Settings.validColors[Settings.allianceSender].Contains(Settings.colorArr[i]))
				{
					if (Settings.names[i] == "")
					{
						cmbAddAlliance.Items.Add("Player " + (i + 1));
					}
					else
					{
						cmbAddAlliance.Items.Add(Settings.names[i]);
					}
				}
			}
		}
	}
}
