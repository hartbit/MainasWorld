﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 target;
    private Rigidbody2D body;

    bool InBoat;
    bool movementLocked;

    public void SetBoat(bool value)
    {
        InBoat = value;
        target = transform.position;
    }

    public bool IsInBoat()
    {
        return InBoat;
    }

    public void LockMovement(bool locked)
    {
        movementLocked = locked;
    }

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        movementLocked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (movementLocked)
        {
            target = transform.position;
            body.velocity = Vector2.zero;
            return;
        }

        if (Input.GetMouseButton(0) && !MouseHandler.MouseOnUI())
        {
            // Get target position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                target = hit.point;
            }
            else
            {
                target = transform.position;
            }

        }

        // Move to target in strait line
        if (target != transform.position)
        {
            //transform.right = target - transform.position;
            Vector3 direction = (target - transform.position).normalized;
            //transform.LookAt(target);
            float dist = Vector3.Distance(transform.position, target);
            if (dist > speed * Time.deltaTime)
                body.velocity = direction * speed;
            else
            {
                transform.position = target;
                body.velocity = Vector2.zero;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        target = transform.position;
        body.velocity = Vector2.zero;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        target = transform.position;
        body.velocity = Vector2.zero;
    }
}