**Disclaimer:**

I coded almost everything in the game, from the player to the game manager code. Some of the code used in the player method is reused from a previous project from a course called "Game Dev Rockets". Things like movement, changing the direction the character is facing, and checking if the player is on the ground. However, aside from that, the player code was developed by me with minimal use of ChatGPT to resolve some minor issues. But besides the player code, everything was self-made with only the basic knowledge I learned from 
the Game Development Course. It's my first personal project in Unity.

**Game Explanation:**

- Black Market(market to buy abilities)
  - Dashes(Short Burst of Horizontal Movement) (Use key "E" to dash when ability is bought)
  - Double Jump(When in the air can jump again)
- Wall Jumps
- Sprinting
- Pushable Blocks
- Spikes (obstacle that needs to be dodged)
- Health system
- Lose/Win Screen
- Moving Platforms

**Class Explantions:**

**Player class**

- manages movement
   - direction the character is facing
   - movement from left to right
   - jumping logic (checks if on ground, wall jumping, double jump(if unlocked))
   - Abilities(locked until bought)
   - Sprinting

**Game Manager**

- manages almost every interaction in the game
    - manages health (damage taken)
    - lose/win screen
    - manages coin system (keeps track of coins / turns on abilities when bought)
    - manages on screen (buy menu, text u can or can't see, healthbar, coins, etc.)
    - manages restarting the game
    

The **other classes** like Pushable block, Spike, and Door, are simple classes that I put on the objects. These classes are used to check if you collide with the object to send to the game manager, which allows the game manager to update the events happening in the game.

**Challenge In the Project:**

Simplified/General Explanation:

-  players movement
  - fixedUpdate() overrided movement, causing premade movement from wall jump to fail
  - created a bool to prevent overriding of premade movement
  - Fixed the timing of premade movement with Inovke() (calls a method after a specific amount of time)

Detailed Version:

I had challenges with the inputs from players. The player's movement from left to right is put in the fixedUpdate method. This method made it so that it only runs when you press a key from left to right. This made it hard because the course never taught me the meaning. This challenged me to find a way to prevent the input movements from the player from overriding the premade movements in the wall jump. When a wall jump fix was found, I still couldn't move my character because the boolean was keeping my player from moving. Then I found a solution to make sure the movement happens, but I had a problem with the timing. It would do the action for one frame, and then after that, it wouldn't work, which prevented the character from doing the movement. The timing was fixed by the Invoke(), which allows me to call a method after some time to turn off the pre-planned movement while also allowing to player to move after a short period. 


**Relations to EE:**

- Taught Debugging
   - similar to EE bc developed problem-solving skills to debug hardware
   - also increased mental fortitude when solving hard problems
- Testing
   - never gonna get the hardware perfect on first try like code 
   - teaches you to have multiple testing stages before finishing
   - prevents mistakes from being in the final product
- Embedded Systems Thinking
   - Embedded Systems thinking is how different parts influence each other
   - similar to how code influences how the game works/ how it overrides other code (similar to challenge above)

