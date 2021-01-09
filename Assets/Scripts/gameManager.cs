using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

using Aux_Classes;

public class gameManager : MonoBehaviour
{
    [SerializeField] private float sloMoScale;
    
    private Dictionary<GameObject, RespawnState> _respawnStates = new Dictionary<GameObject, RespawnState>();
    private bool _isTowerFocused;

    public delegate void EnterFocusMode();
    public event EnterFocusMode enterFocus;
    
    public delegate void ExitFocusMode();
    public event EnterFocusMode exitFocus;



    private static gameManager INSTANCE;
    public static gameManager getInstance
    {
        get { return INSTANCE; }
    }

    private void Awake()
    {
        //SINGLETON check
        if (INSTANCE != null && INSTANCE != this)
        {
            Destroy(this.gameObject);
        }
        //SINGLETON instantiate
        else
        {
            INSTANCE = this;
        }
        
    }

    private void Update()
    {

    }
    


    public void SetRespawnState(GameObject gameObject, Vector3 position, Vector3 velocity, float rotation)
    {
        if (!_respawnStates.ContainsKey(gameObject))
        {
            RespawnState respawnState = new RespawnState(position, velocity, rotation);
            _respawnStates.Add(gameObject, respawnState);
        }
    }

    public RespawnState GetRespawnState(GameObject gameObject)
    { 
        return _respawnStates[gameObject];
    }
    

    public bool IsTowerFocused
    {
        get => _isTowerFocused;
        set => _isTowerFocused = value;
    }

    public void EnterFocus()
    {
        _isTowerFocused = true;
        if (enterFocus != null)
                enterFocus();
        Time.timeScale = sloMoScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

    }

    public void ExitFocus()
    {
        // _isTowerFocused = false;
        if (exitFocus != null)
            exitFocus();
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
}
