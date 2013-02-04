using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace PerlenspielLib
{
    public static class PS
    {
        public static int All           = -1;
        public static int None          = -2;

        public static int ArrowLeft     = (int) Keys.Left;
        public static int ArrowRight    = (int) Keys.Right;
        public static int ArrowUp       = (int) Keys.Up;
        public static int ArrowDown     = (int) Keys.Down;
        public static int Space         = (int) Keys.Space;

        public static int Forward       = -1;
        public static int Backward      = 1;

        #region Grid
        public static void GridSize(int x, int y)
        {
            Singleton<GridManager>.Instance.GridSize(x, y);
        }

        public static void GridBGColor(Color color)
        {
            Singleton<GraphicsManager>.Instance.ScreenColor = color;
        }
        #endregion
        #region Beads
        public static void BeadShow(int x, int y, bool flag)
        {
            Singleton<GridManager>.Instance.BeadShow(x, y, flag);
        }

        public static void BeadColor(int x, int y, Color color)
        {
            Singleton<GridManager>.Instance.BeadColor(x, y, color);
        }

        // Alpha should be 0-100; default 100
        public static void BeadAlpha(int x, int y, int alpha)
        {
            Singleton<GridManager>.Instance.BeadAlpha(x, y, alpha);
        }

        public static void BeadBorderWidth(int x, int y, int width)
        {
            Singleton<GridManager>.Instance.BeadBorderWidth(x, y, width);
        }

        public static void BeadBorderColor(int x, int y, Color color)
        {
            Singleton<GridManager>.Instance.BeadBorderColor(x, y, color);
        }

        public static void BeadBorderAlpha(int x, int y, int alpha)
        {
            Singleton<GridManager>.Instance.BeadBorderAlpha(x, y, alpha);
        }

        public static void BeadGlyph(int x, int y, char glyph)
        {
            Singleton<GridManager>.Instance.BeadGlyph(x, y, glyph);
        }

        public static void BeadGlyphColor(int x, int y, Color color)
        {
            Singleton<GridManager>.Instance.BeadGlyphColor(x, y, color);
        }

        public static void BeadFlash(int x, int y, bool flag)
        {
            Singleton<GridManager>.Instance.BeadFlash(x, y, flag);
        }

        public static void BeadFlashColor(int x, int y, Color flashColor)
        {
            Singleton<GridManager>.Instance.BeadFlashColor(x, y, flashColor);
        }
        
        public static void BeadAudio(int x, int y, string audio, int volume)
        {
            Singleton<GridManager>.Instance.BeadAudio(x, y, audio, volume);
        }

        public static void BeadTouch(int x, int y)
        {
            Singleton<GridManager>.Instance.BeadTouch(x, y);
        }

        public static Color BeadColor(int x, int y)
        {
            return Singleton<GridManager>.Instance.GetBeadColor(x, y);
        }

        public static Color BeadBorderColor(int x, int y)
        {
            return Singleton<GridManager>.Instance.GetBeadBorderColor(x, y);
        }

        public static Color BeadGlyphColor(int x, int y)
        {
            return Singleton<GridManager>.Instance.GetBeadGlyphColor(x, y);
        }

        public static Color BeadFlashColor(int x, int y)
        {
            return Singleton<GridManager>.Instance.GetBeadFlashColor(x, y);
        }
        #endregion
        #region Status
        public static void StatusText(string text)
        {
            Singleton<GraphicsManager>.Instance.StatusText = text;
        }

        public static void StatusColor(Color color)
        {
            Singleton<GraphicsManager>.Instance.StatusColor = color;
        }
        #endregion
    }
}
