using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace GameOnWinForms
{
    public partial class Form1 : Form
    {
        ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
        private Graphics formGraphics;

        internal Scen scen { get; set; }

        public Form1()
        {
            InitializeComponent();
            scen = new Scen(this, resources);

            formGraphics = this.CreateGraphics();
            formGraphics.CompositingQuality = CompositingQuality.HighQuality;
            formGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            this.DoubleBuffered = true;

            timer1.Interval = 10;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            InputSys.DownButtons(e.KeyCode);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            InputSys.UpButtons(e.KeyCode);
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            scen.allUpdateMethods();
            Update();

            scen.player.ReCalculateVariables();
        }

        private void Update()
        {
            var player = scen.player;

            player.obj.Visible = false;

            Refresh();
            Thread.Sleep(20);

        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;    // WS_EX_COMPOSITED
                return cp;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            var player = scen.player;
            var objects = scen.allObjects;

            Graphics g = Graphics.FromImage(this.BackgroundImage);

            for (var i = 1; i < objects.Count; i++)
            {
                if (((objects[i].tag == ObjectTag.Wall)
                    || (objects[i].tag == ObjectTag.InteractiveObject
                    && ((InteractiveObject)objects[i]).interactiveTag == InteractiveObjectTag.Holl))
                    && objects[i].isStatic == false)
                {
                    DrawObj(objects[i], g);
                    objects[i].isStatic = true;
                }
                if (objects[i].tag != ObjectTag.Wall && objects[i].isStatic == false)
                    DrawObj(objects[i], e.Graphics);
            }

            DrawObj(player, e.Graphics);

            void DrawObj(GameObject obj, Graphics gr)
            {
                System.Drawing.Rectangle destRect2
                      = new System.Drawing.Rectangle((int)obj.transform.Position.x, (int)obj.transform.Position.y, obj.transform.Size.Width, obj.transform.Size.Height);

                gr.DrawImage(obj.textur, destRect2);
            }
        }

    }
}
