using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

using UnityEngine.UI;
public class PlayerController : MonoBehaviour {
	private Rigidbody2D myRigidBody;
	private Animator myAnim;
    private float playerDeadTime = -1;
    private Collider2D myCollider;
    public Text scoreText;
    private float startTime;
    private int jumpsLeft = 2;
    public AudioSource jumpSfx;
    public AudioSource deathSfx;

	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody2D> ();
		myAnim = GetComponent<Animator> ();
        myCollider = GetComponent<Collider2D> ();
        startTime = Time.time;
    }
    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel("Title");
           
        }
        if (playerDeadTime == -1)
        {
            if (Input.GetButtonUp("Jump") && jumpsLeft>0)
            {    if(myRigidBody.velocity.y <0)
                { myRigidBody.velocity = Vector2.zero; }

                if (jumpsLeft == 1)
                {
                    myRigidBody.AddForce(transform.up * 300f);
                }
                else
                {
                    myRigidBody.AddForce(transform.up * 400f);
                }

                jumpsLeft--;
                jumpSfx.Play();
            }
            myAnim.SetFloat("vVelocity", myRigidBody.velocity.y);
            scoreText.text = (5*(Time.time - startTime)).ToString("0");
        }
        else
        {   if (Time.time > playerDeadTime + 2)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }
	void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            foreach (PrefabSpawner spawn in FindObjectsOfType<PrefabSpawner>())
            {
                spawn.enabled = false;
            }
            foreach (MoveLeft moveLefter in FindObjectsOfType<MoveLeft>())
            {
                moveLefter.enabled = false;
            }

            playerDeadTime = Time.time;
            myAnim.SetBool("PlayerDead", true);
            myRigidBody.velocity = Vector2.zero;
            myRigidBody.AddForce(transform.up * 400f);
            myCollider.enabled = false;
            deathSfx.Play();

        }
        else if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            jumpsLeft = 2;
        }
         
    }

}
