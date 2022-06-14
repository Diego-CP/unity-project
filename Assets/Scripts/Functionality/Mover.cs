using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// abstract means that it has to be inherited from, it cannot be assigned to an object
public abstract class Mover : Fighter {
    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit2D;
    public float ySpeed = 0.75f;
    public float xSpeed = 1.0f;

    public bool dash = false;




    protected virtual void Start() {
        boxCollider = GetComponent<BoxCollider2D>();   
    }

    
    
    public IEnumerator Dash()
    {
        xSpeed = 5;
        ySpeed = 5;
        dash = true;
        yield return new WaitForSeconds(0.08f);
        xSpeed = 0.75f;
        ySpeed = 1;
        yield return new WaitForSeconds(1.5f);
        dash = false;
    }


    protected virtual void UpdateMotor(Vector3 input) 
    {
        
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);
        float horizontalInput = Input.GetAxis("Horizontal");




        if (moveDelta.x > 0) {
            transform.localScale = Vector3.one;
        } else if (moveDelta.x < 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        }

       
        // Add push vector
        moveDelta += pushDirection;

        // Reduce the push force every frame, based on the recovery speed
        // Lerp linearly interpolates between two vectors, approaching the first to the second
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        hit2D = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        
        if (hit2D.collider == null) {
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }   

        hit2D = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        
        if (hit2D.collider == null) {
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }   

    }
}
