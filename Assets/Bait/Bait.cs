using System.Collections;
using UnityEngine;

public class Bait : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;

	[SerializeField] private Sprite hookSprite;
	[SerializeField] private float submergionTime;
	[SerializeField] private float offScreenY;


	private void OnTriggerEnter2D(Collider2D CoisaBatendo)
	{
		if(CoisaBatendo.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			spriteRenderer.sprite = hookSprite;

			LevelController.Instance.BaitCollected(this);
		}
	}

	public void Despawn()
	{
		GetComponent<Collider2D>().enabled = false;

		StartCoroutine(GoUp());
	}

	private IEnumerator GoUp()
	{
		float timeSpent = 0;
		
		Vector2 originalPosition = transform.position;
		Vector2 targetPosition = new(originalPosition.x, offScreenY);

		Color spriteColor = spriteRenderer.color;
		Color targetColor = new Color(spriteColor.r, spriteColor.g, spriteColor.b, 0.1f);

		while (timeSpent < submergionTime)
		{
			timeSpent += Time.deltaTime;

			Vector2 lerpedPos = Vector2.Lerp(originalPosition, targetPosition, timeSpent / submergionTime);

			transform.position = lerpedPos;

			Color lerpedColor = Color.Lerp(spriteColor, targetColor, timeSpent / submergionTime);
			spriteRenderer.color = lerpedColor;

			yield return null;
		}

		Destroy(gameObject);
	}
}