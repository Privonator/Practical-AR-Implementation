using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using TMPro;

public class WeatherManager : MonoBehaviour
{
    private float timer;
    public float mins_between_update;
    public string API_KEY;
    public GetLocation getlocation;
    private float latitude;
    private float longitude;
    private bool locationInitialised = false;
    public string webAPI;
    private string currentWeather, currentTime, minTemp, maxTemp, currentTemp, currentDate, currentCountry;
    public TextMeshProUGUI T_Date, T_Time, T_Country, T_Description, T_Temp, T_Min, T_Max;
    public bool isAndroid;

    // Start is called before the first frame update
    void Start()
    {

        locationInitialised = false;
        currentDate = DateTime.Now.ToString(DateTime.Now.Date.Day + "/" + DateTime.Now.Date.Month + "/" + DateTime.Now.Date.Year);

    }

    // Update is called once per frame
    void Update()
    {
        currentTime = DateTime.Now.ToString(DateTime.Now.Hour + ":" + DateTime.Now.Minute);
        setText();
        
        if (locationInitialised)
        {
            if (timer <= 0)
            {
                StartCoroutine(GetWeatherInfo());
                timer = mins_between_update * 60;

            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
        else
        {
            if (isAndroid)
            {
                //StartCoroutine(FetchLocationData);
            }
            else { 
                if (getlocation.latitude == 0 && getlocation.longitude == 0)
                {
                    locationInitialised = false;
                }
                else if (getlocation.latitude != 0 && getlocation.longitude != 0)
                {
                latitude = getlocation.latitude;
                longitude = getlocation.longitude;
                locationInitialised = true;
                
                }
            }
        }


    }
    //private IEnumerator FetchLocationData()
    //{

    //}
    private void OnEnable()
    {
        StartCoroutine (GetWeatherInfo());
    }
    private IEnumerator GetWeatherInfo()
    {
        webAPI = "https://api.openweathermap.org/data/2.5/weather?lat=" + latitude + "&lon=" + longitude + "&appid=" + API_KEY + "&units=metric";
        var www = new UnityWebRequest("https://api.openweathermap.org/data/2.5/weather?lat=" + latitude + "&lon=" + longitude + "&appid=" + API_KEY + "&units=metric")
        {
            downloadHandler = new DownloadHandlerBuffer()
        };
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            yield break;

        }
        JSONNode weatherInfo = JSON.Parse(www.downloadHandler.text);
        currentWeather = weatherInfo["weather"][0]["main"];
        currentTemp = (weatherInfo["main"]["temp"]);
        minTemp = (weatherInfo["main"]["temp_min"]);
        maxTemp = (weatherInfo["main"]["temp_max"]);
        currentCountry = (weatherInfo["sys"]["country"]);
        setText();
    }
    private void setText()
    {
        T_Country.text = "Country: " + currentCountry;
        T_Time.text = "Time: " + currentTime;
        T_Date.text = "Date: " + currentDate;
        T_Description.text = "Weather: " + currentWeather;
        T_Temp.text = "Temp: " + currentTemp;
        T_Min.text = "Min Temp: " + minTemp;
        T_Max.text = "Max Temp: " + maxTemp;

    }

}



