using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fullscreenify : MonoBehaviour
{
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        float pos = (cam.nearClipPlane + 0.01f);

        transform.position = cam.transform.position + cam.transform.forward * pos;

        float h = Mathf.Tan(cam.fieldOfView * Mathf.Deg2Rad * 0.5f) * pos * 2f;

        transform.localScale = new Vector3(h * cam.aspect, h, 0f);
    }
    
}
