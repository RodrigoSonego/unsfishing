using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MashingMinigame : Minigame
{
	[SerializeField] private float valueToAdd;

	[SerializeField] private Slider slider;

	protected override void OnEnable()
	{
		base.OnEnable();

		StartSlider();
	}

	void Update()
	{
		if(hasEnded) { return; }
		if(HasFailed()) { return; }

		IncrementIfInputted();

		if(HasWon())
		{
			if (OnMinigameFinish != null)
			{
				OnMinigameFinish(true);
			}

			hasEnded = true;
			return;
		}

		UpdateSliderValue();
	}

	private void StartSlider() 
	{
		//TODO: determinar melhor o valor do slider se pa
		slider.value = Random.Range(0.2f, 0.5f);
	}

	private void IncrementIfInputted()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			slider.value += valueToAdd;
		}
	}

	private bool HasFailed()
	{
		if (timeRemaining <= 0 || slider.value <= 0)
		{
			hasEnded = true;
			OnMinigameFinish(false);
			return true;
		}

		return false;
	}

	private bool HasWon()
	{
		return slider.value >= slider.maxValue;
	}

	private void UpdateSliderValue()
	{
		slider.value -= Time.unscaledDeltaTime * 0.2f;
	}
}
