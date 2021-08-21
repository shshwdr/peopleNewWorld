using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MusicManager : Singleton<MusicManager>
{

    AudioSource[] bgmSources;
    // Start is called before the first frame update
    void Awake()
    {
        bgmSources = GetComponents<AudioSource>();
    }
    public void stopAll()
    {
        foreach (var audio in bgmSources)
        {
            DOTween.To(() => audio.volume, x => audio.volume = x, 0, 1);

            //audio.Stop();
        }
    }
    public void startMusic(int i)
    {
        var audio = bgmSources[i];
        audio.DOKill();
        //audio.time = 0;
        DOTween.To(() => audio.volume, x => audio.volume = x, 1, 1);
    }
    public void playNormal()
    {
        stopAll();
        if (MainGameManager.Instance.finishedGame)
        {

            startMusic(2);
        }
        else
        {

            startMusic(0);
        }
        //bgmSources[0].Play();
    }
    public void playFinal()
    {

        stopAll();
    }
    public void playBattle()
    {

        stopAll();
        startMusic(1);
        //bgmSources[1].Play();
    }
    public void playStart()
    {

        stopAll();
        startMusic(2);
        //bgmSources[2].Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
