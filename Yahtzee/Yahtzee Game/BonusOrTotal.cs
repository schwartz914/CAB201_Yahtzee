using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee_Game {

    /// <summary>
    /// Yahtzee | Class Assignment CAB201 Semester 1 2016
    /// BonusOrTotal.cs
    /// 
    /// Used for Bonus and Sub-Total scores
    /// 
    /// Author: Peter Schwartz (N9500375) and Trent Newton (N9499911)
    /// Date: 7th June 2016
    /// </summary>
    [Serializable]
    class BonusOrTotal : Score {

        public BonusOrTotal(Label label1) : base(label1) {
            done = true;
        }//END BonusOrTotal
    } //END BonusOrTotal : Score
}//END Yahtzee_Game
