using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sound
{
    Bgm, Effect, 
    MaxCount, //�׳� ���� ���� ���� �뵵 �� �ǹ̾���
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

        audioSources[(int)Sound.Bgm].loop = true; //��� ���� ����
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

        if (type == Sound.Bgm) // BGM ������� ���
        {
            AudioSource audioSource = audioSources[(int)Sound.Bgm];
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else // Effect ȿ���� ���
        {
            AudioSource audioSource = audioSources[(int)Sound.Effect];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }


}
