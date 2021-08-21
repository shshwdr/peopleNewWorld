using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : Singleton<SFXManager>
{
    AudioSource audioSource;
    public AudioClip[] clickNext;
    public AudioClip[] clickAction;
    public AudioClip[] clickChar;
    public AudioClip[] clickNextTurn;
    public AudioClip[] characterAppear;
    public AudioClip[] loseBattle;
    public AudioClip[] attackShake;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void playSFXRandom(AudioClip[] clips)
    {
        audioSource.PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }
    public void playNextTurn()
    {
        playSFXRandom(clickNextTurn);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
