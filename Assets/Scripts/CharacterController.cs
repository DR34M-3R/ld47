﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public static CharacterController Instance;

    [SerializeField] private Transform spriteTransform;

    private float axis;
    private int lastDirection;
    [SerializeField] private float moveSpeed;

    [Header("Components")] [SerializeField]
    private Animator anim;

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private Collider2D collider;

    [SerializeField] private ActionType state;
    

    public void Awake()
    {
        Instance = this;
        collider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        lastDirection = 1;
    }

    private void Start()
    {
        anim.SetFloat("walkSpeed", moveSpeed / 2.75f);
    }

    private void Update()
    {
        InputUpdate();
    }

    private void FixedUpdate()
    {
        Move();
    }


    private void InputUpdate()
    {
        switch (state)
        {
            case ActionType.none:
                anim.SetBool("walk", axis != 0);
                
                SetDirection(Input.GetAxis("Horizontal"));
                
                anim.SetFloat("walkSpeed", Math.Abs(axis) * moveSpeed / 2.75f);

                if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
                    axis = 0;

                if (Input.GetKeyDown(KeyCode.E))
                    Use();

                if (Input.GetKeyDown(KeyCode.R))
                    GunShot();

                break;
            case ActionType.action:
                axis = 0;
                break;
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(axis * moveSpeed, rb.velocity.y);
    }

    private void SetDirection(float Dir)
    {
        axis = Dir;

        if (axis < 0 || axis > 0)
        {
            var flip = axis < 0;
            lastDirection = flip ? -1 : 1;
            spriteTransform.rotation = Quaternion.Euler(0, flip ? 180 : 0, 0);
        }
    }

    private void GunShot()
    {
        StartCoroutine(Delay(2f));
        anim.SetTrigger("GunShot");
        var rayCastOffsetPoint = new Vector3(0.5f * lastDirection, 0.4f, 0);
        var hit = Physics2D.Raycast(transform.position + rayCastOffsetPoint,transform.right, 5f);
        Debug.DrawRay(transform.position,transform.right);
        
        hit.transform.GetComponent<GuardScript>()?.StartCoroutine("Dead");
        
        Debug.Log(hit.transform.name);
    }

    private void Use()
    {
        var item = GetUsableComponentInTouch();
        if (item != null)
        {
            StartCoroutine(Using(item, 0.696f));
            anim.SetTrigger("Take");
        }
    }

    private GameObject GetUsableComponentInTouch()
    {
        var results = new List<Collider2D>();
        var filter = new ContactFilter2D();
        filter.layerMask = LayerMask.GetMask("Usable");
        filter.useLayerMask = true;
        filter.useTriggers = true;
        collider.OverlapCollider(filter, results);

        return results.Count > 0 ? results[0]?.gameObject : null;
    }

    private IEnumerator Delay(float delayTime)
    {
        state = ActionType.action;
        yield return new WaitForSeconds(delayTime);
        state = ActionType.none;
    }

    private IEnumerator Using(GameObject item, float delayTime)
    {
        state = ActionType.action;
        yield return new WaitForSeconds(delayTime);

        item.GetComponent<BaseUsableAction>()?.Run();
        
        state = ActionType.none;
    }
}

public enum ActionType
{
    none,
    action
}