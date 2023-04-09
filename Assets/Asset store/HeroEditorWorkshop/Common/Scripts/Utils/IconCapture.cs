using UnityEngine;

namespace Assets.HeroEditorWorkshop.Common.Scripts.Utils
{
    /// <summary>
    /// Used to create icons.
    /// </summary>
    public class IconCapture : MonoBehaviour
    {
        public Camera Camera;
        public SpriteRenderer SpriteRenderer;

        public Texture2D Capture(Texture2D texture, int width, int height, int scale, int rotation, bool flipX)
        {
            SpriteRenderer.sprite = TextureHelper.CreateSprite(texture);
            SpriteRenderer.transform.localScale = scale * Vector3.one;
            SpriteRenderer.transform.localEulerAngles = new Vector3(0, 0, rotation);
            SpriteRenderer.flipX = flipX;

            var cam = GetComponent<Camera>();
            var renderTexture = new RenderTexture(width, height, 24);
            var texture2D = new Texture2D(width, height, TextureFormat.ARGB32, false);

            cam.targetTexture = renderTexture;
            cam.Render();
            RenderTexture.active = renderTexture;
            texture2D.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            texture2D.Apply();
            cam.targetTexture = null;
            RenderTexture.active = null;
            Destroy(renderTexture);

            return texture2D;
        }
    }
}