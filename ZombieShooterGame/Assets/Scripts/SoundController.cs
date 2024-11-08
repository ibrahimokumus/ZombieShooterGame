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

    [SerializeField] private AudioSource backgroundAudioSource; 
    [SerializeField] private AudioSource fxSource;         
    [SerializeField] private AudioSource playerAudioSource;    

    public bool isPlayMusic = true;
    public bool isPlayEffect = true;
    public bool isPlayRunning = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Diger sahnelerde kullanmak i�in yok etme
        }
        else
        {
            Destroy(gameObject);// birden fazla varsa yok et
        }
    }

    private void Start()
    {
        PlayBackgroundMusic();
    }

    // Arka plan m�zi�ini �alma fonksiyonu
    public void PlayBackgroundMusic()
    {
        if (!isPlayMusic || backgroundClips.Length == 0 || !backgroundAudioSource) return;// oynatcak bir sey yoksa

        AudioClip randomClip = ChooseRandomClip(backgroundClips);
        backgroundAudioSource.clip = randomClip;
        backgroundAudioSource.Play();
    }

    // Efekt sesini �alma fonksiyonu
    public void PlaySoundEffect(int index)
    {
        if (!isPlayEffect || fxClips.Length == 0 || index >= fxClips.Length || !fxSource) return;

        fxSource.clip = fxClips[index];
        fxSource.Play();
    }

    // Y�r�y�� sesini �alma fonksiyonu
    public void PlayRunningSound(int index)
    {
        if (!isPlayRunning || playerClips.Length == 0 || index >= playerClips.Length || !playerAudioSource) return;

        playerAudioSource.clip = playerClips[index];
        playerAudioSource.Play();
    }

    // Klip dizisinden rastgele klip se�me fonksiyonu
    private AudioClip ChooseRandomClip(AudioClip[] clips)
    {
        return clips[Random.Range(0, clips.Length)];
    }

    // M�zik a�/kapat i�levi
    public void ToggleMusic()
    {
        isPlayMusic = !isPlayMusic;
        if (isPlayMusic)
        {
            PlayBackgroundMusic();
        }
        else
        {
            backgroundAudioSource.Stop();
        }
    }

    // Efekt a�/kapat i�levi
    public void ToggleEffect()
    {
        isPlayEffect = !isPlayEffect;
        if (!isPlayEffect)
        {
            fxSource.Stop();
        }
    }

    // Y�r�y�� sesi a�/kapat i�levi
    public void ToggleWalking()
    {
        isPlayRunning = !isPlayRunning;
        if (!isPlayRunning)
        {
            playerAudioSource.Stop();
        }
    }
}
