using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PerlenspielEngine;
using PerlenspielEngine.Entities;
using PerlenspielEngine.Systems;
using PerlenspielGame.Components;
using PerlenspielGame.Systems;
using PerlenspielLib;

namespace PerlenspielGame
{
    public class GameState
    {
        private Entity _player;
        // Key is the int value associated with a keypress
        private Dictionary<int, GridPoint> _moveKeys;

        public static int GridWidth  { get { return Singleton<GameState>.Instance._gridWidth; } }
        public static int GridHeight { get { return Singleton<GameState>.Instance._gridHeight; } }
        private int _gridWidth;
        private int _gridHeight;

        public static double MaxAlpha = 1.0;
        public static double MinAlpha = 0.0;

        private IClickHandler _curHandler;
        private IClickHandler _prevHandler;

        public void Initialize(int gridWidth, int gridHeight)
        {
            _gridWidth = gridWidth;
            _gridHeight = gridHeight;
 
            _moveKeys = new Dictionary<int, GridPoint>();
            _moveKeys.Add('W', new GridPoint(0, -1));
            _moveKeys.Add('A', new GridPoint(-1, 0));
            _moveKeys.Add('S', new GridPoint(0, 1));
            _moveKeys.Add('D', new GridPoint(1, 0));

            InitializeSystems();
        }

        public void InitializeSystems()
        {
            Singleton<DrawSystem>.Instance.Initialize();
            Singleton<MoveSystem>.Instance.Initialize();
            Singleton<LightSystem>.Instance.Initialize();
            Singleton<MapSystem>.Instance.Initialize();
            Singleton<ConstructionSystem>.Instance.Initialize();
            Singleton<InventorySystem>.Instance.Initialize();
            Singleton<MenuSystem>.Instance.Initialize();

            _curHandler = Singleton<MapSystem>.Instance;
        }

        #region Static Wrapper Methods
        public static bool OnScreen(int x, int y)
        {
            var maxX = GridWidth - 1;
            var maxY = GridHeight - 1;
            return (x >= 0 && x <= maxX && y >= 0 && y <= maxY);
        }

        public static void DrawEntity(Entity entity)
        {
            Singleton<DrawSystem>.Instance.DrawEntity(entity);
        }

        public static void DrawMap()
        {
            Singleton<MapSystem>.Instance.DrawTiles();
            Singleton<MoveSystem>.Instance.DrawMobs();
        }

        public static void DrawString(int x, int y, string str, Color color)
        {
            Singleton<DrawSystem>.Instance.DrawString(x, y, str, color);
        }

        public static void DrawBeadGraphic(int x, int y, BeadGraphic graphic)
        {
            Singleton<DrawSystem>.Instance.DrawBeadGraphic(x, y, graphic);
        }

        public static void MovePlayer(int key)
        {
            Singleton<GameState>.Instance.PlayerMovement(key);
        }

        public static void HandleClick(int x, int y)
        {
            Singleton<GameState>.Instance._curHandler.HandleClick(x, y);
        }

        public static void ToggleMenu()
        {
            if (Singleton<MenuSystem>.Instance.IsOpen == false)
            {
                Singleton<MenuSystem>.Instance.OpenMenu();
                Singleton<GameState>.Instance._prevHandler = Singleton<GameState>.Instance._curHandler;
                Singleton<GameState>.Instance._curHandler = Singleton<MenuSystem>.Instance;
            }
            else
            {
                Singleton<MenuSystem>.Instance.CloseMenu();
                Singleton<GameState>.Instance._curHandler = Singleton<GameState>.Instance._prevHandler;
                DrawMap();
            }
        }

        public static void SwitchBuildable(string name, bool isPressed)
        {
            //Singleton<ConstructionSystem>.Instance.
        }
        #endregion

        public void SetPlayer(Entity player)
        {
            _player = player;
        }

        private void PlayerMovement(int key)
        {
            if (_moveKeys.ContainsKey(key))
            {
                var dir = _moveKeys[key];
                var pos = _player.Component<Position>().Coords;
                Singleton<MoveSystem>.Instance.MoveEntity(_player, pos, pos.Add(dir));
            }
        }
    }
}
