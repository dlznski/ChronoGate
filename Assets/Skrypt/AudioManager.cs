﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    void Awake() { instance = this; }        
    //Sound Effects
    public AudioClip sfx_landing, sfx_jump, sfx_rewind, sfx_rewind_restore;
    //Music
    public AudioClip music_light, music_dark;
    //Current Music Object
    public GameObject currentMusicObject;

    //Sound Object
    public GameObject soundObject;
    
    public void PlaySFX(string sfxName)
    {
        switch(sfxName)
        {
            case "landing":
                SoundObjectCreation(sfx_landing);
                break;
            case "jump":
                SoundObjectCreation(sfx_jump);
                break;
            case "rewind":
                SoundObjectCreation(sfx_rewind);
                break;
            case "rewindrestore":
                SoundObjectCreation(sfx_rewind_restore);
                break;
            default:
                break;
        }
    }

    void SoundObjectCreation(AudioClip clip)
    {
        //Create SoundsObjct gameobject
        GameObject newObject = Instantiate(soundObject, transform);
        //Assign audioclip to its audiosource
        newObject.GetComponent<AudioSource>().clip = clip;
        //Play the audio
        newObject.GetComponent<AudioSource>().Play();
    }

    public void PlayMusic(string musicName)
    {
        switch (musicName)
        {
            case "light":
                MusicObjectCreation(music_light);
                break;
            case "dark":
                MusicObjectCreation(music_dark);
                break;
            default:
                break;
        }
    }

    void MusicObjectCreation(AudioClip clip)
    {
        //Check if there's an existing music object, if so delete it
        if (currentMusicObject)
            Destroy(currentMusicObject);
        
        //Create SoundsObject gameobject
        currentMusicObject = Instantiate(soundObject, transform);
        //Assign audioclip to its audiosource
        currentMusicObject.GetComponent<AudioSource>().clip = clip;
        //Make the audio source looping
        currentMusicObject.GetComponent<AudioSource>().loop = true;
        //Play the audio
        currentMusicObject.GetComponent<AudioSource>().Play();
    }

}

    