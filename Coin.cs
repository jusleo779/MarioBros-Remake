using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//everything is self created and created this based off what I learned
public class Coin : MonoBehaviour
{
    [HideInInspector] public bool touchedCoin;
    GameManager manager;
    
    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        touchedCoin = false;
        manager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.tag == "Player" && touchedCoin == false)
        {
            touchedCoin = true;
            manager.collectCoin();
            Destroy(gameObject);
        }
    }
}
