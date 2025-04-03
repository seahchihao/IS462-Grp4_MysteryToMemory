using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupUI : MonoBehaviour
{
    public Button closeButton;       // Reference to the close button
    public GameObject popupWindow;   // Reference to the pop-up window
    public Image enlargedPicture;    // Reference to the Image UI element that shows the enlarged picture
    public TMP_Text descriptionText;   // Reference to the Text UI element for the description

    void Start()
    {
        // Initially hide the popup window
        popupWindow.SetActive(false);

        // Add a listener to the close button
        closeButton.onClick.AddListener(CloseWindow);
    }

    // Function to display the pop-up window with an image and description
    public void ShowPopup(Texture2D picture, string description)
    {
        enlargedPicture.sprite = Sprite.Create(picture, new Rect(0.0f, 0.0f, picture.width, picture.height), new Vector2(0.5f, 0.5f));
        descriptionText.text = description;

        // Show the popup window
        popupWindow.SetActive(true);
    }

    // Function to close the pop-up window
    void CloseWindow()
    {
        popupWindow.SetActive(false);
    }
}
