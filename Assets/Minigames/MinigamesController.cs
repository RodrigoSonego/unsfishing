using System;
using System.Collections.Generic;
using UnityEngine;

public class MinigamesController : MonoBehaviour
{
	public static MinigamesController Instance;

	public List<Minigame> minigames;

	private int lastIndex = -1;
	private Minigame activeMinigame;

	private void Awake()
	{
		if(Instance != null)
		{
			Destroy(gameObject);
		}

		Instance = this;
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
