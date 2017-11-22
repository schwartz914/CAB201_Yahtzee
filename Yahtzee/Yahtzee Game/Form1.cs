using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee_Game
{

	/// <summary>
	/// Yahtzee | Class Assignment CAB201 Semester 1 2016
    /// Form1.cs
	/// 
	/// Initialise the game, handles button events and manipulates visible objects
	/// 
	/// Author: Peter Schwartz (N9500375) and Trent Newton (N9499911)
	/// Date: 7th June 2016
	/// </summary>
    public partial class Form1 : Form
    {
        private Label[] dice = new Label[5];
        private Button[] scoreButtons = new Button[(int)ScoreType.Yahtzee + 1];
        private Label[] scoreTotals = new Label[19];
        private CheckBox[] checkBoxes = new CheckBox[5];
        private Game game;

        public Form1() {
            InitializeComponent();
            InitializeLabelsAndButtons();
            DisableAllButtons();
            DisableAndClearCheckBoxes();
            DisableRollButton();
            saveToolStripMenuItem.Enabled = false;

		}//END Form1

        private void InitializeLabelsAndButtons() {
            dice[0] = die1Label;
            dice[1] = die2Label;
            dice[2] = die3Label;
            dice[3] = die4Label;
            dice[4] = die5Label;

            scoreButtons[(int)ScoreType.Ones] = button1;
            scoreButtons[(int)ScoreType.Twos] = button2;
            scoreButtons[(int)ScoreType.Threes] = button3;
            scoreButtons[(int)ScoreType.Fours] = button4;
            scoreButtons[(int)ScoreType.Fives] = button5;
            scoreButtons[(int)ScoreType.Sixes] = button6;
            scoreButtons[(int)ScoreType.ThreeOfAKind] = button7;
            scoreButtons[(int)ScoreType.FourOfAKind] = button8;
            scoreButtons[(int)ScoreType.FullHouse] = button9;
            scoreButtons[(int)ScoreType.SmallStraight] = button10;
            scoreButtons[(int)ScoreType.LargeStraight] = button11;
            scoreButtons[(int)ScoreType.Chance] = button12;
            scoreButtons[(int)ScoreType.Yahtzee] = button13;

            scoreTotals[0] = scoreLabel1;
            scoreTotals[1] = scoreLabel2;
            scoreTotals[2] = scoreLabel3;
            scoreTotals[3] = scoreLabel4;
            scoreTotals[4] = scoreLabel5;
            scoreTotals[5] = scoreLabel6;
            scoreTotals[6] = subTotalScoreLabel;
            scoreTotals[7] = bonus63ScoreLabel;
            scoreTotals[8] = upperTotalScoreLabel;
            scoreTotals[9] = scoreLabel7;
            scoreTotals[10] = scoreLabel8;
            scoreTotals[11] = scoreLabel9;
            scoreTotals[12] = scoreLabel10;
            scoreTotals[13] = scoreLabel11;
            scoreTotals[14] = scoreLabel12;
            scoreTotals[15] = scoreLabel13;
            scoreTotals[16] = yahtzeeBonusScoreLabel;
            scoreTotals[17] = lowerTotalScoreLabel;
            scoreTotals[18] = grandTotalScoreLabel;


            checkBoxes[0] = die1CheckBox;
            checkBoxes[1] = die2CheckBox;
            checkBoxes[2] = die3CheckBox;
            checkBoxes[3] = die4CheckBox;
            checkBoxes[4] = die5CheckBox;
		}//END InitializeLabelsAndButtons

        public Label[] GetDice() {
            return dice;
		}//END GetDice

        public Label[] GetScoresTotals() {
            return scoreTotals;
		}//END GetScoresTotals

        public void ShowPlayerName(string name) {
            playerLabel.Text = name;
		}//END ShowPlayerName

        public void EnableRollButton() {
            rollDieButton.Enabled = true;
		}//END EnableRollButton

        public void DisableRollButton() {
            rollDieButton.Enabled = false;
		}//END DisableRollButton

        public void EnableCheckBoxes() {
            for (int i = 0; i < checkBoxes.Length; i++) {
                checkBoxes[i].Enabled = true;
            }
		}//END EnableCheckBoxes

        public void DisableAndClearCheckBoxes() {
            for (int i = 0; i < checkBoxes.Length; i++) {
                checkBoxes[i].Checked = false;
                checkBoxes[i].Enabled = false;
            }
		}//END DisableAndClearCheckBoxes

        public void EnableScoreButton(ScoreType combo) {
            if (scoreButtons[(int)combo] != null) {
                scoreButtons[(int)combo].Enabled = true;
            }
		}//END EnableScoreButton


        public void DisableScoreButton(ScoreType combo) {
            if (scoreButtons[(int)combo] != null) {
                scoreButtons[(int)combo].Enabled = false;
            }
		}//END DisableScoreButton

        public void CheckCheckBox(int index) {
            checkBoxes[index].Checked = true;
		}//END CheckCheckBox

        public void ShowMessage(string message) {
            messageLabel.Text = message;
		}//END ShowMessage

        public void ShowOKButton() {
            okButton.Visible = !okButton.Visible;
            if (okButton.Visible) {
                DisableAllButtons();
                DisableRollButton();
            } else {
                ClearDieText();
            }
		}//END ShowOKButton

        public void StartNewGame() {
            game = new Game(this);
            EnableRollButton();
            playerBindingSource.DataSource = game.Players;
            numericUpDown1.Enabled = true;
            saveToolStripMenuItem.Enabled = true;
            loadToolStripMenuItem.Enabled = false;
            ClearScreen();
		}//END StartNewGame

        /// <summary>
        /// Clears the Dice Labels
        /// </summary>
        private void ClearDieText() {
            for (int i = 0; i < dice.Length; i++) {
                dice[i].Text = "";
            }
		}//END ClearDieText

        /// <summary>
        /// Disable all buttons
        /// </summary>
        private void DisableAllButtons() {
            for (ScoreType variable = ScoreType.Ones; variable <= ScoreType.Yahtzee; variable++) {
                if (scoreButtons[(int)variable] != null) {
                    scoreButtons[(int)variable].Enabled = false;
                }
            }
		}//END DisableAllButtons

        /// <summary>
        /// Clears the score labels on the form
        /// </summary>
        private void ClearScreen() {
            ClearDieText();
            for (int i = 0; i < scoreTotals.Length; i++) {
                scoreTotals[i].Text = "";
            }
		}//END ClearScreen

        /// <summary>
        /// Updates the Data Grid View
        /// </summary>
        public void UpdateGridView() {
            playerBindingSource.ResetBindings(false);
		}//END UpdateGridView

        /// <summary>
        /// Reads the value of the numericUpDown1
        /// </summary>
        /// <returns>int value of numericUpDown</returns>
        public int NumberPlayers() {
            return (int)numericUpDown1.Value;
		}//END NumberPlayers


        private void newToolStripMenuItem_Click(object sender, EventArgs e) {
            StartNewGame();
		}//END newToolStripMenuItem_Click

        private void rollDieButton_Click(object sender, EventArgs e) {
            numericUpDown1.Enabled = false;
            game.RollDice();
		}//END rollDieButton_Click


        private void DiceCheckBox(object sender, EventArgs e) {
            int checkIndex = Array.IndexOf(checkBoxes, sender);
            if(checkBoxes[checkIndex].Checked) {
                game.HoldDie(checkIndex);
            } else if(!checkBoxes[checkIndex].Checked) {
                game.ReleaseDie(checkIndex);
            }
        }//End DiceCheckBox

        private void ScoreButtonClick(object sender, EventArgs e) {
            int scoreIndex = Array.IndexOf(scoreButtons, sender);
            game.ScoreCombination((ScoreType)scoreIndex);
        } //End ScoreButtonClick


        private void okButton_Click(object sender, EventArgs e) {
            game.NextTurn();
		}//END okButton_Click

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            game.Save();
		}//END saveToolStripMenuItem_Click

        private void loadToolStripMenuItem_Click(object sender, EventArgs e) {
            game = Game.Load(this);
            playerBindingSource.DataSource = game.Players;
            UpdateGridView();
            ClearDieText();
		}//END loadToolStripMenuItem_Click
	}//END Form1 : Form
}//END Yahtzee_Game

