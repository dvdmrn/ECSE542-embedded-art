using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageConnections : MonoBehaviour
{
    MonitorUSB monitor;
    int baseline;
    public int newDevices;
    // Start is called before the first frame update
    void Start()
    {
        monitor = GetComponent<MonitorUSB>();
        baseline = monitor.inferredDeviceCount;
        newDevices = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
