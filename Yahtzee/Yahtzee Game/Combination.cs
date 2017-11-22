using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee_Game {

    /// <summary>
    /// Yahtzee | Class Assignment CAB201 Semester 1 2016
    /// Combination.cs
    /// 
    /// Sorts the array of scores and checks if the die face values are Yahtzee
    /// 
    /// Author: Peter Schwartz (N9500375) and Trent Newton (N9499911)
    /// Date: 7th June 2016
    /// </summary>
    [Serializable]
    abstract class Combination : Score {
        protected bool isYahtzee;
        protected int yahtzeeNumber;


        public Combination(Label label1) : base(label1) {

        }//END Combination

        abstract public void CalculateScore(int[] dieValues);


        public void Sort(int[] dice) {
            Array.Sort(dice);
        }//END Sort

        public bool IsYahtzee {
            get {
                return isYahtzee;
            }

            set {
                isYahtzee = value;
            }
        }//END IsYahtzee

        public int YahtzeeNumber {
            get {
                return yahtzeeNumber;
            }

            set {
                yahtzeeNumber = value;
            }
        }//END YahtzeeNumber

        public void CheckForYahtzee(int[] dieValues) {
            if (dieValues[0] == dieValues[1] && dieValues[1] == dieValues[2] && dieValues[2] == dieValues[3] && dieValues[3] == dieValues[4]) {
                IsYahtzee = true;
            } else {
                IsYahtzee = false;
            }

        }//END CheckForYahtzee
    }//Combination : Score
}//END Yahtzee_Game
