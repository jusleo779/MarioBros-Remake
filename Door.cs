using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Door : MonoBehaviour
{
    [HideInInspector] public bool isTouchingDoor;
    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        isTouchingDoor = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
           isTouchingDoor = true;
        }
    }
}
