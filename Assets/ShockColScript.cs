using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockColScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BlastObstacle")
        {
            Destroy(collision.gameObject);
            GameObject.FindGameObjectWithTag("Player").GetComponent<ShakeyCam>().StartCoroutine("ShakeIt");
        }


    }
}
