using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//everything was self-made with a little help from the internet to find the  .OverlapPoint (never learned it)
public class Pushableblock : MonoBehaviour
{
    public float speed;
    public Transform[] pushingPoints;
    public BoxCollider2D player;
    
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
       
               
    }
    void FixedUpdate()//makes it update on interaction
    {
        if (player.OverlapPoint(pushingPoints[0].position))
        {
            transform.position += Vector3.right * speed;
        }
        else if (player.OverlapPoint(pushingPoints[1].position))
        {
            transform.position += Vector3.left * speed;
        }
    }
}
