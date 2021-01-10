using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class TutorialTriggers : MonoBehaviour
{
    [SerializeField] private GameObject jumpKey;
    
    private Animator _animator;    
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_animator.GetBool("Has Jumped Once"))
        {
            Destroy(jumpKey);
        }
    }



}
