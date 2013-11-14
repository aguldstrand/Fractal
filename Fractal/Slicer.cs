using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Fractal
{
    class Slicer
    {
        public static IEnumerable<Vector3> Slice(int side, float bx, float by, float bw, float bh)
        {
            var mandelCube = new MandelCube(255);

            return Enumerable.Range(0, side)
                .AsParallel()
                .SelectMany(z => zf(side, bx, by, bw, bh, mandelCube, z));
        }

        private static IEnumerable<Vector3> zf(int side, float bx, float by, float bw, float bh, IRenderable r, int z)
        {
            for (int y = 0; y < side; y++)
            {
                for (int x = 0; x < side; x++)
                {
                    var coordinate = new Vector3(
                        x / (float)side * bw + bx,
                        y / (float)side * bw + by,
                        z / (float)side * bw + by);

                    var val = (int)(r.HitTest(coordinate) * 255);

                    if (val > 30)
                    {
                        yield return new Vector3(x, y, z);
                    }
                }
            }
        }
    }
}