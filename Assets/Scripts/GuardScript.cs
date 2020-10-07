using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GuardScript : MonoBehaviour
{
    [SerializeField] List<Transform> PointList = new List<Transform>();
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GuardionActionType actionType;
    [SerializeField] Vector3 TargetPosition;
    [SerializeField] float Axis;
    [SerializeField] float moveSpeed;

    [SerializeField] Animator anim;
    [SerializeField] private bool homeDestination;
    [SerializeField] private Transform PointInHous;

    public static event UnityAction DestroyWall;

    public void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Vector3 target = PointList[Random.Range(0,PointList.Count)].position;
        SetTarget(target);
    }

    private void Update() {
        switch(actionType) {
            case GuardionActionType.move:Move();
               break;
        }
    }

    public void SetTarget(Vector3 target) {
        TargetPosition = target;
        Move();
    }

    public void Move() {

        if(Vector3.Distance(transform.position,TargetPosition) > 2f) {

            actionType = GuardionActionType.move;

            anim.SetBool("Move",true);

            if(transform.position.x >= TargetPosition.x) {
                Right();
                Axis = -1;
            } else {
                Left();
                Axis = 1;
            }
        } else {
            if(homeDestination) {
                transform.SetParent(PointInHous);
                transform.position = PointInHous.position;
                actionType = GuardionActionType.move;
            }
            Axis = 0;
            Idle();
            anim.SetBool("Move",false);
        }

        rb.velocity = new Vector2(Axis * moveSpeed,rb.velocity.y);

    }

    public void Attack() {
        
        StartCoroutine(DelayAttack());
    }

    public void Left() {
        transform.rotation = Quaternion.Euler(0,0,0);
    }

    public void Right() {
        transform.rotation = Quaternion.Euler(0,180,0);
    }

    public void Idle() {
       StartCoroutine(Delay(Random.Range(5,10)));
    }

    public IEnumerator Delay(float time) {
        actionType = GuardionActionType.none;
        yield return new WaitForSeconds(time);
        Vector3 target = PointList[Random.Range(0,PointList.Count)].position;
        SetTarget(target);
    }
    

    public IEnumerator Dead() {
        yield return new WaitForSeconds(1f);
        //Destroy(gameObject);
        actionType = GuardionActionType.none;
        anim.SetTrigger("Dead");
        rb.velocity = new Vector2(0 * moveSpeed,rb.velocity.y);
        rb.isKinematic = true;
        GetComponent<CapsuleCollider2D>().isTrigger = true;
        

    }


    IEnumerator DelayAttack() {
        if(PlayerPrefs.GetInt("LittleGuardInHome") == 1) {
            actionType = GuardionActionType.attac;
            Right();
            anim.SetTrigger("Attack");
            yield return new WaitForSeconds(2f);
            DestroyWall?.Invoke();
            PlayerPrefs.SetInt("LittleGuardInHome",3);
            }

    }

    public void GoHome() {
        transform.position = PointInHous.transform.position;
        transform.SetParent(PointInHous);
        PlayerPrefs.SetInt("LittleGuardInHome",1);
    }
}

public enum GuardionActionType{
    idle,
    move,
    attac,
    done,
    none
}
