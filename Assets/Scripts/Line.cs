using UnityEngine;
using TMPro;

public class Line : MonoBehaviour
{
	[SerializeField] private Character[] _characters;
	[SerializeField] private TextMeshProUGUI _correct;
	[SerializeField] private TextMeshProUGUI _close;

	private int _currentIndex = 0;

	private void Start()
	{
		foreach (var character in _characters)
		{
			character.CharacterString = string.Empty;
		}

		_correct.text = _close.text = string.Empty;
	}

	public bool AddCharacter(char character)
	{
		if (_currentIndex == _characters.Length - 1 && _characters[_characters.Length - 1].CharacterString != "")
			return false;

		_characters[_currentIndex].CharacterString = character.ToString();

		if (_currentIndex + 1 < _characters.Length)
			_currentIndex++;

		return true;
	}

	public void Clear()
	{
		_currentIndex = 0;
		foreach (var character in _characters)
		{
			character.CharacterString = string.Empty;
		}
	}

	public void Cancel()
	{
		if (_currentIndex <= 0)
			return;

		// index > 0
		if (_characters[_currentIndex].CharacterString == string.Empty)
		{
			_currentIndex--;
			_characters[_currentIndex].CharacterString = string.Empty;
		}
		else
		{
			_characters[_currentIndex].CharacterString = string.Empty;
		}
	}

	public int GetWordLength() => _characters.Length;
	public string GetWord()
	{
		string word = "";

		foreach (var character in _characters)
		{
			word += character.CharacterString;
		}

		return word;
	}
	public void SetCorrectAndClose(int correct, int close)
	{
		_correct.text = correct.ToString();
		_close.text = close.ToString();
	}
	public Character GetCharacterByIndex(int index)
	{
		if(index >-1 && index < _characters.Length)
			return _characters[index];

		throw new System.IndexOutOfRangeException();
	}
}
