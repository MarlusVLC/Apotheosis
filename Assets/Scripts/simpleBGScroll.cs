using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBGScroll : MonoBehaviour
{
    [SerializeField] float scrollSpeedX;
    [SerializeField] private float scrollSpeedY;
    
    private Renderer _renderer;


    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 offset = new Vector2(scrollSpeedX, scrollSpeedY) * Time.deltaTime;
        _renderer.material.mainTextureOffset += offset;

    }
}
