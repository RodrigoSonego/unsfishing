using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SequenceMinigame : Minigame
{
	[SerializeField] private int sequenceLength;
	[SerializeField] private List<TextMeshProUGUI> keyLabels;

	[SerializeField] private KeyCode[] KEYS = { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.Q, KeyCode.E };

	[SerializeField] private Queue<KeyCode> sequenceQueue = new Queue<KeyCode>();

	protected override void Start()
	{
		if(sequenceLength > keyLabels.Count)
		{
			Debug.Log("SEQUENCIA MAIOR QUE NUMERO DE LABELS!!");
			return;
		}

		HideKeys();
		GenerateSequence();
		
		ShowSequence();

		base.Start();

		OnTimeRunOut += Defeat;
    }

	void Update()
	{
		if (hasEnded) {	return;	}

		ProcessInput();
	}

	private void ProcessInput()
	{
		if (sequenceQueue.Count == 0) { return; }
		if (Input.anyKeyDown == false) { return; }
		if (Input.GetKeyDown(KeyCode.Mouse0)) { return; }

		int labelIndex = sequenceLength - sequenceQueue.Count;

		if (Input.GetKeyDown(sequenceQueue.Peek()))
		{
			keyLabels[labelIndex].color = Color.green;

			sequenceQueue.Dequeue();

			if (sequenceQueue.Count == 0)
			{
				OnCompleteSequence();
			}

			return;
		}

		keyLabels[labelIndex].color = Color.red;
	}

	private void OnCompleteSequence()
	{
		print("completou a sequencia de boa");
		//do stuff
	}

	private void GenerateSequence()
	{
		sequenceQueue.Clear();

		for (int i = 0; i < sequenceLength; i++) 
		{
			KeyCode randomKey = GetRandomKey();

			// Try not letting key be equal to previous
			if(i > 0 && randomKey == sequenceQueue.ToArray()[i-1])
			{
				randomKey = GetRandomKey();
			}

			sequenceQueue.Enqueue(randomKey);
		}
	}

	private KeyCode GetRandomKey()
	{
		return KEYS[Random.Range(0, KEYS.Length)];
	}

	private void HideKeys()
	{
        foreach (var key in keyLabels)
        {
			// gore
            key.transform.parent.gameObject.SetActive(false);
        }
    }

	private void ShowSequence()
	{
		KeyCode[] keyArray = sequenceQueue.ToArray();

		for (int i = 0; i < sequenceLength; i++)
		{
			keyLabels[i].transform.parent.gameObject.SetActive(true);
			keyLabels[i].text = keyArray[i].ToString();
		}
    }

	private void Defeat()
	{
		print("cabou o tempo da sequencia");
		//do damage
	}
}
