using System.Collections.Generic;
using System.ComponentModel;

namespace GameOnWinForms
{
    public delegate void UpdateMethods();

    public class Scen
    {
        internal List<GameObject> allObjects = new List<GameObject>();

        internal Person player;

        public UpdateMethods allUpdateMethods;

        private Physics phis;
        private ComponentResourceManager resources;

        public Scen(Form1 form, ComponentResourceManager resources)
        {
            this.resources = resources;
            var map = new Map(this, form, resources);
            phis = new Physics(this);

            CalculateAllUpdate();
        }

        private void CalculateAllUpdate()
        {
            foreach (var obj in allObjects)
            {
                if (obj.tag != ObjectTag.Wall)
                {
                    if (obj.tag == ObjectTag.Player)
                        allUpdateMethods += ((Person)obj).animator.Update;

                    allUpdateMethods += obj.Update;
                }
            }

            allUpdateMethods += phis.Update;
        }
    }

    class Map
    {
        private Scen scen;
        private ComponentResourceManager resources;
        private Form1 form;

        public Map(Scen scen, Form1 form, ComponentResourceManager resources)
        {
            this.resources = resources;
            this.scen = scen;
            this.form = form;
            CreateMap();
        }

        private void CreateMap()
        {
            scen.allObjects.Add(new Person(new Vector2(100, 70), ObjectTag.Player, 0, resources));
            scen.allObjects.Add(new Door(new Vector2(300, 100), ObjectTag.InteractiveObject, InteractiveObjectTag.Door, 1, 180, resources));
            scen.allObjects.Add(new ControllPanel(new Vector2(250, 150), ObjectTag.InteractiveObject, InteractiveObjectTag.ControllPanel, 0, 1, 0, resources));
            ((ControllPanel)scen.allObjects[2]).objects.Add(scen.allObjects[1]);

            scen.allObjects.Add(new ControllPanel(new Vector2(450, 150), ObjectTag.InteractiveObject, InteractiveObjectTag.ControllPanel, 0, 2, 0, resources));
            scen.allObjects.Add(new MovePanel(new Vector2(400, 200), ObjectTag.InteractiveObject, InteractiveObjectTag.MovePanel, 1, 0, new Vector2(400, 200), new Vector2(400, 350), resources));
            scen.allObjects.Add(new MovePanel(new Vector2(200, 250), ObjectTag.InteractiveObject, InteractiveObjectTag.MovePanel, 2, 0, new Vector2(200, 250), new Vector2(350, 350), resources));
            ((ControllPanel)scen.allObjects[3]).objects.Add(scen.allObjects[4]);
            ((ControllPanel)scen.allObjects[3]).objects.Add(scen.allObjects[5]);

            scen.allObjects.Add(new ControllPanel(new Vector2(50, 350), ObjectTag.InteractiveObject, InteractiveObjectTag.ControllPanel, 0, 3, 0, resources));
            scen.allObjects.Add(new Door(new Vector2(100, 400), ObjectTag.InteractiveObject, InteractiveObjectTag.Door, 1, 0, resources));
            ((ControllPanel)scen.allObjects[6]).objects.Add(scen.allObjects[7]);

            scen.player = (Person)scen.allObjects[0];

            for (var x = 0; x < 6; x++)//Make holl
                for (var y = 0; y < 4; y++)
                    if (!(y == 0 && x < 2))
                        scen.allObjects.Add(new InteractiveObject(new Vector2(x * 50 + 200, y * 50 + 200), ObjectTag.InteractiveObject, InteractiveObjectTag.Holl, 0, 180, resources));

            for (var i = 0; i <= 10; i++)
            {
                if (i == 1 || i == 6 || i == 10)
                    scen.allObjects.Add(new GameObject(new Vector2(i * 50, 0), ObjectTag.Wall, 360, resources));
                if (i > 1 && i != 6 && i != 10)
                    scen.allObjects.Add(new GameObject(new Vector2(i * 50, 0), ObjectTag.Wall, 0, resources));

                if (i < 7 && i != 0 && i != 1 && i != 6)
                    scen.allObjects.Add(new GameObject(new Vector2(i * 50, 200), ObjectTag.Wall, 0, resources));
                if (i == 0 || i == 1 || i == 6)
                    scen.allObjects.Add(new GameObject(new Vector2(i * 50, 200), ObjectTag.Wall, 360, resources));

                if (i != 2 && i != 0 && i != 10)
                    scen.allObjects.Add(new GameObject(new Vector2(i * 50, 400), ObjectTag.Wall, 0, resources));
                if (i == 0 || i == 10)
                    scen.allObjects.Add(new GameObject(new Vector2(i * 50, 400), ObjectTag.Wall, 360, resources));


                if (i < 4 && i != 0)
                    scen.allObjects.Add(new GameObject(new Vector2(50, i * 50), ObjectTag.Wall, 180, resources));
                if (i > 4 && i != 8)
                    scen.allObjects.Add(new GameObject(new Vector2(0, i * 50), ObjectTag.Wall, 180, resources));
                if (i < 9 && i != 0)
                    scen.allObjects.Add(new GameObject(new Vector2(500, i * 50), ObjectTag.Wall, 180, resources));
                if (i == 1 || i == 3)
                    scen.allObjects.Add(new GameObject(new Vector2(300, i * 50), ObjectTag.Wall, 180, resources));
            }

            foreach (var obj in scen.allObjects)
            {
                obj.Texturing();
            }
        }
    }
}
