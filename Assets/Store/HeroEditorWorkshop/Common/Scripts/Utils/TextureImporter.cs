#if UNITY_EDITOR

using System;
using UnityEditor;
using UnityEngine;

namespace Assets.HeroEditorWorkshop.Common.Scripts.Utils
{
    /// <summary>
    /// Used to set texture import settings.
    /// </summary>
    public static class TextureImporter
    {
        public static void SetImportSettings(string path, SpriteImportMode importMode, SpriteMeshType meshType, Vector2 spritePivot, bool enableReadWrite, string size, FilterMode filterMode, TextureImporterCompression compression, bool crunchedCompression, int compressionQuality)
        {
            path = path.Replace("\\", "/");

            var texture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
            var targetImporter = (UnityEditor.TextureImporter) AssetImporter.GetAtPath(path);

            targetImporter.spriteImportMode = importMode == SpriteImportMode.None ? targetImporter.spriteImportMode : importMode;
            targetImporter.textureType = TextureImporterType.Sprite;
            targetImporter.spritePackingTag = null;
            targetImporter.alphaIsTransparency = true;
            targetImporter.isReadable = enableReadWrite;
            targetImporter.mipmapEnabled = false;
            targetImporter.wrapMode = TextureWrapMode.Clamp;
            targetImporter.filterMode = FilterMode.Bilinear;
            targetImporter.maxTextureSize = size == "[Auto]" ? GetMaxTextureSize(texture) : int.Parse(size);
            targetImporter.filterMode = filterMode;
            targetImporter.textureCompression = compression;
            targetImporter.compressionQuality = compressionQuality;
            targetImporter.crunchedCompression = crunchedCompression;
            
            var textureSettings = new TextureImporterSettings();

            targetImporter.ReadTextureSettings(textureSettings);

            textureSettings.spriteMeshType = meshType;
            textureSettings.spriteAlignment = (int) SpriteAlignment.Custom;
            textureSettings.spritePivot = spritePivot;
            textureSettings.spriteExtrude = 1;

            targetImporter.SetTextureSettings(textureSettings);
            targetImporter.SaveAndReimport();

            Debug.LogFormat("Import Settings set for: {0}", path);
        }

        private static int GetMaxTextureSize(Texture2D texture)
        {
            var maxTextureSize = Math.Max(texture.width, texture.height);

            for (var i = 5; i <= 11; i++)
            {
                var size = (int)Math.Pow(2, i);

                if (size >= maxTextureSize) return size;
            }

            return 2048;
        }
    }
}

#endif