using UnityEngine;
using UnityEngine.UI;

public class MenuSlidersController : MonoBehaviour
{
	[SerializeField] private Slider _musicSlider;
	[SerializeField] private Slider _soundSlider;

	private void Start()
	{
		if (SoundManager.Instance == null)
			return;

		// if !null
		_musicSlider.value = SoundManager.Instance.GetMusicVolume();
		_soundSlider.value = SoundManager.Instance.GetSoundVolume();

		_musicSlider.onValueChanged.AddListener(SoundManager.Instance.SetMusicVolume);
		_soundSlider.onValueChanged.AddListener(SoundManager.Instance.SetSoundVolume);
	}

	private void OnDestroy()
	{
		_musicSlider.onValueChanged.RemoveAllListeners();
		_soundSlider.onValueChanged.RemoveAllListeners();
	}
}
