using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{

    public string NameTag = " ";
    public LayerMask Layer;
    public UnityEvent OnEnter;
    public UnityEvent OnExit;
    public UnityEvent OnStay;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(NameTag) || other.gameObject.layer == Layer)
        {
            OnEnter.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(NameTag) || other.gameObject.layer == Layer)
        {

            OnExit.Invoke();
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag(NameTag) || other.gameObject.layer == Layer)
        {

            OnStay.Invoke();
        }
    }

}
