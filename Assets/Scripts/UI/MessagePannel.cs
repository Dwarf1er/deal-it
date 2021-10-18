using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePannel : MonoBehaviour {
    public Text text;
    public string message;
    private Vector3 offset = new Vector3(0, 100.0f, 0.0f);
    private Vector3 offsetPosition;
    private Vector3 basePosition;

    void Start() {}

    public void Begin() {
        this.basePosition = this.transform.position;
        this.offsetPosition = this.transform.position + offset;
        this.transform.position = offsetPosition;

        StartCoroutine(Animate());
    }

    private IEnumerator Animate() {
        Transform transform = this.transform;
        transform.position = offsetPosition;

        float steps = 100;
        for(float i = 0; i <= steps; i++) {
            transform.position = Vector3.Lerp(transform.position, basePosition, i / steps);
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(2.0f);

        for(float i = 0; i <= steps; i++) {
            transform.position = Vector3.Lerp(transform.position, offsetPosition, i / steps);
            yield return new WaitForSeconds(0.01f);
        }

        transform.position = basePosition;

        Destroy(this.gameObject);
    }

    void Update() {
        text.text = message;
    }
}
