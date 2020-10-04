using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public static CharacterController Instance;

    [SerializeField] private Transform SpriteTransform;

    [Header("Var")] [SerializeField] public float Axis;
    [SerializeField] private float moveSpeed;

    [Header("Components")] [SerializeField]
    private Animator anim;

    [SerializeField] private Rigidbody2D RB;

    [SerializeField] private Collider2D collider;

    [Header("State")] [SerializeField] private ActionType SetAction;


    public void Awake()
    {
        Instance = this;
        collider = GetComponent<Collider2D>();
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
        switch (SetAction)
        {
            case ActionType.none:
                if (Axis != 0)
                {
                    anim.SetBool("walk", true);
                }
                else
                {
                    anim.SetBool("walk", false);
                }

                SetDirection(Input.GetAxis("Horizontal"));

                if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
                    Axis = 0;

                if (Input.GetKeyDown(KeyCode.E))
                    Use();

                if (Input.GetKeyDown(KeyCode.R))
                    GunShot();

                break;
            case ActionType.action:
                Axis = 0;
                break;
        }
    }

    private void Move()
    {
        RB.velocity = new Vector2(Axis * moveSpeed, RB.velocity.y);
    }

    private void SetDirection(float Dir)
    {
        Axis = Dir;

        if (Axis < 0 || Axis > 0)
        {
            SpriteTransform.rotation = Quaternion.Euler(0, Axis < 0 ? 180 : 0, 0);
        }
    }

    public void GunShot()
    {
        StartCoroutine(Delay(2f));
        anim.SetTrigger("GunShot");
    }

    public void Use()
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

    IEnumerator Delay(float delayTime)
    {
        SetAction = ActionType.action;
        yield return new WaitForSeconds(delayTime);
        SetAction = ActionType.none;
    }

    IEnumerator Using(GameObject item, float delayTime)
    {
        SetAction = ActionType.action;
        yield return new WaitForSeconds(delayTime);

        item.GetComponent<BaseUsableAction>()?.Run();
        
        SetAction = ActionType.none;
    }
}

public enum ActionType
{
    none,
    action
}