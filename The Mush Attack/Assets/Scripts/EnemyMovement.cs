using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public Transform player;

    private Rigidbody rb;
    private Animator animator;
    private SphereCollider collider;

    private Vector3 moveDirection;
    private float speed = 2.0F;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        collider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void FixedUpdate () {

        Move();
        Rotate();

	}

    private void Move()
    {
        moveDirection = transform.position - player.position;
        moveDirection = moveDirection.normalized * speed * Time.deltaTime;
        rb.MovePosition(transform.position - moveDirection);

        Animate(moveDirection);
    }

    private void Animate(Vector3 moveDirection){
        // This is to start or stop the Run animation 
        if (!moveDirection.Equals(Vector3.zero))
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
    }

    private void Rotate(){
        Vector3 playerToEnemy = player.position - transform.position;

        playerToEnemy = playerToEnemy * speed * Time.deltaTime;

        playerToEnemy.y = 0F;

        Quaternion rotation = Quaternion.LookRotation(playerToEnemy);

        rb.MoveRotation(rotation);

    }

    private void Attack(){
        Debug.Log("Attaaaaack");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.name);
        if(collider.tag == "Player"){
            Attack();
        }
    }
}
