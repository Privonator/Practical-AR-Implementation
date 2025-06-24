using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class videoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (videoPlayer != null)
        {
            if (videoPlayer.gameObject.activeInHierarchy && !videoPlayer.isPlaying)
            {
                videoPlayer.Play();
            }
            else if (!videoPlayer.gameObject.activeInHierarchy && videoPlayer.isPlaying)
            {
                videoPlayer.Stop();
            }
        }
    }
    private void OnEnable()
    {
        if (videoPlayer != null && videoPlayer.gameObject.activeInHierarchy)
        {
            videoPlayer.time = 0;
            videoPlayer.Play();
        }
    }
    private void OnDisable()
    {
        if(videoPlayer != null)
        {
            videoPlayer.Stop();
        }
    }
}
