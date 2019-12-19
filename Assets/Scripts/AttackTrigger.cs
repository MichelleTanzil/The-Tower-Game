using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Enemy")
        {
            GameObject Enemey = GameObject.Find("Enemy");
            Destroy(Enemey);
            Debug.Log("Touched/Killed");
        }
        if (collision.gameObject.name == "Enemy (1)")
        {
            GameObject Enemey = GameObject.Find("Enemy (1)");
            Destroy(Enemey);
            Debug.Log("Touched/Killed");
        }
    }
}
