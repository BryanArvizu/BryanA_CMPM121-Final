using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class EventManager : MonoBehaviour
{
    // Code taken from Unity Tutorial @ https://unity3d.com/learn/tutorials/topics/scripting/events-creating-simple-messaging-system

    private Dictionary<string, Action<float>> eventDictionary;
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
            eventDictionary = new Dictionary<string, Action<float>>();
    }

    // Start Listening To An Event
    public static void StartListening(string eventName, Action<float> listener)
    {
        Action<float> thisEvent = null;

        // If entry exists, add the listener
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent += listener;
        }
        // if it does not exist, make it.
        else
        {
            thisEvent += listener;
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    // Stop Listening To An Event
    public static void StopListening(string eventName, Action<float> listener)
    {
        if (eventManager == null) return;
        Action<float> thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent -= listener;
        }
    }

    // Trigger An Event
    public static void TriggerEvent(string eventName, float x)
    {
        Action<float> thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(x);
        }
    }
}