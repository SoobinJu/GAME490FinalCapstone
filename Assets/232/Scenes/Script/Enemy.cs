using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    BoxCollider2D myBox2D;
    Rigidbody2D myRb;


    Vector2 point_L;
    Vector2 point_R;
    private bool m_FacingRight = true;
    private bool goingR = true;

    public float movespace;
    public float speed = 1;


    // Start is called before the first frame update
    void Start()
    {
        myBox2D = gameObject.GetComponent<BoxCollider2D>();
        myRb = gameObject.GetComponent<Rigidbody2D>();
        point_L = myRb.position;
        point_R = myRb.position + new Vector2(movespace, 0);
        print(point_L);
        print(point_R);
    }

    // Update is called once per frame
    void Update()
    {
        if (goingR == true)
        {
            movement(point_R);
        }
        if (goingR == false)
        {
            movement(point_L);  
        }
        if (myRb.position == point_R)
        {
            Flip();
            goingR = false;
        }
        if (myRb.position == point_L)
        {
            Flip();
            goingR = true;
        }
    }

    void movement(Vector2 target)
    {
        myRb.position = Vector2.MoveTowards(myRb.position, target, Time.deltaTime * speed);
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(point_L,point_R);
    }
}
