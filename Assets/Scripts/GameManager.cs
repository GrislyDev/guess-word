using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static string RiddleWord { get; private set; }

	[SerializeField] private Transform _wordsList;
	[SerializeField] private Line _linePrefab;
	[SerializeField] private EndGamePanel _endGamePanel;
	[SerializeField] private Keyboard _keyboard;
	[SerializeField] private GameObject _menuCanvas;
	[SerializeField] private GameObject _gameCanvas;

	private const int WORD_LENGTH = 5;
	private const int LINES_COUNT = 8;

	private WordLoader _wordLoader;
	private List<Line> _lines;
	private Line _currentLine;
	private int _currentIndex = 0;

	private int _correctCount;
	private int _closeCount;
	private int _attempts;

	private void Awake()
	{
		Initialize();
	}
	private void Initialize()
	{
		_wordLoader = new WordLoader();
		RiddleWord = _wordLoader.GetRandomWord();
		CreateLines(LINES_COUNT);
	}

	private void Start()
	{
		SoundManager.Instance.PlayMusic();
	}
	public void Play()
	{
		_menuCanvas.SetActive(false);
		_gameCanvas.SetActive(true);
		SoundManager.Instance.PlayStartSound();
	}
	public void Restart()
	{
		SceneManager.LoadScene("Game");
	}
	public void AddCharacterToWord(string character)
	{
		SoundManager.Instance.PlayClickSound();
		if (_currentLine.AddCharacter(character[0]))
			UndoHighlightWord(_currentLine);
	}
	public void ClearLine()
	{
		SoundManager.Instance.PlayClickSound();
		_currentLine.Clear();
		UndoHighlightWord(_currentLine);
	}
	public void CancelLast()
	{
		SoundManager.Instance.PlayClickSound();
		_currentLine.Cancel();
		UndoHighlightWord(_currentLine);
	}
	public void Surrender()
	{
		SoundManager.Instance.PlayClickSound();
		OnLosingGame();
	}
	public void Hint()
	{
		SoundManager.Instance.PlayClickSound();
		_keyboard.MarkUnusedCharacters(5);
	}
	public void CheckWordGuess()
	{
		SoundManager.Instance.PlayClickSound();
		var word = _currentLine.GetWord();

		if (!_wordLoader.IsWordInDictionary(word))
		{
			HighlightInvalidWord(_currentLine);
			return;
		}

		List<int> skip = new List<int>();

		char[] riddleWord = RiddleWord.ToCharArray();
		if (word.Length < WORD_LENGTH)
			return;

		_closeCount = _correctCount = 0;

		for (int i = 0; i < WORD_LENGTH; i++)
		{
			if (word[i] == riddleWord[i])
			{
				_correctCount++;
				riddleWord[i] = ' ';
				skip.Add(i);
			}
		}
		for (int i = 0; i < WORD_LENGTH; i++)
		{
			if (skip.Contains(i))
				continue;

			for (int j = 0; j < WORD_LENGTH; j++)
			{
				if (word[i] == riddleWord[j])
				{
					_closeCount++;
					riddleWord[j] = ' ';
					break;
				}
			}
		}

		_currentLine.SetCorrectAndClose(_correctCount, _closeCount);
		_attempts++;

		if (_correctCount == WORD_LENGTH)
			OnWinningGame();
		else
			ChangeCurrentLine();
	}
	private void CreateLines(int lines)
	{
		_lines = new List<Line>();

		for (int i = 0; i < lines; i++)
		{
			var line = Instantiate(_linePrefab, _wordsList);
			_lines.Add(line);
		}

		_currentLine = _lines[_currentIndex];
	}
	private void ChangeCurrentLine()
	{
		if (++_currentIndex < _lines.Count)
			_currentLine = _lines[_currentIndex];
		else
			OnLosingGame();
	}
	private void OnWinningGame()
	{
		SetCharactersColorByWord();
		_endGamePanel.OnEndingGame(GameState.Won);
		SoundManager.Instance.PauseMusic();
		SoundManager.Instance.PlayWonSound();
	}
	private void OnLosingGame()
	{
		SetCharactersColorByWord();
		_endGamePanel.OnEndingGame(GameState.Lost);
		SoundManager.Instance.PauseMusic();
		SoundManager.Instance.PlayLostSound();
	}

	private void HighlightInvalidWord(Line line)
	{
		for (int i = 0; i < WORD_LENGTH; i++)
		{
			line.GetCharacterByIndex(i).GetComponent<CharacterColorChanger>().SetImageColor(CharacterState.Incorrect);
		}
	}
	private void UndoHighlightWord(Line line)
	{
		for (int i = 0; i < WORD_LENGTH; i++)
		{
			line.GetCharacterByIndex(i).GetComponent<CharacterColorChanger>().SetImageColor(CharacterState.None);
		}
	}
	private void SetCharactersColorByWord()
	{
		foreach(var line in _lines)
			UndoHighlightWord(line);

		char[] riddleWord;
		List<int> skip = new List<int>();

		for (int a = 0; a < _attempts; a++)
		{
			skip.Clear();
			riddleWord = RiddleWord.ToCharArray();

			for (int i = 0; i < WORD_LENGTH; i++)
			{
				if (_lines[a].GetCharacterByIndex(i).CharacterString[0] == riddleWord[i])
				{
					_lines[a].GetCharacterByIndex(i).GetComponent<CharacterColorChanger>().SetImageColor(CharacterState.Correct);
					riddleWord[i] = ' ';
					skip.Add(i);
				}
			}
			for (int i = 0; i < WORD_LENGTH; i++)
			{
				if (skip.Contains(i))
					continue;

				for (int j = 0; j < WORD_LENGTH; j++)
				{
					if (_lines[a].GetCharacterByIndex(i).CharacterString[0] == riddleWord[j])
					{
						_lines[a].GetCharacterByIndex(i).GetComponent<CharacterColorChanger>().SetImageColor(CharacterState.Close);
						riddleWord[j] = ' ';
						break;
					}
				}
			}
		}
	}
}