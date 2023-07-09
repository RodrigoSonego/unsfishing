using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
	[SerializeField] private Slider foodBar;

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
}
