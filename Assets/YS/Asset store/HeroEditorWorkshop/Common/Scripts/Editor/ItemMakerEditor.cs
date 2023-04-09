using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Assets.HeroEditorWorkshop.Common.Scripts.Editor
{
    /// <summary>
    /// Add [Refresh] button to ItemMaker script.
    /// </summary>
    [CustomEditor(typeof(ItemMaker))]
    public class ItemMakerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var itemMaker = (ItemMaker) target;

            if (GUILayout.Button("Refresh"))
            {
                Debug.ClearDeveloperConsole();

                foreach (var group in itemMaker.SpriteGroups)
                {
                    var root = AssetDatabase.GetAssetPath(group.SpriteFolder);
                    var files = Directory.GetFiles(root, "*.png", SearchOption.AllDirectories).ToList();

                    group.Sprites.Clear();

                    foreach (var path in files)
                    {
                        var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);

                        group.Sprites.Add(sprite);
                    }
                }

                EditorUtility.SetDirty(itemMaker);
            }
        }
    }
}