﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroySelf", 15f);
    }

    // Update is called once per frame
    void DestroySelf()
    {
        GameObject.Destroy(gameObject);
    }
}
