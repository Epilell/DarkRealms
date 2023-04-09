using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Assets.HeroEditorWorkshop.Common.Scripts.Enums;
using Assets.HeroEditorWorkshop.Common.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR

using UnityEditor;

#endif

namespace Assets.HeroEditorWorkshop.Common.Scripts
{
    /// <summary>
    /// The main script to make item sprites.
    /// </summary>
    public class ItemMaker : MonoBehaviour
    {
        public string Id = "Item";
        public List<SpriteGroup> SpriteGroups;
        public Image Icon;
        public InputField NameInput;
        public IconCapture IconCapture;
        public IconCaptureParams IconCaptureParams;
        public bool Center;
        
        public void Start()
        {
            foreach (var group in SpriteGroups)
            {
                group.Index = group.Sprites.IndexOf(group.Image.sprite);
                
                if (group.Controls)
                {
                    group.Init();
                    group.Controls.Prev.onClick.AddListener(() => Switch(group, -1));
                    group.Controls.Next.onClick.AddListener(() => Switch(group, +1));
                    group.Controls.Hide.onClick.AddListener(() => Hide(group));
                    group.Controls.Paint.onClick.AddListener(() => Paint(group));
                    group.Controls.Hue.onValueChanged.AddListener(value => { Rebuild(group); UpdateIcon(); });
                    group.Controls.Saturation.onValueChanged.AddListener(value => { Rebuild(group); UpdateIcon(); });
                    group.Controls.Brightness.onValueChanged.AddListener(value => { Rebuild(group); UpdateIcon(); });
                    group.Controls.Alpha.value = group.Image.color.a;
                    group.Controls.Alpha.onValueChanged.AddListener(value => SetColor(group, new Color(group.Image.color.r, group.Image.color.g, group.Image.color.b, value)));
                    group.Controls.OnSelectFixedColor = color => SetColor(group, new Color(color.r, color.g, color.b, group.Image.color.a));
                }

                if (group.MaskedBy != "")
                {
                    group.Mask = SpriteGroups.SingleOrDefault(i => i.Name == group.MaskedBy);
                }
            }

            UpdateIcon();
        }

        private void SetColor(SpriteGroup group, Color color)
        {
            group.Image.color = color;

            foreach (var masked in SpriteGroups.Where(i => i.Mask == group))
            {
                Rebuild(masked);
            }

            UpdateIcon();
        }

        public void Hide(SpriteGroup group)
        {
            if (!group.CanBeEmpty)
            {
                Debug.LogWarning($"{group.Name} can't be empty.");
                return;
            }

            group.Image.enabled = !group.Image.enabled && group.Image.sprite != null;
            Rebuild(group);
            UpdateIcon();
        }

        public void Paint(SpriteGroup group)
        {
            #if UNITY_EDITOR

            ColorPicker.Open(group.Image.color);
            ColorPicker.OnValueChanged = (color, confirm) =>
            {
                if (group.PaintMode == PaintMode.IgnoreGray)
                {
                    var texture = group.Sprites[group.Index].texture;

                    if (confirm)
                    {
                        PaintIgnoreGray(color, texture, group.Image);
                    }
                    else
                    {
                        group.Image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100f);
                    }
                }
                else
                {
                    group.Image.color = color;
                }
            };

            #else

            throw new NotSupportedException("This feature is supported only inside Unity. You can use 3-rd party assets for other platforms, for example http://u3d.as/1cTo");

            #endif
        }

        public void Switch(SpriteGroup group, int direction)
        {
            group.Switch(direction);
            Rebuild(group);

            foreach (var linked in SpriteGroups.Where(i => i.LinkedTo == group.Name))
            {
                Switch(linked, direction);
            }

            foreach (var masked in SpriteGroups.Where(i => i.Mask == group))
            {
                Rebuild(masked);
            }

            UpdateIcon();
        }

        private void Rebuild(SpriteGroup group)
        {
            if (group.Index == -1) return;

            if (group.Controls == null)
            {
                group.Image.sprite = group.Sprites[group.Index];

                return;
            }

            var texture = group.GetTexture();

            group.Image.sprite = TextureHelper.CreateSprite(texture);
        }

        private void UpdateIcon()
        {
            if (Icon) Icon.sprite = TextureHelper.CreateSprite(Build(icon: true));
        }

