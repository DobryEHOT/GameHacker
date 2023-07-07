using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace GameOnWinForms
{
    class InteractiveObject : GameObject
    {
        public InteractiveObjectTag interactiveTag;
        public readonly int indexObj;

        public InteractiveObject(Vector2 position, ObjectTag tag, InteractiveObjectTag interactiveTag, int indexObj, float startAngle, ComponentResourceManager res)
            : base(position, tag, startAngle, res)
        {
            this.interactiveTag = interactiveTag;
            this.indexObj = indexObj;
            obj.Visible = false;
        }

        public override void Texturing()
        {
            switch (interactiveTag)
            {
                case InteractiveObjectTag.Holl:
                    textur = (Image)res.GetObject("holl");
                    break;
                case InteractiveObjectTag.Door:
                    textur = (Image)res.GetObject("doorClosed_" + startAngle);
                    break;
                case InteractiveObjectTag.ControllPanel:
                    textur = (Image)res.GetObject("consolePanel");
                    break;
                default:
                    textur = (Image)res.GetObject("default");
                    break;
            }
        }
    }

    class Door : InteractiveObject
    {
        public bool isOpen { get; private set; }

        public Door(Vector2 position, ObjectTag tag, InteractiveObjectTag interactiveTag, int indexObj, float startAngle, ComponentResourceManager res)
            : base(position, tag, interactiveTag, indexObj, startAngle, res)
        {}

        public void Open()
        {
            isOpen = true;
            textur = (Image)res.GetObject("doorOpen_" + startAngle);
        }

        public void Closed()
        {
            isOpen = false;
            textur = (Image)res.GetObject("doorClosed_" + startAngle);
        }
    }

    class MovePanel : InteractiveObject
    {
        private Vector2 firstPosition = new Vector2();
        private Vector2 secondPosition = new Vector2();
        private PanelPositions position = PanelPositions.First;

        public MovePanel(Vector2 position, ObjectTag tag, InteractiveObjectTag interactiveTag, int indexObj, float startAngle, Vector2 firstPosition, Vector2 secondPosition, ComponentResourceManager res)
            : base(position, tag, interactiveTag, indexObj, startAngle, res)
        {
            this.firstPosition = firstPosition;
            this.secondPosition = secondPosition;
        }


        public void Move()
        {
            if (position == PanelPositions.First)
            {
                position = PanelPositions.Second;
                return;
            }
            if (position == PanelPositions.Second)
                position = PanelPositions.First;
        }

        private Vector2 oldVector = new Vector2();
        public override void Update()
        {
            if (position == PanelPositions.First)
                transform.Position.Lerp(firstPosition, 1.5f);
            if (position == PanelPositions.Second)
                transform.Position.Lerp(secondPosition, 1.5f);
            transform.vectorMove = transform.Position - oldVector;
            oldVector = new Vector2(transform.Position.x, transform.Position.y);
        }

        public override void Texturing()
        {
            textur = (Image)res.GetObject("movePanel");
        }
    }

    class ControllPanel : InteractiveObject
    {
        private HackerConsole console;
        public List<GameObject> objects = new List<GameObject>();
        internal int lvlIndex;

        public ControllPanel(Vector2 position, ObjectTag tag, InteractiveObjectTag interactiveTag, int indexObj, int lvlIndex, float startAngle, ComponentResourceManager res)
            : base(position, tag, interactiveTag, indexObj, startAngle, res)
        {
            this.lvlIndex = lvlIndex;
            console = new HackerConsole(this);
        }

        public override void Update()
        {
            if (console.inputCommands.Count != 0)
                console.ComeCommand(console.inputCommands.Dequeue());
        }

        public void OpenPanel()
        {
            console.OpenConsole();
        }
    }
}
