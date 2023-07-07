using System.Drawing;
using System.Windows.Forms;

namespace GameOnWinForms
{
    static class Debug
    {
        private static TextBox space;

        public static void Instantate(Form1 form)
        {
            space = new TextBox();
            space.Location = new Point(100, form.Height * 2 - 10);
            space.Size = new Size(300, 200);
            form.Controls.Add(space);
        }

        public static void Log(string text)
        {
            space.Text += text;
            space.Refresh();
        }
    }
}
