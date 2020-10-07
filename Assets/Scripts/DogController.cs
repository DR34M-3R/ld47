using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController:MonoBehaviour {
    public Transform TargetPoint;
    public Transform LittleGuard;

    public float moveSpeed;
    public float lerpColorSpeed;

    public DogState dogState = DogState.moveToGuard;

    public Rigidbody2D rb;

    public Animator anim;

    public Color alphaColor;

    public float timeToBark;

    public GuardScript guardScript;

    public void OnEnable() {
        CharacterController.Shot += BarkDecline;
    }

    public void OnDisable() {
        CharacterController.Shot -= BarkDecline;
    }

    public void Update() {
        switch(dogState) {
            case DogState.moveToGuard:
                MoveToGuard();
                break;
            case DogState.moveOut:
                MoveOut();
                break;

        }
    }

    public void StartEvent() {
        dogState = DogState.moveToGuard;
    }

    public void MoveToGuard() {
        if(Vector3.Distance(transform.position,LittleGuard.position) > 2f) {
            if(LittleGuard.position.x > transform.position.x) {
                MoveRight();
            } else {
                MoveLeft();
            }
        } else {
            StartCoroutine(BarkDelay());
            dogState = DogState.bark;
        }
    }

    public void MoveLeft() {
        rb.velocity = new Vector2(-moveSpeed,rb.velocity.y);
        Left();
    }
    public void MoveRight() {
        rb.velocity = new Vector2(moveSpeed,rb.velocity.y);
        Right();
    }

    public void Left() {
        transform.rotation = Quaternion.Euler(0,-180f,0);
    }

    public void Right() {
        transform.rotation = Quaternion.Euler(0,0,0);
    }

    public IEnumerator BarkDelay() {

        yield return new WaitForSeconds(timeToBark);
        anim.SetTrigger("Bark");
        //guardScript.Dead();
    }

    public void BarkDecline() { 
        if(dogState == DogState.bark){
            if(20f > Vector3.Distance(transform.position,CharacterController.Instance.transform.position)) {
                StopCoroutine(BarkDelay());
                StartCoroutine(MoveOutDelay());
            }
        }
    }

    public IEnumerator MoveOutDelay() {

        yield return new WaitForSeconds(1f);//
        guardScript.GoHome();
        dogState = DogState.moveOut;

    }
        public void MoveOut() {
        if(Vector3.Distance(transform.position,LittleGuard.position) > 2f) {
            if(TargetPoint.position.x > transform.position.x) {
                MoveRight();
            } else {
                MoveLeft();
            }
        } else {
            Destroy(gameObject);
        }

        GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color,alphaColor,lerpColorSpeed * Time.deltaTime);
    }

}

public enum DogState {
    none,
    moveToGuard,
    bark,
    moveOut
}

