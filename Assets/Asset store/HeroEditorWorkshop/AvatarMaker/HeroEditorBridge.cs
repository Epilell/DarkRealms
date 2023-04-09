using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.HeroEditorWorkshop.Common.Scripts;
using UnityEngine;

namespace Assets.HeroEditorWorkshop.AvatarMaker
{
    /// <summary>
    /// This script can be used to create avatars for characters made in Hero Editor 4D (Fantasy Heroes).
    /// Just save a character as JSON and then load it with this script to build an avatar.
    /// </summary>
    public class HeroEditorBridge : MonoBehaviour
    {
        public ItemMaker ItemMaker;

        #if UNITY_EDITOR

        public void Load()
        {
            var path = UnityEditor.EditorUtility.OpenFilePanel("Open character JSON", "", "json");
            
            if (path.Length != 0)
            {
                throw new Exception("Please add Newtonsoft.Json and uncomment 3 lines below.");

                //var json = File.ReadAllText(path);
                //var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                //Load(data["Hair"], data["Expression.Default.Eyebrows"], data["Expression.Default.Eyes"], data["Expression.Default.Mouth"], data["Body"], data["Ears"], data["Mask"], data["Helmet"]);
            }
        }

        #endif

        public void Load(string hair, string eyebrows, string eyes, string mouth, string head, string ears, string mask, string helmet)
        {
            Set(ItemMaker.SpriteGroups.Single(i => i.Name == "Hair"), hair);
            Set(ItemMaker.SpriteGroups.Single(i => i.Name == "Eyebrows"), eyebrows);
            Set(ItemMaker.SpriteGroups.Single(i => i.Name == "Eyes"), eyes);
            Set(ItemMaker.SpriteGroups.Single(i => i.Name == "Mouth"), mouth);
            Set(ItemMaker.SpriteGroups.Single(i => i.Name == "Head"), head);
            Set(ItemMaker.SpriteGroups.Single(i => i.Name == "Ears"), ears);
            Set(ItemMaker.SpriteGroups.Single(i => i.Name == "Mask"), mask);
            Set(ItemMaker.SpriteGroups.Single(i => i.Name == "Helmet"), helmet);

            if (hair != null && hair.Contains("HideEars") || helmet != null && !helmet.Contains("ShowEars"))
            {
                ItemMaker.SpriteGroups.Single(i => i.Name == "Ears").Image.enabled = false;
            }
        }

        private static void Set(SpriteGroup group, string serialized)
        {
            if (serialized == null)
            {
                group.Image.sprite = null;
            }
            else
            {
                var parts = serialized.Split('#');
                var id = parts[0];
                var color = Color.white;

                if (parts.Length > 1)
                {
                    ColorUtility.TryParseHtmlString("#" + parts[1], out color);
                }

                group.Image.sprite = group.Sprites.FirstOrDefault(i => i.name == id.Split('.').Last());

                if (group.Name == "Eyes" && group.Image.sprite != null)
                {
                    ItemMaker.PaintIgnoreGray(color, group.Image.sprite.texture, group.Image);
                }
                else
                {
                    group.Image.color = color;
                }
            }
           
            group.Image.enabled = group.Image.sprite != null;
        }
    }
}