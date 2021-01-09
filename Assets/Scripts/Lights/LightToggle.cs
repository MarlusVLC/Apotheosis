using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class LightToggle : MonoBehaviour
{
    [SerializeField] private Camera maincamera;
    [SerializeField] private Transform background;
    
    [SerializeField] private Light _light;

    private Vector3 _lightCenterOnObject;
    private Vector3 _circunferenceOfLightCone;

    private float _positiveCircunferencePosX;
    private float _negativeCircunferencePosX;
    private float _positiveCircunferencePosY;
    private float _negativeCircunferencePosY;
    
    private float _camWidth;
    private float _camHeight;
    private Vector2 _camPos;

    private float _camXLimitMax;
    private float _camXLimitMin;
    private float _camYLimitMax;
    private float _camYLimitMin;

    void Awake()
    {
        _light = GetComponent<Light>();
        // _light.enabled = false;
    }
    void Start()
    {
        _camWidth = maincamera.orthographicSize * Screen.width / Screen.height;
        _camHeight = maincamera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        _camPos = maincamera.transform.position;

        _camXLimitMax = _camPos.x + _camWidth;
        _camXLimitMin = _camPos.x - _camWidth;
        _camYLimitMax = _camPos.y + _camHeight;
        _camYLimitMin = _camPos.y - _camHeight;

        _positiveCircunferencePosX = transform.position.x + AbsRadiusOnObject(background);
        _negativeCircunferencePosX = transform.position.x - AbsRadiusOnObject(background);
        _positiveCircunferencePosY = transform.position.y + AbsRadiusOnObject(background);
        _negativeCircunferencePosY = transform.position.y - AbsRadiusOnObject(background);

        
        
        _light.enabled = ToggleLight();
    }

    private void OnDrawGizmos()
    {
        _lightCenterOnObject = new Vector3(transform.position.x, transform.position.y, background.position.z);
        _circunferenceOfLightCone = new Vector3(transform.position.x + AbsRadiusOnObject(background),
            transform.position.y, background.position.z);
        
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(_lightCenterOnObject, _circunferenceOfLightCone);
        Gizmos.DrawSphere(_lightCenterOnObject, 0.2f);
        Gizmos.DrawSphere(_circunferenceOfLightCone, 0.1f);
    }
    
    
    

    private bool ToggleLight()
    {
        return (_negativeCircunferencePosX < _camXLimitMax && _positiveCircunferencePosX > _camXLimitMin &&
                _negativeCircunferencePosY < _camYLimitMax && _positiveCircunferencePosY > _camYLimitMin);
    }

    private float DistDepth_LightToObject(Transform other)
    {
        return Mathf.Abs(transform.position.z - other.position.z);
    }

    private float AbsRadiusOnObject(Transform other)
    {
        
        return DistDepth_LightToObject(other) * Mathf.Tan(_light.spotAngle * Mathf.Deg2Rad/2);
    }
}
