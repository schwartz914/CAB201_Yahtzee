using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee_Game {

    /// <summary>
    /// Yahtzee | Class Assignment CAB201 Semester 1 2016
    /// 
    /// Calculate Sum of points for Chance, 3 and 4 of a Kind
    /// 
    /// Author: Peter Schwartz (N9500375) and Trent Newton (N9499911)
    /// Date: 7th June 2016
    /// </summary>
    [Serializable]
    class TotalOfDice : Combination {
        private int numberOfOneKind;

        public TotalOfDice(ScoreType score, Label label1) : base(label1) {
            switch (score) {
                case (ScoreType.ThreeOfAKind):
                    numberOfOneKind = 3;
                    break;
                case (ScoreType.FourOfAKind):
                    numberOfOneKind = 4;
                    break;
                case (ScoreType.Chance):
                    numberOfOneKind = 0;
                    break;
            }
        }//END TotalOfDice

        override public void CalculateScore(int[] dieValues) {
            CheckForYahtzee(dieValues);
            Sort(dieValues);
            switch (numberOfOneKind) {
                //Checks for 3 of a kind
                case (3): 
                    if ((dieValues[0] == dieValues[1] && dieValues[1] == dieValues[2]) || (dieValues[1] == dieValues[2] && dieValues[1] == dieValues[3]) ||
                        (dieValues[2] == dieValues[3] && dieValues[2] == dieValues[4])) {
                        Points = dieValues.Sum();
                    }
                    break;
                //Checks for 4 of a kinds
                case (4): 
                    if ((dieValues[0] == dieValues[1] && dieValues[1] == dieValues[2] && dieValues[0] == dieValues[3]) ||
                        (dieValues[1] == dieValues[2] && dieValues[1] == dieValues[3] && dieValues[1] == dieValues[4])) {
                        Points = dieValues.Sum();
                    }
                    break;
                case (0):
                    //Adds dice for chance score.
                    Points = dieValues.Sum(); 
                    break;
            }
            done = true;
        }//END CalculateScore
    }//END TotalOfDice : Combination
}//END Yahtzee_Game
