using UnityEngine;

public class Student : StateHuman, IInteractable, IDealable {
    private IEndEvent endEvent;
    private bool canDeal = false;
    [SerializeField] private int skinIndex = -1;

    protected override void Start() {
        base.Start();

        EventManager.Get()
            .Subscribe((ToggleEvent toggleEvent) => OnToggle(toggleEvent))
            .Subscribe((ClassStartEvent classEvent) => OnClassStart(classEvent))
            .Subscribe((DealStartEvent dealEvent) => OnDealStart(dealEvent))
            .Subscribe((ClassEndEvent classEvent) => OnClassEnd(classEvent))
            .Subscribe((DealEndEvent dealEvent) => OnDealEnd(dealEvent));
    }

    protected override string GetTextureName() {
        if(skinIndex == -1) {
            this.skinIndex = Random.Range(2, 6);
        }

        return "student" + skinIndex;
    }

    public override float GetSpeed() {
        return 0.75f;
    }

    public override State GetBaseState() {
        return new IdleState(this);
    }

    private void OnClassStart(ClassStartEvent classEvent) {
        endEvent = classEvent.GetEndEvent();

        Vector3 position = classEvent.GetPosition();

        this.SetNextState(new SequenceState(this, new State[]{
            new GotoState(this, position),
            new GotoObjectState(this, "Chair"),
            new LookAtState(this, Vector2.down)
        }));
    }

    public bool IsDealable() {
        return canDeal && endEvent == null;
    }

    public bool IsInteractable() {
        return !canDeal;
    }

    private void OnToggle(ToggleEvent toggleEvent) {
        if(!toggleEvent.GetTarget().Equals(transform)) return;
        canDeal = !canDeal;
    }

    private void OnDealStart(DealStartEvent dealEvent) {
        if(!dealEvent.GetTo().Equals(this)) return;

        endEvent = dealEvent.GetEndEvent();

        this.SetNextState(new LookAtState(this, dealEvent.GetFrom().GetTransform()));
    }

    private void OnEndEvent(IEndEvent endEvent) {
        if(this.endEvent != endEvent) return;

        this.endEvent = null;

        this.SetNextState(new IdleState(this));
    }

    private void OnClassEnd(ClassEndEvent classEvent) {
        OnEndEvent(classEvent);
    }

    private void OnDealEnd(DealEndEvent dealEvent) {
        OnEndEvent(dealEvent);
    }
}
