using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class Movement : NetworkBehaviour
{
    public float moveSpeed = 30f;

    void Update()
    {
        if (IsLocalPlayer)
        {
            if (Input.GetKey(KeyCode.A))
            {
                MoveLeft();
            }

            if (Input.GetKey(KeyCode.D))
            {
                MoveRight();
            }

            if (Input.GetKey(KeyCode.W))
            {
                MoveUp();
            }

            if (Input.GetKey(KeyCode.S))
            {
                MoveDown();
            }
        }
    }

    void MoveLeft()
    {
        Vector3 temp = transform.position;
        temp.x -= moveSpeed * Time.deltaTime;
        transform.position = temp;
    }

    void MoveRight()
    {
        Vector3 temp = transform.position;
        temp.x -= -moveSpeed * Time.deltaTime;
        transform.position = temp;
    }

    void MoveUp()
    {
        Vector3 temp = transform.position;
        temp.y -= -moveSpeed * Time.deltaTime;
        transform.position = temp;
    }

    void MoveDown()
    {
        Vector3 temp = transform.position;
        temp.y -= moveSpeed * Time.deltaTime;
        transform.position = temp;
    }
}
