using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    // Code taken from Unity Tutorial @ https://unity3d.com/learn/tutorials/topics/scripting/events-creating-simple-messaging-system

    private Dictionary<string, UnityEvent> eventDictionary;
    private static EventManager eventManager;
    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                else
                    eventManager.Init();
            }
            return eventManager;
        }
    }

    // Initialize Event Dictionary
    void Init()
    {
        if (eventDictionary == null)
            eventDictionary = new Dictionary<string, UnityEvent>();
    }

    // Start Listening To An Event
    public static void StartListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;

        // If entry exists, add the listener
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        // if it does not exist, make it.
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    // Stop Listening To An Event
    public static void StopListening(string eventName, UnityAction listener)
    {
        if (eventManager == null) return;
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    // Trigger An Event
    public static void TriggerEvent(string eventName)
    {
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}