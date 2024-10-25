using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager;
    public AudioMixer audioMixer;
    public Slider bgmSlider; 


    // Start is called before the first frame update
    void Awake()
    {
        if(audioManager == null) {
            audioManager = this;
            DontDestroyOnLoad(audioManager);
        }
        else if(audioManager != this) {
            Destroy(this);
            return;
        }

        bgmSlider.onValueChanged.AddListener(SetBGMVol);
    }

    void Start() {
        if(PlayerPrefs.HasKey("Volume")) {
            bgmSlider.value = PlayerPrefs.GetFloat("Volume");
        }
        else {
            bgmSlider.value = 0.5f;
        }

        audioMixer.SetFloat("BGM", Mathf.Log10(bgmSlider.value)*20);
    }

    public void SetBGMVol(float volume) {
        audioMixer.SetFloat("BGM", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("Volume", bgmSlider.value);
    }

}
