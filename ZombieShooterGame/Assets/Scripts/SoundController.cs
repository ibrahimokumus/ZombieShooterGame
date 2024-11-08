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
            DontDestroyOnLoad(gameObject); // Diger sahnelerde kullanmak için yok etme
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

    // Arka plan müziðini çalma fonksiyonu
    public void PlayBackgroundMusic()
    {
        if (!isPlayMusic || backgroundClips.Length == 0 || !backgroundAudioSource) return;// oynatcak bir sey yoksa

        AudioClip randomClip = ChooseRandomClip(backgroundClips);
        backgroundAudioSource.clip = randomClip;
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

    // Klip dizisinden rastgele klip seçme fonksiyonu
    private AudioClip ChooseRandomClip(AudioClip[] clips)
    {
        return clips[Random.Range(0, clips.Length)];
    }

    // Müzik aç/kapat iþlevi
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

    // Efekt aç/kapat iþlevi
    public void ToggleEffect()
    {
        isPlayEffect = !isPlayEffect;
        if (!isPlayEffect)
        {
            fxSource.Stop();
        }
    }

    // Yürüyüþ sesi aç/kapat iþlevi
    public void ToggleWalking()
    {
        isPlayRunning = !isPlayRunning;
        if (!isPlayRunning)
        {
            playerAudioSource.Stop();
        }
    }
}
