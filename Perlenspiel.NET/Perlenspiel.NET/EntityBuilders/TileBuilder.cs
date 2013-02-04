using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PerlenspielEngine.Entities;
using PerlenspielGame.Components;
using PerlenspielGame.Components.Maps;
using PerlenspielGame.Components.Menu;
using PerlenspielLib;

namespace PerlenspielGame.EntityBuilders
{
    class TileBuilder
    {
        private List<Component> _requiredComponents;
        private List<Component> _optionalComponents; 
        private BeadGraphic _graphic;
        private Tile _tile;
        private string _name;

        private Color _buttonColor = Color.Aquamarine;
        private Color _buttonGlyphColor = Color.Black;
        private Color _buttonBorderColor = Color.Black;
        private int _buttonBorderWidth = 0;

        public TileBuilder()
        {
            Initialize();
        }

        #region Static Methods
        public static void Start(string name, string description, Color color)
        {
            Singleton<TileBuilder>.Instance.PrivateStart(name, description, color);
        }

        public static void AddBorder(Color borderColor, int borderWidth)
        {
            Singleton<TileBuilder>.Instance._graphic.BorderColor = borderColor;
            Singleton<TileBuilder>.Instance._graphic.BorderWidth = borderWidth;
        }

        public static void AddGlyph(Color glyphColor, char glyph)
        {
            Singleton<TileBuilder>.Instance._graphic.GlyphColor = glyphColor;
            Singleton<TileBuilder>.Instance._graphic.Glyph = glyph;
        }

        public static void AddObstacle(IEnumerable<string> blockFlags = null)
        {
            Singleton<TileBuilder>.Instance._optionalComponents.Add(new Obstacle(blockFlags));
        }

        public static void AddHarvestable(string message, string harvestItem, int itemYield = 1,
            string harvestPrereq = "None", string baseTile = "Default")
        {
            Singleton<TileBuilder>.Instance._optionalComponents.Add(
                new Harvestable(message, harvestItem, itemYield, harvestPrereq, baseTile));
        }

        public static void AddBuildable(string material, int cost = 1, string buildPrereq = "None")
        {
            Singleton<TileBuilder>.Instance.PrivateAddBuildable(material, cost, buildPrereq);
        }

        public static void Build()
        {
            Singleton<TileBuilder>.Instance.BuildEntity();
        }

        public static void Build(string name, string description, Color color)
        {
            Start(name, description, color);
            Build();
        }

        public static EntityList TileList()
        {
            return new EntityList(Singleton<TileBuilder>.Instance._requiredComponents);
        }
        #endregion

        #region Private Methods
        private void Initialize()
        {
            _requiredComponents = new List<Component>();
            _optionalComponents = new List<Component>();
            _graphic = new BeadGraphic(Color.Black);
            _tile = new Tile("");

            _requiredComponents.Add(_graphic);
            _requiredComponents.Add(_tile);
            _requiredComponents.Add(new MultiplePositions());
        }

        private void PrivateStart(string name, string description, Color color)
        {
            Initialize();
            _name = name;
            _graphic.Color = color;
            _tile.Description = description;

            _optionalComponents = new List<Component>();
            _requiredComponents = new List<Component>();
            _requiredComponents.Add(_graphic);
            _requiredComponents.Add(new Tile(description));
            _requiredComponents.Add(new MultiplePositions());
        }

        private void PrivateAddBuildable(string material, int cost, string buildPrereq)
        {
            _optionalComponents.Add(new Buildable(material, cost, buildPrereq));

            bool hasHarvestable = (from component in _requiredComponents
                                   where component is Harvestable
                                   select component).Any();
            if (hasHarvestable == false)
            {
                var harvestable = new Harvestable("You tear down the " + _name.ToLower() + ".", material, cost,
                                                  buildPrereq);
                _optionalComponents.Add(harvestable);
            }

        }

        private void BuildEntity()
        {
            if (_name == null)
            {
                throw new Exception("Error: Call start before calling build!");
            }

            var tile = new Entity(_name, _requiredComponents.Union(_optionalComponents));

            if (tile.Component<Buildable>() != null)
            {
                Action<bool> callback = isPressed => GameState.SwitchBuildable(_name, isPressed);
                ButtonBuilder.Start(_name, ButtonType.Build, _graphic, _buttonColor, callback);
                ButtonBuilder.AddBorder(_buttonBorderColor, _buttonBorderWidth);
                if (_graphic.Glyph != ' ')
                    ButtonBuilder.AddGlyph(_buttonGlyphColor, _graphic.Glyph);
                ButtonBuilder.Build();
            }

            Initialize();
        }
        #endregion

    }
}
