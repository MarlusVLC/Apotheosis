using System;
using System.Collections;
using System.Collections.Generic;
using Aux_Classes;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class LightMoveSimple : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float focusRadius;
    
    private Light _spotLight;
    private GameObject _focalCircle;
    private float _depth;
    private bool _isPlayerGettingOut;




    void Start()
    {
        _isPlayerGettingOut = false;
        
        _spotLight = GetComponent<Light>();
        _depth = transform.position.z;
        
        
        gameManager.getInstance.enterFocus += enterFocus;
        gameManager.getInstance.exitFocus += exitFocus;
        
        gameManager.getInstance.SetRespawnState(this.gameObject,transform.position,Vector3.zero, _spotLight.spotAngle);

    }

    void Update()
    {

        
        
        var dist = Vector2.Distance(transform.position, player.position);
        if (dist < focusRadius && !_isPlayerGettingOut)
        {
            if (!gameManager.getInstance.IsTowerFocused)
                gameManager.getInstance.EnterFocus();
        }
        else
        {
            gameManager.getInstance.IsTowerFocused = false;
        }

        if (gameManager.getInstance.IsTowerFocused && !_isPlayerGettingOut)
        {
            transform.position = new Vector3(player.position.x, player.position.y, _depth);
        }

        if (_isPlayerGettingOut && dist > 1.5f * focusRadius)
            _isPlayerGettingOut = false;
    }

    // private void OnEnable()
    // {
    //     gameManager.getInstance.enterFocus += enterFocus;
    //     gameManager.getInstance.exitFocus += exitFocus;
    //
    // }
    
    private void OnDisable()
    {
        gameManager.getInstance.enterFocus -= enterFocus;
        gameManager.getInstance.exitFocus -= exitFocus;

    }

    void enterFocus()
    {
        transform.position = new Vector3(player.position.x, player.position.y, _depth);
        _spotLight.spotAngle = 15;
    }

    void exitFocus()
    {
        _isPlayerGettingOut = true;
        RespawnState respawnState = gameManager.getInstance.GetRespawnState(this.gameObject);
        transform.position = respawnState.Position;
        _spotLight.spotAngle = respawnState.Rotation;
    }
}
