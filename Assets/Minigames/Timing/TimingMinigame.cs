using System.Collections;
using TMPro;
using UnityEngine;

public class TimingMinigame : MonoBehaviour
{
	[SerializeField] private RectTransform border;
	[SerializeField] private TextMeshProUGUI keyText;

	[SerializeField] private float timeToPress;
	[SerializeField] private float timeOffset;

	private Vector3 borderScale;
	private float timeElapsed = 0;

	public KeyCode[] KEYS = { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.Q, KeyCode.E };

	private KeyCode correctInput;

	private void Start()
	{
		borderScale = border.localScale;

		correctInput = KEYS[Random.Range(0, KEYS.Length)];

		print("correct input: " + correctInput.ToString());
		keyText.text = correctInput.ToString();
    }

	private void Update()
	{
		CheckInput();
		
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
		float minTime = timeToPress - timeOffset;

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

		if (Input.anyKeyDown) { return; }
		Vector3 lerpedScale = Vector3.Lerp(borderScale, new Vector3(0.2f, 0.2f), timeElapsed / maxTime);

		border.localScale = lerpedScale;

		timeElapsed += Time.deltaTime;
	}

	private void OnRightPress()
	{
		//TODO: do something
	}

	private void OnWrongPress()
	{
		Restart();

		//TODO: toma dano e pa
	}

	private void Restart()
	{
		border.localScale = borderScale;
		timeElapsed = 0;
	}
}
