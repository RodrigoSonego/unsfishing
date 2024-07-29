using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
	[SerializeField] private int maxFood;
	[SerializeField] private int maxHealth;
	[SerializeField] private int maxTime;
	[Space]
	[SerializeField] private Bait baitPrefab;
	[SerializeField] private string nextLevelName;

	public static LevelController Instance;

	private int currentHealth;
	private int currentFood;

	private Bait currentBait;

	private MinigamesController minigamesController;
	private LevelUI levelUI;

	private PeixeController peixeController;

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

		currentHealth = maxHealth;

		Time.timeScale = 1;

		peixeController = FindFirstObjectByType<PeixeController>();
	}

	public void BaitCollected(Bait bait)
	{
		Time.timeScale = 0;

		currentBait = bait;

		minigamesController.StartRandomMinigame();

		peixeController.enabled = false;
	}

	public void ApplyMinigameResult(bool hasSucceeded)
	{
		Time.timeScale = 1;
		
		currentBait.Despawn();

		peixeController.enabled = true;

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
			StartCoroutine(WaitThenShowCongrats());
		}
	}

	private void DealDamage()
	{
		currentHealth--;
							// isso ta bem estranho
		levelUI.DecreaseHeart(maxHealth - currentHealth);
		//TODO: um screen shake foda aqui

		if (currentHealth <= 0)
		{
			HookFish();
			print("fisgado");
			
			StartCoroutine(WaitThenShowGameOver());
		}
	}

	private void HookFish()
	{
		peixeController.GetHooked(currentBait.hookPoint);
	}

	public void LoadNextLevel()
	{
		LevelLoader.Instance.LoadScene(nextLevelName);
	}

	public void ReloadLevel()
	{
		LevelLoader.Instance.ReloadScene();
	}

	public void GameOver()
	{
		Time.timeScale = 0;
		levelUI.ShowGameOver();
	}

	private IEnumerator WaitThenShowGameOver()
	{
		yield return new WaitForSeconds(1.5f);

		GameOver();
	}

	private IEnumerator WaitThenShowCongrats()
	{
		yield return new WaitForSeconds(1f);

		levelUI.ShowCongratsScreen();

		Time.timeScale = 0;
	}
}
