using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terrainToggle : MonoBehaviour
{
    // Start is called before the first frame update
    public bool toggle = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void ButtonClicked()
    {
        toggle = !toggle;
    }

}
