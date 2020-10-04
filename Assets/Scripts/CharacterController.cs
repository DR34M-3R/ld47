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
    [SerializeField] private ActionType SetAction;


    public void Awake() {
        Instance = this;
    }

    private void Update() {
        InputUpdate();
    }

    private void FixedUpdate() {
        
                Move();
    }


    private void InputUpdate() {

        switch(SetAction) {

            case ActionType.none:
                if(Axis != 0) {

                    anim.SetBool("walk",true);
                } else {
                    anim.SetBool("walk",false);
                }

                if(Input.GetKeyDown(KeyCode.D))
                    Right();

                if(Input.GetKeyDown(KeyCode.A))
                    Left();

                if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
                    Axis = 0;

                if(Input.GetKeyDown(KeyCode.E))
                    Take();

                if(Input.GetKeyDown(KeyCode.R))
                    GunShot();

                break;
            case ActionType.action:
                Axis = 0;
                break;
        }
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

    public void GunShot() {
        StartCoroutine(Delay(2f));
        anim.SetTrigger("GunShot");
    }

    public void Take() {
        StartCoroutine(Delay(1.7f));
        anim.SetTrigger("Take");
    }

    IEnumerator Delay(float delayTime) {
        SetAction = ActionType.action;
        yield return new WaitForSeconds(delayTime);
        SetAction = ActionType.none;
    }
}

public enum ActionType{
    none,
    action
}
