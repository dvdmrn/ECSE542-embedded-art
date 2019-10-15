using System;
using System.Collections.Generic;
using System.Management; // need to add System.Management to your project references.
using UnityEngine;

public class GetUSBInfo:MonoBehaviour
{
    void Start(){
    
        var usbDevices = GetUSBDevices();

        foreach (var usbDevice in usbDevices)
        {
            print("Device ID:"+usbDevice.DeviceID+", PNP Device ID:"+usbDevice.PnpDeviceID+", Description: "+usbDevice.Description);
        }
        Environment.Exit(0);
    

        List<USBDeviceInfo> GetUSBDevices()
        {
            List<USBDeviceInfo> devices = new List<USBDeviceInfo>();

            ManagementObjectCollection collection;
            print("trying to make management searcher obj");
            using (var searcher = new ManagementObjectSearcher(@"Select * From Win32_USBHub"))
            collection = searcher.Get();

            foreach (var device in collection)
            {
                devices.Add(new USBDeviceInfo(
                (string)device.GetPropertyValue("DeviceID"),
                (string)device.GetPropertyValue("PNPDeviceID"),
                (string)device.GetPropertyValue("Description")
                ));
            }

            collection.Dispose();
            return devices;
        }
    }


    public class USBDeviceInfo
    {
        public USBDeviceInfo(string deviceID, string pnpDeviceID, string description)
        {
            this.DeviceID = deviceID;
            this.PnpDeviceID = pnpDeviceID;
            this.Description = description;
        }
        public string DeviceID { get; private set; }
        public string PnpDeviceID { get; private set; }
        public string Description { get; private set; }
    }
}