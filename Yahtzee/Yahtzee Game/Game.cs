using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Yahtzee_Game {

    public enum ScoreType {
        Ones, Twos, Threes, Fours, Fives, Sixes,
        SubTotal, BonusFor63Plus, SectionATotal,
        ThreeOfAKind, FourOfAKind, FullHouse,
        SmallStraight, LargeStraight, Chance, Yahtzee,
        YahtzeeBonus, SectionBTotal, GrandTotal
    }


    /// <summary>
    /// Yahtzee | Class Assignment CAB201 Semester 1 2016
    /// Game.cs
    /// 
    /// Updates the properties of the objects on Form1
    /// 
    /// Author: Peter Schwartz (N9500375) and Trent Newton (N9499911)
    /// Date: 7th June 2016
    /// </summary>
    [Serializable]
    class Game {
        public static string defaultPath = Environment.CurrentDirectory;
        private static string savedGameFile = defaultPath + "\\YahtzeeGame.dat";

        private readonly string[] ROLLMESSAGES = new string[] {"Roll 1", "Roll 2 or choose a combination to score",
                    "Roll 3 or choose a combination to score", "Choose a combination to score", "Click OK" };

        private BindingList<Player> players;
        private int currentPlayerIndex;
        private Player currentPlayer;
        private Die[] dice = new Die[5];
        private int playersFinished;
        private int numRolls;
        [NonSerialized]
        private Form1 form;
        [NonSerialized]
        private Label[] dieLabels;

        public Game(Form1 form1) {
            form = form1;
            playersFinished = 0;
            currentPlayerIndex = 0;
            players = new BindingList<Player>();
            dieLabels = form.GetDice();
            numRolls = 0;
            int numPlayers = form.NumberPlayers();
            for (int i = 0; i < numPlayers; i++) {
                string name = "Player " + (i + 1);
                players.Add(new Player(name, form.GetScoresTotals()));
            }

            currentPlayer = players[currentPlayerIndex];
            form.ShowPlayerName(currentPlayer.Name);
            form.ShowMessage(ROLLMESSAGES[numRolls]);
            for (int i = 0; i < dice.Length; i++) {
                dice[i] = new Die(dieLabels[i]);
            }
        }//END Game

        public BindingList<Player> Players {
            get {
                return players;
            }
        }//END Players

        public void NextTurn() {
            //If the last player chnage to the first(0) player otherwise moves to the next player.
            if (currentPlayerIndex == players.Count - 1) {
                currentPlayerIndex = 0;
            } else {
                currentPlayerIndex++;
            }

            IncreasePlayersFinished();
            SwitchPlayers();
            IsGameFinished();
        }//END NextTurn

        public void RollDice() {
            //Used to check the number of players on the first roll and adjusts to the value of numericUpDown1
            CheckNumPlayers(); 
            for (int i = 0; i < dice.Length; i++) {
                if (dice[i].Active) {
                    dice[i].Roll();
                }
            }
            numRolls++;
            if (numRolls == 1) {
                form.ShowMessage(ROLLMESSAGES[numRolls]);
                form.EnableCheckBoxes();
                EnableScoreButtons();
                DisableUsedButtons();
            } else if (numRolls == 2) {
                form.ShowMessage(ROLLMESSAGES[numRolls]);
            } else if (numRolls == 3) {
                form.ShowMessage(ROLLMESSAGES[numRolls]);
                form.DisableRollButton();
            }
        }//END RollDice

        public void HoldDie(int dieNumber) {
            dice[dieNumber].Active = false;
        }//END HoldDie

        public void ReleaseDie(int dieNumber) {
            dice[dieNumber].Active = true;
        }//END ReleaseDie

        public void ScoreCombination(ScoreType score) {
            //Used to Show the correct message after the user selects scoring option.
            numRolls = 4; 
            form.ShowMessage(ROLLMESSAGES[numRolls]);
            int[] dieValues = new int[5];
            for (int i = 0; i < 5; i++) {
                dieValues[i] = dice[i].FaceValue;
            }
            currentPlayer.ScoreCombinations(score, dieValues);
            form.ShowOKButton();
            form.UpdateGridView();
        }//END ScoreCombination

        public static Game Load(Form1 form) {
            Game game = null;
            if (File.Exists(savedGameFile)) {
                try {
                    Stream bStream = File.Open(savedGameFile, FileMode.Open);
                    BinaryFormatter bFormatter = new BinaryFormatter();
                    game = (Game)bFormatter.Deserialize(bStream);
                    bStream.Close();
                    game.form = form;
                    game.ContinueGame();
                    return game;
                } catch {
                    MessageBox.Show("Error reading saved game file.\nCannot load saved game.");
                }
            } else {
                MessageBox.Show("No current saved game.");
            }
            return null;
        }//END Load


        public void Save() {
            try {
                Stream bStream = File.Open(savedGameFile, FileMode.Create);
                BinaryFormatter bFormatter = new BinaryFormatter();
                bFormatter.Serialize(bStream, this);
                bStream.Close();
                MessageBox.Show("Game saved");
            } catch (Exception e) {

                MessageBox.Show(e.ToString());
                MessageBox.Show("Error saving game.\nNo game saved.");
            }
        }//END Save

        private void ContinueGame() {
            LoadLabels(form);
            for (int i = 0; i < dice.Length; i++) {
                //uncomment one of the following depending how you implmented Active of Die
                // dice[i].SetActive(true);
                dice[i].Active = true;
            }

            form.ShowPlayerName(currentPlayer.Name);
            form.EnableRollButton();
            form.EnableCheckBoxes();
            // can replace string with whatever you used
            form.ShowMessage(ROLLMESSAGES[numRolls]);
            currentPlayer.ShowScores();
        }//END ContinueGame

        private void LoadLabels(Form1 form) {
            Label[] diceLabels = form.GetDice();
            for (int i = 0; i < dice.Length; i++) {
                dice[i].Load(diceLabels[i]);
            }
            for (int i = 0; i < players.Count; i++) {
                players[i].Load(form.GetScoresTotals());
            }
        }//END LoadLabels

        /// <summary>
        /// Checks the numericUpDown1 value after new game is started and adjusts the players based off this value after
        /// the rolldice button is pressed.
        /// </summary>
        private void CheckNumPlayers() {
            if (players.Count < form.NumberPlayers()) {
                for (int i = players.Count; i < form.NumberPlayers(); i++) {
                    string name = "Player " + (i + 1);
                    players.Add(new Player(name, form.GetScoresTotals()));
                }
            } else if (players.Count > form.NumberPlayers()) {
                for (int i = players.Count - 1; i >= form.NumberPlayers(); i--) {
                    players.Remove(players[i]);
                }
            }
        }//END CheckNumPlayers

        /// <summary>
        /// Sets the player to next player and sets the buttons and checkbuttons to the correct set.
        /// </summary>
        private void SwitchPlayers() {
            currentPlayer = players[currentPlayerIndex];
            form.ShowPlayerName(currentPlayer.Name);
            currentPlayer.ShowScores();
            numRolls = 0;
            form.ShowMessage(ROLLMESSAGES[numRolls]);
            DisableAllButtons();
            form.EnableRollButton();
            form.DisableAndClearCheckBoxes();
            form.ShowOKButton();
        }//END SwitchPlayers

        /// <summary>
        /// Checks if the player is finished and if so increments playerFinished.
        /// </summary>
        private void IncreasePlayersFinished() {
            if (currentPlayer.IsFinished()) {
                playersFinished++;
            }
        }//END IncreasePlayersFinished

        /// <summary>
        /// checks if all players are finished and finishes the game if so.
        /// </summary>
        private void IsGameFinished() {
            if (playersFinished == players.Count) {
                form.UpdateGridView();
                FinishGame();
            }
        }//END IsGameFinished

        /// <summary>
        /// Enables all the score buttons
        /// </summary>
        private void EnableScoreButtons() {
            for (ScoreType i = ScoreType.Ones; i < ScoreType.YahtzeeBonus; i++) {
                form.EnableScoreButton(i);
            }
        }//END EnableScoreButtons

        /// <summary>
        /// Disables all the score buttons
        /// </summary>
        private void DisableAllButtons() {
            for (ScoreType i = ScoreType.Ones; i < ScoreType.YahtzeeBonus; i++) {
                form.DisableScoreButton(i);
            }
        }//END DisableAllButtons

        /// <summary>
        /// Finishes the game and asks if the user wants  to play again
        /// </summary>
        private void FinishGame() {
            string text;
            string playerWinners = "";
            if (players.Count > 1) {
                int highScore = 0;
                for (int i = 0; i < players.Count; i++) {
                    if (players[i].GrandTotal > highScore) {
                        highScore = players[i].GrandTotal;
                    }
                }
                for (int i = 0; i < players.Count; i++) {
                    if (players[i].GrandTotal == highScore) {
                        playerWinners += players[i].Name + ", ";
                    }
                }
                text = string.Format("The Winner is {0} with a score of: {1}", playerWinners, highScore);
                MessageBox.Show(text);
            } else {
                text = string.Format("Congratulations {0}, you finished with a score of: {1}", players[0].Name, players[0].GrandTotal);
                MessageBox.Show(text);
            }
            DialogResult result = MessageBox.Show("Would you like to Play Again?", "Play Again?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes) {
                form.StartNewGame();
            } else if (result == DialogResult.No) {
                MessageBox.Show("Thanks for Playing!");
                Application.Exit();
            }
        }//END FinishGame

        /// <summary>
        /// Disables the buttons for scores that have been used.
        /// </summary>
        private void DisableUsedButtons() {
            for (int i = 0; i < (int)ScoreType.YahtzeeBonus; i++) {
                if (currentPlayer.IsAvailable((ScoreType)i) == false &&
                  (i < (int)ScoreType.SubTotal || (i > (int)ScoreType.SectionATotal && i < (int)ScoreType.YahtzeeBonus))) {
                    form.DisableScoreButton((ScoreType)i);
                }
            }
        }//END DisableUsedButtons
    }//END Game
}//END Yahtzee_Game
