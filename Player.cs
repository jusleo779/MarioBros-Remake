using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//parts of the player is taken from what I learned from my course like the movement 
//parts of it was created based off what I know from my coding knowledge I have like the wall jumps
//little parts of it was helped by ChatGPT to help me fix some of my broken code because I was stuck and couldn't find the problem in my code (marked)
public class Player : MonoBehaviour
{
    //movement
    public int speed;
    public int runningSpeed;
    int currentSpeed;
    Rigidbody2D rb;
    float inputX;

    //character direction facing
    bool facingRight = true;
    int directionFacing;
    //allows me to make the animations change directions
    Animator anim;


    //checks if player is on ground to jump up/down
    bool onGround;
    public LayerMask whatIsTheGround;
    public Transform groundCheck;
    public float jumpPower;


    //wall Jump
    public BoxCollider2D[] wallHitbox;
    public BoxCollider2D playerHitbox;
    public int maxWallJumps;
    int wallJumpsLeft;
    public float horizontalJumpPower;
    bool isWallJumping;
    

    //dash
    bool isDashing;
    public int dashSpeed;
    public int maxDashes;
    int dashesLeft;

    //double jump
    int doubleJumpsLeft;
    public int maxDoubleJumps;

    //buy menu
    [HideInInspector] public bool inBuyMenu;


    GameManager manager;

    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentSpeed = speed;
        wallJumpsLeft = maxWallJumps;
        isWallJumping = false;
        manager = FindObjectOfType<GameManager>();
        inBuyMenu = false;
        doubleJumpsLeft = maxDoubleJumps;
    }

    // Update is called once per frame
    void Update()
    {
        //gets the direction of inputs (on the x-axis)
        inputX = Input.GetAxisRaw("Horizontal");
        onGround = Physics2D.OverlapCircle(groundCheck.position, 0.5f, whatIsTheGround);
        movementDirection();
        movementJump();
        sprinting();
        dash();
    }

    void FixedUpdate()//must use fix update because physics is involved with rigid body 
                      //physics apply when you move
    {
        //updates the characters movements
        if (!isWallJumping && !isDashing && !inBuyMenu)//ChatGPT helped me with this as my character couldnt do the wallJump because of the fixedUpdate method        
        {                                              //also taught me thatmovement outside of the fixedUpdate will be overrided which causes things like the wallJump to fail
                                                       //based off thiss knowledge I added the dashing to the checks to allow the character to do an ability movement
            rb.velocity = new Vector2(inputX * currentSpeed, rb.velocity.y);
        }

    }
    bool checkIfWallJumping()
    {
        for (int i = 0; i < wallHitbox.Length; i++)
        {
            if (playerHitbox.IsTouching(wallHitbox[i]))
            {
                return true;
            }
        }
        return false;
    }

    public void freeze()
    {
        inBuyMenu = true;
        rb.velocity = Vector2.zero;
    }

    public void unfreeze()
    {
        inBuyMenu = false;
    }

    void dash()
    {
        if(dashesLeft != 0 && manager.dashOn == true && !inBuyMenu)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isDashing = true;
                checkDirectionFacing();
                rb.velocity = new Vector2(directionFacing * dashSpeed, rb.velocity.y);
                Invoke("stopDashing", 0.1f);
                dashesLeft--;
            }
        }
        if(onGround)
        {
            dashesLeft = maxDashes;
        }
    }

    void stopDashing()
    {
        isDashing = false;
    }

    void sprinting()
    {
        //if your holding down shift key then you will sprint
        if (Input.GetKey(KeyCode.LeftShift))//for holding a key (GetKey) 
        {
            currentSpeed = runningSpeed;
        }
        else
        {
            currentSpeed = speed;
        }

    }
    void movementJump()
    {
        if(onGround)
        {
            wallJumpsLeft = maxWallJumps;
            doubleJumpsLeft = maxDoubleJumps;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !inBuyMenu)//for a single press (GetKeyDown)
        { 
            if (onGround)//jumping from when ur on the ground
            {
                rb.velocity = Vector2.up * jumpPower;
            }
            else if (doubleJumpsLeft != 0 && checkIfWallJumping() == false && manager.doubleJumpOn == true)
            {
                rb.velocity = Vector2.up * jumpPower;
                doubleJumpsLeft--;
            }
            //allows you to wall jump a specific amount of times
            else if (wallJumpsLeft != 0)
            {
                for (int i = 0; i < wallHitbox.Length; i++)
                {
                    if (playerHitbox.IsTouching(wallHitbox[i]))
                    {
                        checkDirectionFacing();
                        rb.velocity = new Vector2(-directionFacing * horizontalJumpPower, jumpPower);
                        wallJumpsLeft--;
                        isWallJumping = true;
                        Invoke("isWallJumpingOff", 0.2f);//plays method in 0.2 seconds
                        break;
                    }
                }

            }
            
        }
    }
    void checkDirectionFacing()
    {
        if (facingRight)//since there is no input of direction basing it off the direction facing
        {
            directionFacing = 1;//right
        }
        else
        {
            directionFacing = -1;//left
        }
    }

    void isWallJumpingOff()
    {
        isWallJumping = false;
    }

    void movementDirection()
    {
        //animations
        if (!inBuyMenu)
        {
            if (inputX == 0)
            {
                anim.SetBool("isRunning", false);
            }
            else
            {
                anim.SetBool("isRunning", true);
            }

            if (inputX == 1 && facingRight == false)//checks direction facing to flip character 
            {//quaternion works with rotations and euler makes it so you only work with x,y,z 
                transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
                facingRight = true;
            }
            else if (inputX == -1 && facingRight == true)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, 180f, transform.rotation.z);
                facingRight = false;
            }
        }
    }
}