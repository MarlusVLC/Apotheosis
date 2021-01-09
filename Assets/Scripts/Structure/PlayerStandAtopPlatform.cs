using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerStandAtopPlatform : MonoBehaviour
{
    [SerializeField] private GameObject player;




    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == player)
        {
            other.transform.SetParent(transform);
        }
    }



    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject == player)
        {
            other.transform.SetParent(null);
        }
    }
}
