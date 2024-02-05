using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasDestroyer : MonoBehaviour
{
    public float lifeTime;

    void Start()
    {
        Destroy(this.gameObject.GetComponent<Canvas>(), lifeTime);
        Destroy(this.gameObject, lifeTime + 1f);
    }
}
