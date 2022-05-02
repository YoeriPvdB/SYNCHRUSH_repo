using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;


public class PlayerSwitch : MonoBehaviour
{
    public PlayerChoice playerChoice;

    bool canJump, canSlow;
    public bool inTandem;

    //public float switchTime;

    PlayerMovement moveScript;

    int joystickCount, modeSelect = 0, modeCount = 0;

    public string p1JumpButton, p2JumpButton;

    public UIScript uiScript;

    GameObject[] turnSwitches;

    RythmScript _rhythmScript;

    SpriteRenderer redSprite, blueSprite;

    Color red, blue, pink, purple;

    public int checkPointCount = 1, nextCount = 0;

    public float counter = 0, previousTime;



    public List<float> Times = new List<float>();

    //public float[] Times;

    public float[] reqCount = {13f, 19f, 13f, 10f};

    public AudioScript _audioScript;


    private void Start()
    {
        

        moveScript = gameObject.GetComponent<PlayerMovement>();
       

        redSprite = GameObject.Find("Red Player Sprite").GetComponent<SpriteRenderer>();
        blueSprite = GameObject.Find("Blue Player Sprite").GetComponent<SpriteRenderer>();

        turnSwitches = GameObject.FindGameObjectsWithTag("TurnSwitch");

        //Get Joystick Names
        string[] temp = Input.GetJoystickNames();

        //Check whether array contains anything
        if (temp.Length > 0)
        {
            //Iterate over every element
            for (int i = 0; i < temp.Length; ++i)
            {
                //Check if the string is empty or not
                if (!string.IsNullOrEmpty(temp[i]))
                {
                    //Not empty, controller temp[i] is connected
                    Debug.Log("Controller " + i + " is connected using: " + temp[i]);
                    joystickCount++;
                
                }
                else
                {
                    //If it is empty, controller i is disconnected
                    //where i indicates the controller number
                    Debug.Log("Controller: " + i + " is disconnected.");

                }
            }
        }

        Debug.Log(joystickCount);

        if(joystickCount <= 1)
        {
            p1JumpButton = "Jump";
            p2JumpButton = "Fire1";
        } else
        {
            p1JumpButton = "P1Jump";
            p2JumpButton = "P2Jump";
        }

        inTandem = true;

        canJump = true;

        playerChoice = PlayerChoice.Player1;

        pink = new Color(245, 175, 234, 150);
        red = new Color(231, 38, 38, 150);
        blue = new Color(28, 264, 233, 150);


        
    }

    private void Update()
    {
        switch (playerChoice)
        {
            case PlayerChoice.Player1:

                blueSprite.enabled = true;
                redSprite.enabled = false;
                //uiScript.swapSlider.fillRect.GetComponentInChildren<Image>().color = Color.blue;
                //uiScript.botSlider.fillRect.GetComponentInChildren<Image>().color = Color.red;

                if (Input.GetButtonDown(p1JumpButton) && canJump)
                {
                    moveScript.isJumping = true;

                    _audioScript.JumpAudio();

                    if (inTandem)
                    {
                        StartCoroutine("TandemSwitch");
                    }
                }



                if (Input.GetButtonDown(p2JumpButton) && GetComponent<BlastScript>().canBlast && inTandem == false)
                {
                    _audioScript.BlastAudio();
                    GetComponent<BlastScript>().StartCoroutine("Blast");
                    
                }

                foreach (GameObject turnSwitch in turnSwitches)
                {
                    turnSwitch.GetComponent<SpriteRenderer>().color = Color.red;
                }
                
                break;

            case PlayerChoice.Player2:

                blueSprite.enabled = false;
                redSprite.enabled = true;
                //uiScript.swapSlider.fillRect.GetComponentInChildren<Image>().color = Color.red;
                //uiScript.botSlider.fillRect.GetComponentInChildren<Image>().color = Color.blue;

                if (Input.GetButtonDown(p2JumpButton) && canJump)
                {
                    moveScript.isJumping = true;

                    _audioScript.JumpAudio();

                    if (inTandem)
                    {
                        StartCoroutine("TandemSwitch");
                    }

                }

                

                if(Input.GetButtonDown(p1JumpButton) && GetComponent<BlastScript>().canBlast && inTandem == false)
                {
                    _audioScript.BlastAudio();
                    GetComponent<BlastScript>().StartCoroutine("Blast");
                }



                foreach (GameObject turnSwitch in turnSwitches)
                {
                    turnSwitch.GetComponent<SpriteRenderer>().color = Color.blue;
                }

                break;
        }

        counter = uiScript.leveltime - previousTime;
    }

