using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimingMinigame : Minigame
{
	[SerializeField] private RectTransform border;
	[SerializeField] private Color defaultBorderColor;
	[SerializeField] private Color rightTimingBorderColor;
	[SerializeField] private TextMeshProUGUI keyText;
	[SerializeField] private TextMeshProUGUI successCounter;

	[Space]
	[SerializeField] private float timeToPress;
	[SerializeField] private float timeOffset;
	[SerializeField] private int requiredSuccesses = 2;

	private Vector3 borderScale;
	private float timeElapsed = 0;

	[SerializeField] private KeyCode[] KEYS = { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.Q, KeyCode.E };

	private KeyCode correctInput;

	private int successes = 0;

	private float minTime;

	private void Awake()
	{
		borderScale = border.localScale;
	}

	protected override void OnEnable()
	{
		successes = 0;

		RandomizeKey();

		base.OnEnable();

		minTime = timeToPress - timeOffset;

		UpdateSuccessCounter();
	}

	void Update()
	{
		if (hasEnded) { return; }
		CheckInput();
	}

	private void LateUpdate()
	{
		if (hasEnded) { return; }
		ShrinkBorder();
	}

	private void CheckInput()
	{
		if(Input.anyKeyDown == false) { return; }

		if (Input.GetKeyDown(correctInput))
		{
			CheckPressTiming();
			return;
		}

		OnWrongPress();
	}

	private void CheckPressTiming()
	{
		if (timeElapsed >= minTime)
		{
			OnRightPress();
			return;
		}

		OnWrongPress();
	}

	// deus me perdoe por chamar isso no update
	private void ShrinkBorder()
	{
		float maxTime = timeToPress + timeOffset;
		if (timeElapsed > maxTime) {

			print("n apertou aí bobao");
			OnWrongPress();
			return;
		}


		border.GetComponent<Image>().color = timeElapsed >= minTime ? rightTimingBorderColor : defaultBorderColor;

		if (Input.anyKeyDown) { return; }
		Vector3 lerpedScale = Vector3.Lerp(borderScale, new Vector3(0.2f, 0.2f), timeElapsed / maxTime);

		border.localScale = lerpedScale;

		timeElapsed += Time.unscaledDeltaTime;
	}

	private void OnRightPress()
	{
		successes++;

		if (successes >= requiredSuccesses)
		{
			OnMinigameFinish(true);
			hasEnded = true;
			return;
		}

		UpdateSuccessCounter();

		Restart();
	}

	private void OnWrongPress()
	{
		Restart();
	}

	private void Restart()
	{
		RandomizeKey();
		border.localScale = borderScale;
		timeElapsed = 0;
	}

	private void RandomizeKey()
	{
		correctInput = KEYS[Random.Range(0, KEYS.Length)];

		keyText.text = correctInput.ToString();
	}

	private void UpdateSuccessCounter()
	{
		successCounter.text = $"{successes}/{requiredSuccesses}";
	}
}
