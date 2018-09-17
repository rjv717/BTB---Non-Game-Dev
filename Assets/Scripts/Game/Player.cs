using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour {

    //CharacterController cc;
    Rigidbody rb;

    public float speed = 3f;
    public float jumpForce = 10f;

    int hp = 6;
    float xMove;
    float zMove;
    bool jump;

    // Event Handling for OnCollisionEnter
    public delegate void DieHandler(bool levelCompleted);
    public static event DieHandler OnDie;

    void Awake () {
        //cc = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
	}

    // OnCollisionEnter requires a Rigidbody. Rigidbody brings physic behaviours
    // I don't want to deal with yet.

    private void OnCollisionEnter(Collision collision)
    {
        // Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Obstacle" )
        {
           if (hp > 0)
           {
                hp--;
                Debug.Log("HP = " + hp);
           }
           if (hp <= 0)
           {
                Debug.Log("HP = " + hp);
                OnDie(false);
           }
        }
    }

    // Update is called once per frame
    void Update () {
        xMove = Input.GetAxisRaw("Horizontal");
        zMove = Input.GetAxisRaw("Vertical");
        jump = Input.GetButtonDown("Jump");
        if (xMove != 0 || zMove != 0)
        {
            //cc.SimpleMove(new Vector3(xMove, 0, zMove).normalized * speed);
            rb.AddForce(new Vector3(xMove, 0, zMove).normalized * speed * Time.deltaTime, ForceMode.Impulse);
        }
        if (gameObject.transform.position.y < -8)
        {
            OnDie(false);
        }
	}

    private void FixedUpdate()
    {
        if (jump)
        {
            rb.AddForce(new Vector3(0, 3, 0).normalized * jumpForce * Time.deltaTime, ForceMode.Impulse);
        }
    }
}
