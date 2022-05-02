using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUps powerUps;

    public float ogSpeed, newSpeed;

    public float zoomSpace;

    int lifeCount, currentLife;

    UIScript _uiScript;

    PlayerMovement _moveScript;

    ShakeyCam _camScript;

    public AudioScript _audioScript;

    private void Start()
    {
        ogSpeed = 1300f;
        newSpeed = ogSpeed * 3f;
        lifeCount = GetComponent<PlayerMovement>().lifeCount;

        _uiScript = GameObject.Find("UI Handler").GetComponent<UIScript>();
        _moveScript = GetComponent<PlayerMovement>();
        _camScript = GetComponent<ShakeyCam>();
    }

    private void Update()
    {
        switch(powerUps)
        {
            case PowerUps.None:

                currentLife = GetComponent<PlayerMovement>().lifeCount;
                break;

            case PowerUps.Speed:

                GetComponent<PlayerMovement>().lifeCount = 1;

                _camScript.Zoom(zoomSpace);

                break;

            case PowerUps.Life:

                _uiScript.leveltime -= 10f;
                powerUps = PowerUps.None;

                break; 
        }

        

    }

    IEnumerator SpeedUp()
    {
        //_audioScript.Music2();
        

        _moveScript.SlowMo();

       

        zoomSpace = 6f;

        _moveScript.speed = newSpeed;

       

        yield return new WaitForSecondsRealtime(_moveScript.slowdownLength);

        
        //GetComponent<PlayerMovement>().Jump();
        zoomSpace = 10f;

        
        
        

        yield return new WaitForSeconds(1.5f);

        _moveScript.speed = ogSpeed;
        _moveScript.lifeCount = currentLife;
        
        powerUps = PowerUps.None;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(GetComponent<BlastScript>().canBlast == true)
        {
            if (collision.gameObject.tag == "Life")
            {
                powerUps = PowerUps.Life;
                Destroy(collision.gameObject);
            }

            if (collision.gameObject.tag == "SpeedBoost")
            {
                GetComponent<PlayerMovement>().playerPositions = PlayerMovement.PlayerPositions.Bot;
                powerUps = PowerUps.Speed;
                Destroy(collision.gameObject);
                StartCoroutine("SpeedUp");
            }

        }



    }


    public enum PowerUps
    {
        None,
        Speed,
        Life
    }
}
