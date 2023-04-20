using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sound
{
    Bgm, Effect, 
    MaxCount, //그냥 사운드 갯수 세는 용도 별 의미없음
}

public class SoundMaster : MonoBehaviour
{
    AudioSource[] audioSources = new AudioSource[(int)Sound.MaxCount];

    public void SoundInit()
    {
        string[] soundNames = System.Enum.GetNames(typeof(Sound)); // "Bgm", "Effect"

        for(int i = 0; i < soundNames.Length - 1; i++)
        {
            GameObject temp = new GameObject { name = soundNames[i] };
            audioSources[i] = temp.AddComponent<AudioSource>();
            temp.transform.parent = this.transform;
        }

        audioSources[(int)Sound.Bgm].loop = true; //얘는 무한 루프
    }

    public void SoundClear()
    {
        foreach(AudioSource audioSource in audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
    }

    public void Play(AudioClip audioClip, Sound type = Sound.Effect, float pitch = 1.0f)
    {
        if (audioClip == null)
            return;

        if (type == Sound.Bgm) // BGM 배경음악 재생
        {
            AudioSource audioSource = audioSources[(int)Sound.Bgm];
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else // Effect 효과음 재생
        {
            AudioSource audioSource = audioSources[(int)Sound.Effect];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }


}
