using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour
{
	[SerializeField] private Button[] _buttons;

	private List<Button> _activeButtons;
	private int _count;
	private int _minActiveButtons = 5;
	private Color32 _gray = new Color32(135, 135, 135, 255);

	private void Start()
	{
		_activeButtons = new List<Button>(_buttons);
	}

	public void MarkUnusedCharacters(int charactersCount)
	{
		if (_activeButtons.Count - charactersCount <= _minActiveButtons)
			return;

		_count = 0;
		Button randomButton;
		int randomIndex;

		while (_count < charactersCount)
		{
			randomIndex = Random.Range(0, _activeButtons.Count);
			randomButton = _activeButtons[randomIndex];

			bool hasCharInWord = GameManager.RiddleWord.Contains(randomButton.GetComponentInChildren<TextMeshProUGUI>().text);

			if (!hasCharInWord)
			{
				ChangeButtonColor(randomButton);
				_activeButtons.RemoveAt(randomIndex);
			}
			else
			{
				continue;
			}

			_count++;
		}
	}

	private void ChangeButtonColor(Button button)
	{
		button.GetComponent<Image>().color = _gray;
	}
}
