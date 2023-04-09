using System;
using System.Collections.Generic;
using Assets.HeroEditorWorkshop.Common.Scripts.Enums;
using Assets.HeroEditorWorkshop.Common.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.HeroEditorWorkshop.Common.Scripts
{
    /// <summary>
    /// Sprite group representation.
    /// </summary>
    [Serializable]
    public class SpriteGroup
    {
        public string Name;
        public UnityEngine.Object SpriteFolder;
        public List<Sprite> Sprites;
        public Image Image;
        public SpriteGroupControls Controls;
        public int Index;
        public bool CanBeEmpty = true;

        [Header("Mask")]
        public string MaskedBy;
        public BlendMode BlendMode;

        [Header("Other")]
        public string LinkedTo;
        public PaintMode PaintMode;

        public SpriteGroup Mask { set; get; }

        public void Init()
        {
            Controls.Name.text = Index == -1 ? "None" : Sprites[Index].name;
        }

        public void Switch(int direction)
        {
            Index += direction;

            var min = CanBeEmpty ? -1 : 0;

            if (Index < min) Index = Sprites.Count - 1;
            if (Index == Sprites.Count) Index = min;

            Image.sprite = Index == -1 ? null : Sprites[Index];
            Image.enabled = Index != -1;

            if (Controls) Controls.Name.text = Index == -1 ? "None" : Sprites[Index].name;
        }

        public Color[] GetPixels(bool finalize = false)
        {
            var pixels = Sprites[Index].texture.GetPixels();

            if (Mask != null && Mask.Index != -1)
            {
                var mask = Mask.GetPixels(finalize: true);

                for (var i = 0; i < pixels.Length; i++)
                {
                    switch (BlendMode)
                    {
                        case BlendMode.None: pixels[i].a = mask[i].a; break;
                        case BlendMode.Multiply: pixels[i] = BlendMultiply(mask[i] * pixels[i], pixels[i]); break;
                        case BlendMode.Overlay: pixels[i] = BlendOverlay(mask[i], pixels[i]); break;
                        case BlendMode.Ornament: pixels[i] = BlendPattern(mask[i], pixels[i]); break;
                        default: throw new NotSupportedException();
                    }
                }
            }

            if (Controls)
            {
                var h = Controls.Hue.value;
                var s = Controls.Saturation.value;
                var v = Controls.Brightness.value;

                for (var i = 0; i < pixels.Length; i++)
                {
                    if (pixels[i].a <= 0) continue;

                    pixels[i] = TextureHelper.AdjustColor(pixels[i], h, s, v);

                    if (finalize) pixels[i] *= Image.color;
                }
            }

            return pixels;
        }

        public Texture2D GetTexture()
        {
            var pixels = GetPixels();
            var texture = new Texture2D(Sprites[0].texture.width, Sprites[0].texture.height);

            texture.SetPixels(pixels);
            texture.Apply();

            return texture;
        }

        private Color BlendMultiply(Color background, Color color)
        {
            if (background.a == 0 && color.a == 0) return Color.clear;

            var result = background * color;

            result.a = background.a * color.a;

            return result;
        }

        private Color BlendOverlay(Color background, Color color)
        {
            if (background.a == 0 && color.a == 0) return Color.clear;

            Color.RGBToHSV(background, out _, out _, out var v);

            var result = v < 0.5f
                ? 2 * background * color
                : Color.white - 2 * (Color.white - background) * (Color.white - color);

            result.a = background.a * color.a;

            return result;
        }

        private Color BlendPattern(Color background, Color color)
        {
            if (background.a == 0 && color.a == 0) return Color.clear;

            Color.RGBToHSV(background, out var h, out var s, out var v);

            var result = v < 0.7f ? Color.HSVToRGB(h, s, v - 0.25f) : Color.HSVToRGB(h, s, v + 0.25f);
            //var result = Color.HSVToRGB(h, s, v - 0.25f);
            
            result.a = background.a < 1 ? 0 : color.a;

            return result;
        }
    }
}