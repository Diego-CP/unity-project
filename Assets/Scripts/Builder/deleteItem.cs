using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class deleteItem : MonoBehaviour
{
    public int ID;
    private Level_Manager editor;
    private Scene currentScene;

    // Retrieve the name of this scene.
    string sceneName;

    
    void Start()
    {
        currentScene = SceneManager.GetActiveScene(); 
        editor = GameObject.FindGameObjectWithTag("Manager").GetComponent<Level_Manager>();
        sceneName = currentScene.name;

    }

    private void OnMouseOver()
    {
        if(sceneName == "Editor")
        {
            if (Input.GetMouseButtonDown(1) && this.gameObject.name.Contains("Player"))
            {
                Destroy(this.transform.parent.gameObject);
                editor.itemButtons[0].quantity++;
                editor.itemButtons[0].quantityText.text = editor.itemButtons[ID].quantity.ToString();
            }
                
            else if (Input.GetMouseButtonDown(1))
            {
                Destroy(this.gameObject);
                editor.itemButtons[ID].quantity++;
                editor.itemButtons[ID].quantityText.text = editor.itemButtons[ID].quantity.ToString();

            }
        }
        
    }


}
