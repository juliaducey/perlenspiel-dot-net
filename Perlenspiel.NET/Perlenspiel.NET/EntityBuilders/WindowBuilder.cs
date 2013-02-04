using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PerlenspielEngine;
using PerlenspielEngine.Entities;
using PerlenspielGame.Components;
using PerlenspielLib;

namespace PerlenspielGame.EntityBuilders
{
    class WindowBuilder
    {
        private string _name;
        private int _buffer;
        private GridPoint _position;
        private GridPoint _size;
        // Free spots in the window; points are relative to the window, not the whole screen!
        //private GridContents<BeadGraphic> _windowGraphics;

        private List<Component> _requiredComponents;
        private MultiplePositions _positions;
        private BeadGraphic _graphic;
        private Window _window;
        private List<GridPoint> _freeSpots;

        public WindowBuilder()
        {
            Initialize();
        }

        #region Static Methods
        public static void Start(string name, int x, int y, int width, int height, Color color, int buffer = 0)
        {
            Singleton<WindowBuilder>.Instance.StartWindow(name, x, y, width, height, color, buffer);
        }

        #endregion
        #region Private Methods
        private void Initialize()
        {
            _positions = new MultiplePositions();
            _graphic = new BeadGraphic();
            _window = new Window(Color.Black);
            _freeSpots = new List<GridPoint>();

            _requiredComponents = new List<Component>();
            _requiredComponents.Add(_positions);
            _requiredComponents.Add(_graphic);
            _requiredComponents.Add(_window);
        }

        private void StartWindow(string name, int x, int y, int width, int height, Color color, int buffer)
        {
            _name = name;
            _position = new GridPoint(x, y);
            _size = new GridPoint(width, height);
            _buffer = buffer;
            _graphic.Color = color;

            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    _positions.AddPosition(i + x, j + y);
                    if (i > buffer && i < width - buffer && j > buffer && j < width - buffer)
                    {
                        // Note that these are relative to the window, not the whole screen!
                        _freeSpots.Add(new GridPoint(i, j));
                    }
                }
            }
        }

        /// <summary>
        /// Adds a graphic to the window
        /// </summary>
        /// <param name="graphic">Graphic to be added</param>
        /// <param name="x">Horizontal offset relative to drawable space inside of window (starts inside buffer)</param>
        /// <param name="y">Vertical offset as above</param>
        private void AddGraphic(BeadGraphic graphic, int x = 0, int y = 0)
        {
            var pos = new RelativePosition(_position, x, y);
            new Entity(_name, new Component[] { pos, _graphic });
            // TODO: add a case for figuring out the next available spot if x and y are taken
        }

        private GridPoint GetNextPosition(int x, int y)
        {
            // TODO this
            return new GridPoint(0, 0);
        }
        #endregion
    }
}
