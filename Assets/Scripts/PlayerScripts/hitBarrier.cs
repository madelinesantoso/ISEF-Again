using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitBarrier : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.position = new Vector2(1, -5);
    }
}
