using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovingPlatformFix : MonoBehaviour
{
    private LayerMask _ground;
    
    void Start()
    {
        _ground = LayerMask.NameToLayer("Ground");
    }




    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == _ground)
        {
            transform.SetParent(other.transform);
        }
    }
    
    
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == _ground) 
        {
            transform.SetParent(null);
        }
    }



    
}
