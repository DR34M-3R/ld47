using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour{

    public static CharacterController Instance;

    [SerializeField]private Transform SpriteTransform;

    [Header("Var")]
    [SerializeField] public float Axis;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector3 RaycastOffsetPoint;
    
    [Header("Components")]
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D RB;

    [Header("State")]
    [SerializeField] private ActionType SetAction;

    RaycastHit2D hit;


    public void Awake() {
        Instance = this;
        
    }

    private void Start() {
        anim.SetFloat("walkSpeed",moveSpeed / 2.75f);
        RaycastOffsetPoint = new Vector3(0.5f,0.4f,0);
    }

    private void Update() {
        InputUpdate();
        Debug.DrawRay(transform.position + RaycastOffsetPoint,transform.right);
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
        transform.rotation = Quaternion.Euler(0,0,0);
        RaycastOffsetPoint = new Vector3(0.5f,0.4f,0);   
    }

    private void Left() {
        Axis = -1;
        transform.rotation = Quaternion.Euler(0,180,0);
        RaycastOffsetPoint = new Vector3(-0.5f,0.4f,0);
    }

    public void GunShot() {
        StartCoroutine(Delay(2f));
        anim.SetTrigger("GunShot");
        hit = Physics2D.Raycast(transform.position + RaycastOffsetPoint,transform.right, 5f);
        Debug.DrawRay(transform.position,transform.right);
        if(hit.transform.GetComponent<GuardScript>()!= null) {
            hit.transform.GetComponent<GuardScript>().StartCoroutine("Dead");
        }
        Debug.Log(hit.transform.name);
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
