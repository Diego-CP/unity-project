using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    // Do not destroy the object that has this script
    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }
}
