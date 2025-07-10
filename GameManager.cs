using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//everything here was created by me and used what I learned from my course
//ex the losing health/healthbar was taught by course in which i added to the game
public class GameManager : MonoBehaviour
{
    //player object
    public Player player;

    //Screen
    public GameObject loseScreen;
    public GameObject winScreen;

    //healthbar
    public GameObject healthBarImage;
    public int health;
    public Slider healthBar;

    //coin manager
    public TMP_Text coinCounterText;
    int currentAmtofCoins = 0;

    //Buy Menu
    public TMP_Text buyText;
    bool buyMenuActive;
    public GameObject buyScreen;
   // public GameObject NotEnoughtextDisable; (this is an addon if i want/ isnt necessary but could make the buy menu look better)
    public GameObject[] buttons;
    bool[] buttonsDisable;
    int[] price;

    //dash
    [HideInInspector] public bool dashOn;
    //Double jump
    [HideInInspector] public bool doubleJumpOn;

    //lets me know the game is over
    bool gameOver;

    //Dash text
    public GameObject dashText;

    Door door;

    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        gameOver = false;
        buyMenuActive = false;
        buyMenuActive = false;
        dashOn = false;
        buttonsDisable = new bool[buttons.Length];
        price = new int[buttons.Length];
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
            buttonsDisable[i] = false;
            price[i] = 2 * (i + 1);
        }
        door = FindObjectOfType<Door>();
    }

    // Update is called once per frame
    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.R) && gameOver)
        {
         SceneManager.LoadScene("Level1");
        }
        else if(Input.GetKeyDown(KeyCode.B))
        {
            buyMenuActive = !buyMenuActive;
            checkCoins();
            buyMenu();  
        }
        if (door.isTouchingDoor)
        {
            Destroy(player.gameObject);
            winScreen.SetActive(true);
            healthBarImage.SetActive(false);
            coinCounterText.text = " ";
            gameOver = true;
        }
    }



    public void DoubleJumpAbilityOn()
    {
        if(currentAmtofCoins >= 4)
        {
            doubleJumpOn = true;
            buttons[1].SetActive(false);
            currentAmtofCoins -= 4;
            coinCounterText.text = "Coins: " + currentAmtofCoins.ToString();
            buttonsDisable[1] = true;
            Destroy(buttons[1].gameObject);
        }
        
    }
    
    public void DashAbilityOn()
    {
        if(currentAmtofCoins >= 2)
        {
            dashOn = true;
            buttons[0].SetActive(false);
            currentAmtofCoins -= 2;
            coinCounterText.text = "Coins: " + currentAmtofCoins.ToString();
            buttonsDisable[0] = true;
            dashText.SetActive(true);
            Destroy(buttons[0].gameObject);
        }     
    }

    void buyMenu()
    {
        if(buyMenuActive)
        {
            healthBarImage.SetActive(false);
            buyText.text = "Press B to Exit";
            buyScreen.SetActive(true);
            coinCounterText.text = "Coins: " + currentAmtofCoins.ToString();
            checkCoins();
            player.freeze();
        }
        else
        {
            healthBarImage.SetActive(true);
            buyText.text = "Press B to Buy";
            buyScreen.SetActive(false);
            player.unfreeze();
        }
    }

    void checkCoins()//fix check coins so that the button for the double jump ability can be seen
    { 
        for(int i = 0; i < buttons.Length; i++)
        {
            if(currentAmtofCoins >= price[i] && buttonsDisable[i] != true)
            {
                 buttons[i].SetActive(true);
            }
            else if (buttonsDisable[i] != true)
            {
                buttons[i].SetActive(false);
            }
               
        }
      
    }
    public void takeDamage(int damage)
    {
        health -= damage;
        healthBar.value = health;
        if (health <= 0)
        {
            Destroy(player.gameObject);
            loseScreen.SetActive(true);
            healthBarImage.SetActive(false);
            coinCounterText.text = " ";
            gameOver = true;
        }
    }
    public void collectCoin()
    {
        currentAmtofCoins++;
        coinCounterText.text = "Coins: " + currentAmtofCoins.ToString();
    }

     void OnTriggerEnter2D(Collider2D other)
     {
        if(other.tag == "Player")
        {
            Destroy(player.gameObject);
            loseScreen.SetActive(true);
            healthBarImage.SetActive(false);
            coinCounterText.text = " ";
            gameOver = true;
        }
     }
}
