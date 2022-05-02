using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShakeyCam : MonoBehaviour
{
    public CinemachineVirtualCamera vCam;

    CinemachineBasicMultiChannelPerlin shakeCam;
   

    public float shakeAmp, shakeFreq;

    

    private void Start()
    {
        vCam = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        shakeCam = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        BoogieStatus();

        if (Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine("ShakeIt");
        }
    }

    void BoogieStatus()
    {
        shakeCam.m_AmplitudeGain = shakeAmp;
        shakeCam.m_FrequencyGain = shakeFreq;
    }

    public void Zoom(float zoomSpace)
    {
        vCam.m_Lens.OrthographicSize = Mathf.Lerp(10f, zoomSpace, 0.5f);
    }

    IEnumerator ShakeIt()
    {
        shakeAmp = .3f;
        shakeFreq = 1f;

        yield return new WaitForSecondsRealtime(0.15f);

        shakeAmp = 0f;
        shakeFreq = 0f;

    }

}
