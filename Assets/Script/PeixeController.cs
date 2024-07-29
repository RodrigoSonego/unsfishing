using UnityEngine;

public class PeixeController : MonoBehaviour


{
	bool FacingRight = true;
	private Rigidbody2D rb;

	public Animator anim;

	public int teste;

	public float rotationForce;

	public float Velocidade;
	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{

		//Movimento Basicão
		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");
		Vector3 movementDirection = new Vector3(horizontal, vertical).normalized * Velocidade;
		rb.velocity = movementDirection;


		//Animação
		if (horizontal != 0)
		{
			anim.SetBool("Swimming", true);
			anim.SetBool("isIdle", false);
		}
		else
		{
			anim.SetBool("isIdle", true);
			anim.SetBool("Swimming", false);
		}

		//Flip Basicão
		if (horizontal < 0 && FacingRight)
		{
			Flip();


		}

		else if (horizontal > 0 && !FacingRight)
		{
			Flip();

		}


		//Agora uma tentativa de inclinar o peixe
		if (Input.GetKey(KeyCode.RightArrow))
		{
			//Esse aqui ele vai gradualmente tombando
			// transform.Rotate(Vector3.forward * rotationForce * Time.deltaTime);

			//Esse não
			Vector3 eulerRotation = transform.rotation.eulerAngles;
			Vector3 rotation = transform.rotation.eulerAngles;
			rotation.z = Mathf.Clamp(rotation.z, -20, 0);
			transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, rotationForce);
		}

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			Vector3 eulerRotation = transform.rotation.eulerAngles;
			Vector3 rotation = transform.rotation.eulerAngles;
			rotation.z = Mathf.Clamp(rotation.z, -20, 0);
			transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, rotationForce);

			//transform.Rotate(Vector3.forward * rotationForce * Time.deltaTime);
		}

		if (horizontal == 0)
		{
			Vector3 eulerRotation = transform.rotation.eulerAngles;
			transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);
		}


		// Vector3 rotation = transform.rotation.eulerAngles;
		// rotation.z = Mathf.Clamp(rotation.z, -20, 0);
		// transform.rotation = Quaternion.Euler(rotation);
	}

	void Flip()
	{

		//transform.Rotate(Vector3.forward * rotationForce * Time.deltaTime);
		FacingRight = !FacingRight;
		transform.Rotate(0f, 180f, 0f);

	}

	public void GetHooked(Transform hookTransform)
	{
		transform.parent = hookTransform;
		transform.localPosition = Vector3.zero;
		rb.velocity = Vector3.zero;

		GetComponent<Collider2D>().enabled = false;
		anim.GetComponent<SpriteRenderer>().sortingOrder = 2;

		this.enabled = false;
	}
}

