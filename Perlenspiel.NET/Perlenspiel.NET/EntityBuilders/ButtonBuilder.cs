using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PerlenspielEngine;
using PerlenspielEngine.Entities;
using PerlenspielGame.Components;
using PerlenspielGame.Components.Maps;
using PerlenspielGame.Components.Menu;
using PerlenspielLib;

namespace PerlenspielGame.EntityBuilders
{
    class ButtonBuilder
    {
        private string _name;
        private BeadGraphic _graphic;
        private ButtonGraphic _buttonGraphic;
        private Position _position;
        private MenuButton _button;

        private List<Component> _requiredComponents;
        private List<Component> _optionalComponents;

        // TODO: Don't hard-code this
        private GridPoint _buildPos = new GridPoint(1, 1);
        private GridPoint _buildDelta = new GridPoint(0, 2);

        public ButtonBuilder()
        {
            Initialize();
        }
        #region Static Methods
        public static void Start(string name, ButtonType type, BeadGraphic baseGraphic, Color pressedColor, Action<bool> callback)
        {
            Singleton<ButtonBuilder>.Instance.StartButton(name, type, baseGraphic, pressedColor, callback);
        }

        public static void Build()
        {
            Singleton<ButtonBuilder>.Instance.BuildEntity();
        }

        public static void AddBorder(Color borderColor, int borderWidth)
        {
            Singleton<ButtonBuilder>.Instance._graphic.BorderColor = borderColor;
            Singleton<ButtonBuilder>.Instance._graphic.BorderWidth = borderWidth;
        }

        public static void AddGlyph(Color glyphColor, char glyph)
        {
            Singleton<ButtonBuilder>.Instance._graphic.GlyphColor = glyphColor;
            Singleton<ButtonBuilder>.Instance._graphic.Glyph = glyph;
        }

        public static void AddBuildable(Buildable buildable)
        {
            Singleton<ButtonBuilder>.Instance._optionalComponents.Add(buildable);
        }

        public static EntityList ButtonList()
        {
            return new EntityList(Singleton<ButtonBuilder>.Instance._requiredComponents);
        }

        public static EntityList BuildButtonList()
        {
            var comps = Singleton<ButtonBuilder>.Instance._requiredComponents.Union(
                new[] {new Buildable("") }).ToList();
            return new EntityList(comps);
        }
        #endregion
        #region Private Methods
        private void Initialize()
        {
            _optionalComponents = new List<Component>();
            _requiredComponents = new List<Component>();

            _graphic = new BeadGraphic();
            _position = new Position();
            _button = new MenuButton();
            _buttonGraphic = new ButtonGraphic(_graphic);
            _requiredComponents.Add(_graphic);
            _requiredComponents.Add(_buttonGraphic);
            _requiredComponents.Add(_position);
            _requiredComponents.Add(_button);
        }

        private void StartButton(string name, ButtonType type, BeadGraphic baseGraphic, Color pressedColor, Action<bool> callback)
        {
            Initialize();
            _name = name;
            _graphic = baseGraphic;
            _button.Callback = callback;
            _button.Type = type;

            _buttonGraphic = new ButtonGraphic(baseGraphic, pressedColor);

            switch (type)
            {
                case (ButtonType.Build):
                    _position.Coords = GetBuildCoords();
                    break;
            }
        }

        private GridPoint GetBuildCoords()
        {
            _buildPos = _buildPos.Add(_buildDelta);
            return _buildPos;
        }

        private void BuildEntity()
        {
            new Entity(_name, _requiredComponents.Union(_optionalComponents));
        }
        #endregion
    }
}
