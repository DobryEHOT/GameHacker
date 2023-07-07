using System;
using System.Drawing;

namespace GameOnWinForms
{
    public class Transform
    {
        public Rectangle rectangle;

        private Size size;
        public Size Size
        {
            get => size;
            set
            {
                rectangle.Recalculate(Position, value);
                size = value;
            }
        }

        private Vector2 oldPosition;

        public Vector2 vectorMove;

        private Vector2 position;
        public Vector2 Position
        {
            get => position;
            set
            {
                oldPosition = Position;
                position = value;
            }
        }

        public float angle
        {
            get
            {
                var directVerb = oldPosition.x < position.x ? 1f : -1f;
                var t = (float)((Math.Acos(vectorMove.y / vectorMove.GetMagnitude())) * 360d / Math.PI) / 2f * directVerb;
                return float.IsNaN(t) ? 0 : angle = t;
            }
            private set { }

        }

        public Transform(Vector2 position, Size size)
        {
            Position = position;
            vectorMove = new Vector2();
            angle = 0;

            rectangle = new Rectangle(position, size);
            this.Size = size;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Transform))
                throw new InvalidOperationException();

            var timeTransform = (Transform)obj;

            var one = Position.Equals(timeTransform.Position);
            var two = Size.Equals(timeTransform.Size);

            return one && two;
        }
    }

    public class Vector2
    {
        public float x = 0;
        public float y = 0;

        public Vector2()
        {
            x = 0;
            y = 0;
        }

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Point ConvertToPoint()
        {
            return new Point((int)Math.Ceiling(x), (int)Math.Ceiling(y));
        }

        public float GetMagnitude()
        {
            return (float)Math.Sqrt((double)(x * x + y * y));
        }

        public Vector2 Lerp(Vector2 b, float delta)
        {
            if (Math.Abs(this.x - b.x) < delta && Math.Abs(this.y - b.y) < delta)
            {
                this.x = b.x;
                this.y = b.y;
            }

            if (this.x < b.x)
                this.x += delta;
            if (this.y < b.y)
                this.y += delta;

            if (this.x > b.x)
                this.x -= delta;
            if (this.y > b.y)
                this.y -= delta;

            return this;
        }

        public override string ToString()
        {
            return ("X= " + x + ", " + "Y= " + y).ToString();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Vector2))
                throw new InvalidOperationException();

            var go = (Vector2)obj;

            if (go.x == x && go.y == y)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            var v = new Vector2(a.x + b.x, a.y + b.y);

            return v;
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }

        public static Vector2 operator *(Vector2 a, float b)
        {
            return new Vector2(a.x * b, a.y * b);
        }

        public static Vector2 operator *(float b, Vector2 a)
        {
            return new Vector2(a.x * b, a.y * b);
        }

        public static Vector2 operator /(Vector2 a, float b)
        {
            return new Vector2(a.x / b, a.y / b);
        }
    }

    public class Rectangle
    {
        public Rectangle(Vector2 position, Size size)
        {
            Recalculate(position, size);
        }

        public void Recalculate(Vector2 position, Size size)
        {
            Left = position.x;
            Top = position.y;
            Width = size.Width;
            Height = size.Height;
        }

        public override string ToString()
        {
            return String.Format("Left: {0}, Top: {1}, Width: {2}, Height: {3}", Left, Top, Width, Height);
        }

        public float Left, Top, Width, Height;
        public float Bottom { get { return Top + Height; } }
        public float Right { get { return Left + Width; } }
        public Vector2 Center { get { return new Vector2(Right - (Right - Left) / 2, Bottom - (Bottom - Top) / 2); } }
    }
}
