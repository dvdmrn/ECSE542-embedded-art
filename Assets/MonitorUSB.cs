using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorUSB : MonoBehaviour
{
    // Maximum device number is 4 in computer prototype (inferred device count's max value is 3)
    // https://answers.unity.com/questions/1024305/gameobject-find-nullreferenceexception-object-refe.html lol
    public bool debug;
    public int devices;

    int lastAmountOfDevices;
    public int inferredDeviceCount;
    GameObject[] FoundObject;
    GameObject[] FoundVideoPlayer;

    void Awake()
    {
        inferredDeviceCount = 0;
        devices = getNumberOfUSB();
        lastAmountOfDevices = devices;
        print("henlo");
        StartCoroutine("PingUSB");
        FoundObject = GameObject.FindGameObjectsWithTag("Quad");
        FoundVideoPlayer = GameObject.FindGameObjectsWithTag("Video Player");
        foreach (GameObject obj in FoundObject)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in FoundVideoPlayer)
        {
            obj.SetActive(false);
        }
    }

    int getNumberOfUSB()
    {
        System.Diagnostics.Process proc = new System.Diagnostics.Process();
        proc.EnableRaisingEvents = false;
        string relPath = Application.dataPath;
        proc.StartInfo.FileName = relPath + "/DetectUSB.exe";
        proc.StartInfo.CreateNoWindow = true;
        proc.StartInfo.UseShellExecute = false;
        proc.StartInfo.RedirectStandardOutput = true;
        proc.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        proc.Start();

        string fingerprint = proc.StandardOutput.ReadToEnd();
        string[] consoleLines = fingerprint.Split('\n');

        if (debug)
        {
            print(fingerprint);
        }

        proc.WaitForExit();
        return consoleLines.Length;
    }

    IEnumerator PingUSB()
    {
        while (true)
        {
            devices = getNumberOfUSB();
            if (devices > lastAmountOfDevices)
            { // chunking like this because sometimes a phone shows up as >1 device...
                switch (inferredDeviceCount)
                {
                    case 0:
                        Debug.Log("in! FoundObject is " + FoundObject[0].name + " to be deactive");
                        FoundObject[0].SetActive(false);
                        FoundVideoPlayer[3].SetActive(false);
                        break;
                    case 1:
                        Debug.Log("in! FoundObject is " + FoundObject[3].name + " to be deactive");
                        FoundObject[3].SetActive(false);
                        FoundVideoPlayer[2].SetActive(false);
                        break;
                    case 2:
                        Debug.Log("in! FoundObject is " + FoundObject[2].name + " to be deactive");
                        FoundObject[2].SetActive(false);
                        FoundVideoPlayer[1].SetActive(false);
                        break;
                }

                inferredDeviceCount++;

                for (int i = 1; i < FoundObject.Length; i++)
                {
                    if (i == inferredDeviceCount)
                    {
                        FoundObject[4 - i].SetActive(i == inferredDeviceCount);   //SetActive true if equal
                        Debug.Log("in! FoundObject is " + FoundObject[4 - i].name + " to be active");
                    }
                }
                for (int i = 1; i < FoundVideoPlayer.Length; i++)
                {
                    if (i == inferredDeviceCount)
                    {
                        FoundVideoPlayer[3 - i].SetActive(i == inferredDeviceCount);   //SetActive true if equal
                    }
                }

                lastAmountOfDevices = devices;
            }
            else if (devices < lastAmountOfDevices)
            {

                for (int i = 1; i < FoundObject.Length; i++)
                {
                    if (i == inferredDeviceCount)
                    {
                        FoundObject[4 - i].SetActive(false);
                        Debug.Log("out! FoundObject is " + FoundObject[4 - i].name + " to be deactive");
                    }
                }
                for (int i = 1; i < FoundVideoPlayer.Length; i++)
                {
                    if (i == inferredDeviceCount)
                    {
                        FoundVideoPlayer[3 - i].SetActive(false);
                    }
                }

                inferredDeviceCount--;

                switch (inferredDeviceCount)
                {
                    case 0:
                        Debug.Log("out! FoundObject is " + FoundObject[0].name + " to be active");
                        FoundObject[0].SetActive(true);
                        FoundVideoPlayer[3].SetActive(true);
                        break;
                    case 1:
                        Debug.Log("out! FoundObject is " + FoundObject[3].name + " to be active");
                        FoundObject[3].SetActive(true);
                        FoundVideoPlayer[2].SetActive(true);
                        break;
                    case 2:
                        Debug.Log("out! FoundObject is " + FoundObject[2].name + " to be active");
                        FoundObject[2].SetActive(true);
                        FoundVideoPlayer[1].SetActive(true);
                        break;
                    case 3:
                        Debug.Log("out! FoundObject is " + FoundObject[1].name + " to be active");
                        FoundObject[1].SetActive(true);
                        FoundVideoPlayer[0].SetActive(true);
                        break;
                }

                lastAmountOfDevices = devices;
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
