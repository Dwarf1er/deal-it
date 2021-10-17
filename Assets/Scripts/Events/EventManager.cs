using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
    private Dictionary<object, HashSet<object>> subscriberEventHandlers;
    private Dictionary<object, object> eventHandlerSubcribers;
    private static EventManager eventManager;

    protected void Awake() {
        if(eventManager) Destroy(this);
        eventManager = this;

        this.subscriberEventHandlers = new Dictionary<object, HashSet<object>>();
        this.eventHandlerSubcribers = new Dictionary<object, object>();
    }

    protected void Start() {}

    /// Gets singleton instance of class.
    public static EventManager Get() {
        return eventManager;
    }
    
    /// Subscribe to a type of event.
    public void Subscribe<T>(Action<T> eventHandler) where T: IEvent {
        object subscriber = eventHandler.Target;

        if(!(subscriber is ISubscriber)) throw new TypeLoadException("Subscriber needs to be instance of ISubscriber.");

        if(!this.subscriberEventHandlers.ContainsKey(subscriber)) {
            this.subscriberEventHandlers[subscriber] = new HashSet<object>();
        }

        this.subscriberEventHandlers[subscriber].Add(eventHandler);
        this.eventHandlerSubcribers[eventHandler] = eventHandler.Target;
    }

    private IEnumerator BroadcastDelayedEnumerator<T>(T eventObject, float seconds) where T: IEvent {
        yield return new WaitForSeconds(seconds);

        BroadcastAll(eventObject);
    }

    /// Sends an event to all subscribers after delay in seconds.
    public void BroadcastDelayed<T>(T eventObject, float seconds) where T: IEvent {
        StartCoroutine(this.BroadcastDelayedEnumerator(eventObject, seconds));
    }

    /// Sends an event to all subscribers in certain distance from origin.
    public void BroadcastRange<T>(T eventObject, Vector3 origin, float distance) where T: IEvent {
        foreach(object subscriber in subscriberEventHandlers.Keys) {
            if(subscriber is ISubscriber) {
                ISubscriber castedSubscriber = subscriber as ISubscriber;
                Transform transform = castedSubscriber.GetTransform();

                if(Vector3.Distance(origin, transform.position) <= distance) {
                    HashSet<object> eventHandlers = subscriberEventHandlers[subscriber];

                    BroadcastList(eventObject, eventHandlers);
                }
            }
        }
    }

    private void BroadcastList<T, U>(T eventObject, ICollection<U> eventHandlers) where T: IEvent {
        foreach(object eventHandler in eventHandlers) {
            if(eventHandler is Action<T>) {
                Action<T> castedEventHandler = eventHandler as Action<T>;
                castedEventHandler(eventObject);
            }
        }
    }

    /// Sends an event to all subscribers.
    public void BroadcastAll<T>(T eventObject) where T: IEvent {
        BroadcastList(eventObject, eventHandlerSubcribers.Keys);
    }

    /// Unsubscribe from event type.
    public void UnSubscribe<T>(Action<T> eventHandler) where T: IEvent {
        object subscriber = eventHandler.Target;
        this.subscriberEventHandlers[subscriber].Remove(eventHandler);
        this.eventHandlerSubcribers.Remove(eventHandler);
    }

    /// Unsubscribe all event types for a subscriber.
    /// NOTE: Needs to be used when a subscriber is destroyed!
    public void UnSubcribeAll(object subscriber) {
        if(!subscriberEventHandlers.ContainsKey(subscriber)) return;

        foreach(object eventHandler in subscriberEventHandlers[subscriber]) {
            this.eventHandlerSubcribers.Remove(eventHandler);
        }

        this.subscriberEventHandlers.Remove(subscriber);
    }
}
