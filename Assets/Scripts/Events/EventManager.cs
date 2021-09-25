using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
    public void Start() {
        StartCoroutine("Routine");
    }

    private IEnumerator Routine() {
        yield return new WaitForSeconds(5.0f);
        Debug.Log("Class started");
        this.ClassStart();
        yield return new WaitForSeconds(30.0f);
        Debug.Log("Class ended");
        this.ClassEnd();
    }

    public event Action onClassStart;
    public void ClassStart() {
        if(onClassStart != null) onClassStart();
    }

    public event Action onClassEnd;
    public void ClassEnd() {
        if(onClassStart != null) onClassEnd();
    }
}
