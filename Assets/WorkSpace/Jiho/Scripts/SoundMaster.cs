using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundMaster : MonoBehaviour
{
    public AudioClip[] clips;
    [SerializeField] private AudioSource[] audioSources;

    public void Play(AudioClip audioClip, bool _isBgm, float pitch = 1.0f)
    {
        if (audioClip == null)
            return;

        if (_isBgm) // BGM ������� ���
        {
           
            if (audioSources[0].isPlaying)
                audioSources[0].Stop();

            audioSources[0].pitch = pitch;
            audioSources[0].clip = audioClip;
            audioSources[0].Play();
        }
        else // Effect ȿ���� ���
        {
            audioSources[1].pitch = pitch;
            audioSources[1].PlayOneShot(audioClip);
        }
    }


}
