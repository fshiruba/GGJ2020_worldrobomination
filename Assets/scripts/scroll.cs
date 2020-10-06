using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scroll : MonoBehaviour
{
    public float speed = 0.5f;
    private MeshRenderer meshrend;
    public bool brake;

    // Start is called before the first frame update
    void Start()
    {
        meshrend = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!brake)
        {
            Vector2 offset = new Vector2(Time.time * speed, 0);
            meshrend.material.mainTextureOffset = offset;
        }
        else
        {
            speed = 0;
        }
    }

    internal void Brake()
    {
        brake = true;
    }
}
