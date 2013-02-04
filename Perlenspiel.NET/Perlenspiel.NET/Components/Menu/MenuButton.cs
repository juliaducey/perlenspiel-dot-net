using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PerlenspielEngine.Entities;

namespace PerlenspielGame.Components.Menu
{
    enum ButtonType { Build }

    class MenuButton : Component
    {
        public ButtonType Type          { get; set; }
        public bool IsPressed           { get; set; }
        public Action<bool> Callback    { get; set; }
        public int MenuPosition         { get; set; }

        public MenuButton()
        {
            Callback = isPressed => { };
        }

        public MenuButton(Action<bool> callback)
        {
            Callback = callback;
        }

        public void Toggle()
        {
            IsPressed = !IsPressed;
        }
    }
}
