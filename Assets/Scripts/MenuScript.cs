using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject tutorialMenu, EndMenu, EndBackground;

    public MenuState menuState;

    public Text playerScoreText, bestScoreText, endRushText, endR, endU, endS, endH;

    public List<Text> gameRushText = new List<Text>();

    UIScript uiScript;

    public float EndScore;

    bool endState = false;

    public AudioScript _audioScript;

    private void Start()
    {
        menuState = MenuState.Off;

        uiScript = GameObject.Find("UI Handler").GetComponent<UIScript>();
    }

    private void Update()
    {
        switch (menuState)
        {
            case MenuState.Off:

                tutorialMenu.SetActive(false);

                

                if(Input.GetKeyDown(KeyCode.F))
                {
                    
                    menuState = MenuState.On;
                }

                break;

            case MenuState.On:

                tutorialMenu.SetActive(true);

                

                if (Input.GetKeyDown(KeyCode.F))
                {
                    menuState = MenuState.Off;
                }

                break;
        }

       

        
    }

    public IEnumerator End()
    {
        _audioScript.Music2();
        _audioScript.ambienceEmitter1.enabled = false;
        _audioScript.ambienceEmitter2.enabled = false;


        yield return new WaitForSecondsRealtime(1f);

        
        endState = true;
        EndMenu.SetActive(true);
        EndBackground.SetActive(true);
        GetComponent<PlayerMovement>().speed = 0;

        foreach (Text textObj in gameRushText)
        {
            if(textObj.color.a > 0.1f)
            {
                if(textObj.gameObject.name == "R")
                {
                    endR.color = new Color(endR.color.r, endR.color.g, endR.color.b, 1f);
                }

                if (textObj.gameObject.name == "U")
                {
                    endU.color = new Color(endU.color.r, endU.color.g, endU.color.b, 1f);
                }

                if (textObj.gameObject.name == "S")
                {
                    endS.color = new Color(endS.color.r, endS.color.g, endS.color.b, 1f);
                }

                if (textObj.gameObject.name == "H")
                {
                    endH.color = new Color(endH.color.r, endH.color.g, endH.color.b, 1f);
                }

            } else
            {
                if (textObj.gameObject.name == "R")
                {
                    endR.color = new Color(endR.color.r, endR.color.g, endR.color.b, 0.1f);
                }

                if (textObj.gameObject.name == "U")
                {
                    endU.color = new Color(endU.color.r, endU.color.g, endU.color.b, 0.1f);
                }

                if (textObj.gameObject.name == "S")
                {
                    endS.color = new Color(endS.color.r, endS.color.g, endS.color.b, 0.1f);
                }

                if (textObj.gameObject.name == "H")
                {
                    endH.color = new Color(endH.color.r, endH.color.g, endH.color.b, 0.1f);
                }
            }
        } 
        playerScoreText.text = "YOUR SCORE: " + Mathf.RoundToInt(EndScore);

        

        


        //SceneManager.LoadScene("Game Scene");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "End Trigger")
        {
            EndScore = uiScript.leveltime;
            StartCoroutine("End");
        }
    }

    public enum MenuState
    {
        On,
        Off
    }
}
