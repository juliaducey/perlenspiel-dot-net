using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PerlenspielEngine;
using PerlenspielEngine.Entities;
using PerlenspielEngine.Systems;
using PerlenspielGame.Components;
using PerlenspielGame.Components.Lighting;
using PerlenspielLib;

namespace PerlenspielGame.Systems
{
    class LightSystem : AbstractSystem
    {
        private Dictionary<Entity, List<Entity>> _sourcesAndLights; 
        private Entity[,] _darkSpots;
        private Color _fogColor = Color.Black;

        #region ISystem Members
        public LightSystem()
        {
            _darkSpots = new Entity[GameState.GridWidth, GameState.GridHeight];
            _sourcesAndLights = new Dictionary<Entity, List<Entity>>();
        }

        public override void Register(Entity entity)
        {
            var lightSource = entity.Component<LightSource>();
            if (lightSource != null)
            {
                CreateGlow(entity);
            }
        }

        public override void Deregister(Entity entity)
        {
            return;
        }
        #endregion

        public void CreateFog()
        {
            for (var i = 0; i < GameState.GridWidth; i++)
            {
                for (var j = 0; j < GameState.GridHeight; j++)
                {
                    _darkSpots[i, j] = new Entity("Darkness", new Component[] { new Darkness(_fogColor), new Position(i, j) });
                }
            }
        }

        public void CreateGlow(Entity lightSource)
        {
            var center = lightSource.Component<Position>().Coords;
            var source = lightSource.Component<LightSource>();
            // Radius, light strength, fog strength; must start in the center
            var lightPools = new[]
                                 {
                                     new[] {1, 30, 0},
                                     new[] {2, 25, 5},
                                     new[] {Convert.ToInt32(source.Radius*.6), 20, 10},
                                     new[] {Convert.ToInt32(source.Radius*.75), 12, 25},
                                     new[] {Convert.ToInt32(source.Radius*.9), 5, 40}
                                 };

            var glow = new Dictionary<GridPoint, Entity>();
            foreach (var pool in lightPools)
            {
                var points = Utilities.CreateCircle(center, pool[0], true);
                foreach (var point in points)
                {
                    if (glow.ContainsKey(point) == false)
                    {
                        var lightStrength = source.Strength * pool[1] / 100.0;
                        var fogStrength = source.Strength * pool[2] / 100.0;
                        var light = new Light(source.Color, lightStrength, fogStrength);
                        glow[point] = new Entity("Light", new Component[] { new Position(point.X, point.Y), light } );
                    }
                }
            }
            _sourcesAndLights.Add(lightSource, glow.Values.ToList());
        }

        public int[][] GenerateLightPools(LightSource source)
        {
            return null;
        }
    }
}
