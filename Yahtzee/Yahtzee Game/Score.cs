using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee_Game {

    /// <summary>
    /// Yahtzee | Class Assignment CAB201 Semester 1 2016
    /// Score.cs
    /// 
    /// Accesses Done, Points and Display the Score
    /// 
    /// Author: Peter Schwartz (N9500375) and Trent Newton (N9499911)
    /// Date: 7th June 2016
    /// </summary>
    [Serializable]
    abstract class Score {
        private int points;
        [NonSerialized]
        private Label label;
        protected bool done;

        public Score(Label label1) {
            label = label1;
            done = false;
        }//END Score

        public bool Done {

            get {
                return done;
            }
        }//END Done

        public int Points {
            get {
                return points;
            }

            set {
                points = value;
            }
        }//END Points

        public void ShowScore() {
            if (done) {
                label.Text = Points.ToString();
            } else {
                label.Text = "";
            }
        }//END ShowScore

        public void Load(Label label) {
            this.label = label;
        } //END Load
    }//END Score
}//END Yahtzee_Game
