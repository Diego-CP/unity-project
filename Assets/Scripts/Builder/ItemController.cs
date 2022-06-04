using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemController : MonoBehaviour
{
    // Start is called before the first frame update

    public int ID;
    public int quantity;
    public TextMeshProUGUI quantityText;
    public bool clicked = false;
    public bool limited = false;
    private Level_Manager editor;

    void Start()
    {

        quantityText.text = quantity.ToString();
        editor = GameObject.FindGameObjectWithTag("Manager").GetComponent<Level_Manager>();

        
    }

    public void ButtonClicked()
    {
        if(quantity > 0 && limited)
        {
            Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

            clicked = true;


            Debug.Log("limited");
            quantity--;
            quantityText.text = quantity.ToString();
            editor.currentButton = ID;
        }
        else if(!limited)
        {
            Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
            Debug.Log("unlimited");
            clicked = true;
            editor.currentButton = ID;

        }
       
    }

    
}
