using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioMixer am;
    bool toggled = false;
    [SerializeField] Canvas sfxCanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            ToggleMusic();
        }
    }

    void ToggleMusic()
    {
        toggled = !toggled;
        if(toggled)
        {
            sfxCanvas.gameObject.SetActive(true);
        }
        else{
            sfxCanvas.gameObject.SetActive(false);
        }
    }

    public void ChangeSfxVol(float volume)
    {
        if(volume <= -30)
        {
            am.SetFloat("SFXvol", -80);
        }
        else{
        am.SetFloat("SFXvol", volume);
        }
    }
    public void ChangeMusicVol(float volume)
    {
        if(volume <= -30)
        {
            am.SetFloat("MusicVol", -80);
        }
        else{
        am.SetFloat("MusicVol", volume);
        }
    }
}
