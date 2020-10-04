using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardScript : MonoBehaviour
{
    

    public IEnumerator Dead() {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
