using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D CoisaBatendo)
    {
        if(CoisaBatendo.name == "bob")
        {
            Debug.Log("Peguei isca");
            Destroy(gameObject);    
        }
    }
}