using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PerlenspielGame.Systems
{
    class Region
    {
        public string Name                  { get; private set; }
        public string Default               { get; private set; }
        private List<int> _probabilities;
        private int _probSum;
        private List<string> _tiles;
        private Random _rng;

        public Region(string name, string defaultTile)
        {
            Name = name;
            Default = defaultTile;
            _probSum = 0;
            _probabilities = new List<int>();
            _tiles = new List<string>();
            _rng = new Random();
        }

        public void AddTerrain(int probability, string tile)
        {
            _probabilities.Add(probability);
            _probSum += probability;
            _tiles.Add(tile);
        }

        public string[,] Generate(int width, int height)
        {
            var region = new string[width, height];
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    region[i, j] = GenerateTile();
                }
            }
            return region;
        }

        private string GenerateTile()
        {
            var rand = _rng.Next(0, _probSum);
            var total = 0;
            for (var i = 0; i < _probabilities.Count; i++)
            {
                if (rand <= _probabilities[i] + total)
                {
                    return _tiles[i];
                }
                total += _probabilities[i];
            }
            return null;
        }
    }
}
