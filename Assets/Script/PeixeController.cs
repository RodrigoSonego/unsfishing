using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PeixeController : MonoBehaviour


{
    bool FacingRight = true;
    private Rigidbody2D RigidDirection;
    public Animator anim;
    public float Velocidade;
    // Start is called before the first frame update
    void Start()
    {
        RigidDirection = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 movementDirection = new Vector2(horizontal, vertical).normalized * Velocidade;
        RigidDirection.velocity = movementDirection;


      if (horizontal < 0 && FacingRight)
      {
          Flip();
  
       }

       else if(horizontal > 0 && !FacingRight)
        {
           Flip();
        }
        
       if(movementDirection != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movementDirection);
    
        }
       
    }

    void Flip()
   {
        FacingRight = !FacingRight;
        transform.Rotate(0f, 180f, 10f);
   }
}

