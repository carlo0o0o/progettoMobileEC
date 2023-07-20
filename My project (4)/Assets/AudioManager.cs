using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private AudioSource[] bgm;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        PlayBGM(0);
    }

    public void PlaySFX(int sfxToPlay)
    {
        if (sfxToPlay < sfx.Length)
        {
            sfx[sfxToPlay].pitch = Random.Range(0.85f, 1.15f);
            sfx[sfxToPlay].Play();
        }
    }

    public void StopSFX(int sfxToStop)
    {
        sfx[sfxToStop].Stop();
    }

    public void PlayBGM(int bgmToPlay)
    {
        for(int i=0; i<bgm.Length; i++)
        {
            bgm[i].Stop();
        }
        bgm[bgmToPlay].Play();
    }

}
