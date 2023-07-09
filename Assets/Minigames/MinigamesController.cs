using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigamesController : MonoBehaviour
{
	public static MinigamesController Instance;

	public List<Minigame> minigames;
	[SerializeField] private Image background;

	private int lastIndex = -1;
	private Minigame activeMinigame;

	private void Awake()
	{
		if(Instance != null)
		{
			Destroy(gameObject);
		}

		Instance = this;

		background.enabled = false;
	}

	public void StartRandomMinigame()
	{
		int randomIndex = UnityEngine.Random.Range(0, minigames.Count);

		if(randomIndex == lastIndex)
		{
			randomIndex = UnityEngine.Random.Range(0, minigames.Count);
		}

		activeMinigame = minigames[randomIndex];
		
		activeMinigame.gameObject.SetActive(true);

		background.enabled = true;

		activeMinigame.OnMinigameFinish += ReturnMinigameResult;

		lastIndex = randomIndex;
	}

	private void ReturnMinigameResult(bool hasSuceeded)
	{
		activeMinigame.gameObject.SetActive(false);
		activeMinigame.OnMinigameFinish -= ReturnMinigameResult;

		LevelController.Instance.ApplyMinigameResult(hasSuceeded);
	}
}
