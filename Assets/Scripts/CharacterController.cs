using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour{

    public static CharacterController Instance;

    [SerializeField]private Transform SpriteTransform;

    public Rigidbody2D RB;
    public float Axis;
    public float moveSpeed;
    [SerializeField] private Animator anim;


    public void Awake() {
        Instance = this;
    }

    void Update()
    {




        if(Input.GetKeyDown(KeyCode.D))
            Right();

        if(Input.GetKeyDown(KeyCode.A))
            Left();

        if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            Axis = 0;

        
    }

    private void FixedUpdate() {
        if(Axis != 0) {
            
            anim.SetBool("walk",true);
        } else {
            anim.SetBool("walk",false);
        }
        Move();
    }

    private void Move() {

        RB.velocity = new Vector2(Axis * moveSpeed,RB.velocity.y);
        
    }

    private void Right() {
        Axis = 1;
        SpriteTransform.rotation = Quaternion.Euler(0,0,0);
        
    }

    private void Left() {
        Axis = -1;
        SpriteTransform.rotation = Quaternion.Euler(0,180,0);
    }
}
