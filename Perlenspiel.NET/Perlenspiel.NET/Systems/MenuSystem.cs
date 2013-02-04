using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PerlenspielEngine;
using PerlenspielEngine.Entities;
using PerlenspielEngine.Systems;
using PerlenspielGame.Components;
using PerlenspielGame.Components.Maps;
using PerlenspielGame.Components.Menu;
using PerlenspielGame.EntityBuilders;
using PerlenspielLib;

namespace PerlenspielGame.Systems
{
    class MenuSystem : AbstractSystem, IClickHandler
    {
        public bool IsOpen { get; set; }
        public int NextInventoryPosition
        {
            get { return _inventory.Count; }
        }
        public int NextBuildButtonPosition
        {
            get { return _buildButtons.Count; }
        }

        private EntityList _background;
        private EntityList _buttons;
        private EntityList _buildButtons;
        private EntityList _inventory;

        private Color _bgColor = Color.Black;
        private Color _invCounterColor = Color.SkyBlue;
        private Color _buildCounterColor = Color.SkyBlue;
        
        private int _height = GameState.GridHeight - 2;
        private int _width  = GameState.GridWidth - 2;

        private GridPoint _start = new GridPoint(1, 1);

        private GridPoint _invPos = new GridPoint(2, 2);
        private GridPoint _invDelta = new GridPoint(0, 2);

        private GridPoint _buildPos = new GridPoint(GameState.GridWidth / 2, 2);
        private GridPoint _buildDelta = new GridPoint(0, 2);

        #region System Members
        public MenuSystem()
        {
            _background     = new EntityList(new List<Component> { new BeadGraphic(), new Window(), new MultiplePositions() });
            _buttons        = ButtonBuilder.ButtonList();
            _buildButtons   = ButtonBuilder.BuildButtonList();
            _inventory      = InventoryEntryBuilder.InventoryEntryList();

            EntityLists.Add(_background);
            EntityLists.Add(_buttons);
            EntityLists.Add(_buildButtons);
            EntityLists.Add(_inventory);
        }

        public override void Initialize()
        {
            var bgGraphic = new BeadGraphic(Color.Black);
            var bgPositions = new MultiplePositions();
            for (var i = 0; i < _width; i++)
            {
                for (var j = 0; j < _height; j++)
                {
                    bgPositions.AddPosition(_start.X + i, _start.Y + j);
                }
            }
            var bg = new Window();
            var bgComps = new List<Component> { bgPositions, bgGraphic, bg };
            new Entity("Menu Background", bgComps);
        }

        #endregion
        #region Public Methods

        public void OpenMenu()
        {
            IsOpen = true;

            DrawBackground();
            DrawInventoryItems();
            DrawButtons();
        }

        public void CloseMenu()
        {
            IsOpen = false;
        }

        public void HandleClick(int x, int y)
        {
            var clicked = (from button in _buttons
                           where button.Component<IPosition>().IsAtPosition(x, y)
                           select button).FirstOrDefault();
            if (clicked != null)
            {
                PressButton(clicked);
            }
        }

        #endregion
        #region Private Methods
        private void PressButton(Entity clicked)
        {
            var button = clicked.Component<MenuButton>();
            button.Toggle();

            var buttonGraphic = clicked.Component<ButtonGraphic>();
            var beadGraphic = clicked.Component<BeadGraphic>();
            if (button.IsPressed == true)
            {
                buttonGraphic.ChangeToPressedGraphic(beadGraphic);
            }
            else
            {
                buttonGraphic.ChangeToOriginalGraphic(beadGraphic);
            }
            button.Callback(button.IsPressed);
            OpenMenu();
        }

        private void DrawInventoryItems()
        {
            foreach (var inv in _inventory)
            {
                DrawInventoryItem(inv);
            }
        }

        private void DrawBackground()
        {
            foreach (var bg in _background)
            {
                GameState.DrawEntity(bg);
            }
        }

        private void DrawButtons()
        {
            foreach (var button in _buttons)
            {
                DrawButton(button);
            }
        }

        private void DrawInventoryItem(Entity item)
        {
            // TODO: Fix this
            /*
            var invItem = item.Component<InventoryItem>();
            var x = p
            var y = pos.Y;

            var graphic = item.Component<BeadGraphic>();
            var inv = item.Component<InventoryItem>();
            var name = item.Name;

            GameState.DrawBeadGraphic(x, y, graphic);
            x += 1;

            DrawCounter(x, y, inv.Amount, 3, _invCounterColor, _bgColor);

            GameState.DrawString(x+4, y, name, Color.White);
             */
        }

        private void DrawButton(Entity buttonEntity)
        {
            var pos = buttonEntity.Component<Position>();
            GameState.DrawEntity(buttonEntity);

            var buildable = buttonEntity.Component<Buildable>();
            if (buildable != null)
            {
                DrawCounter(pos.X + 1, pos.Y, buildable.Cost, 1, _buildCounterColor, _bgColor);
                GameState.DrawString(pos.X + 3, pos.Y, buttonEntity.Name, _buildCounterColor);   
            }
        }
        
        private void DrawCounter(int x, int y, int amount, int length, Color glyphColor, Color bgColor)
        {
            string num = Convert.ToString(amount);
            while (num.Length < length)
            {
                num = "0" + num;
            }
            foreach (char t in num)
            {
                var counterGraphic = CounterGraphic(t, glyphColor, bgColor);
                GameState.DrawBeadGraphic(x, y, counterGraphic);
                x += 1;
            }
        }

        private BeadGraphic CounterGraphic(char num, Color glyphColor, Color bgColor)
        {
            var graphic = new BeadGraphic(bgColor);
            graphic.GlyphColor = glyphColor;
            graphic.Glyph = num;
            return graphic;
        }
        #endregion
    }
}
