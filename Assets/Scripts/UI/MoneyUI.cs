using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour, ISubscriber {
    public Text text;
    private int money = 0;
    private float currentMoney = 0;

    private void Start() {
        EventManager.Get()
            .Subscribe((DealEndEvent dealEvent) => OnDealEnd(dealEvent))
            .Subscribe((InteractEndEvent interactEvent) => OnInteractEnd(interactEvent))
            .Subscribe((QuestEndEvent questEvent) => OnQuestEnd(questEvent));
    }

    private void OnDestroy() {
        EventManager.Get().UnSubcribeAll(this);
    }

    private void Update() {
        text.text = "" + (int)currentMoney;

        if(Mathf.Abs(money - currentMoney) < 2) {
            currentMoney = money;
        } else {
            float direction = money > currentMoney ? 1 : -1;
            currentMoney += direction * Mathf.Abs(money - currentMoney) * 10.0f * Time.deltaTime;
        }
    }

    private void OnDealEnd(DealEndEvent dealEvent) {
        if(dealEvent.IsCancelled()) return;
        // money += 25;
    }

    private void OnQuestEnd(QuestEndEvent questEvent) {
        money += questEvent.GetQuest().reward;
    }

    private void OnInteractEnd(InteractEndEvent interactEvent) {
        if(interactEvent.GetTo() is Trash) {
            money += Random.Range(0, 10);
        }
    }

    public bool HasDistance() {
        return false;
    }

    public Transform GetTransform() {
        return transform;
    }
}
