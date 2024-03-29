﻿using UnityEngine;
using System.Collections;

public class PixelSort : MonoBehaviour
{
    public ComputeShader shader;

    RenderTexture tex;

    public GameObject quad;

    public Texture2D _src;
    [SerializeField,Range(0,5)] private float _th = 0.5f;

    public int phoneCount;
    public float amp = 0.05f;
    public Texture2D[] textures;

    void Start ()
    {
        textures = Resources.LoadAll<Texture2D>("photos_to_display");
        RandomImg();
        print(textures);
        tex = new RenderTexture(256, 256, 0);
        tex.enableRandomWrite = true;
        tex.Create();
        Graphics.Blit(_src,tex);
        print(tex);

    }

    void OnGUI()
    {
        int w = Screen.width/2;
        int h = Screen.height/2;
        // int s = 512;

        // GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), tex);
    }

    void OnDestroy()
    {
        tex.Release();
    }

    void Update(){

        quad.GetComponent<Renderer>().material.mainTexture = tex;
        Graphics.Blit(_src,tex);    
        shader.SetFloat("th",_th);
        shader.SetFloat("w", tex.width);
        shader.SetFloat("h", tex.height);
        shader.SetTexture(0, "tex", tex);
        shader.SetFloat("th",_th);

        // range: 1-1.5
        _th = 1 + (phoneCount * 0.125f) + Mathf.Sin(Time.realtimeSinceStartup * 0.2f) * amp;
        shader.Dispatch(0,  1, 1, 1);
        
    }

    public void RandomImg()
    {
        _src = textures[Random.Range(0, textures.Length - 1)];
    }

}