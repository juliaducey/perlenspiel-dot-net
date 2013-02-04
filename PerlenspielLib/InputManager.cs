using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PerlenspielLib
{
    class InputManager
    {
        // Tracks the state of the keyboard
        private KeyboardState _newKeyState;
        private KeyboardState _oldKeyState;

        // Tracks the state of the mouse
        private MouseState _newMouseState;
        private MouseState _oldMouseState;

        // Tracks the bead the mouse is currently in
        private Point _prevBeadCoords = new Point(PS.None, PS.None);

        // PSGame instance from game project
        private Perlenspiel _game;

        public void Initialize(Perlenspiel game)
        {
            _game = game;
            _oldKeyState = Keyboard.GetState();
            _oldMouseState = Mouse.GetState();
        }

        public void Update()
        {
            // Get the current mouse and keyboard states
            _newKeyState = Keyboard.GetState();
            _newMouseState = Mouse.GetState();

            // Check for new key state
            if (_oldKeyState != _newKeyState)
            {
                // Iterate through all the keys
                foreach (Keys key in Enum.GetValues(typeof(Keys)))
                {
                    // Check for a new keypress
                    if (_newKeyState.IsKeyDown(key) && !_oldKeyState.IsKeyDown(key))
                    {
                        var keyVal = (int) key;
                        var shift = false;
                        var ctrl = false;
                        if (_newKeyState.IsKeyDown(Keys.LeftShift) || _newKeyState.IsKeyDown(Keys.RightShift))
                            shift = true;
                        if (_newKeyState.IsKeyDown(Keys.LeftControl) || _newKeyState.IsKeyDown(Keys.RightControl))
                            ctrl = true;
                        _game.KeyDown(keyVal, shift, ctrl);
                    }
                    // Check for a new key release
                    else if (_newKeyState.IsKeyUp(key) && !_oldKeyState.IsKeyUp(key))
                    {
                        var keyVal = (int)key;
                        var shift = false;
                        var ctrl = false;
                        if (_newKeyState.IsKeyUp(Keys.LeftShift) || _newKeyState.IsKeyUp(Keys.RightShift))
                            shift = true;
                        if (_newKeyState.IsKeyUp(Keys.LeftControl) || _newKeyState.IsKeyUp(Keys.RightControl))
                            ctrl = true;
                        _game.KeyUp(keyVal, shift, ctrl);
                    }
                }
            }
            // Check for new mouse state
            else if (_newMouseState != _oldMouseState)
            {
                Point beadCoords = Singleton<GridManager>.Instance.GetBeadCoords(_newMouseState.X, _newMouseState.Y);
                // Check for new mouse position
                if (_newMouseState.X != _oldMouseState.X || _newMouseState.Y != _oldMouseState.Y)
                {
                    // Check if mouse has moved to a new bead and call Enter and Leave if so
                    if (beadCoords.X != _prevBeadCoords.X || beadCoords.Y != _prevBeadCoords.Y)
                    {
                        // If we're in a bead, notify the game that we entered it
                        if (beadCoords.X != PS.None && beadCoords.Y != PS.None)
                        {
                            _game.Enter(beadCoords.X, beadCoords.Y);
                        }

                        // If we were previously in a bead, notify the game that we left it
                        if (_prevBeadCoords.X != PS.None && _prevBeadCoords.Y != PS.None)
                        {
                            _game.Leave(_prevBeadCoords.X, _prevBeadCoords.Y);
                        }

                        _prevBeadCoords = beadCoords;
                    }
                }
                else if (beadCoords.X != PS.None && beadCoords.Y != PS.None)
                {
                    // Check for left button click
                    if (_newMouseState.LeftButton == ButtonState.Pressed &&
                        _oldMouseState.LeftButton != ButtonState.Pressed)
                    {
                        _game.Click(beadCoords.X, beadCoords.Y);
                    }
                    // Check for right button click
                    else if (_newMouseState.RightButton == ButtonState.Pressed &&
                             _oldMouseState.RightButton != ButtonState.Pressed)
                    {
                        _game.RightClick(beadCoords.X, beadCoords.Y);
                    }
                    // Check for left button release
                    else if (_newMouseState.LeftButton == ButtonState.Released &&
                             _oldMouseState.LeftButton != ButtonState.Released)
                    {
                        _game.Release(beadCoords.X, beadCoords.Y);
                    }
                }
                // Check for mouse wheel scroll
                else if (_newMouseState.ScrollWheelValue != _oldMouseState.ScrollWheelValue)
                {
                    if (_newMouseState.ScrollWheelValue > _oldMouseState.ScrollWheelValue)
                        _game.Wheel(PS.Forward);
                    else
                        _game.Wheel(PS.Backward);
                }
            }
            // Save mouse and keyboard states for the next update
            _oldKeyState = _newKeyState;
            _oldMouseState = _newMouseState;
        }
    }
}
