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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            toggle = !toggle;
        }
    }

    // Update is called once per frame
    public void ButtonClicked()
    {
        toggle = !toggle;
    }

}
