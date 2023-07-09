using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class CountdownTimer : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI countdownText;

	public event Action OnTimeRunOut;

	float timeRemaining = 0f;

	public void StartTimer(float maxTime)
	{
		timeRemaining = maxTime;

		StartCoroutine(UpdateTimer());
	}

	private IEnumerator UpdateTimer()
	{
		if (countdownText == null) { yield break; }
		while (timeRemaining >= 0)
		{
			timeRemaining -= Time.unscaledDeltaTime;

			TimeSpan time = TimeSpan.FromSeconds(timeRemaining);

			countdownText.text = time.ToString(@"mm\:ss");

			yield return null;
		}

		if (OnTimeRunOut != null) { OnTimeRunOut(); }
	}
}