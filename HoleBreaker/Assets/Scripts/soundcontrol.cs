using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soundcontrol : MonoBehaviour
{
	private static readonly string FirstPlay = "FirstPlay";
	private static readonly string BackgroundPref = "BackgroundPref";
	private static readonly string SoundEffectsPref = "SoundEffectsPref";
	private int FirstPlayInt;
	public Slider backgroundSlider, soundEffectsSlider;
	public float backgroundFloat, soundEffectsFloat;
	public AudioSource backgroundAudio;
	public AudioSource[] soundEffectsAudio;
	public AudioClip AudioClip1;
  public AudioClip AudioClip2;
	public AudioClip AudioClip3;
	
    // Start is called before the first frame update
    void Start()
    {
		backgroundAudio.clip = AudioClip1;

		backgroundAudio.Play();

        FirstPlayInt = PlayerPrefs.GetInt(FirstPlay);
		
		if(FirstPlayInt == 0)
		{
			backgroundFloat = .125f;
			soundEffectsFloat = .75f;
			backgroundSlider.value = backgroundFloat;
			soundEffectsSlider.value = soundEffectsFloat;
			PlayerPrefs.SetFloat(BackgroundPref, backgroundFloat);
			PlayerPrefs.SetFloat(SoundEffectsPref, soundEffectsFloat);
			PlayerPrefs.SetInt(FirstPlay, -1);
		}
		else
		{
			backgroundFloat = PlayerPrefs.GetFloat(BackgroundPref, backgroundSlider.value);
			backgroundSlider.value = backgroundFloat;
			soundEffectsFloat = PlayerPrefs.GetFloat(SoundEffectsPref);
			soundEffectsSlider.value = soundEffectsFloat;
		}
    }

    // Update is called once per frame
    public void SaveSoundSettings()
	{
		PlayerPrefs.SetFloat(BackgroundPref, backgroundSlider.value);
		PlayerPrefs.SetFloat(SoundEffectsPref, soundEffectsSlider.value);
	}
	
	void OnApplicationFocus(bool inFocus)
	{
		if(!inFocus)
		{
			SaveSoundSettings();
		}
	}
	
	public void UpdateSound()
	{
		backgroundAudio.volume = backgroundSlider.value;
		
		for(int i = 0; i < soundEffectsAudio.Length; i++)
		{
			soundEffectsAudio[i].volume = soundEffectsSlider.value;
		}
	}

	public void ChangeMusic() 
	{
		if (backgroundAudio)
		{

			backgroundAudio.clip = AudioClip2;

			backgroundAudio.Play();

		} 
	}

	public void ChangeMusic2() 
	{
		if (backgroundAudio.clip == AudioClip2)
		{

			backgroundAudio.clip = AudioClip1;

			backgroundAudio.Play();

		} 
	}

	public void ChangeMusic3() 
	{
		if (backgroundAudio.clip == AudioClip2)
		{

			backgroundAudio.clip = AudioClip3;

			backgroundAudio.Play();

		} 
	}

	public void ChangeMusic4() 
	{
		if (backgroundAudio.clip == AudioClip3)
		{

			backgroundAudio.clip = AudioClip1;

			backgroundAudio.Play();

		} 
	}
	
}
