using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector3 targetPosition;
    private Vector3 globalOldPosition;
    private Vector3 globalNewPosition;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // sterujemy ruchem rêcznie
        targetPosition = transform.position;
        globalOldPosition = transform.position;
        globalNewPosition = transform.position;
    }

    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        Move();
    }

    void HandleInput()
    {
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            Vector3 newTarget = targetPosition;
            globalOldPosition = targetPosition;

            if (Input.GetKeyDown(KeyCode.UpArrow))
                newTarget += new Vector3(0, 0, -1);
            else if (Input.GetKeyDown(KeyCode.DownArrow))
                newTarget += new Vector3(0, 0, 1);
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
                newTarget += new Vector3(1, 0, 0);
            else if (Input.GetKeyDown(KeyCode.RightArrow))
                newTarget += new Vector3(-1, 0, 0);

            // sprawdzamy zakres planszy
            if (Mathf.Abs(newTarget.x) <= 3.5f && Mathf.Abs(newTarget.z) <= 3.5f)
            {
                // sprawdzamy kolizjê
                if (!Physics.CheckBox(newTarget, Vector3.one * 0.4f))
                {
                    targetPosition = newTarget;
                    globalNewPosition = newTarget;

                    // wywo³anie NextTurn tylko przy faktycznym ruchu
                    if (globalNewPosition != globalOldPosition)
                        GameManager.Instance.NextTurn();
                }
                else
                {
                    Debug.Log("Pole zajête!");
                }
            }
        }
    }

    void Move()
    {
        rb.MovePosition(Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.fixedDeltaTime));
    }
}