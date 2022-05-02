using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using FMODUnity;

public class UIScript : MonoBehaviour
{
    public Text modeText, timeText, addTimeText, rushText, R, U, S, H, startText;

    public Slider swapSlider, botSlider;

    public PlayerSwitch _playerSwitch;

    PlayerMovement _moveScript;

    public float cdNum, leveltime = 0, textOpacity = 1f, rushOpacity = 0f, textScale = 1;

    public bool isCounting;

    Color timeAlpha;

    public Color uiColour;

    Transform timeTextTransform;

    public AudioScript _audioScript;



    private void Start()
    {
        _moveScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        timeTextTransform = addTimeText.gameObject.transform;

        timeTextTransform.position = addTimeText.gameObject.transform.position;

        rushOpacity = 0;

        textOpacity = 0;

        timeText.enabled = false;

        StartCoroutine("Countdown");
    }

    IEnumerator Countdown()
    {
        float startTimer = 3f;

        while (startTimer > 0)
        {

            startTimer -= Time.deltaTime;

            startText.text = "" + Mathf.RoundToInt(startTimer);

            yield return null;
        }

        if(startTimer <= 0)
        {
            _audioScript.musicEmitter1.enabled = true;
            startText.text = "SYNCH";
            StartCoroutine("Pop", startText.gameObject);
            yield return new WaitForSecondsRealtime(1f);

            startText.enabled = false;
            timeText.enabled = true;
            
            GameObject.Find("Ambience 1").GetComponent<StudioEventEmitter>().enabled = true;
            
            _moveScript.speed = 1300f;


        }

    }



    private void Update()
    {
        //cdNum = _playerSwitch.switchTime;

        //Counter();
        

        Timer();

        addTimeText.color = new Color(addTimeText.color.r, addTimeText.color.g, addTimeText.color.b, textOpacity);

        uiColour = new Color(uiColour.r, uiColour.g, uiColour.b, rushOpacity);

        
    }

    public void Timer()
    {
        if (_moveScript.isPaused == false && startText.enabled == false)
        {
            leveltime += Time.deltaTime;
        } 
        
        timeText.text = "" + Mathf.RoundToInt(leveltime);
    }

    public void Counter()
    {
        if (isCounting)
        {            
            swapSlider.gameObject.SetActive(true);
            //swapSlider.maxValue = cdNum;
            cdNum -= Time.deltaTime;
            swapSlider.value = cdNum;
            
        }
        else
        {
            swapSlider.gameObject.SetActive(false);
            
        }
    }

    public IEnumerator ModeSwap()
    {
        modeText.enabled = true;

        if(_playerSwitch.inTandem)
        {
            modeText.text = "TANDEM " + " ON";

        } else
        {
            modeText.text = "TANDEM " + " OFF";
        }

        

        yield return new WaitForSecondsRealtime(2f);

        modeText.enabled = false;
    }

    public IEnumerator AddTimeText(int num)
    {
        addTimeText.enabled = true;
        textOpacity = 1f;

        addTimeText.text = "+ " + num;

        leveltime += num;

        while (textOpacity > 0)
        {
            addTimeText.gameObject.transform.Translate(Vector2.up * Time.deltaTime);
            textOpacity -= Time.deltaTime;
            yield return null;

            //
        }

        if(textOpacity<= 0)
        {
            
            addTimeText.gameObject.transform.position = new Vector2(addTimeText.gameObject.transform.position.x, 4.5f);
            addTimeText.enabled = false;
            
        }
    }

    IEnumerator Rush(Text rushText)
    {
        //rushText.color = new Color(rushText.color.r, rushText.color.g, rushText.color.g, rushOpacity);

        //rushOpacity = 0;

        while (rushOpacity < 1)
        {
            rushText.color = uiColour;
            rushText.gameObject.transform.Translate(Vector2.up * Time.deltaTime);
            rushOpacity += Time.deltaTime;

            yield return null;

            
        }

        rushOpacity = 0;
    }

    IEnumerator Pop (GameObject ting)
    {
        float popTime = 1;

        
        while (popTime > 0)
        {
            textScale += (0.001f * 360f) * Time.deltaTime;

            //ting.transform.localScale = new Vector2(textScale, textScale);
            ting.transform.position -= new Vector3(0, 0, 0.05f);

            popTime -= Time.deltaTime;

            yield return null;
        }
    }

    

    
}
