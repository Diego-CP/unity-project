using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    // All floatingText object prefabs fall under textContainer
    public GameObject textContainer;
    // Specific objects defined by a prefab
    public GameObject textPrefab;
    // List of floating text objects
    //  Instead of creating a new FloatingText object every time one is needed, we utilize
    //  the same objects and change their parameters
    private List<FloatingText> floatingTexts = new List<FloatingText>();

    // Update the floating text
    private void Update() {
        // Update every floating text in the array every frame
        foreach(FloatingText txt in floatingTexts)
            txt.UpdateFloatingText();
    }

    private void Start()
    {
        textContainer = this.gameObject;
    }

    // Function to show the text with th given parameters
    public void Show(string message, int fontSize, Color color, Vector3 position, Vector3 motion, float duration) {
        // Get a FloatingText object
        FloatingText floatingText = GetFloatingText();

        // Set all variables as the parameters
        floatingText.txt.text = message;
        floatingText.txt.fontSize = fontSize;
        floatingText.txt.color = color;
        // This requires a transformation from world space (in-scene for game) to screen space (in-screen for UI)
        floatingText.go.transform.position = Camera.main.WorldToScreenPoint(position);
        floatingText.motion = motion;
        floatingText.duration = duration;

        // Show the floating text on-screen
        floatingText.Show();
    }

    // Function to get a FloatingText object from the floatingTexts list
    private FloatingText GetFloatingText() {
        // Get the first FloatingText object that is not active in the list
        FloatingText txt = floatingTexts.Find(t => !t.active);

        // If an object that isn't active is not found, we create a new one
        if (txt == null) {
            // Create new FloatingText object
            txt = new FloatingText();
            // Create a new game object and assign it to txt.go 
            txt.go = Instantiate(textPrefab);
            // Set the parent of the new game object as the transform of the textContainer
            txt.go.transform.SetParent(textContainer.transform);
            // Set the text of txt as the Text of the game object component
            txt.txt = txt.go.GetComponent<Text>();

            // Add the new object to the list
            floatingTexts.Add(txt);
        }

        return txt;

    }
}
