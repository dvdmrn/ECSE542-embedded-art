using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bouncey : MonoBehaviour
{
    Image img;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, pos.y + Mathf.Sin(Mathf.Cos(Time.time)));
        float a = Mathf.Sin(Time.time * 2f)*0.01f;
        //print(a);
        img.color += new Color(0f, 0f, 0f, a);

    }
}
