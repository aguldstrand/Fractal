#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
#endregion

namespace Fractal
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        VertexBuffer vb;
        VertexPositionColor[] vertices;
        int boxCount;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var side = 80;
            var sizeMultiplier = 2f / side;

            vertices = new VertexPositionColor[side * side * side * 36];
            vb = new VertexBuffer(GraphicsDevice, VertexPositionColor.VertexDeclaration, vertices.Length, BufferUsage.WriteOnly);

            var boxes = Slicer.Slice(side, -6.5f, -6.5f, 13.0f, 13.0f);

            Task.Factory.StartNew(() =>
            {
                foreach (var box in boxes)
                {
                    //lock (vb)
                    {
                        var m = Matrix.CreateTranslation(box) *
                            Matrix.CreateScale(sizeMultiplier);

                        var c = new Color(
                            box.X / side,
                            box.Y / side,
                            box.Z / side);

                        // Calculate the position of the vertices on the top face.
                        Vector3 topLeftFront = Vector3.Transform(new Vector3(-0.5f, 0.5f, -0.5f), m);
                        Vector3 topLeftBack = Vector3.Transform(new Vector3(-0.5f, 0.5f, 0.5f), m);
                        Vector3 topRightFront = Vector3.Transform(new Vector3(0.5f, 0.5f, -0.5f), m);
                        Vector3 topRightBack = Vector3.Transform(new Vector3(0.5f, 0.5f, 0.5f), m);

                        // Calculate the position of the vertices on the bottom face.
                        Vector3 btmLeftFront = Vector3.Transform(new Vector3(-0.5f, -0.5f, -0.5f), m);
                        Vector3 btmLeftBack = Vector3.Transform(new Vector3(-0.5f, -0.5f, 0.5f), m);
                        Vector3 btmRightFront = Vector3.Transform(new Vector3(0.5f, -0.5f, -0.5f), m);
                        Vector3 btmRightBack = Vector3.Transform(new Vector3(0.5f, -0.5f, 0.5f), m);

                        // Add the vertices for the FRONT face.
                        vertices[boxCount * 36 + 0] = new VertexPositionColor(topLeftFront, c);
                        vertices[boxCount * 36 + 1] = new VertexPositionColor(btmLeftFront, c);
                        vertices[boxCount * 36 + 2] = new VertexPositionColor(topRightFront, c);
                        vertices[boxCount * 36 + 3] = new VertexPositionColor(btmLeftFront, c);
                        vertices[boxCount * 36 + 4] = new VertexPositionColor(btmRightFront, c);
                        vertices[boxCount * 36 + 5] = new VertexPositionColor(topRightFront, c);

                        // Add the vertices for the BACK face.
                        vertices[boxCount * 36 + 6] = new VertexPositionColor(topLeftBack, c);
                        vertices[boxCount * 36 + 7] = new VertexPositionColor(topRightBack, c);
                        vertices[boxCount * 36 + 8] = new VertexPositionColor(btmLeftBack, c);
                        vertices[boxCount * 36 + 9] = new VertexPositionColor(btmLeftBack, c);
                        vertices[boxCount * 36 + 10] = new VertexPositionColor(topRightBack, c);
                        vertices[boxCount * 36 + 11] = new VertexPositionColor(btmRightBack, c);

                        // Add the vertices for the TOP face.
                        vertices[boxCount * 36 + 12] = new VertexPositionColor(topLeftFront, c);
                        vertices[boxCount * 36 + 13] = new VertexPositionColor(topRightBack, c);
                        vertices[boxCount * 36 + 14] = new VertexPositionColor(topLeftBack, c);
                        vertices[boxCount * 36 + 15] = new VertexPositionColor(topLeftFront, c);
                        vertices[boxCount * 36 + 16] = new VertexPositionColor(topRightFront, c);
                        vertices[boxCount * 36 + 17] = new VertexPositionColor(topRightBack, c);

                        // Add the vertices for the BOTTOM face. 
                        vertices[boxCount * 36 + 18] = new VertexPositionColor(btmLeftFront, c);
                        vertices[boxCount * 36 + 19] = new VertexPositionColor(btmLeftBack, c);
                        vertices[boxCount * 36 + 20] = new VertexPositionColor(btmRightBack, c);
                        vertices[boxCount * 36 + 21] = new VertexPositionColor(btmLeftFront, c);
                        vertices[boxCount * 36 + 22] = new VertexPositionColor(btmRightBack, c);
                        vertices[boxCount * 36 + 23] = new VertexPositionColor(btmRightFront, c);

                        // Add the vertices for the LEFT face.
                        vertices[boxCount * 36 + 24] = new VertexPositionColor(topLeftFront, c);
                        vertices[boxCount * 36 + 25] = new VertexPositionColor(btmLeftBack, c);
                        vertices[boxCount * 36 + 26] = new VertexPositionColor(btmLeftFront, c);
                        vertices[boxCount * 36 + 27] = new VertexPositionColor(topLeftBack, c);
                        vertices[boxCount * 36 + 28] = new VertexPositionColor(btmLeftBack, c);
                        vertices[boxCount * 36 + 29] = new VertexPositionColor(topLeftFront, c);

                        // Add the vertices for the RIGHT face. 
                        vertices[boxCount * 36 + 30] = new VertexPositionColor(topRightFront, c);
                        vertices[boxCount * 36 + 31] = new VertexPositionColor(btmRightFront, c);
                        vertices[boxCount * 36 + 32] = new VertexPositionColor(btmRightBack, c);
                        vertices[boxCount * 36 + 33] = new VertexPositionColor(topRightBack, c);
                        vertices[boxCount * 36 + 34] = new VertexPositionColor(topRightFront, c);
                        vertices[boxCount * 36 + 35] = new VertexPositionColor(btmRightBack, c);

                        boxCount++;

                        if (boxCount % 10000 == 0)
                        {
                            vb.SetData(vertices);
                        }
                    }
                }

                //lock (vb)
                {
                    vb.SetData(vertices);
                }
            }, TaskCreationOptions.LongRunning);

            //vb.SetData(vertices);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //lock (vb)
            {
                using (var effect = new BasicEffect(GraphicsDevice))
                {
                    effect.World = Matrix.CreateTranslation(0, 0, 0);
                    effect.View = Matrix.CreateLookAt(
                        new Vector3(3, 3, 3),
                        new Vector3(2.5f, 2.5f, 2.5f),
                        Vector3.Up);
                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(
                        MathHelper.ToRadians(45),
                        (float)GraphicsDevice.Viewport.Width / (float)GraphicsDevice.Viewport.Height,
                        1f,
                        100f);
                    effect.VertexColorEnabled = true;

                    graphics.GraphicsDevice.SetVertexBuffer(vb);

                    foreach (var pass in effect.CurrentTechnique.Passes)
                    {
                        pass.Apply();
                        graphics.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, boxCount * 12);
                    }
                }
            }

            base.Draw(gameTime);
        }
    }
}