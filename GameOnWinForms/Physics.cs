using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;


namespace GameOnWinForms
{
    public class Physics
    {
        private Scen scen;
        private bool playerStayOnPlatform;
        private bool playerStayOnHoll;
        private Queue<InteractiveObject> interactiveObjects = new Queue<InteractiveObject>();

        public Physics(Scen scen)
        {
            this.scen = scen;
            CalculationAllCollisions();
        }

        private int timer = 0;
        public void Update()
        {
            timer++;
            if (timer >= 0)
            {
                timer = 0;
                InpsectionCollisions();
            }

            CalculationCollisionsPlayer();
        }

        private void InpsectionCollisions()
        {
            var player = scen.player;
            var obj = scen.allObjects;

            for (var i = 1; i < obj.Count; i++)
            {
                if (AreIntersected(player.transform.rectangle, obj[i].transform.rectangle))
                {
                    if (obj[i].tag == ObjectTag.Wall
                        || (obj[i].tag == ObjectTag.InteractiveObject
                        && ((InteractiveObject)obj[i]).interactiveTag == InteractiveObjectTag.Door
                        && !((Door)obj[i]).isOpen))
                    {
                        player.transform.Position += player.transform.vectorMove * -1f;
                        player.transform.Position += (player.transform.rectangle.Center - obj[i].transform.rectangle.Center) * 0.02f * player.transform.vectorMove.GetMagnitude();
                        player.inputSpeedX = 0;
                        player.inputSpeedY = 0;
                    }
                    if (obj[i].tag == ObjectTag.InteractiveObject)
                    {
                        CalculationCollisionsInteractiveObjects();
                        player.OnTriggerStay((InteractiveObject)obj[i]);

                        if (((InteractiveObject)obj[i]).interactiveTag == InteractiveObjectTag.MovePanel)
                            playerStayOnPlatform = true;
                        else if (((InteractiveObject)obj[i]).interactiveTag == InteractiveObjectTag.Holl)
                        {
                            playerStayOnHoll = true;
                            interactiveObjects.Enqueue((InteractiveObject)obj[i]);
                        }
                    }

                }
            }

            if (playerStayOnHoll && !playerStayOnPlatform)
            {
                while (interactiveObjects.Count != 0)
                    if (IntersectionSquare(player.transform.rectangle, interactiveObjects.Dequeue().transform.rectangle) > 800)
                        player.Dead();
            }

            playerStayOnHoll = false;
            playerStayOnPlatform = false;

            bool AreIntersected(Rectangle r1, Rectangle r2)
            {
                return !(r1.Right < r2.Left || r2.Right < r1.Left || r1.Bottom < r2.Top || r2.Bottom < r1.Top);
            }

            int IntersectionSquare(Rectangle r1, Rectangle r2)
            {
                if (AreIntersected(r1, r2))
                {
                    int xIntersection = Math.Min((int)r1.Right, (int)r2.Right) - Math.Max((int)r1.Left, (int)r2.Left);
                    int yIntersection = Math.Min((int)r1.Bottom, (int)r2.Bottom) - Math.Max((int)r1.Top, (int)r2.Top);
                    return (xIntersection * yIntersection);
                }
                else return 0;
            }
        }

        private void CalculationAllCollisions()
        {
            var obj = scen.allObjects;

            foreach (var elm in obj)
                elm.transform.rectangle.Recalculate(elm.transform.Position, elm.transform.Size);
        }

        private void CalculationCollisionsInteractiveObjects()
        {
            var obj = scen.allObjects;

            foreach (var elm in obj)
                if (elm.tag == ObjectTag.InteractiveObject)
                    elm.transform.rectangle.Recalculate(elm.transform.Position, elm.transform.Size);
        }

        private void CalculationCollisionsPlayer()
        {
            var obj = scen.player;

            obj.transform.rectangle.Recalculate(obj.transform.Position, obj.transform.Size);
        }
    }
}
