using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour 
{
    GameObject Fire;
    GameObject Wind;
    GameObject Lightning;

    int characterselect;

    private void Start()
    {
        characterselect = 1;
        Fire = GameObject.Find("Fire");
        Wind = GameObject.Find("Wind");
        Lightning = GameObject.Find("Lightning");
        
    }

    private void Update()
    {
        
    }
}

