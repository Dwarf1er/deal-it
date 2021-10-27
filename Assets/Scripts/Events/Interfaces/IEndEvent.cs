public interface IEndEvent : IEvent {
    IEvent GetStartEvent();
}
