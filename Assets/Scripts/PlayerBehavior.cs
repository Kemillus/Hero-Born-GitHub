using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotateSped = 75f;
    public float jumpVelocity = 5f;

    public GameObject bullet;
    public float bulletSpeed = 100f;

    public float distanceToGround = 0.1f;

    public LayerMask groundLayer;

    GameBehavior gameManager;
    CapsuleCollider col;

    float vInput;
    float hInput;

    Rigidbody rb;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameBehavior>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        vInput = Input.GetAxis("Vertical") * moveSpeed;
        hInput = Input.GetAxis("Horizontal") * rotateSped;
        //this.transform.Translate(Vector3.forward * vInput * Time.deltaTime);
        //this.transform.Rotate(Vector3.up * hInput * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        Vector3 rotation = Vector3.up * hInput;
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        rb.MovePosition(this.transform.position + this.transform.forward * vInput * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * angleRot);
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
            rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
        if (Input.GetMouseButtonDown(0))
        {
            var newBullet = Instantiate(bullet, transform.position + new Vector3(1, 0, 0), transform.rotation) as GameObject;
            var bulletRB = newBullet.GetComponent<Rigidbody>();
            bulletRB.velocity = transform.forward * bulletSpeed;
        }
    }

    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z);
        var grounded = Physics.CheckCapsule(col.bounds.center, capsuleBottom, distanceToGround, groundLayer, QueryTriggerInteraction.Ignore);
        return grounded;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Enemy")
            gameManager.HP -= 1;
    }
}
