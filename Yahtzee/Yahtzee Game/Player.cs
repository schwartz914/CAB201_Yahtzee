using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee_Game {
    /// <summary>
    /// Yahtzee | Class Assignment CAB201 Semester 1 2016
    /// Player.cs
    /// 
    /// Determines Playable Combinations and Calculates Scores
    /// 
    /// Author: Peter Schwartz (N9500375) and Trent Newton (N9499911)
    /// Date: 7th June 2016
    /// </summary>
    [Serializable]
    class Player {
        private string name;
        private int combinationsToDo = 0;
        private Score[] scores;
        private int grandTotal;


        public Player(string playerName, Label[] playerScores) {
            name = playerName;
            grandTotal = 0;
            scores = new Score[19];
            for (int i = 0; i <= (int)ScoreType.GrandTotal; i++) {
                switch ((ScoreType)i) {
                    case (ScoreType.Ones):
                    case (ScoreType.Twos):
                    case (ScoreType.Threes):
                    case (ScoreType.Fours):
                    case (ScoreType.Fives):
                    case (ScoreType.Sixes):
                        scores[i] = new CountingCombination((ScoreType)i, playerScores[i]);
                        break;

                    case (ScoreType.ThreeOfAKind):
                    case (ScoreType.FourOfAKind):
                    case (ScoreType.Chance):
                        scores[i] = new TotalOfDice((ScoreType)i, playerScores[i]);
                        break;
                    case (ScoreType.FullHouse):
                    case (ScoreType.SmallStraight):
                    case (ScoreType.LargeStraight):
                    case (ScoreType.Yahtzee):
                        scores[i] = new FixedScore((ScoreType)i, playerScores[i]);
                        break;
                    case (ScoreType.SubTotal):
                    case (ScoreType.BonusFor63Plus):
                    case (ScoreType.SectionATotal):
                    case (ScoreType.YahtzeeBonus):
                    case (ScoreType.SectionBTotal):
                    case (ScoreType.GrandTotal):
                        scores[i] = new BonusOrTotal(playerScores[i]);
                        break;
                }
            }
        }//END Player

        public string Name {

            get {
                return name;
            }

            set {
                name = value;
            }
        }//END Name

        public int GrandTotal {

            get {
                return grandTotal;
            }

            set {
                grandTotal = value;
            }
        }//END GrandTotal

        public void ScoreCombinations(ScoreType type, int[] dieValues) {

            //Checks if the Yahtzee has been played before.
            bool yahtzeePlayed = HasYahtzeePlayed();


            //Applies the score.
            ((Combination)scores[(int)type]).CalculateScore(dieValues);

            //Appies the Yahtzee Bonus if Yahtzee has been played.
            YahtzeeBonus(type, yahtzeePlayed);

            //Checks for Yahtzee Joker and applies the score if the upper section has been scored.
            YahtzeeJoker(type, dieValues);

            combinationsToDo++;
            CalculateLowerScore();
            CalculateUpperScore();
            CalculateGrandScore();
            ShowScores();
        }//END ScoreCombinations

        /// <summary>
        /// Checks if yahtzee has been played
        /// </summary>
        /// <returns>True or False</returns>
        private bool HasYahtzeePlayed() {
            if (scores[(int)ScoreType.Yahtzee].Done && scores[(int)ScoreType.Yahtzee].Points == 50) {
                return true;
            } else {
                return false;
            }
        }//END HasYahtzeePlayed

        /// <summary>
        /// Checks if Yahtzee has been played
        /// </summary>
        /// <param name="type">ScoreType of scored variable</param>
        /// <param name="yahtzeePlayed">Bool value if yahtzee is played</param>
        private void YahtzeeBonus(ScoreType type, bool yahtzeePlayed) {
            if (((Combination)scores[(int)type]).IsYahtzee && yahtzeePlayed) {
                scores[(int)ScoreType.YahtzeeBonus].Points += 100; 
            }
        }//END YahtzeeBonus

        /// <summary>
        /// Calculates if Yahtzee Joker is playable.
        /// </summary>
        /// <param name="type">ScoreType of scored variable</param>
        /// <param name="dieValues">DieValue Array of Rolled dice.</param>
        private void YahtzeeJoker(ScoreType type, int[] dieValues) {
            if (((Combination)(scores[(int)type])).IsYahtzee) {
                int jokerCheck = dieValues[0] - 1; ;
                if ((scores[jokerCheck]).Done && ((int)type == (int)ScoreType.SmallStraight || (int)type == (int)ScoreType.LargeStraight)) {
                    ((FixedScore)scores[(int)type]).PlayYahtzeeJoker();
                }
            }
        }//END YahtzeeJoker

        /// <summary>
        /// Calculates the upper section scores.
        /// </summary>
        private void CalculateUpperScore() {
            int upperTotal = 0;
            int subTotal = 0;

            //Calculate the scores for subTotal            
            for (int i = 0; i < (int)ScoreType.SubTotal; i++) {
                subTotal += scores[i].Points;
            }

            scores[(int)ScoreType.SubTotal].Points = subTotal;
            if (scores[(int)ScoreType.SubTotal].Points >= 63) {
                scores[(int)ScoreType.BonusFor63Plus].Points = 35;
            }

            //Calculate the Upper total score.            
            upperTotal = scores[(int)ScoreType.SubTotal].Points + scores[(int)ScoreType.BonusFor63Plus].Points;

            scores[(int)ScoreType.SectionATotal].Points = upperTotal;
        }//END CalculateUpperScore

        /// <summary>
        /// Calculates the Lower Section totals
        /// </summary>
        private void CalculateLowerScore() {
            int lowerTotal = 0;

            //Calculate the Lower total
            for (int i = (int)ScoreType.ThreeOfAKind; i < (int)ScoreType.YahtzeeBonus + 1; i++) {
                lowerTotal += scores[i].Points;
            }
            scores[(int)ScoreType.SectionBTotal].Points = lowerTotal;
        }//END CalculateLowerScore

        /// <summary>
        /// Calculates the Grand Total score
        /// </summary>
        private void CalculateGrandScore() {
            //Calculate the grand total
            GrandTotal = scores[(int)ScoreType.SectionATotal].Points + scores[(int)ScoreType.SectionBTotal].Points;
            scores[(int)ScoreType.GrandTotal].Points = GrandTotal;
        }//END CalculateGrandScore


        public bool IsAvailable(ScoreType typeOfScore) {
            if (scores[(int)typeOfScore].Done) {
                return false;
            } else {
                return true;
            }
        }//END IsAvailable

        public void ShowScores() {
            for (int i = 0; i < (int)ScoreType.GrandTotal + 1; i++) {
                scores[i].ShowScore();
            }
        }//END ShowScores

        public bool IsFinished() {
            if (combinationsToDo == 13) {
                return true;
            } else {
                return false;
            }
        }//END IsFinished

        public void Load(Label[] scoreTotals) {
            for (int i = 0; i < scores.Length; i++) {
                scores[i].Load(scoreTotals[i]);
            }
        }//END Load
    }//END Player
}//END Yahtzee_Game