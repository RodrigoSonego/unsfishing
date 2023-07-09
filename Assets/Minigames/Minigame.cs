using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Minigame : MonoBehaviour
{
	[SerializeField] protected float maxTime;
	[SerializeField] private TextMeshProUGUI timerText;

	protected bool hasEnded;

	protected float timeRemaining;

	protected event Action OnTimeRunOut;
	public Action<bool> OnMinigameFinish;

	protected virtual void Awake()
	{
		StartTimer();

		StartCoroutine(UpdateTimer());
	}

	private void StartTimer()
	{
		timeRemaining = maxTime;
	}

	private IEnumerator UpdateTimer()
	{
		if(timerText == null) { yield break; }
		while (timeRemaining >= 0)
		{
			timeRemaining -= Time.unscaledDeltaTime;
			timerText.text = "00:" + timeRemaining.ToString("00");

			yield return null;
		}

		if (OnTimeRunOut != null) { OnTimeRunOut(); }
		hasEnded = true;
		OnMinigameFinish(false);
	}
}
