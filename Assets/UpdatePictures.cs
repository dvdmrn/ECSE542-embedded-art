using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePictures : MonoBehaviour
{
    // Start is called before the first frame update
    MonitorUSB usbMon;
    public PixelSort ps;
    public bool flag = true;
    public GameObject phoneDown;
    // Update is called once per frame

    void Start()
    {
        usbMon = GetComponent<MonitorUSB>();
    }
    void Update()
    {
        if (usbMon.inferredDeviceCount == 0)
        {
            if (flag)
            {
                ps.RandomImg();
                flag = false;
                phoneDown.SetActive(true);     
            }
        }
        else
        {
            if (!flag)
            {
                phoneDown.SetActive(false);
                flag = true;
            }
            
        }
    }
}
