using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSystem : MonoBehaviour
{
    [SerializeField] private float dashForce;
    [SerializeField] private float dashRadius;
    [SerializeField] private float dashDamping;

    
    private Vector2 _dashDirection;
    private Vector2 _startPos;
    private Rigidbody2D _rb;
    private MainMovement _mainMovement;
    private float _dashDist;
    private bool _isDashing;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _mainMovement = GetComponent<MainMovement>();
    }

    private void Update()
    {
        if (gameManager.getInstance.IsTowerFocused)
        {
            _dashDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            _dashDirection.Normalize();
            if (Input.GetButtonDown("FocusEffect"))
            {
                gameManager.getInstance.ExitFocus();

                if (_dashDirection != Vector2.zero)
                {
                    _startPos = transform.position;
                    _isDashing = true;
                    _rb.velocity = _dashDirection * dashForce;
                }
            }
                
        }

        if (_isDashing)
        {
            _mainMovement.DisableMoveDamping();
            _dashDist = Vector2.Distance(_startPos, transform.position);
            if (_dashDist >= dashRadius || _mainMovement.CanJump)
            {
                _rb.velocity = (dashForce/10)*_dashDirection;
                _isDashing = false;
                _mainMovement.EnableMoveDamping();
            }
        }
    }

}
