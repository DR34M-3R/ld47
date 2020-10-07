using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondHouseWallController : MonoBehaviour
{
    public GameObject[] parts;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable() {
        GuardScript.DestroyWall += Action;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            foreach (var part in parts)
            {
                part.AddComponent<Rigidbody2D>().velocity = Vector2.left * (10 * (Random.value - 0.5f));
            }
        }
    }

    private void Action() {
        foreach(var part in parts) {
            part.AddComponent<Rigidbody2D>().velocity = Vector2.left * (10 * (Random.value - 0.5f));
        }
    }
}
