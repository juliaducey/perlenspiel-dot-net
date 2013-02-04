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
    class MapSystem : AbstractSystem, IClickHandler
    {
        private Region _region;
        private string[,] _map;
        private EntityList _tiles;
        
        #region ISystem Members
        public MapSystem()
        {
            _map = new string[GameState.GridWidth, GameState.GridHeight];
            _tiles = TileBuilder.TileList();
        }

        public override void Initialize()
        {
            // TODO: Move all of this shit to xml files or something
            var stoneColor = new Color(130, 140, 140);

            TileBuilder.Build("Stone", "This stone is rough and uneven. Long cracks run through it.", stoneColor);

            TileBuilder.Start("Boulder", "A large boulder. It sits in a shallow depression.", new Color(120, 115, 110));
            TileBuilder.AddBorder(new Color(100, 95, 90), 1);
            TileBuilder.AddGlyph(new Color(140, 135, 130), 'O');
            TileBuilder.AddObstacle(new[] { "walk", "swim" });
            TileBuilder.AddHarvestable("The boulder falls apart. You gather the pieces.", "Stone", 4);
            TileBuilder.Build();

            TileBuilder.Start("Pebble", "A smooth, round pebble.", new Color(150, 155, 145));
            TileBuilder.AddBorder(stoneColor, 5);
            TileBuilder.Build();

            TileBuilder.Start("Dead Plant", "A withered shrub. It looks quite dead.", new Color(150, 160, 120));
            TileBuilder.AddBorder(stoneColor, 4);
            TileBuilder.AddGlyph(new Color(120, 130, 90), 'w');
            TileBuilder.Build();

            TileBuilder.Start("Stone Wall", "A stone wall build out of crude chunks of rock.", new Color(110, 105, 100));
            TileBuilder.AddBorder(new Color(100, 95, 90), 1);
            TileBuilder.AddObstacle();
            TileBuilder.AddBuildable("Stone", 2);
            TileBuilder.Build();

            TileBuilder.Start("Stone Floor", "A few sheets of rock form a crude floor here.", new Color(90, 85, 80));
            TileBuilder.AddBuildable("Stone");
            TileBuilder.Build();

            MaterialBuilder.Build("Stone", Color.Gray);

            // TODO: Refactor region into components somehow
            _region = new Region("Wasteland", "Stone");
            _region.AddTerrain(1, "Boulder");
            _region.AddTerrain(1, "Dead Plant");
            _region.AddTerrain(3, "Pebble");
            _region.AddTerrain(15, "Stone");

            GenerateRegion(0, 0, GameState.GridWidth, GameState.GridHeight);
        }

        public override void Register(Entity entity)
        {
            _tiles.Add(entity);
        }

        public override void Deregister(Entity entity)
        {
            if (entity.Component<Tile>() != null)
            {
                var positions = entity.Component<IPosition>().Positions;
                foreach (var pos in positions)
                {
                    _map[pos.X, pos.Y] = null;
                }
                SetTiles(_region.Default, positions);
            }
        }
        #endregion
        #region Public Methods

        public void DrawTiles()
        {
            foreach (var tile in _tiles)
            {
                GameState.DrawEntity(tile);
            }
        }

        public void HandleClick(int x, int y)
        {
            Singleton<ConstructionSystem>.Instance.Harvest(x, y);
            var tileEntity = _tiles[_map[x, y]];
            PS.StatusText(tileEntity.Component<Tile>().Description);
        }

        public void SetTile(int x, int y, string tile)
        {
            var currentTileName = _map[x, y];
            _tiles[currentTileName].Component<MultiplePositions>().RemovePosition(x, y);

            Entity newTile;
            if (tile == "Default")
            {
                newTile = _tiles[_region.Default];
                tile = _region.Default;
            }
            else
            {
                newTile = _tiles[tile];
            }
            newTile.Component<MultiplePositions>().AddPosition(x, y);

            _map[x, y] = tile;
            GameState.DrawMap();
        }

        public BeadGraphic GetTileGraphic(string tile)
        {
            return _tiles[tile].Component<BeadGraphic>();
        }
        #endregion
        #region Private Methods

        public void GenerateRegion(int startX, int startY, int stopX, int stopY)
        {
            var tiles = _region.Generate(stopX - startX, stopY - startY);
            SetTiles(tiles, startX, startY);
        }

        private void SetTiles(string tile, IEnumerable<GridPoint> points)
        {
            foreach (var point in points)
            {
                SetTile(point.X, point.Y, tile);
            }
        }
        
        private void SetTiles(string[,] tiles, int startX, int startY)
        {
            for (var i = 0; i < tiles.GetLength(0); i++)
            {
                for (var j = 0; j < tiles.GetLength(1); j++)
                {
                    var tile = tiles[i, j];
                    var mapX = startX + i;
                    var mapY = startY + j;
                    _map[mapX, mapY] = tile;
                    _tiles[tile].Component<MultiplePositions>().AddPosition(mapX, mapY);
                }
            }
            GameState.DrawMap();
        }

        #endregion
    }
}
