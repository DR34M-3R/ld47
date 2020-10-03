using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour
{
    public static CameraRig Instance;
    private Camera CameraComponent;

    [SerializeField] private Transform PlayerTransform;
    [SerializeField] private float zoomDistance;
    [SerializeField] private float offsetY;
    [SerializeField] private float moveSpeed;
    //[SerializeField] private float Zoffset;


    private void Awake() {
        Instance = this;
    }

    private void Start() {
        CameraComponent = GetComponent<Camera>();

        PlayerTransform = CharacterController.Instance.GetComponent<Transform>();
    }

    private void FixedUpdate() {
        CameraUpdate();
    }

    public void SetZoom(float zoom) {
        zoomDistance = zoom;
    }
    public void SetOffsetY(float offset) {
        offsetY = offset;
    }

    private void CameraUpdate() {
        Vector3 targetPoint = new Vector3(PlayerTransform.transform.position.x,PlayerTransform.transform.position.y + offsetY ,-zoomDistance);
        transform.position = Vector3.Lerp(transform.position,targetPoint,moveSpeed * Time.deltaTime);
    }

}
