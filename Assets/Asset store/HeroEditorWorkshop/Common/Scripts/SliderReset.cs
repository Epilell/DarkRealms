using UnityEngine;
using UnityEngine.UI;

namespace Assets.HeroEditorWorkshop.Common.Scripts
{
    /// <summary>
    /// Used to set a default value for Slider.
    /// </summary>
    public class SliderReset : MonoBehaviour
    {
        public float DefaultValue = 0;

        public void Reset()
        {
            GetComponentInParent<Slider>().value = DefaultValue;
        }
    }
}