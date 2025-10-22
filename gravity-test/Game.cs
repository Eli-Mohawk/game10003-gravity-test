using System;
using System.Drawing;
using System.Numerics;

namespace MohawkGame2D
{
    public class Game
    {
        Vector2 rectPos = new Vector2(380, 100);
        Vector2 rectSize = new Vector2(40, 60);

        Vector2 rectVel = new Vector2(0, 0);

        Vector2 gravity = new Vector2(0, 500);

        float frictionY = 0.9f;
        float frictionX = 0.8f;

        bool gameRun = false;

        public void Setup()
        {
            Window.SetTitle("Gravity Test");
            Window.SetSize(800, 600);
            Window.TargetFPS = 60;
        }

        public void Update()
        {
            Window.ClearBackground(Color.White);

            MajorGrid();
            MinorGrid();

            Controls();

            if (gameRun)
            {
                Gravity();
                Collision();
            }

            Object();
        }

        void Collision()
        {
            float bottomEdge = rectPos.Y + rectSize.Y;
            float topEdge = rectPos.Y;
            float leftEdge = rectPos.X;
            float rightEdge = rectPos.X + rectSize.X;
            
            if (bottomEdge > Window.Height)
            {
                // inverses the velocity (-1) and reduces its bounce (friction)
                rectVel.Y *= -1 * frictionY;

                // snaps the rectangle up so it doesnt clip / makes it 1/1 velocity
                rectPos.Y = Window.Height - rectSize.Y;
            }

            if (topEdge < 0)
            {
                rectVel.Y *= -1 * frictionY;
                rectPos.Y = 0;
            }

            if (leftEdge < 0)
            {
                rectVel.X *= -1 * frictionX;
                rectPos.X = 0;
            }

            if (rightEdge > Window.Width)
            {
                rectVel.X *= -1 * frictionX;
                rectPos.X = Window.Width - rectSize.X;
            }

        }

        void Gravity()
        {
            rectVel += gravity * Time.DeltaTime;
            rectPos += rectVel * Time.DeltaTime;
        }

        void Object()
        {
            Draw.LineSize = 2;
            Draw.LineColor = Color.Black;
            Draw.FillColor = Color.White;

            Draw.Rectangle(rectPos, rectSize);

        }

        void MajorGrid()
        {
            Draw.LineSize = 2;
            Draw.LineColor = Color.LightGray;

            for (int x = 0; x < Window.Width; x += 100)
            {
                Draw.Line(x, 0, x, Window.Height);
            }

            for (int y = 0; y < Window.Height; y += 100)
            {
                Draw.Line(0, y, Window.Width, y);
            }
            
        }

        void MinorGrid()
        {
            Draw.LineSize = 1;
            Draw.LineColor = Color.LightGray;

            for (int x = 0; x < Window.Width; x += 20)
            {
                Draw.Line(x, 0, x, Window.Height);
            }

            for (int y = 0; y < Window.Height; y += 20)
            {
                Draw.Line(0, y, Window.Width, y);
            }
        }

        void Controls()
        {
            Text.Draw("Reset: Spacebar", 10, 10);
            Text.Draw("Start: Enter", 10, 50);

            // when you press spacebar it moves object to start pos AND resets velocity
            if (Input.IsKeyboardKeyPressed(KeyboardInput.Space))
            {
                rectVel = new Vector2(0, 0);
                rectPos = new Vector2(380, 100);

                // freezes game
                gameRun = false;
            }

            if (Input.IsKeyboardKeyPressed(KeyboardInput.Enter))
            {
                gameRun = true;
                rectVel = 2500f * Random.Vector2();
            }
        }
    }

}
