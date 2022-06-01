using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{

    private int moveRight = 1;
    private Vector2 velocity;
    public float speed = 5;

    private Rigidbody2D shroomBody;
    // Start is called before the first frame update
    void Start()
    {
        shroomBody = GetComponent<Rigidbody2D>();
        ComputeVelocity();
        shroomBody.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
        shroomBody.AddForce(Vector2.right * 5, ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {
        if (shroomBody.velocity.magnitude < speed){
            shroomBody.AddForce(velocity, ForceMode2D.Force);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            StopShroom();
            this.gameObject.SetActive(false);
        }
        else{
            foreach(ContactPoint2D contactPoint in col.contacts)
            {
                //Debug.Log(contactPoint.normal);
                if(Mathf.Abs(contactPoint.normal.x) > 0)
                {
                    Debug.Log("FLIP THAT SHROOM!");
                    FlipShroom();
                    break;  // Stop calculating for multi-point contacts
                }
            }
        }
        
    }

    void ComputeVelocity()
    {
        velocity = new Vector2(speed * moveRight, 0);
    }
    
    void FlipShroom()
    {
        moveRight *= -1;
        ComputeVelocity();
        shroomBody.AddForce(velocity, ForceMode2D.Impulse);
    }

    void StopShroom()
    {
        moveRight = 0;
        ComputeVelocity();
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
