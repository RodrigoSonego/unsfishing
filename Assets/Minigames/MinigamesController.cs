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

	private bool canStartMinigame = true;

	public bool CanStartMinigame { get { return canStartMinigame; } }

	private void OnEnable()
	{
		if(Instance != null)
		{
			Destroy(gameObject);
		}

		Instance = this;

		background.gameObject.SetActive(true);
		background.enabled = false;
	}

	public void StartRandomMinigame()
	{
		if (canStartMinigame == false) { return; }

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
		activeMinigame.OnMinigameFinish -= ReturnMinigameResult;
		activeMinigame.gameObject.SetActive(false);

		LevelController.Instance.ApplyMinigameResult(hasSuceeded);

		background.enabled = false;
	}

	// Normally will be called only at the end of the game when fish already died
	public void DisableMinigames()
	{
		canStartMinigame = false;
	}
}
