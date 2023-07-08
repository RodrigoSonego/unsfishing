using System.Collections;
using TMPro;
using UnityEngine;

public class TimingMinigame : MonoBehaviour
{
	[SerializeField] private RectTransform border;
	[SerializeField] private TextMeshProUGUI keyText;
	[Space]
	[SerializeField] private float timeToPress;
	[SerializeField] private float timeOffset;
	[SerializeField] private int keysToShow = 2;

	private Vector3 borderScale;
	private float timeElapsed = 0;

	[SerializeField] private KeyCode[] KEYS = { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.Q, KeyCode.E };

	private KeyCode correctInput;

	private int keysShown = 0;

	private void Start()
	{
		borderScale = border.localScale;
		RandomizeKey();
    }

	private void Update()
	{
		CheckInput();
	}

	private void LateUpdate()
	{
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
		Restart();
		//TODO: do something
	}

	private void OnWrongPress()
	{
		Restart();

		//TODO: toma dano e pa
	}

	private void Restart()
	{
		keysShown++;
		
		if(keysShown >= keysToShow)
		{
			gameObject.SetActive(false);
			return;
		}

		RandomizeKey();
		border.localScale = borderScale;
		timeElapsed = 0;
	}

	private void RandomizeKey()
	{
		correctInput = KEYS[Random.Range(0, KEYS.Length)];

		keyText.text = correctInput.ToString();
	}
}
