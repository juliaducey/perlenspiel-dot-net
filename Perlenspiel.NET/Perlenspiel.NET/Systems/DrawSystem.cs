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
    // TODO: Get rid of all the lighting bullshit in here
    class DrawSystem : AbstractSystem
    {
        private List<Entity> _graphics;
        private List<Entity> _overlays;
        private List<Entity> _darkness;
        private List<Entity> _lights;

        #region ISystem Members
        public DrawSystem()
        {
            _graphics = new List<Entity>();
            _overlays = new List<Entity>();
            _darkness = new List<Entity>();
            _lights = new List<Entity>();
        }

        public override void Register(Entity entity)
        {
            var position = entity.Component<IPosition>();
            if (position == null) return;

            var beadGraphic = entity.Component<BeadGraphic>();
            var overlay = entity.Component<Overlay>();
            var light = entity.Component<Light>();
            var darkness = entity.Component<Darkness>();

            if (beadGraphic != null)
            {
                _graphics.Add(entity);
            }
            if (overlay != null)
            {
                _overlays.Add(entity);
            }
            if (light != null)
            {
                _lights.Add(entity);
            }
            if (darkness != null)
            {
                _darkness.Add(entity);
            }
            if (beadGraphic != null || overlay != null || light != null || darkness != null)
            {
                GameState.DrawMap();
            }
        }

        public override void Deregister(Entity entity)
        {
            _graphics.Remove(entity);
            _overlays.Remove(entity);
            GameState.DrawMap();
        }
        #endregion
        #region Public Methods

        public void DrawEntity(Entity entity)
        {
            var position = entity.Component<IPosition>();
            if (position == null) return;

            var graphic = entity.Component<BeadGraphic>();
            if (graphic != null)
            {
                foreach (var pos in position.Positions)
                {
                    if (GameState.OnScreen(pos.X, pos.Y))
                        DrawBeadGraphic(pos.X, pos.Y, graphic);
                }
            }

            var overlay = entity.Component<Overlay>();
            if (overlay != null)
            {
                foreach (var pos in position.Positions)
                {
                    DrawOverlay(pos.X, pos.Y, overlay);
                }
            }
        }

        public void DrawBeadGraphic(int x, int y, BeadGraphic beadGraphic)
        {
            PS.BeadColor(x, y, beadGraphic.Color);
            PS.BeadAlpha(x, y, Convert.ToInt32(beadGraphic.Alpha * 100));
            PS.BeadShow(x, y, beadGraphic.Show);

            PS.BeadBorderColor(x, y, beadGraphic.BorderColor);
            PS.BeadBorderWidth(x, y, beadGraphic.BorderWidth);
            PS.BeadBorderAlpha(x, y, beadGraphic.BorderAlpha);

            PS.BeadGlyph(x, y, beadGraphic.Glyph);
            PS.BeadGlyphColor(x, y, beadGraphic.GlyphColor);

            PS.BeadFlash(x, y, beadGraphic.Flash);
            PS.BeadFlashColor(x, y, beadGraphic.FlashColor);
        }

        public void DrawString(int x, int y, string str, Color color)
        {
            for (var i = 0; i < str.Length; i++)
            {
                PS.BeadGlyph(x + i, y, str[i]);
                PS.BeadGlyphColor(x + i, y, color);
            }
        }

        #endregion
        #region Lighting Methods
        private List<Overlay> CalcLighting(int x, int y)
        {
            var lighting = new List<Overlay>();

            var darkness = from dark in _darkness
                           where dark.Component<IPosition>().IsAtPosition(x, y)
                           select dark.Component<Darkness>();

            var lights = from light in _lights
                         where light.Component<IPosition>().IsAtPosition(x, y)
                         select light.Component<Light>();

            if (darkness.Count() > 0)
                lighting.Add(CalcDarkness(lights, darkness.First()));

            var lightOverlay = CombineLights(lights);
            if (lightOverlay != null)
                lighting.Add(lightOverlay);

            return lighting;
        }

        private Overlay CalcDarkness(IEnumerable<Light> lights, Darkness dark)
        {
            if (dark != null)
            {
                var fogStrength = 1.0;
                foreach (var light in lights)
                {
                    fogStrength -= (1.0 - light.FogStrength);
                }
                fogStrength = Utilities.Constrain(fogStrength, GameState.MinAlpha, GameState.MaxAlpha);

                return new Overlay(255 - dark.FogColor.R, 255 - dark.FogColor.G, 255 - dark.FogColor.B, false, fogStrength);
            }
            return null;
        }

        private Overlay CombineLights(IEnumerable<Light> lights)
        {
            var numLights = lights.Count();
            if (numLights > 0)
            {
                var lightStrengths = new List<double>();
                var r = 0;
                var g = 0;
                var b = 0;
                foreach (var light in lights)
                {
                    lightStrengths.Add(light.LightStrength);
                    r += light.LightColor.R;
                    g += light.LightColor.G;
                    b += light.LightColor.B;
                }
                r = r/numLights;
                g = g/numLights;
                b = b/numLights;
                var maxStr = lightStrengths.Max();
                lightStrengths.Remove(maxStr);
                var str = Utilities.Constrain(maxStr + (lightStrengths.Sum()*.25), GameState.MinAlpha, GameState.MaxAlpha);
                return new Overlay(r, g, b, true, str);
            }
            return null;
        }
        #endregion
        #region Private Methods

        private void DrawOverlay(int x, int y, Overlay overlay)
        {
            if (overlay.AffectsColor)
            {
                var beadColor = PS.BeadColor(x, y);
                var newBeadColor = ApplyOverlay(overlay, beadColor);
                PS.BeadColor(x, y, newBeadColor);
            }
            if (overlay.AffectsBorder)
            {
                var borderColor = PS.BeadBorderColor(x, y);
                var newBorderColor = ApplyOverlay(overlay, borderColor);
                PS.BeadBorderColor(x, y, newBorderColor);
            }
            if (overlay.AffectsGlyph)
            {
                var glyphColor = PS.BeadGlyphColor(x, y);
                var newGlyphColor = ApplyOverlay(overlay, glyphColor);
                PS.BeadGlyphColor(x, y, newGlyphColor);
            }
            if (overlay.AffectsFlash)
            {
                var flashColor = PS.BeadFlashColor(x, y);
                var newFlashColor = ApplyOverlay(overlay, flashColor);
                PS.BeadFlashColor(x, y, newFlashColor);
            }
        }

        private static Color ApplyOverlay(IOverlay overlay, Color baseColor)
        {
            int newR = baseColor.R + Convert.ToInt32(overlay.R * overlay.Alpha);
            int newG = baseColor.G + Convert.ToInt32(overlay.G * overlay.Alpha);
            int newB = baseColor.B + Convert.ToInt32(overlay.B * overlay.Alpha);
            return new Color(newR, newG, newB);
        }
        #endregion
    }
}
