using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoController : MonoBehaviour
{
    
    VideoPlayer video;
    
    void Awake()
    {
        video = GetComponent<VideoPlayer>();
        video.Play();
        StartCoroutine("WaitForMovieEnd");
    } 
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("WaitForMovieEnd");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            //volatileHunter.ExportData();
            SceneManager.LoadScene("Tutorial");
        }
    }

    public IEnumerator WaitForMovieEnd()
    {
        while(video.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }
        OnMovieEnded();
    }

    void OnMovieEnded()
    {
        SceneManager.LoadScene("Tutorial");
    }

}
