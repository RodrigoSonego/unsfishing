using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
	[SerializeField] private int maxFood;
	[SerializeField] private int maxHealth;
	[SerializeField] private int maxTime;

	public static LevelController Instance;

	private int currentHealth;
	private int currentFood;

	private MinigamesController minigamesController;
	private LevelUI levelUI;

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
		levelUI = LevelUI.Instance;
		
		levelUI.StartUI(maxFood, maxTime);
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
		levelUI.SetFoodValue(currentFood);

		if(currentFood >= maxFood)
		{
			print("GANHOU, PEGOU TODA COMIDA");
			// TODO: telinha de congratuleixo -> load proxima fase
		}
	}

	private void DealDamage()
	{
		currentHealth--;
		//TODO: um screen shake foda aqui

		if (currentHealth <= 0)
		{
			print("fisgado");
		}
	}
}
