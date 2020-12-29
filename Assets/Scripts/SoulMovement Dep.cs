using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public enum JUMPSTATE
{
    ONGROUND,
    RISING,
    FALLING
};
public class SoulMovementDep : MonoBehaviour
{
    private Collider2D _collider;
    private float jumpMaxHeight; 
    private float soulHeight;
    private JUMPSTATE _jumpstate;
    
    [SerializeField] private float speed = 2f;
    [SerializeField] private float jumpUpRate = 2f;
    [SerializeField] private float jumpDownRate = 2f;
    [SerializeField] private int maxBodiesJump = 2; //The maximum jump distance is defined by this times the sprite's height;
    [SerializeField] private int numberOfJumps = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<CapsuleCollider2D>();
        soulHeight = _collider.bounds.size.y;
        _jumpstate = JUMPSTATE.FALLING;

    }

    private void Update()
    {
        
    }

    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        transform.Translate(new Vector2(speed * moveHorizontal,0) * Time.deltaTime, Space.World);
        Jump();
        
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
        if (numberOfJumps > 0)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                jumpMaxHeight = soulHeight * maxBodiesJump + transform.position.y;
                _jumpstate = JUMPSTATE.RISING;
                numberOfJumps--;
            }
        }


        if (_jumpstate == JUMPSTATE.RISING)
        {
            if (transform.position.y < jumpMaxHeight)
            {
                transform.Translate(new Vector2(0,jumpUpRate) * Time.deltaTime);
            }

            else
            {
                _jumpstate = JUMPSTATE.FALLING;
            }
        }
        
        if (_jumpstate == JUMPSTATE.FALLING)
        {
            transform.Translate(new Vector2(0,-jumpDownRate) * Time.deltaTime);
        }
        
        
    }
    
    
}


