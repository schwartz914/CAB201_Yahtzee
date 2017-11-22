using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee_Game {

    /// <summary>
    /// Yahtzee | Class Assignment CAB201 Semester 1 2016
    /// CountingCombination.cs
    /// 
    /// Calculates Total Score
    /// 
    /// Author: Peter Schwartz (N9500375) and Trent Newton (N9499911)
    /// Date: 7th June 2016
    /// </summary>
    [Serializable]
    class CountingCombination : Combination {
        private int dieValue;

        public CountingCombination(ScoreType score, Label label1) : base(label1) {
            dieValue = (int)score + 1;
        }//END CountingCombination

        override public void CalculateScore(int[] dieValues) {
            int totalScore = 0;
            CheckForYahtzee(dieValues);

            for (int i = 0; i < dieValues.Length; i++) {
                if (dieValues[i] == dieValue) {
                    totalScore += dieValue;
                }
            }
            done = true;
            Points = totalScore;

        }//END CalculateScore
    }//END CountingCombination : Combination
}//END Yahtzee_Game
