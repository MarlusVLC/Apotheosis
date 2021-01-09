using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Aux_Classes;
using UnityEditor;

public class MainMovement : MonoBehaviour
{
    private enum MoveState
    {
        Idle,
        Walking,
        Jumping,
        Falling
    };
    
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask trapLayer;
    [SerializeField] private float horizontalMoveRate;
    [SerializeField] private float jumpMoveRate;
    // [SerializeField] private float maximumBodyCount; //The jump maximum height should be defined by a multiple of the character's height;
    // [SerializeField] private float fallMoveRate;
    [SerializeField] private float deathHeight = -10; //TALVEZ TENHA QUE MUDAR DEPOIS
    [SerializeField] private float DEFAULT_coyoteTime;
    [SerializeField] private float DEFAULT_jumpBuffer;
    [SerializeField] private float hDampingBasic;
    [SerializeField] private float hDampingStop;
    [SerializeField] private float hDampingTurn;
    [SerializeField] private float jumpHDampingBasic;
    [SerializeField] private float jumpHDampingStop;
    [SerializeField] private float jumpHDampingTurn;
    [SerializeField] private float jumpCut;
    [SerializeField] private float freeFallForce;
     
    private Rigidbody2D _rb;
    private BoxCollider2D _boxCollider;
    private CapsuleCollider2D _capsuleCollider;
    private MoveState _moveState;
    private float _coyoteTime;
    private float _jumpBuffer;
    // private float _maximumJumpHeight;
    private bool _canJump;
    // private bool _isOnAir;
    private bool _areMovementsDamped;
    
    
    
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        // _rb.gravityScale = 0;
        _boxCollider = GetComponent<BoxCollider2D>();
        _areMovementsDamped = true;
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        // _coyoteTime = DEFAULT_coyoteTime;
        // jumpHDampingBasic = hDampingBasic + 0.1f;
    }

    void Start()
    {
        gameManager.getInstance.SetRespawnState(this.gameObject, transform.position, _rb.velocity, _rb.rotation);
    }


    private void Update()
    {
        if (_moveState == MoveState.Idle && _rb.velocity.x != 0)
        {
            _moveState = MoveState.Walking;
        }
        
    }

    void FixedUpdate()
    {

        
        if (transform.position.y <= deathHeight || _capsuleCollider.IsTouchingLayers(trapLayer)) //TALVEZ TENHA QUE MUDAR PRA CONSIDERAR ALGO MAIS GENÉRICO
        {
            Die();
        }


        if (_areMovementsDamped)
        {
            float horizontalVelocity = _rb.velocity.x;
        
            horizontalVelocity += Input.GetAxisRaw("Horizontal");
            if (_rb.velocity.y == 0)
            {
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.01f)
                    horizontalVelocity *= Mathf.Pow(1f - hDampingStop, Time.deltaTime * 10f);
                else if (Mathf.Sign(Input.GetAxisRaw("Horizontal")) != Mathf.Sign(horizontalVelocity))
                    horizontalVelocity *= Mathf.Pow(1f - hDampingTurn, Time.deltaTime * 10f);
                else
                    horizontalVelocity *= Mathf.Pow(1f - hDampingBasic, Time.deltaTime * 10f);
            }
            else
            {
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.01f)
                    horizontalVelocity *= Mathf.Pow(1f - jumpHDampingStop, Time.deltaTime * 10f);
                else if (Mathf.Sign(Input.GetAxisRaw("Horizontal")) != Mathf.Sign(horizontalVelocity))
                    horizontalVelocity *= Mathf.Pow(1f - jumpHDampingTurn, Time.deltaTime * 10f);
                else
                    horizontalVelocity *= Mathf.Pow(1f - jumpHDampingBasic, Time.deltaTime * 10f);
            }
        
            _rb.velocity = new Vector2(horizontalVelocity, _rb.velocity.y);
        }
       

        
        
        
        _canJump = _boxCollider.IsTouchingLayers(groundLayer) || _coyoteTime > 0;

        _jumpBuffer -= Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            _jumpBuffer = DEFAULT_jumpBuffer;
        }

        if ((_jumpBuffer > 0) && _canJump)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpMoveRate);
            _coyoteTime = 0;

        }

        if (Input.GetButtonUp("Jump"))
        {
            if (_rb.velocity.y > jumpMoveRate * jumpCut)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, jumpMoveRate * jumpCut);
            }
        }
        
        // FreeFall();
    }



    private void FreeFall()
    {
        if (!_canJump && Input.GetButtonDown("Jump"))
        {
            _rb.AddForce(new Vector2(0,-freeFallForce));
            _jumpBuffer = 0;
        }
    }


    private void Die()
    {
        RespawnState respawnState = gameManager.getInstance.GetRespawnState(this.gameObject);
        transform.position = respawnState.Position;
        _rb.rotation = respawnState.Rotation;
        _rb.velocity = respawnState.Velocity;
    }
    

    public void EnableMoveDamping()
    {
        _areMovementsDamped = true;
    }

    public void DisableMoveDamping()
    {
        _areMovementsDamped = false;
    }

    public bool CanJump
    {
        get { return _canJump; }
    }
    
}
