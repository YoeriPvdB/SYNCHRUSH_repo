using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    Rigidbody2D playerRb;

    UIScript _uiScript;

    PowerUp _powerUp;

    ShakeyCam _shakeScript;

    public int addedTime;

    public AudioScript _audioScript;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        _uiScript = GameObject.Find("UI Handler").GetComponent<UIScript>();
        _powerUp = GetComponent<PowerUp>();
        _shakeScript = GetComponent<ShakeyCam>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent != null && collision.transform.parent.gameObject.tag == "Obstacle")
        {
            _audioScript.SmallCrashAudio();

            if (_powerUp.powerUps != PowerUp.PowerUps.Speed)
            {
                addedTime = 5;
                _uiScript.StartCoroutine("AddTimeText", addedTime);
            }

            _shakeScript.StartCoroutine("ShakeIt");

            Destroy(collision.gameObject);
        }

        if(collision.transform.parent != null && collision.transform.parent.gameObject.tag == "Wall")
        {
            //GetComponent<AudioScript>().StartCoroutine("CrashAudio");

            _audioScript.LargeCrashAudio();

            if (_powerUp.powerUps != PowerUp.PowerUps.Speed)
            {
                addedTime = 10;
                _uiScript.StartCoroutine("AddTimeText",addedTime);
            }

            _shakeScript.StartCoroutine("ShakeIt");

            Destroy(collision.gameObject);
        }
    }

    
}
