using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour {
    public Text text;
    private float currentMoney = 0;
    private PlayerInventory playerInventory;

    private void Start() {
        this.playerInventory = FindObjectOfType<PlayerInventory>();
    }

    private void Update() {
        text.text = "" + (int)currentMoney;

        if(Mathf.Abs(playerInventory.money - currentMoney) < 2) {
            currentMoney = playerInventory.money;
        } else {
            float direction = playerInventory.money > currentMoney ? 1 : -1;
            currentMoney += direction * Mathf.Abs(playerInventory.money - currentMoney) * 10.0f * Time.deltaTime;
        }
    }

    public bool HasDistance() {
        return false;
    }

    public Transform GetTransform() {
        return transform;
    }
}
