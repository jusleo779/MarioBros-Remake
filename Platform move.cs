using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platformmove : MonoBehaviour
{
    public float speed;
    public float timeBetweenDChange;//how long until it goes back the other direction
    float nextTimeDChange;
    bool movingRight;
    // Start is called before the first frame update
    void Start()
    {
        movingRight = true;
        nextTimeDChange = timeBetweenDChange;
    }

    // Update is called once per frame
    void Update()
    {
        movePlatform();
    }

    void movePlatform()
    {
        Check();
        if (Time.time <= nextTimeDChange && movingRight)
        {
            transform.position += Vector3.right * speed;
            
        }
        else if (Time.time <= nextTimeDChange && !movingRight)
        {
            transform.position += Vector3.left * speed;
        }

    }
    void Check()
    {
        if (Time.time >= nextTimeDChange)
        {
            nextTimeDChange = Time.time + timeBetweenDChange;
            movingRight = !movingRight;
        }
    }
}
