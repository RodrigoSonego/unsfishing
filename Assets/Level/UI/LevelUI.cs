using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
	[SerializeField] private Slider foodBar;
	[SerializeField] private List<Image> hearts;

	[SerializeField] private Sprite emptyHeart;
	[SerializeField] private Sprite fullHeart;

	public static LevelUI Instance;

	private void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
		}
		Instance = this;
	}

	public void StartUI(int maxFood, float maxTime)
	{
		StartFoodBar(maxFood);
		StartHearts();
	}

	public void SetFoodValue(int foodValue)
	{
		foodBar.value = foodValue;
	}

	private void StartFoodBar(int maxFood)
	{
		foodBar.value = 0;
		foodBar.maxValue = maxFood;
	}

	private void StartHearts()
	{
		foreach (var heart in hearts)
		{
			heart.sprite = fullHeart;
		}
	}

	public void DecreaseHeart(int healthLost)
	{
		int heartIndex = hearts.Count - healthLost;
		hearts[heartIndex].sprite = emptyHeart;
	}
}
