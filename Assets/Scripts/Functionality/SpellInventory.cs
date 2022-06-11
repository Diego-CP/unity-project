using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellInventory : MonoBehaviour {
    public bool[] isFull;
    public GameObject[] slots;
    public GameObject[] uiSlots;
    public GameObject ui;

    private void Start()
    {
        Invoke("getPlayer",0.01f);
    }
    // Method to add a spell to the inventory
    public void AddSpell(GameObject newSpell) {
        
          
            for (int i = 0; i < slots.Length; i++)
            {
                // Check if the current slot is full
                if (!isFull[i])
                {
                    // Item can be added to inventory
                    isFull[i] = true;
                    slots[i] = newSpell;
                    AddImage(newSpell, i);
                    break;
                }
            }
            // Pass through the inventory
        


    }

    private void getPlayer()
    {
        if (GameObject.Find("UI") != null && GameObject.Find("Player") != null)
        {
            
            ui = GameManager.instance.ui;
            uiSlots[0] = GameManager.instance.si.uiSlots[0];
            uiSlots[1] = GameManager.instance.si.uiSlots[1];
            slots[0] = GameManager.instance.si.slots[0];
            slots[1] = GameManager.instance.si.slots[1];
        }
    }

    private void Update()
    {
        
        if (ui == null)
        {
            getPlayer();
        }
    }

    // Method to remove a spell from the inventory
    public void RemoveSpell(int index) {

       
            // Set the slot as empty
            isFull[index] = false;
            // Remove the image from the UI
            RemoveImage(index);
        
            
        
    }

    // Method to add an image of the spell in the correct inventory slot
    public void AddImage(GameObject image, int index) {

        
            image.layer = 5;
            image.transform.parent = uiSlots[index].transform;
            image.transform.position = uiSlots[index].transform.position;
        
     
            // Instantiate the image at the inventory slot position and clarify that
            //  we do not want to use world coordinates
            
        
    }

    // Method to remove the image of the spell in the correct inventory slot
    public void RemoveImage(int index) {

     
            // Delete all children of the ui slot
            for (var i = uiSlots[index].transform.childCount - 1; i >= 0; i--)
            {
                Object.Destroy(uiSlots[index].transform.GetChild(i).gameObject);
            }
        
        
            
        
    }
}
