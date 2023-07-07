using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MashingMinigame : MonoBehaviour
{
	[SerializeField] private float maxTime;
	[SerializeField] private TextMeshProUGUI timerText;
	[Space]
	[SerializeField] private float valueToAdd;

	[SerializeField] private Slider slider;

	private float timeRemaining;

	private void Start()
	{
		StartTimerAndSlider();
	}

	private void Update()
	{
		if(HasFailed()) { return; }

		IncrementIfInputted();

		if(HasWon()) { print("mashou bem"); return; }

		UpdateSliderValue();

		UpdateTimerText();
	}

	private void StartTimerAndSlider() 
	{
		//TODO: determinar melhor o valor do slider se pa
		slider.value = Random.Range(0.2f, 0.5f);

		timeRemaining = maxTime;
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
			print("N deu tempo");
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
		slider.value -= Time.deltaTime * 0.2f;
	}

	private void UpdateTimerText()
	{
		timeRemaining -= Time.deltaTime;

		timerText.text = "00:" + timeRemaining.ToString("00");
	}
}
