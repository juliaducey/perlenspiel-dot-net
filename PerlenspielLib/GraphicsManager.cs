using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace PerlenspielLib
{
    class GraphicsManager
    {
        // XNA drawing
        private GraphicsDevice _graphicsDevice;
        private SpriteBatch _spriteBatch;
        private RasterizerState _rasterizerState;

        // Current graphics configuration
        public Color ScreenColor { private get; set; }
        public string StatusText { private get; set; }
        public Color StatusColor { private get; set; }
        private Rectangle _screenDims;
        private Rectangle _statusBox;

        // A 1px white texture; stretched and recolored to draw beads
        private Texture2D _texture;

        // Font at different sizes; key is point size
        private Dictionary<int, SpriteFont> _fonts;

        private SpriteFont _glyphFont;
        private SpriteFont _statusFont;

        /// <summary>
        /// Initialize GraphicsManager
        /// </summary>
        /// <param name="graphicsDevice"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="fonts"></param>
        public void InitializeGraphics(GraphicsDeviceManager graphicsDevice, SpriteBatch spriteBatch, Dictionary<int, SpriteFont> fonts)
        {
            // Assign private variables
            _graphicsDevice = graphicsDevice.GraphicsDevice;
            _spriteBatch = new SpriteBatch(_graphicsDevice);
            _fonts = fonts;
            _rasterizerState = new RasterizerState() { ScissorTestEnable = true };

            // Construct 1px white texture
            _texture = new Texture2D(_graphicsDevice, 1, 1, false, SurfaceFormat.Color);
            _texture.SetData(new Color[1] { Color.White });
        }

        /// <summary>
        /// Initialize the screen
        /// </summary>
        /// <param name="screenWidth"></param>
        /// <param name="screenHeight"></param>
        /// <param name="statusHeight"></param>
        /// <param name="topMargin"></param>
        /// <param name="leftMargin"></param>
        /// <param name="rightMargin"></param>
        public void InitializeScreen(int screenWidth, int screenHeight, int statusHeight, 
            int topMargin, int leftMargin, int rightMargin)
        {
            _screenDims = new Rectangle(0, 0, screenWidth, screenHeight);
            _statusBox = new Rectangle(leftMargin, topMargin, screenWidth - leftMargin - rightMargin, statusHeight);
            _statusFont = PickFont(_statusBox.Height);

            // Default colors
            ScreenColor = Color.White;
            StatusColor = Color.Black;
            StatusText = "";
        }

        /// <summary>
        /// This is the primary draw method of the library; should be called in draw loop
        /// </summary>
        public void DrawScreen()
        {
            _spriteBatch.Begin();
            // Draw background
            _spriteBatch.Draw(_texture, _screenDims, ScreenColor);
            // Draw grid
            Singleton<GridManager>.Instance.DrawGrid();
            _spriteBatch.End();

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, _rasterizerState);
            DrawStatus();
            _spriteBatch.End();
        }

        /// <summary>
        /// Draws a bead
        /// </summary>
        /// <param name="bead"></param>
        public void DrawBead(Bead bead)
        {
            // Draw bead + border if the bead has a border
            if (bead.BorderWidth > 0)
            {
                // Fill up the square with the border color
                DrawSquare(bead.Area, bead.BorderColor);

                // Draw the center on top, shrunk by the border width
                Rectangle beadCenter = ShrinkRectangle(bead.Area, bead.BorderWidth);
                DrawSquare(beadCenter, bead.Color);
            }
            // Otherwise, just draw the bead color
            else
            {
                DrawSquare(bead.Area, bead.Color);
            }

            // If the bead has a glyph, draw it
            if (bead.Glyph != ' ')
            {
                DrawLetter(bead.Glyph, bead.GlyphColor, bead.Area);
            }
        }

        public void SetGlyphFont (int fontHeight)
        {
            _glyphFont = PickFont(fontHeight);
        }

        #region Private Methods
        /// <summary>
        /// Draws a solid square of the given color in the given rectangle
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="color"></param>
        private void DrawSquare(Rectangle rectangle, Color color)
        {
            _spriteBatch.Draw(_texture, rectangle, color);
        }

        /// <summary>
        /// Draws a character of the given color centered in the given rectangle
        /// </summary>
        /// <param name="letter"></param>
        /// <param name="color"></param>
        /// <param name="area"></param>
        private void DrawLetter(char letter, Color color, Rectangle area)
        {
            var str = new string(letter, 1);

            // Center string in rectangle
            var centerX = area.X + (area.Width/2);
            var centerY = area.Y + (area.Height/2);
            Vector2 strDims = _glyphFont.MeasureString(str);
            var pos = new Vector2(centerX - Convert.ToInt32(strDims.X/2), Convert.ToInt32(centerY - strDims.Y/2));

            _spriteBatch.DrawString(_glyphFont, str, pos, color);
        }

        /// <summary>
        /// Draws status text in the status box
        /// </summary>
        private void DrawStatus()
        {
            Rectangle currentRect = _spriteBatch.GraphicsDevice.ScissorRectangle;
            // Set the scissor rectangle so text gets cut off outside the status box
            _spriteBatch.GraphicsDevice.ScissorRectangle = _statusBox;

            // If the status is smaller than the status box, center it
            Vector2 statusDims = _statusFont.MeasureString(StatusText);
            Vector2 pos = new Vector2(_statusBox.X, _statusBox.Y);
            if (statusDims.X < _statusBox.Width)
            {
                pos.X = Convert.ToInt32(_statusBox.X + _statusBox.Width/2 - (statusDims.X/2));
            }

            // Draw the status
            _spriteBatch.DrawString(_statusFont, StatusText, pos, StatusColor);

            _spriteBatch.GraphicsDevice.ScissorRectangle = currentRect;
        }

        /// <summary>
        /// Picks a font and scale factor such that drawn text will be the given height
        /// </summary>
        /// <param name="fontHeight"></param>
        private SpriteFont PickFont(int fontHeight)
        {
            var test = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

            // Find the largest font under the fontHeight limit
            var fontKeys = from key in _fonts.Keys
                           let height = _fonts[key].MeasureString(test).Y
                           where height <= fontHeight
                           orderby height descending
                           select key;
            int fontKey = _fonts.Keys.First();
            if (fontKeys.Count() > 0)
                fontKey = fontKeys.First();

            return _fonts[fontKey];
        }

        /// <summary>
        /// Takes a rectangle and returns a smaller rectangle centered on the original
        /// </summary>
        /// <param name="rect">Original rectangle</param>
        /// <param name="amount">Amount to shrink new rectangle by in all directions</param>
        /// <returns></returns>
        private Rectangle ShrinkRectangle(Rectangle rect, int amount)
        {
            var newRect = new Rectangle(rect.X + amount,
                                        rect.Y + amount,
                                        rect.Width - (amount*2),
                                        rect.Height - (amount*2));
            return newRect;
        }
        #endregion
    }
}
