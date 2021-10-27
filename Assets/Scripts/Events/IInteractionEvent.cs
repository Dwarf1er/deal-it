public interface IInteractionEvent : IEvent {
    AbstractHuman GetFrom();
    AbstractHuman GetTo();
}
