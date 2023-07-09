using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
	[SerializeField] private int maxFood;

	[SerializeField] private int maxHealth;

	public static LevelController Instance;

	private int currentHealth;
	private int currentFood;

	private MinigamesController minigamesController;

	private void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
		}

		Instance = this;
	}

	private void Start()
	{
		minigamesController = MinigamesController.Instance;
	}

	public void BaitCollected()
	{
		print("pegou a isca");
		Time.timeScale = 0;
		minigamesController.StartRandomMinigame();
	}

	public void ApplyMinigameResult(bool hasSucceeded)
	{
		Time.timeScale = 1;
		
		if (hasSucceeded)
		{
			AddFood();
			return;
		}

		DealDamage();
	}

	private void AddFood()
	{
		currentFood++;
		print("mim de");
		//TODO: call UI
	}

	private void DealDamage()
	{
		currentHealth--;
		if (currentHealth <= 0)
		{
			print("fisgado");
		}
	}
}
