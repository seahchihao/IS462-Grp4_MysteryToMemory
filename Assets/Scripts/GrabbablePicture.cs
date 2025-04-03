using UnityEngine;

public class GrabbablePicture : MonoBehaviour
{
    public PopupUI popupUI;  // Reference to the PopupUI script

    // Call this when the object is clicked
    private void OnMouseDown()
    {
        // Ensure the popupUI is assigned in the inspector
        if (popupUI != null)
        {
            // Convert the sprite to a Texture2D
            Texture2D texture = GetTextureFromSprite(GetComponent<SpriteRenderer>().sprite);

            // Show the popup with the texture and description
            popupUI.ShowPopup(texture, "Description of the picture");
        }
    }

    // Convert Sprite to Texture2D
    private Texture2D GetTextureFromSprite(Sprite sprite)
    {
        Texture2D texture = sprite.texture;
        Rect spriteRect = sprite.textureRect;

        // Create a new Texture2D with the sprite's size
        Texture2D newTexture = new Texture2D((int)spriteRect.width, (int)spriteRect.height);

        // Extract the pixels from the sprite's texture
        newTexture.SetPixels(sprite.texture.GetPixels((int)spriteRect.x, (int)spriteRect.y, (int)spriteRect.width, (int)spriteRect.height));

        // Apply the pixels to the new texture
        newTexture.Apply();

        return newTexture;
    }
}
