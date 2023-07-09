using UnityEngine;

public class Bait : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D CoisaBatendo)
	{
		if(CoisaBatendo.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			LevelController.Instance.BaitCollected();

			Destroy(gameObject);
		}
	}
}