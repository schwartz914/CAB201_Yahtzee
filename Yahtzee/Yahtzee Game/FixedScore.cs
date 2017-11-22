using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Yahtzee_Game {

    /// <summary>
    /// Yahtzee | Class Assignment CAB201 Semester 1 2016
    /// FixedScore.cs
    /// 
    /// Assigns points based on the button pressed and dice combinations
    /// 
    /// Author: Peter Schwartz (N9500375) and Trent Newton (N9499911)
    /// Date: 7th June 2016
    /// </summary>
    [Serializable]
    class FixedScore : Combination {
        private ScoreType scoreCombo;

        public FixedScore(ScoreType type, Label label1) : base(label1) {
            scoreCombo = type;
        }//END FixedScore

        override public void CalculateScore(int[] dieValues) {
            Sort(dieValues);
            CheckForYahtzee(dieValues);
            if (IsYahtzee) {
                YahtzeeNumber = dieValues[0];
            }

            switch (scoreCombo) {
                case (ScoreType.FullHouse):
                    if ((dieValues[0] == dieValues[1] && dieValues[0] == dieValues[2] && dieValues[3] == dieValues[4]) || (dieValues[0] == dieValues[1] && dieValues[2] == dieValues[3] && dieValues[2] == dieValues[4])) {
                        Points = 25;
                    } else {
                        Points = 0;
                    }
                    break;
                case (ScoreType.SmallStraight):
                    //If not Yahztee zero's any duplicate values.
                    if (!IsYahtzee) {
                        for (int i = 1; i < dieValues.Length; i++) {
                            if (dieValues[i - 1] == dieValues[i]) {
                                dieValues[i] = 0;
                            }
                        }
                    }
                    Sort(dieValues);
                    if ((dieValues[0] == dieValues[1] - 1 && dieValues[1] == dieValues[2] - 1 && 
                        dieValues[2] == dieValues[3] - 1 && dieValues[3] == dieValues[4] - 1) || 
                        (dieValues[1] == dieValues[2] - 1 && dieValues[2] == dieValues[3] - 1 && 
                        dieValues[3] == dieValues[4] - 1 && dieValues[4] == dieValues[3] + 1)) {
                        Points = 30;
                    } else {
                        Points = 0;
                    }
                    break;
                case (ScoreType.LargeStraight):
                    if (dieValues[0] == dieValues[1] - 1 && dieValues[1] == dieValues[2] - 1 && dieValues[2] == dieValues[3] - 1
                        && dieValues[3] == dieValues[4] - 1 && dieValues[4] == dieValues[3] + 1) {
                        Points = 40;
                    } else {
                        Points = 0;
                    }
                    break;
                case (ScoreType.Yahtzee):
                    if (dieValues[0] == dieValues[1] && dieValues[1] == dieValues[2] && dieValues[2] == dieValues[3] && 
                        dieValues[3] == dieValues[4] && dieValues[4] == dieValues[0]) {
                        Points = 50;
                    } else {
                        Points = 0;
                    }
                    break;
            }
            done = true;

        }//END CalculateScore

        public void PlayYahtzeeJoker() {
            switch (scoreCombo) {
                case (ScoreType.SmallStraight):
                    Points = 30;
                    break;
                case (ScoreType.LargeStraight):
                    Points = 40;
                    break;
            }
        }//END PlayYahtzeeJoker
    }//END FixedScore : Combination
}//END Yahtzee_Game
