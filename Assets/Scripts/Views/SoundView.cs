using Assets.Scripts.Models;
using Assets.Scripts.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Views
{
    public class SoundView : MonoBehaviour
    {
        [SerializeField] private AudioSource audioEffects;
        [SerializeField] private AudioSource backgroundMusic;
        [SerializeField] private Sound[] audioList;

        private void Start() => PlayBackgroundMusic(SoundType.BackgroundMusic, true);

        public void PlaySoundEffects(SoundType soundType, bool loopSound = false)
        {
            AudioClip clip = getSoundClip(soundType);
            if (clip != null)
            {
                audioEffects.loop = loopSound;
                audioEffects.PlayOneShot(clip);
            }
            else
            {
                Debug.LogError("No Audio Clip got selected");
            }
        }

        private void PlayBackgroundMusic(SoundType soundType, bool loopSound = false)
        {
            AudioClip clip = getSoundClip(soundType);
            if (clip != null)
            {
                backgroundMusic.loop = loopSound;
                backgroundMusic.PlayOneShot(clip);
            }
            else
            {
                Debug.LogError("No Audio Clip got selected");
            }
        }


        private AudioClip getSoundClip(SoundType soundType)
        {
            Sound st = Array.Find(audioList, item => item.Type == soundType);
            if (st != null)
            {
                return st.Clip;
            }
            return null;
        }
    }
}