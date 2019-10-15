using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorUSB : MonoBehaviour
{

    public bool debug;
    public int devices;

    int lastAmountOfDevices;
    public int inferredDeviceCount;

    void Awake(){
        inferredDeviceCount = 0;
        devices = getNumberOfUSB();
        lastAmountOfDevices = devices;
        print("henlo");
        StartCoroutine("PingUSB");
    }

    int getNumberOfUSB(){
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.EnableRaisingEvents = false;
            string relPath = Application.dataPath;
            proc.StartInfo.FileName = relPath+"/DetectUSB.exe";
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            proc.Start();

            string fingerprint = proc.StandardOutput.ReadToEnd();
            string[] consoleLines = fingerprint.Split('\n');            

            if(debug){
                print(fingerprint);
            }

            proc.WaitForExit();
            return consoleLines.Length;
    }

    IEnumerator PingUSB(){
        while(true){
            devices = getNumberOfUSB();
            if(devices > lastAmountOfDevices){ // chunking like this because sometimes a phone shows up as >1 device...
                inferredDeviceCount++;
                lastAmountOfDevices = devices;
            }
            else if(devices < lastAmountOfDevices){
                inferredDeviceCount--;
                lastAmountOfDevices = devices;
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
