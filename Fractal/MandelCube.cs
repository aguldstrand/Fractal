using Microsoft.Xna.Framework;

namespace Fractal
{
    class MandelCube : Fractal.IRenderable
    {
        private readonly int maxIterations;

        public MandelCube(int maxIterations)
        {
            this.maxIterations = maxIterations;
        }

        public float HitTest(Vector3 c)
        {
            const float scale = 2f;
            const float r = .5f;
            const float f = 1.0f;

            var z = c;

            for (int i = 0; i < maxIterations; i++)
            {
                z = BoxFold(z) * f;
                z = BallFold(r, z) * scale;
                z = z + c;

                if (z.Length() > 40000000.0)
                {
                    return i / (float)maxIterations;
                }
            }

            return 0;
        }

        static Vector3 BoxFold(Vector3 v)
        {
            return new Vector3
            {
                X = v.X > 1 ? 2 - v.X : v.X < -1 ? -2 - v.X : v.X,
                Y = v.Y > 1 ? 2 - v.Y : v.Y < -1 ? -2 - v.Y : v.Y,
                Z = v.Z > 1 ? 2 - v.Z : v.Z < -1 ? -2 - v.Z : v.Z,
            };
        }

        private static Vector3 BallFold(float r, Vector3 v)
        {
            float len = v.Length();
            if (len < r)
            {
                v.Normalize();
                return v * (len / (r * r));
            }
            else if (len < 1)
            {
                v.Normalize();
                return v * (1 / len);
            }
            return v;
        }

    }
}
