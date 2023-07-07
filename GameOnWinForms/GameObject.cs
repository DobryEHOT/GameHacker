using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GameOnWinForms
{
    class GameObject
    {
        public string name = "GameObject";
        public bool isStatic;

        public Transform transform;
        public ObjectTag tag;

        public PictureBox obj;
        public Image textur;

        protected ComponentResourceManager res;
        protected float startAngle = 0;


        public GameObject(Vector2 position, ObjectTag tag, float startAngle, ComponentResourceManager res)
        {
            this.res = res;
            this.tag = tag;

            obj = new PictureBox
            {
                Location = new Point
                {
                    X = (int)Math.Ceiling(position.x),
                    Y = (int)Math.Ceiling(position.y)
                },

                Size = new Size
                {
                    Width = 50,
                    Height = 50
                }
            };


            transform = new Transform(position, new Size { Width = 50, Height = 50 });
            transform.Position = position;

            obj.BackgroundImageLayout = ImageLayout.Zoom;
            obj.BackColor = Color.White;
            obj.Visible = false;
            this.startAngle = startAngle;
        }

        public void Instantate(Form1 form)
        {
            form.Controls.Add(obj);
        }

        public void ReCalculateVariables()
        {

            obj.Location = transform.Position.ConvertToPoint();

        }

        public override bool Equals(object obj)
        {
            var timeObj = (GameObject)obj;

            var one = transform.Equals(timeObj.transform);
            var two = obj.Equals(timeObj.obj);

            return one && two && timeObj.tag == tag;
        }

        public virtual void Texturing()
        {
            switch (tag)
            {
                case ObjectTag.Wall:
                    textur = (Image)res.GetObject("wall_" + startAngle);
                    break;

                case ObjectTag.Player:
                    textur = (Image)res.GetObject("0");
                    break;
                default:
                    textur = (Image)res.GetObject("default");
                    break;

            }

            obj.BackgroundImage = textur;
        }

        public virtual void Update()
        {

        }
    }
}
