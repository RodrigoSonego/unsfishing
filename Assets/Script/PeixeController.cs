using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeixeController : MonoBehaviour


{
    private Rigidbody2D meuRB;
    public float Velocidade = 5f;
    // Start is called before the first frame update
    void Start()
    {
        meuRB = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 minhaVelocidade = new Vector2(horizontal, vertical) * Velocidade;
        meuRB.velocity = minhaVelocidade;

       
    }
}

