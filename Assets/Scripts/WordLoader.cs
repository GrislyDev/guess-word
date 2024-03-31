using System.Collections.Generic;
using UnityEngine;

public class WordLoader
{
	public List<string> Words;

    public WordLoader()
    {
		Words = new List<string>();
		LoadWords();
    }
    private void LoadWords()
	{
		TextAsset wordFile = Resources.Load<TextAsset>("words");

		if (wordFile != null)
		{
			Words.AddRange(wordFile.text.Split(','));
			Debug.Log("Successfully loaded " + Words.Count + " words!");
		}
		else
		{
			Debug.LogError("Failed to load words file!");
		}
	}
	public string GetRandomWord()
	{
		if (Words == null)
		{
			Debug.LogError("No words loaded!");
			throw new System.NullReferenceException();
		}

		return Words[Random.Range(0, Words.Count)].ToUpper();
	}

	public bool IsWordInDictionary(string word)
	{
		return Words.Contains(word.ToLower());
	}
}
