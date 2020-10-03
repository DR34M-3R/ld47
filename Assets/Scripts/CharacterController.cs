using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour{

    public static CharacterController Instance;

    public Rigidbody2D RB;
    public float Axis;
    public float moveSpeed;


    public void Awake() {
        Instance = this;
    }

    void Update()
    {
        Axis = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate() {
        Move();
    }

    private void Move() {

        RB.velocity = new Vector2(Axis * moveSpeed,RB.velocity.y);
    }
}
