using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    public GameObject option;
    public AudioSource bgm;
    public AudioSource sfx;
    public Scrollbar bgm_bar;
    public Scrollbar sfx_bar;


    private void Update()
    {
        if(option.activeSelf)
        {
            bgm.volume = bgm_bar.value;
            sfx.volume = sfx_bar.value;
        }
    }

    public void ExitOption()
    {
        option.SetActive(false);
    }

    public void TitleOption()
    {
        ExitOption();
        option.SetActive(true);
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
