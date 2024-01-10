using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestoryer : MonoBehaviour {
    public float destroyTime;
    void Start() {
        StartCoroutine(DestroyerCo());
    }
    IEnumerator DestroyerCo() {
        yield return new WaitForSeconds(destroyTime);
        Destroy(this.gameObject);
    }
}