using System.Linq;
using UnityEngine;

namespace Assets.HeroEditorWorkshop.Common.Scripts
{
    /// <summary>
    /// Used to set select random parts for demo recording.
    /// </summary>
    public class Randomizer : MonoBehaviour
    {
        public void Start()
        {
            InvokeRepeating("Rand", 0, 0.5f);
        }

        private void Rand()
        {
            var itemMaker = GetComponent<ItemMaker>();

            foreach (var group in itemMaker.SpriteGroups)
            {
                group.Index = Random.Range(group.CanBeEmpty ? -1 : 0, group.Sprites.Count);

                if (group.Name == "Dirt" && Random.Range(0, 2) == 0) group.Index = -1;

                itemMaker.Switch(group, 0);

                var colors = group.Controls.ColorButtons.Where(i => i.gameObject.activeSelf).ToList();

                if (colors.Count > 0)
                {
                    colors[Random.Range(0, colors.Count)].onClick.Invoke();
                }
            }
        }
    }
}