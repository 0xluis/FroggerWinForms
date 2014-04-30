#region File Description
//-----------------------------------------------------------------------------
// SpinningTriangleControl.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using frogger;
#endregion

namespace frogger
{
    /// <summary>
    /// Example control inherits from GraphicsDeviceControl, which allows it to
    /// render using a GraphicsDevice. This control shows how to draw animating
    /// 3D graphics inside a WinForms application. It hooks the Application.Idle
    /// event, using this to invalidate the control, which will cause the animation
    /// to constantly redraw.
    /// </summary>
    class GraphicsHandler : GraphicsDeviceControl
    {
        //initialize some varaibles here

        SpriteBatch spriteBatch;
        static Dictionary<string, Texture2D> sprites;
        SpriteFont font;
        public ContentManager Content;

        protected override void Initialize()
        {
            ServiceContainer services = new ServiceContainer();
            Content = new ContentManager(Services, "Content");
            
            sprites = new Dictionary<string, Texture2D>();

            frogger.Object.allObjects = new List<frogger.Object>();
            Row.allRows = new List<Row>();
            new Row(64 * 0, 2.5f);
            new Row(64 * 1, 2);
            new Row(64 * 2, 1.5f);
            new Row(64 * 3, 1, Spawns.LOG);
            Form1.score = 0;
            Form1.lives = Form1.startingLives;
            //put the player at the bottom of the screen
            Form1.player = new Player(new Vector2(200, 256));

            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Segoe");
            sprites.Add("placeholder", this.Content.Load<Texture2D>("placeholder"));
            sprites.Add("frog", this.Content.Load<Texture2D>("frog"));
            sprites.Add("road", this.Content.Load<Texture2D>("road"));
            sprites.Add("water", this.Content.Load<Texture2D>("water"));
            Form1.player.setSprite("frog");

        }

        /// <summary>
        /// Draws the control.
        /// </summary>
        protected override void Draw()
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            for (int i = 0; i < Row.allRows.Count; i++)
            {
                Row.allRows[i].drawRow(this.spriteBatch);
            }
            Form1.player.draw(this.spriteBatch);
            //draw score and lives
            //use the difference at the bottom of the screen for this
            spriteBatch.DrawString(font, "Score: " + Form1.score, new Vector2(0, (Form1.height - 30)), Color.Red);
            spriteBatch.DrawString(font, "Lives Remaining: " + Form1.lives, new Vector2(200, (Form1.height - 30)), Color.Red);
            spriteBatch.End();
        }
        public static Texture2D getSprite(string s)
        {
            return sprites[s];
        }
    }
}
