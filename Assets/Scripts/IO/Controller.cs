using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
    public string horizontalInput;
    public string verticalInput;

    public float GetHorizontal() {
        return Input.GetAxis(horizontalInput);
    }

    public float GetVertical() {
        return Input.GetAxis(verticalInput);
    }
}
