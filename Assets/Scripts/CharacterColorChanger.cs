using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum CharacterState
{
	None,
	Correct,
	Close,
	Incorrect
}

public class CharacterColorChanger : MonoBehaviour, IPointerClickHandler
{
	private Image _image;
	private Color32[] _colors = { new Color32(35, 49, 58, 255), new Color32(110, 161, 83, 255), new Color32(192, 183, 46, 255), new Color32(184, 77,75,255)};
	private int _currentColorIndex;

	private void Start()
	{
		_image = GetComponent<Image>();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		_image.color = GetNextColor();
	}
	public void SetImageColor(CharacterState colorIndex)
	{
		_image.color = _colors[(int)colorIndex];
	}
	private Color32 GetNextColor()
	{
		_currentColorIndex++;

		if (_currentColorIndex >= _colors.Length)
			_currentColorIndex = 0;

		return _colors[_currentColorIndex];
	}
}
