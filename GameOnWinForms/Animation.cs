using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOnWinForms
{
    class AnimationPerson
    {
        private GameObject GO;
        private ComponentResourceManager resources;

        public AnimationPerson(GameObject GO, ComponentResourceManager resources)
        {
            this.GO = GO;
            this.resources = resources;
        }

        public void Update()
        {
            RotationPerson();
        }

        private float oldClearAngle = 0;
        private void RotationPerson()
        {
            var clearAngle = GO.transform.angle;

            if (clearAngle <= 22.5f && clearAngle >= -22.5f) clearAngle = 0;
            if (clearAngle <= 67.5f && clearAngle >= 22.5f) clearAngle = 45;
            if (clearAngle <= 110.5f && clearAngle >= 67.5f) clearAngle = 90;
            if (clearAngle <= 157.5f && clearAngle >= 110.5f) clearAngle = 135;
            if (clearAngle <= 180f && clearAngle >= 157.5f) clearAngle = 180;

            if (clearAngle >= -67.5f && clearAngle <= -22.5f) clearAngle = -45;
            if (clearAngle >= -110.5f && clearAngle <= -67.5f) clearAngle = -90;
            if (clearAngle >= -157.5f && clearAngle <= -110.5f) clearAngle = -135;
            if (clearAngle >= -180f && clearAngle <= -157.5f) clearAngle = 180;
            if (float.IsNaN(clearAngle))
                clearAngle = 0;

            if (clearAngle != oldClearAngle &&
                (clearAngle == 0
                || clearAngle == 45
                || clearAngle == 90
                || clearAngle == 135
                || clearAngle == 180
                || clearAngle == -45
                || clearAngle == -90
                || clearAngle == -135))
                GO.textur = (Image)resources.GetObject(((int)clearAngle).ToString());

            oldClearAngle = clearAngle;
        }
    }
}
