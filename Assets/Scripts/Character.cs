using UnityEngine;
using TMPro;

public class Character : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _character;
	public string CharacterString { get { return _character.text;} set { _character.text = value; } }
}
