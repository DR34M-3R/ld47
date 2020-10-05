using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardScript : MonoBehaviour
{
    [SerializeField] List<Transform> PointList = new List<Transform>();
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GuardionActionType actionType;
    [SerializeField] Vector3 TargetPosition;
    [SerializeField] float Axis;
    [SerializeField] float moveSpeed;

    public void Start() {
        rb = GetComponent<Rigidbody2D>();

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

        if(Vector3.Distance(transform.position,TargetPosition) > 0.5f) {

            actionType = GuardionActionType.move;

            if(transform.position.x > TargetPosition.x) {
                Right();
                Axis = -1;
            } else {
                Left();
                Axis = 1;
            }
        } else {
            Axis = 0;
            Idle();
        }

        rb.velocity = new Vector2(Axis * moveSpeed,rb.velocity.y);

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
        Destroy(gameObject);
    }
}

public enum GuardionActionType{
    idle,
    move,
    none
}
