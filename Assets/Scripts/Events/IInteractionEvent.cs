public interface IInteractionEvent<T, U> : IEvent {
    T GetFrom();
    U GetTo();
}
