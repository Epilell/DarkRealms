using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.HeroEditorWorkshop.Common.Scripts
{
    public class ItemMakerSwitch : MonoBehaviour
    {
        public List<GameObject> ItemMakers;

        #if UNITY_WEBGL

        public void Start()
        {
            foreach (var button in FindObjectsOfType<Button>(true))
            {
                if (button.name.Contains("Export") || button.name.Contains("Save") || button.name.Contains("Load"))
                {
                    button.interactable = false;
                }
            }
        }

        #endif

        public void Open(int index)
        {
            for (var i = 0; i < ItemMakers.Count; i++)
            {
                ItemMakers[i].SetActive(i == index);
            }
        }

        public void Navigate(string url)
        {
            Application.OpenURL(url);
        }
    }
}