using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace Yahtzee_Game {

    /// <summary>
    /// Yahtzee | Class Assignment CAB201 Semester 1 2016
    /// Die.cs
    /// 
    /// Rolls Dice and returns face values
    /// 
    /// Author: Peter Schwartz (N9500375) and Trent Newton (N9499911)
    /// Date: 7th June 2016
    /// </summary>
    [Serializable]
    class Die {
        private static string rollFileName = Game.defaultPath + "\\basictestrolls.txt";

        private const int MAX_FACE_VALUE = 6;
        private int faceValue;
        private bool active;
        [NonSerialized]
        private Label label;
        private static Random random = new Random();
        [NonSerialized]
        private static StreamReader rollFile = new StreamReader(rollFileName);
        private static bool DEBUG = false;


        public Die(Label dieNumber) {
            label = dieNumber;
            active = true;
        }//END Die

        public int FaceValue {
            get {
                return faceValue;
            }
        }//END FaceValue

        public bool Active {
            get {
                return active;
            }

            set {
                active = value;
            }
        }//END Active


        public void Roll() {
            if (!DEBUG) {
                int iterations = random.Next(5, 16);
                for (int i = 0; i < iterations; i++) {
                    faceValue = random.Next(1, MAX_FACE_VALUE + 1);
                    label.Text = faceValue.ToString();
                    label.Refresh();
                    Thread.Sleep(100);
                }
            } else {
                faceValue = int.Parse(rollFile.ReadLine());
                label.Text = faceValue.ToString();
                label.Refresh();
            }
        }//END Roll

        public void Load(Label label) {
            this.label = label;
            if (faceValue == 0) {
                label.Text = string.Empty;
            } else {
                label.Text = faceValue.ToString();
            }
        }//END Load
    }//END Die
}//END Yahtzee_Game