    IEnumerator TandemSwitch()
    {
        canJump = false;
        float switchTime = 0.01f;

        yield return new WaitForSecondsRealtime(switchTime);
        
        canJump = true;

        if(playerChoice == PlayerChoice.Player1)
        {
            playerChoice = PlayerChoice.Player2;
        } else
        {
            playerChoice = PlayerChoice.Player1;
        }
    }

    /*IEnumerator LongSwitch()
    {
        if(inTandem == false)
        {
            float switchTime = Random.Range(7f, 10f);
            uiScript.isCounting = true;
            uiScript.cdNum = switchTime;
           
            if (inTandem == false)
            {
                yield return new WaitForSecondsRealtime(switchTime);
            }
            else
            {
                yield return null;
            }

            uiScript.isCounting = false;

            
            moveScript. SlowMo();
            //yield return new WaitForSecondsRealtime(0.01f);
            //canSlow = false;

            if (inTandem == false)
            {
                if (playerChoice == PlayerChoice.Player1)
                {
                    playerChoice = PlayerChoice.Player2;
                }
                else
                {
                    playerChoice = PlayerChoice.Player1;
                }
                
                StartCoroutine("LongSwitch");
            } else
            {
                yield return null;
            }

            
        } else
        {
            yield return null;
        }
    }*/

    

    public enum PlayerChoice
    {
        Player1,
        Player2
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ModeSwitch")
        {
            Text rushText;
            //int nextCheckPoint = 1;




            Times.Add(counter);

            if (Times[nextCount] <= reqCount[nextCount])
            {
                // nextCheckPoint++;


                if (checkPointCount == 1)
                {
                    
                    rushText = uiScript.R;
                    //nextCheckPoint = 1;
                    uiScript.StartCoroutine("Rush", rushText);
                    //GetComponent<MenuScript>().gameRushText.Add(rushText);

                    

                }

                if (checkPointCount == 2)
                {
                    
                    rushText = uiScript.U;
                    //nextCheckPoint = 1;
                    uiScript.StartCoroutine("Rush", rushText);
                    
                    //GetComponent<MenuScript>().gameRushText.Add(rushText);
                }

                if (checkPointCount == 3)
                {
                    rushText = uiScript.S;
                    //nextCheckPoint = 1;
                    uiScript.StartCoroutine("Rush", rushText);
                    
                    //GetComponent<MenuScript>().gameRushText.Add(rushText);
                }

                if (checkPointCount == 4)
                {
                    rushText = uiScript.H;
                    //nextCheckPoint = 1;
                    uiScript.StartCoroutine("Rush", rushText);
                    //GetComponent<MenuScript>().gameRushText.Add(rushText);
                }



            }

            if (checkPointCount == 1)
            {
                playerChoice = PlayerChoice.Player1;
                inTandem = false;
                //_audioScript.ambienceTrigger.value = 0.9f;
                GameObject.Find("Ambience 1").GetComponent<StudioEventEmitter>().enabled = false;
                GameObject.Find("Ambience 2").GetComponent<StudioEventEmitter>().enabled = true;
                _audioScript.Music3();
            }

            if(checkPointCount == 2)
            {
                playerChoice = PlayerChoice.Player2;
                _audioScript.Music3(); 
            }

            if(checkPointCount == 3)
            {
                inTandem = true;
                GameObject.Find("Ambience 1").GetComponent<StudioEventEmitter>().enabled = true;
                GameObject.Find("Ambience 2").GetComponent<StudioEventEmitter>().enabled = false;
                _audioScript.Music4();
                //_audioScript.ambienceTrigger.value = 0.1f;
            }

            //checkPointCount = nextCheckPoint;

            counter = 0;

            checkPointCount++;
            nextCount++;

            for (int i = 0; i <= Times.Count - 1; i++)
            {
                if(i >= Times.Count - 1)
                {
                    previousTime += Times[i];
                }
                
            }
            
            
            //checkPointCount += 1;
            Debug.Log(checkPointCount + " " + uiScript.leveltime);

            /*if(!inTandem)
            {
                inTandem = true;
                
                //_rhythmScript.rhythmStuff.SetActive(false);

            } else
            {
                inTandem = false;
                if (modeCount == 0)
                {
                    playerChoice = PlayerChoice.Player1;
                    modeCount = 1;
                }
                else
                {
                    playerChoice = PlayerChoice.Player2;
                }
            }*/
            //uiScript.StartCoroutine("ModeSwap");

            

            //Debug.Log("next: " + nextCheckPoint);
            return;

            
        }

        

        if(collision.gameObject.tag == "TurnSwitch")
        {
            if (inTandem == false)
            {
                if (playerChoice == PlayerChoice.Player1)
                {
                    collision.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                    playerChoice = PlayerChoice.Player2;
                    
                }
                else
                {
                    collision.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                    playerChoice = PlayerChoice.Player1;
                    
                }
            }

            
        }
    }
    

}
