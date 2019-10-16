using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorUSB : MonoBehaviour
{
// https://answers.unity.com/questions/1024305/gameobject-find-nullreferenceexception-object-refe.html lol
    public bool debug;
    public int devices;

    int lastAmountOfDevices;
    public int inferredDeviceCount;
    GameObject[] FoundObject;

    void Awake(){
        inferredDeviceCount = 0;
        devices = getNumberOfUSB();
        lastAmountOfDevices = devices;
        print("henlo");
        StartCoroutine("PingUSB");
        FoundObject = GameObject.FindGameObjectsWithTag("Quad");
        foreach (GameObject obj in FoundObject) {
            obj.SetActive (false);    
        }
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
                Debug.Log("in");
                for(int i = 0; i < FoundObject.Length; i++)  
                {
                    FoundObject[i].SetActive(i == inferredDeviceCount);
                }
            //  foreach (GameObject obj in FoundObject) {
            //     if(obj.name == "Quad "+ inferredDeviceCount) {
            //         obj.SetActive (true);    
            //     }
            //  }
            //        FoundObject[3-inferredDeviceCount].SetActive(true);
                     lastAmountOfDevices = devices;
            }
            else if(devices < lastAmountOfDevices){
                //FoundObject[3-inferredDeviceCount].SetActive(false);
                inferredDeviceCount--;
                //FoundObject[3-inferredDeviceCount].SetActive(true);
                lastAmountOfDevices = devices;
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
