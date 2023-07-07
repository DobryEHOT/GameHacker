using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;

namespace GameOnWinForms
{
    class Person : GameObject
    {
        public float inputSpeedX = 0;
        public float inputSpeedY = 0;
        public AnimationPerson animator { get; private set; }

        public Person(Vector2 position, ObjectTag tag, float startAngle, ComponentResourceManager res) : base(position, tag, startAngle, res)
        {
            animator = new AnimationPerson(this, res);
            transform.Size = obj.Size = new Size(40, 40);
            obj.Visible = false;
        }

        public override void Update()
        {
            if (InputSys.D)
                inputSpeedX = Lerp(inputSpeedX, 10, 0.4f);
            if (InputSys.W)
                inputSpeedY = Lerp(inputSpeedY, -10, 0.4f);
            if (InputSys.A)
                inputSpeedX = Lerp(inputSpeedX, -10, 0.4f);
            if (InputSys.S)
                inputSpeedY = Lerp(inputSpeedY, 10, 0.4f);

            if (!InputSys.W && !InputSys.S)
                inputSpeedY = Lerp(inputSpeedY, 0, 0.4f);
            if (!InputSys.D && !InputSys.A)
                inputSpeedX = Lerp(inputSpeedX, 0, 0.4f);

            MovePerson();

            Thread.Sleep(10);

            void MovePerson()
            {
                transform.vectorMove = ((inputSpeedX * new Vector2(1, 0)) + (inputSpeedY * new Vector2(0, 1))) / 2f;
                transform.Position += transform.vectorMove;
            }

            float Lerp(float a, float b, float delta)
            {
                if (Math.Abs(a - b) < delta)
                    a = b;
                if (a < b)
                    a += delta;
                if (a > b)
                    a -= delta;

                return a;
            }

        }
        public override void Texturing()
        {
            base.Texturing();
        }

        public void Dead()
        {
            transform.Position = new Vector2(150, 100);
        }

        public void OnTriggerStay(InteractiveObject obj)
        {
            if (InputSys.PressDown_E)
            {
                if (obj.interactiveTag == InteractiveObjectTag.ControllPanel)
                {
                    ((ControllPanel)obj).OpenPanel();
                }
            }
            if (obj.interactiveTag == InteractiveObjectTag.MovePanel)
            {
                transform.Position += obj.transform.vectorMove;
            }

        }
    }
}
