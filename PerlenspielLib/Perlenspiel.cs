using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PerlenspielLib
{
    public abstract class Perlenspiel
    {
        /// <summary>
        /// Any game initialization logic goes here.
        /// </summary>
        public virtual void Init()
        {
        }

        /// <summary>
        /// This method is called whenever the mouse is clicked.
        /// </summary>
        /// <param name="x">X coordinate of bead</param>
        /// <param name="y">Y coordinate of bead</param>
        /// <param name="data">Data associated with bead</param>
        public virtual void Click(int x, int y)
        {
        }

        public virtual void RightClick(int x, int y)
        {
        }
        
        /// <summary>
        /// This method is called whenever the mouse is released over a bead.
        /// </summary>
        /// <param name="x">X coordinate of bead</param>
        /// <param name="y">Y coordinate of bead</param>
        /// <param name="data">Data associated with bead</param>
        public virtual void Release(int x, int y)
        {
        }

        /// <summary>
        /// This method is called whenever the mouse enters a bead.
        /// </summary>
        /// <param name="x">X coordinate of bead</param>
        /// <param name="y">Y coordinate of bead</param>
        /// <param name="data">Data associated with bead</param>
        public virtual void Enter(int x, int y)
        {
        }

        /// <summary>
        /// This method is called whenever the mouse leaves a bead.
        /// </summary>
        /// <param name="x">X coordinate of previous bead</param>
        /// <param name="y">Y coordinate of previous bead</param>
        /// <param name="data">Data associated with bead</param>
        public virtual void Leave(int x, int y)
        {
        }

        /// <summary>
        /// This method is called whenever a key is pressed.
        /// </summary>
        /// <param name="key">Key that's pressed down</param>
        /// <param name="shift">True if shift is pressed down</param>
        /// <param name="ctrl">True if ctrl is pressed down</param>
        public virtual void KeyDown(int key, bool shift, bool ctrl)
        {
        }

        /// <summary>
        /// This method is called when a key is released.
        /// </summary>
        /// <param name="key">Key that was released</param>
        /// <param name="shift">True if shift was held down</param>
        /// <param name="ctrl">True if ctrl was held down</param>
        public virtual void KeyUp(int key, bool shift, bool ctrl)
        {
        }

        /// <summary>
        /// This method is called when the mouse wheel moves.
        /// </summary>
        /// <param name="dir">1 if wheel moved forward; -1 if wheel moved backward</param>
        public virtual void Wheel(int dir)
        {
        }

        /// <summary>
        /// This method is called every tick if a timer has been activated.
        /// </summary>
        public virtual void Tick()
        {
        }
    }
}
