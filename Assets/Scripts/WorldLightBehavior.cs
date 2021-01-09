using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class WorldLightBehavior : MonoBehaviour
{
    private float DEFAULT_intensity;
    private Light _globalIlum;

    private void Awake()
    {
        _globalIlum = GetComponent<Light>();
        DEFAULT_intensity = _globalIlum.intensity;
    }

    void Start()
    {
        gameManager.getInstance.enterFocus += enterFocus;
        gameManager.getInstance.exitFocus += exitFocus;
    }

    private void OnDisable()
    {
        gameManager.getInstance.enterFocus -= enterFocus;
        gameManager.getInstance.exitFocus -= exitFocus;

    }
    
    void enterFocus()
    {
        _globalIlum.intensity = 0.5f*DEFAULT_intensity;
    }

    void exitFocus()
    {
        _globalIlum.intensity = DEFAULT_intensity;
    }
}
