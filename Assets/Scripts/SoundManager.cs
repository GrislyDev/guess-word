using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
	public static SoundManager Instance { get; private set; }

	[Header("AUDIO SOURCES")]
	[SerializeField] private AudioSource _musicSource;
	[SerializeField] private AudioSource _soundSource;

	[Header("AUDIO CLIPS")]
	[SerializeField] private AudioClip _clickSound;
	[SerializeField] private AudioClip _playSound;
	[SerializeField] private AudioClip _lostSound;
	[SerializeField] private AudioClip _wonSound;

	private float _musicVolume;
	private float _soundVolume;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
			return;
		}
		LoadVolumes();
	}
	public void PlayMusic()
	{
		_musicSource.Play();
	}
	public void PauseMusic()
	{
		_musicSource.Pause();
	}
	public void UnpauseMusic()
	{
		_musicSource.UnPause();
	}
	public void StopMusic()
	{
		_musicSource.Stop();
	}
	public void PlayStartSound()
	{
		_soundSource.PlayOneShot(_playSound);
	}
	public void PlayClickSound()
	{
		_soundSource.PlayOneShot(_clickSound);
	}
	public void PlayLostSound()
	{
		_soundSource.PlayOneShot(_lostSound);
	}
	public void PlayWonSound()
	{
		_soundSource.PlayOneShot(_wonSound);
	}
	public void SetMusicVolume(float value)
	{
		_musicVolume = value;
		PlayerPrefs.SetFloat("MusicVolume", value);
		ApplyVolumes();
	}
	public float GetMusicVolume()
	{
		return _musicVolume;
	}
	public float GetSoundVolume()
	{
		return _soundVolume;
	}
	public void SetSoundVolume(float value)
	{
		_soundVolume = value;
		PlayerPrefs.SetFloat("SoundVolume", value);
		ApplyVolumes();
	}
	private void OnMusicValueChangedHandler(float value)
	{
		_musicSource.volume = _musicVolume = value;
	}
	private void OnSoundValueChangedHandler(float value)
	{
		_soundSource.volume = _soundVolume = value;
	}
	private void LoadVolumes()
	{
		_musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
		_soundVolume = PlayerPrefs.GetFloat("SoundVolume", 1f);
		ApplyVolumes();
	}
	private void ApplyVolumes()
	{
		_musicSource.volume = _musicVolume;
		_soundSource.volume = _soundVolume;
	}
}
