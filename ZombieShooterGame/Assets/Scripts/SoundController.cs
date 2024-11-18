
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    // Singleton pattern
    public static SoundController instance;

   
    [SerializeField] private AudioClip[] backgroundClips;
    [SerializeField] private AudioClip[] fxClips;
    [SerializeField] private AudioClip[] playerClips;
    [SerializeField] private AudioClip[] fxAroundClips;
    [SerializeField] private AudioClip[] addicionalClips;

    [SerializeField] private AudioSource backgroundAudioSource; 
    public AudioSource fxSource;         
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private AudioSource fxAroundAudioSource;
    [SerializeField] private AudioSource addicionalAudioSource;
    
    public bool isPlayMusic = true;
    public bool isPlayEffect = true;
    public bool isPlayRunning = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Diger sahnelerde kullanmak için yok etme
        }
        else
        {
            Destroy(gameObject);// birden fazla varsa yok et
        }
    }

    private void Start()
    {
        
        //PlayBackgroundMusic(1);
    }

    // Arka plan müziðini çalma fonksiyonu
    public void PlayBackgroundMusic(int index)
    {
        if (!isPlayMusic || backgroundClips.Length == 0 || !backgroundAudioSource) return;// oynatcak bir sey yoksa

        backgroundAudioSource.clip = backgroundClips[index]; 
        backgroundAudioSource.Play();
    }

    // Efekt sesini çalma fonksiyonu
    public void PlaySoundEffect(int index)
    {
        if (!isPlayEffect || fxClips.Length == 0 || index >= fxClips.Length || !fxSource) return;

        fxSource.clip = fxClips[index];
        fxSource.Play();
    }

    // Yürüyüþ sesini çalma fonksiyonu
    public void PlayRunningSound(int index)
    {
        if (!isPlayRunning || playerClips.Length == 0 || index >= playerClips.Length || !playerAudioSource) return;

        playerAudioSource.clip = playerClips[index];
        playerAudioSource.Play();
    }


    // Müzik aç/kapat iþlevi
    public void PlayAroundSounds(int index)
    {
       
        if (fxAroundClips == null  || !fxAroundAudioSource || index < 0 || index >= fxAroundClips.Length) return;
        
        fxAroundAudioSource.clip = fxAroundClips[index];
        fxAroundAudioSource.Stop();
        fxAroundAudioSource.Play();
    }

    public void PlayAddictinalSounds(int index)
    {
        if(!addicionalAudioSource || addicionalClips == null || index < 0 || index >= addicionalClips.Length) return;
        addicionalAudioSource.clip = addicionalClips[index];
        addicionalAudioSource.Stop();
        addicionalAudioSource.Play();
    }
}
