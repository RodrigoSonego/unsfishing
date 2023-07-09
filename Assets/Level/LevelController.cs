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

		currentHealth = 1;
	}

	public void BaitCollected(Bait bait)
	{
		print("pegou a isca");
		Time.timeScale = 0;

		currentBait = bait;

		minigamesController.StartRandomMinigame();
	}

	public void ApplyMinigameResult(bool hasSucceeded)
	{
		Time.timeScale = 1;
		
		currentBait.Despawn();
		
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
		// substitui por um singleton depois
		PeixeController controller = FindObjectOfType<PeixeController>();

		controller.enabled = false;
		controller.transform.SetParent(currentBait.transform, false);
		controller.transform.position = currentBait.transform.position;
	}

	public void LoadNextLevel()
	{
		LevelLoader.Instance.LoadScene(nextLevelName);
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
}
