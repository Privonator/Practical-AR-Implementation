using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.Networking;

public class GetLocation : MonoBehaviour
{
    private string IPAddress;
    public LocationInfo info;
    public float latitude, longitude;
     
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetIP());
    }
    private IEnumerator GetIP()
    {
        var www = new UnityWebRequest("https://api.ipify.org")
        {
            downloadHandler = new DownloadHandlerBuffer() { }
        };
        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            yield break;

        }
        IPAddress = www.downloadHandler.text;
        StartCoroutine(GetCoordinates());


    }
    private IEnumerator GetCoordinates()
    {
        var www = new UnityWebRequest("http://ip-api.com/json/" + IPAddress)
        {
            downloadHandler = new DownloadHandlerBuffer()
        };
        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            yield break;

        }
        info = JsonUtility.FromJson<LocationInfo>(www.downloadHandler.text);
        latitude = info.lat;
        longitude = info.lon;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
[Serializable]
public class LocationInfo
{
    public string status;
    public string country;
    public string countrycode;
    public string region;
    public string regionname;
    public string city;
    public string zip;
    public float lat;
    public float lon;
    public string timezone;
    public string isp;
    public string org;
    public string @as;
    public string query;
}
