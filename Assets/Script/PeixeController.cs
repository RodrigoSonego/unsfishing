using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PeixeController : MonoBehaviour


{
    bool FacingRight = true;
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

        if (horizontal > 0 && FacingRight)
        {
            Flip();
  
        }

        else if(horizontal < 0 && !FacingRight)
        {
            Flip();
        }
       
       
    }

    void Flip()
    {
        FacingRight = !FacingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}

