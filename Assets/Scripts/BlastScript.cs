using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastScript : MonoBehaviour
{
    public GameObject shockWave;

    CircleCollider2D shockCol;

    public float blastTime, blastRadius, blastOpacity;

    PlayerSwitch _switchScript;

    public Material blueBlastMat, redBlastMat;


    public bool canBlast;

    

    private void Start()
    {
        shockCol = shockWave.GetComponent<CircleCollider2D>();

        blastOpacity = 255f;

        _switchScript = GetComponent<PlayerSwitch>();

        canBlast = true;

        shockCol.enabled = false;

        
    }

    private void Update()
    {
        //shockWave.GetComponent<SpriteRenderer>().color = Color.red;

        if(_switchScript.inTandem)
        {
            shockWave.SetActive(false);
        } else
        {
            shockWave.SetActive(true);
        }

        shockWave.transform.localScale = new Vector2(blastRadius, blastRadius);

        if(_switchScript.playerChoice == PlayerSwitch.PlayerChoice.Player1)
        {
            shockWave.GetComponent<SpriteRenderer>().color = new Color(1f, 0, 0, blastOpacity);
            shockWave.GetComponent<SpriteRenderer>().material = redBlastMat;
        } else
        {
            shockWave.GetComponent<SpriteRenderer>().color = new Color(0, 0, 1f, blastOpacity);
            shockWave.GetComponent<SpriteRenderer>().material = blueBlastMat;
        }

        shockWave.transform.position = this.transform.position;


    }

    private void FixedUpdate()
    {
        
    }

    public IEnumerator Blast()
    {
        shockCol.enabled = true;
        
        blastRadius += (0.7f * 180f) * Time.deltaTime;        
        canBlast = false;
        if(blastRadius >= 4)
        {
            blastOpacity -= 5f;
        }

        yield return new WaitForSecondsRealtime(blastTime);

        

        if(blastRadius <= 12)
        {
            StartCoroutine("Blast");
        } else
        {
            shockCol.enabled = false;
            StartCoroutine("Recharge");
            blastRadius = 0.6f;
            blastOpacity = 255f;
        }
    }

    IEnumerator Recharge()
    {
        blastRadius += (0.01f * 180f) * Time.deltaTime;

        yield return new WaitForSecondsRealtime(blastTime);

        if(blastRadius <= 1.2f)
        {
            StartCoroutine("Recharge");
        } else
        {
            canBlast = true;
        }
    }

    

    
}
