using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GameOnWinForms
{
    static class InputSys
    {
        static public bool W;
        static public bool A;
        static public bool S;
        static public bool D;
        static public bool E;

        static private bool pressDown_E = false;
        static public bool PressDown_E
        {
            get
            {
                var oldValue = pressDown_E;
                pressDown_E = false;
                return oldValue;
            }
            set => pressDown_E = value;
        }

        public static void DownButtons(Keys key)
        {
            switch (key)
            {
                case Keys.W:
                    W = true;
                    break;
                case Keys.A:
                    A = true;
                    break;
                case Keys.S:
                    S = true;
                    break;
                case Keys.D:
                    D = true;
                    break;
                case Keys.E:
                    E = true;
                    pressDown_E = true;
                    break;
            }
        }

        public static void UpButtons(Keys key)
        {
            switch (key)
            {
                case Keys.W:
                    W = false;
                    break;
                case Keys.A:
                    A = false;
                    break;
                case Keys.S:
                    S = false;
                    break;
                case Keys.D:
                    D = false;
                    break;
                case Keys.E:
                    E = false;
                    pressDown_E = false;
                    break;
            }
        }
    }
}
