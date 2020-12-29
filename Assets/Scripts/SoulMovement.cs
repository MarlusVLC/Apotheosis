using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;


public class SoulMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float jumpForce = 2f;
    //[SerializeField] float intervalTimer = 1.5f;
    [SerializeField] private int numberOfJumps = 1;

    private Rigidbody2D rb;
    //private bool isCountingTime;
    //private float stockTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //stockTimer = intervalTimer;
    }

    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        transform.Translate(new Vector2(speed * moveHorizontal,0) * Time.deltaTime, Space.World);
        Jump();
        //jumpTimer();
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag.ToLower().Contains("ground"))
        {
            numberOfJumps = 1;
        }
    }

    private void Jump()
    {
        if (numberOfJumps > 0 /*&& !isCountingTime*/)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                rb.AddForce(new Vector2(0, jumpForce) * Time.deltaTime, ForceMode2D.Impulse);
                numberOfJumps--;
                //isCountingTime = true;
            }
        }
        
        
    }

    // private void jumpTimer()
    // {
    //     if (isCountingTime)
    //     {
    //         intervalTimer -= Time.deltaTime;
    //         if (intervalTimer <= 0)
    //         {
    //             isCountingTime = false;
    //             intervalTimer = stockTimer;
    //         }
    //     }
    // }
    
    
    
    
}
