using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FancyTextMotions : MonoBehaviour
{
    TextMeshProUGUI tmp;
    
    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    float Rand()
    {
        return Random.Range(0f, 1.0f);
     
    }

    // Update is called once per frame
    void Update()
    {

        float sin = Mathf.Sin(Time.time)*0.01f;
        tmp.color = new Color(0.9f + (Mathf.Sin(Time.time)*0.2f), 0.9f,0.95f);
        transform.localScale = new Vector3(transform.localScale.x + sin, 
                                           transform.localScale.y + sin,1f);

    }
}
