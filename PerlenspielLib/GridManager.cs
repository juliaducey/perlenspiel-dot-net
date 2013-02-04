using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PerlenspielLib
{
    class GridManager
    {
        #region Variable Declarations
        // The grid
        private Bead[,] _grid;

        // Current dimensions of the grid in beads
        private int _gridWidth;
        private int _gridHeight;

        // Grid's position on the screen in pixels
        private int _gridScreenWidth;
        private int _gridScreenHeight;
        private int _gridXStart;
        private int _gridYStart;

        // These represent the maximum size of the grid
        private const int MaxGridWidth = 32;
        private const int MaxGridHeight = 32;

        #endregion
        #region ScreenManager Methods

        public void Initialize(int gridScreenWidth, int gridScreenHeight, int gridXStart, int gridYStart)
        {
            _gridScreenWidth = gridScreenWidth;
            _gridScreenHeight = gridScreenHeight;
            _gridXStart = gridXStart;
            _gridYStart = gridYStart;
            _gridWidth = MaxGridWidth;
            _gridHeight = MaxGridHeight;
            InitializeGrid();
        }

        public void InitializeGrid()
        {
            _grid = new Bead[MaxGridWidth, MaxGridHeight];
            for (var i = 0; i < MaxGridWidth; i++)
            {
                for (var j = 0; j < MaxGridHeight; j++)
                {
                    _grid[i, j] = new Bead(i, j);
                }
            }
        }

        public void DrawGrid()
        {
            Action<Bead> draw = bead => Singleton<GraphicsManager>.Instance.DrawBead(bead);
            ActOnBead(PS.All, PS.All, draw, false);
        }

        public Point GetBeadCoords(int x, int y)
        {
            for (var i = 0; i < _gridWidth; i++)
            {
                for (var j = 0; j < _gridHeight; j++)
                {
                    var bead = _grid[i, j];
                    if (bead.IsInArea(x, y) == true)
                        return bead.Coords;
                }
            }
            return new Point(PS.None, PS.None);
        }
        #endregion
        #region Grid Commands

        /// <summary>
        /// Changes the size of the grid; can be safely called while game is running
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void GridSize(int x, int y)
        {
            var beadWidth = _gridScreenWidth/x;
            var beadHeight = _gridScreenHeight/y;
            Action<Bead> changeArea = bead =>
                                 {
                                     var newX = _gridXStart + beadWidth*bead.Coords.X;
                                     var newY = _gridYStart + beadHeight*bead.Coords.Y;
                                     bead.Area = new Rectangle(newX, newY, beadWidth, beadHeight);
                                 };

            ActOnBead(PS.All, PS.All, changeArea);

            Singleton<GraphicsManager>.Instance.SetGlyphFont(beadHeight);
        }

        #endregion
        #region Bead Commands
        /// <summary>
        /// Reassigns bead parameters; -1 means all
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="action"></param>
        /// <param name="invisible"></param>
        private void ActOnBead(int x, int y, Action<Bead> action, bool invisible = true)
        {
            var width = MaxGridWidth;
            var height = MaxGridHeight;
            if (!invisible)
            {
                width = _gridWidth;
                height = _gridHeight;
            }

            if (x < PS.None || x >= width || y < PS.None || y >= height) return;

            if (x == PS.All && y == PS.All)
            {
                for (var i = 0; i < width; i++)
                {
                    for (var j = 0; j < height; j++)
                    {
                        action(_grid[i, j]);
                    }
                }
            }
            else if (x == PS.All)
            {
                for (var i = 0; i < height; i++)
                    action(_grid[i, y]);
            }
            else if (y == PS.All)
            {
                for (var j = 0; j < width; j++)
                    action(_grid[x, j]);
            }
            else if (x != PS.None && y != PS.None)
            {
                action(_grid[x, y]);
            }
        }

        public void BeadShow(int x, int y, bool flag)
        {
            Action<Bead> action = bead => bead.Show = flag;
            ActOnBead(x, y, action);
        }

        public void BeadColor(int x, int y, Color color)
        {
            Action<Bead> action = bead => bead.Color = color;
            ActOnBead(x, y, action);
        }

        // Alpha should be 0-100; default 100
        public void BeadAlpha(int x, int y, int alpha)
        {
            Action<Bead> action = bead => bead.Alpha = alpha;
            ActOnBead(x, y, action);
        }

        public void BeadBorderWidth(int x, int y, int width)
        {
            Action<Bead> action = bead => bead.BorderWidth = width;
            ActOnBead(x, y, action);
        }

        public void BeadBorderColor(int x, int y, Color color)
        {
            Action<Bead> action = bead => bead.BorderColor = color;
            ActOnBead(x, y, action);
        }

        public void BeadBorderAlpha(int x, int y, int alpha)
        {
            Action<Bead> action = bead => bead.BorderAlpha = alpha;
            ActOnBead(x, y, action);
        }

        public void BeadGlyph(int x, int y, char glyph)
        {
            Action<Bead> action = bead => bead.Glyph = glyph;
            ActOnBead(x, y, action);
        }

        public void BeadGlyphColor(int x, int y, Color color)
        {
            Action<Bead> action = bead => bead.GlyphColor = color;
            ActOnBead(x, y, action);
        }

        public void BeadFlash(int x, int y, bool flag)
        {
            Action<Bead> action = bead => bead.Flash = flag;
            ActOnBead(x, y, action);
        }

        public void BeadFlashColor(int x, int y, Color flashColor)
        {
            Action<Bead> action = bead => bead.FlashColor = flashColor;
            ActOnBead(x, y, action);
        }

        public void BeadAudio(int x, int y, string audio, int volume)
        {
            throw new NotImplementedException();
        }

        public void BeadTouch(int x, int y)
        {
            throw new NotImplementedException();
        }

        public Color GetBeadColor(int x, int y)
        {
            return _grid[x, y].Color;
        }

        public Color GetBeadBorderColor(int x, int y)
        {
            return _grid[x, y].BorderColor;
        }

        public Color GetBeadGlyphColor(int x, int y)
        {
            return _grid[x, y].GlyphColor;
        }

        public Color GetBeadFlashColor(int x, int y)
        {
            return _grid[x, y].FlashColor;
        }
        #endregion

    }
}
