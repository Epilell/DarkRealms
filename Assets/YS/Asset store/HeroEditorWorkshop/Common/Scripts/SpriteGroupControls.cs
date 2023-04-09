using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.HeroEditorWorkshop.Common.Scripts
{
    /// <summary>
    /// A set of controls for SpriteGroup.
    /// </summary>
    public class SpriteGroupControls : MonoBehaviour
    {
        public Text Name;
        public Button Prev;
        public Button Next;
        public Button Hide;
        public Button Paint;
        public Slider Hue;
        public Slider Saturation;
        public Slider Brightness;
        public Slider Alpha;
        public List<Button> ColorButtons;
        public Action<Color> OnSelectFixedColor;

        public void Start()
        {
            ColorButtons.ForEach(i => i.onClick.AddListener(() => SelectFixedColor(i.targetGraphic.color)));
        }

        public void SelectFixedColor(Color color)
        {
            OnSelectFixedColor?.Invoke(color);
        }
    }
}