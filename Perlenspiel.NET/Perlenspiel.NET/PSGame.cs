using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PerlenspielEngine;
using PerlenspielEngine.Entities;
using PerlenspielGame.Components;
using PerlenspielGame.Components.Maps;
using PerlenspielGame.Systems;
using PerlenspielLib;

namespace PerlenspielGame
{
    class PSGame : Perlenspiel
    {
        /// <summary>
        /// Any game initialization logic goes here.
        /// </summary>
        public override void Init()
        {
            Singleton<GameState>.Instance.Initialize(32, 32);
            PS.GridBGColor(Color.Black);
            PS.StatusColor(Color.White);

            PS.GridSize(GameState.GridHeight, GameState.GridWidth);

            //new Entity("Glow", new LightSource(20, new Color(255, 255, 150), 1.0), 7, 7);
            //new Entity("Glow", new LightSource(20, new Color(255, 150, 255), 1.0), 23, 23);
            //new Entity("Glow", new LightSource(20, new Color(150, 255, 255), 1.0), 7, 23);
            //new Entity("Glow", new LightSource(20, new Color(255, 200, 200), 1.0), 23, 7);

            var playerGraphic = new BeadGraphic(new Color(200, 100, 100), new Color(100, 50, 50), 2);
            playerGraphic.Glyph = ' ';
            var playerComps = new List<Component> { playerGraphic, new Mob(), new Obstacle(), new Position(15, 15) };
            var player = new Entity("Player", playerComps);
            Singleton<GameState>.Instance.SetPlayer(player);
            GameState.DrawMap();
        }

        /// <summary>
        /// This method is called whenever the mouse is clicked.
        /// </summary>
        /// <param name="x">X coordinate of bead</param>
        /// <param name="y">Y coordinate of bead</param>
        /// <param name="data">Data associated with bead</param>
        public override void Click(int x, int y)
        {
            GameState.HandleClick(x, y);
        }

        /// <summary>
        /// This method is called whenever a right-click happens.
        /// </summary>
        /// <param name="x">X coordinate of bead</param>
        /// <param name="y">Y coordinate of bead</param>
        /// <param name="data">Data associated with bead</param>
        public override void RightClick(int x, int y)
        {
            Singleton<ConstructionSystem>.Instance.Build(x, y, "Stone Wall", "Stone", 1);
        }

        /// <summary>
        /// This method is called whenever the mouse is released over a bead.
        /// </summary>
        /// <param name="x">X coordinate of bead</param>
        /// <param name="y">Y coordinate of bead</param>
        /// <param name="data">Data associated with bead</param>
        public override void Release(int x, int y)
        {
        }

        /// <summary>
        /// This method is called whenever the mouse enters a bead.
        /// </summary>
        /// <param name="x">X coordinate of bead</param>
        /// <param name="y">Y coordinate of bead</param>
        /// <param name="data">Data associated with bead</param>
        public override void Enter(int x, int y)
        {
            //PS.BeadBorderColor(x, y, Color.Red);
            //PS.BeadBorderWidth(x, y, 3);
        }

        /// <summary>
        /// This method is called whenever the mouse leaves a bead.
        /// </summary>
        /// <param name="x">X coordinate of previous bead</param>
        /// <param name="y">Y coordinate of previous bead</param>
        /// <param name="data">Data associated with bead</param>
        public override void Leave(int x, int y)
        {
            //PS.BeadBorderColor(x, y, Color.Black);
            //PS.BeadBorderWidth(x, y, 0);
        }

        /// <summary>
        /// This method is called whenever a key is pressed. You can do (char) key if the key is alphanumeric.
        /// </summary>
        /// <param name="key">Integer keycode corresponding to key, ASCII code if alphanumeric</param>
        /// <param name="shift">True if shift is pressed down</param>
        /// <param name="ctrl">True if ctrl is pressed down</param>
        public override void KeyDown(int key, bool shift, bool ctrl)
        {
            GameState.MovePlayer(key);
            if (key == PS.Space)
            {
                GameState.ToggleMenu();
            }
        }

        /// <summary>
        /// This method is called when a key is released.
        /// </summary>
        /// <param name="key">Key that was released</param>
        /// <param name="shift">True if shift was held down</param>
        /// <param name="ctrl">True if ctrl was held down</param>
        public override void KeyUp(int key, bool shift, bool ctrl)
        {
        }

        /// <summary>
        /// This method is called when the mouse wheel moves.
        /// </summary>
        /// <param name="dir">1 if wheel moved forward; -1 if wheel moved backward</param>
        public override void Wheel(int dir)
        {
        }
        
        /// <summary>
        /// This method is called every tick if a timer has been activated.
        /// </summary>
        public override void Tick()
        {
        }
    }
}
