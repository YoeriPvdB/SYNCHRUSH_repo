using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D playerRB;

    public float speed, maxSpeed, maxJumpVelocity, jumpForce, maxJumpDist, slowdownLength, slowdownFactor;

    public bool isGrounded, isFalling = false, hasChecked = false, isJumping = false, isPaused = false, isDashing = false;

    public int jumpCount, lifeCount;

    PlayerSwitch switchScript;

    public Vector2 currentVelocity, previousVelocity, startYPos;

    public string jumpButton;

    TrailRenderer trail;

    public PlayerPositions playerPositions;

    public AudioScript _audioScript;


    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        startYPos = playerRB.position;
        trail = GetComponent<TrailRenderer>();
        switchScript = GetComponent<PlayerSwitch>();

        playerPositions = PlayerPositions.Bot;

        speed = 0;
    }

    

    private void Update()
    {
        RenderTrail();

        Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;

        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);

        if (Time.timeScale >= 0.99f)
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;
        }

        if (Input.GetKeyDown(KeyCode.F) && GameObject.Find("UI Handler").GetComponent<UIScript>().timeText.enabled == true)
        {
            if(isPaused == false)
            {
                speed = 1;
                isPaused = true; 
            } else
            {
                speed = 1300f;
                isPaused = false;
            }
             
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Game Scene");
        }

        switch(playerPositions)
        {
            case PlayerPositions.Bot:

                /*if (isJumping == true)
                {
                    //transform.position = new Vector2(transform.position.x, GameObject.Find("Top").transform.position.y - 1f);
                    playerRB.gravityScale = 0f;
                }
                else
                {
                    playerRB.gravityScale = 1f;
                }*/

                
                break;

            case PlayerPositions.Top:

                if(isJumping == false && speed < GetComponent<PowerUp>().newSpeed)
                {
                    //transform.position = new Vector2(transform.position.x, GameObject.Find("Top").transform.position.y - 0.7f);
                    playerRB.velocity = new Vector2(playerRB.velocity.x, GameObject.Find("Top").transform.position.y - 0.7f);
                }

                

                /*if(isJumping == true)
                {
                    
                    playerRB.gravityScale = 1f;
                } else
                {
                    playerRB.gravityScale = 0f;
                }*/

                break;
        }


    }

    public enum PlayerPositions
    {
        Top,
        Bot
    }

    private void FixedUpdate()
    {

        //playerRB.velocity = new Vector2((speed * Time.timeScale) * Time.deltaTime, (playerRB.velocity.y * Time.timeScale));

        playerRB.velocity = new Vector2(speed * Time.deltaTime, playerRB.velocity.y);

        if (isJumping)
        {
            maxJumpDist = 0.4f;
            isGrounded = false;
            Jump();
            //
        }

        /*if(isJumping && isGrounded == false && jumpCount == 1 && Time.timeScale >= 0.99f)
        {
            Debug.Log("double jump");
            isFalling = false;
            maxJumpDist = 3f;
            jumpForce *= 1.5f;
            Jump();
        }

        

        if (isDashing && isGrounded == false && jumpCount == 2)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, ((jumpForce * 1.5f) * Time.deltaTime) * - 1);
        }

        /*if(Vector2.Distance(new Vector2(0, playerRB.position.y), new Vector2(0,startYPos.y)) > maxJumpDist)
        {
            playerRB.gravityScale = 2f;
            isFalling = true;
            StartCoroutine("Fall");
        }*/
    }

    public void Jump()
    {

        

        

        if (playerPositions == PlayerPositions.Bot)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, (jumpForce) * Time.deltaTime);
        } else
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, (-jumpForce) * Time.deltaTime);
        }

        Debug.Log("jump");
                                                                         
        //Mathf.Clamp(playerRB.velocity.y, 0f, 5f);
        jumpCount++;
        
        
        isDashing = false;

        
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Ground")
        {
            

            ResetValues();
            playerPositions = PlayerPositions.Bot;
            //GetComponent<ShakeyCam>().StartCoroutine("ShakeIt");

            if (speed > 2000f)
            {
                _audioScript.JumpAudio();
                Jump();
                Debug.Log("Check");
            }

        }

        if (collision.gameObject.tag == "Top")
        {
            

            ResetValues();
            //isJumping = false;
            playerPositions = PlayerPositions.Top;
            //GetComponent<ShakeyCam>().StartCoroutine("ShakeIt");

            if (speed >= 2000f)
            {
                // isJumping = true;
                //ResetValues();
                _audioScript.JumpAudio();
                Jump();
                Debug.Log("Check");
            }


        }

    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Top" && isJumping)
        {
            /*if (speed == GetComponent<PowerUp>().newSpeed)
            {
                ResetValues();
                Jump();
                Debug.Log("Check");
            }

            ResetValues();
            isJumping = false;
            playerPositions = PlayerPositions.Top;
            GetComponent<ShakeyCam>().StartCoroutine("ShakeIt");

            
        }
    }*/

    void RenderTrail()
    {
        if (isGrounded && speed != GetComponent<PowerUp>().newSpeed)
        {
            trail.time = 0.05f;
        }
        else
        {
            trail.time = 0.2f;
        }

        if(switchScript.playerChoice == PlayerSwitch.PlayerChoice.Player1)
        {
            trail.startColor = Color.blue;
        } else
        {
            trail.startColor = Color.red;
        }
        
    }

    void ResetValues()
    {
        
        playerRB.velocity = new Vector2(playerRB.velocity.x, 0);
        isJumping = false;
        isDashing = false;
        jumpCount = 0;
        maxJumpDist = 0.4f;
        maxJumpVelocity = 500f;
        //playerRB.gravityScale = 1f;
        jumpForce = 3000f;
        isGrounded = true;
        isFalling = false;
        
    }

    public void SlowMo()
    {
        
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;

    }

    IEnumerator Fall()
    {
        if(isGrounded == false)
        {
            playerRB.velocity -= new Vector2(0, (80f)) * Time.deltaTime;
        }

        yield return new WaitForSeconds(0.2f);

        if (playerRB.velocity.y > 0.5f && isGrounded == false)
        {
            StartCoroutine("Fall");
        }
    }

    


}
