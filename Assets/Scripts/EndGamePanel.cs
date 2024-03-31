using TMPro;
using UnityEngine;

public enum GameState
{
	Lost,
	Won
}

public class EndGamePanel : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _wonLoseText;
	[SerializeField] private TextMeshProUGUI _riddleWordText;

	private void Start()
	{
		gameObject.SetActive(false);
	}

	public void OnEndingGame(GameState state)
	{
		_riddleWordText.text = "���������� �����: " + GameManager.RiddleWord;

		if (state == GameState.Won)
		{
			_wonLoseText.text = "�����!";
			_wonLoseText.color = Color.green;
		}
		else if (state == GameState.Lost)
		{
			_wonLoseText.text = "�� ���������!";
			_wonLoseText.color= Color.red;
		}

		gameObject.SetActive(true);
	}
}
