using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;



public class AudioScript : MonoBehaviour
{
    public AudioSource audioSource;

    public StudioEventEmitter jumpSound, blastSound, crashSound1, crashSound2, ambienceEmitter1, ambienceEmitter2, musicEmitter1, musicEmitter2, music3, music4;

    public StudioGlobalParameterTrigger ambienceTrigger;
    
    

    GameObject Player;




    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        

        
    }


    public void Music3()
    {
        music3.Play();

        return;
    }

    public void Music4()
    {
        music4.Play();

        return;
    }

    public void Music2 ()
    {
        musicEmitter2.enabled = true;
        musicEmitter1.enabled = false;
    }

    public void SmallCrashAudio()
    {
        crashSound1.Play();
        return;
    }

    public void LargeCrashAudio()
    {
        crashSound2.Play();

        return;
    }

    public void JumpAudio()
    {
        jumpSound.Play();

        return;
    }


    public  void BlastAudio()
    {
        blastSound.Play();

        return;
    }
    /*public IEnumerator BlastAudio()
    {
        audioSource.clip = blastSound;
        audioSource.Play();

        yield return new WaitForSecondsRealtime(0.7f);

        audioSource.Stop();
    }

    public IEnumerator JumpAudio()
    {
        audioSource.clip = jumpSound;
        audioSource.Play();

        yield return null;
    }

    public IEnumerator CrashAudio()
    {
        audioSource.clip = crashSound;
        audioSource.Play();

        yield return new WaitForSecondsRealtime(1f);

        audioSource.Stop();
    }*/
}