        public static void PaintIgnoreGray(Color color, Texture2D source, Image image)
        {
            var pixels = source.GetPixels();

            for (var i = 0; i < pixels.Length; i++)
            {
                if (pixels[i].a <= 0) continue;
                if (Math.Abs(pixels[i].r - pixels[i].g) < 0.1 && Math.Abs(pixels[i].r - pixels[i].b) < 0.1) continue;

                Color.RGBToHSV(pixels[i], out _, out _, out var v);

                pixels[i] = new Color(color.r * v, color.g * v, color.b * v, color.a);
            }

            var texture = new Texture2D(source.width, source.height);

            texture.SetPixels(pixels);
            texture.Apply();
            image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100f);
        }

        public void Save(bool icon)
        {
            #if UNITY_EDITOR

            var path = EditorUtility.SaveFilePanel($"Save {Id} as PNG", "", $"{GetSpriteName()}.png", "png");

            if (path != "") Save(path, icon);
            
            #else

            throw new NotSupportedException("This feature is supported only inside Unity. You can use 3-rd party assets for other platforms, for example http://u3d.as/2QLg or http://u3d.as/2W52");

            #endif
        }

        private void Save(string path, bool icon)
        {
            var texture = Build(icon);
            var bytes = texture.EncodeToPNG();

            File.WriteAllBytes(path, bytes);
            Debug.Log($"Saved as {path}");
        }

        private Texture2D Build(bool icon)
        {
            var groups = SpriteGroups.Where(i => i.Image.gameObject.activeSelf && i.Image.enabled).ToList();
            var layers = groups.Select(i => i.GetPixels(finalize: true)).ToArray();
            var width = SpriteGroups[0].Sprites[0].texture.width;
            var height = SpriteGroups[0].Sprites[0].texture.height;
            var texture = TextureHelper.MergeLayers(width, height, layers);

            if (Center || icon)
            {
                texture = TextureHelper.Center(texture);
            }

            if (icon)
            {
                texture = IconCapture.Capture(texture, 192, 192, IconCaptureParams.Scale, IconCaptureParams.Rotation, IconCaptureParams.FlipX);
                texture = TextureHelper.AddShadow(texture, IconCaptureParams.Shadow, 100f / 255f);
            }

            return texture;
        }

        public void ExportToHeroEditor(string path)
        {
            #if UNITY_EDITOR

            string spritesFolder, iconsFolder;

            switch (Id)
            {
                case "Sword":
                    spritesFolder = $"{path}/Sprites/Equipment/MeleeWeapon1H/Workshop/Sword/";
                    iconsFolder = $"{path}/Icons/Equipment/MeleeWeapon1H/Workshop/Sword/";
                    break;
                case "Potion":
                case "Ring":
                case "Rune":
                {
                    if (!EditorUtility.DisplayDialog("Select target asset", "Where do you want to export?", "HeroEditor", "HeroEditor4D"))
                    {
                        path = path.Replace("/HeroEditor/", "/HeroEditor4D/");
                    }

                    spritesFolder = $"{path}/Sprites/Equipment/Supplies/Workshop/{Id}/";
                    iconsFolder = $"{path}/Icons/Equipment/Supplies/Workshop/{Id}/";

                    break;
                }
                default: throw new NotSupportedException(Id);
            }

            if (!Directory.Exists(path))
            {
                if (EditorUtility.DisplayDialog("Error", $"Target asset not found: {path}", "Download", "Cancel"))
                {
                    Application.OpenURL("https://assetstore.unity.com/publishers/11086");
                }

                return;
            }

            if (!Directory.Exists(spritesFolder)) Directory.CreateDirectory(spritesFolder);
            if (!Directory.Exists(iconsFolder)) Directory.CreateDirectory(iconsFolder);

            var spritePath = spritesFolder + GetSpriteName() + ".png";
            var iconPath = iconsFolder + GetSpriteName() + ".png";

            Save(spritePath, false);
            Save(iconPath, true);

            AssetDatabase.Refresh();

            var spritePivot = spritePath.Contains("/MeleeWeapon") ? new Vector2(0.5f, 0.25f) : new Vector2(0.5f, 0.5f);

            Utils.TextureImporter.SetImportSettings(spritePath, SpriteImportMode.Single, SpriteMeshType.Tight, spritePivot, false, "[Auto]", FilterMode.Bilinear, TextureImporterCompression.Compressed, true, 50);
            Utils.TextureImporter.SetImportSettings(iconPath, SpriteImportMode.Single, SpriteMeshType.FullRect, new Vector2(0.5f, 0.5f), false, "[Auto]", FilterMode.Bilinear, TextureImporterCompression.Compressed, true, 50);

            EditorUtility.DisplayDialog("Next steps", "Now select SpriteCollection and IconCollection in Project window and press [Refresh]. They are located in Assets/HeroEditor(4D)/FantasyHeroes/Resources.", "Yes, my lord!");
            
            #endif
        }

        private string GetSpriteName()
        {
            var spriteName = Regex.Replace(NameInput.text, "[^A-Za-z0-9_ ]", "");

            if (spriteName == "") spriteName = Id;

            return spriteName;
        }
    }

    [Serializable]
    public class IconCaptureParams
    {
        public int Scale;
        public int Rotation;
        public bool FlipX;
        public int Shadow;
    }
}