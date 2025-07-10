using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class was created by me based of a similar idea from another project from course
public class Spike : MonoBehaviour
{
    
    public int damage;
    GameManager manager;

    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            manager.takeDamage(damage);
            Destroy(gameObject);
        }
    }
}
