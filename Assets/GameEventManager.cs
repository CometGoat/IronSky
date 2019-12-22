using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*   ----- Game Event Manager ------
This script is responsible for receiving and dispatching delegate calls to any other classes.
For now, this can be used for any kinda delegate related to UI and any other non gameplay systems.
(things like UI, Settings, Save Systems ...).

 
NOTE by SAM: I'm at the moment not sure if it's a smart idea to use this EventManager for gameplay
delegates, we might want to create one specifically for it.
*/

/*  ----- How to use it ------

-- Adding an Event to the Event Manager (this script)

    public event Action onEventExample; // Declare the Event (or delegate or action or whatevs)
    public void EventExample() // Declaring this methods allows us to add some logic between senders and listeners.
    {
        if (onSettingsRefreshed != null) // Check there are some listeners subscribed to the event
        {
            onSettingsRefreshed(); // Call the Event
        }
    }


-- Call an event (as sender) 

    GameEventManager.current.onEventExample();


-- Subscribe as a listener to this event from any other scripts

    GameEventManager.current.onEventExample += MethodToExecute;

-- Unsubscribe from this event (NEEDS TO BE DONE BEFORE DESTROYING ANY SUBSCRIBED OBJECTS)

    GameEventManager.current.onEventExample -= MethodToExecute;


NOTE: You can also pass in parameters/arguments through events. It's done by adding <type> after the Action declaration.
See examples in the script.

*/

public class GameEventManager : MonoBehaviour
{

    public static GameEventManager current;

    private void Awake()
    {
        current = this;
    }

    //////////////////
    /// SETTINGS  ////
    //////////////////
    
    public event Action onSettingsRefreshed;
    public void SettingsRefreshed()
    {
        if (onSettingsRefreshed != null)
        {
            onSettingsRefreshed();
        }
    }

    public event Action<float> onVolumeChanged;
    public void VolumeChanged(float volume)
    {
        if (onVolumeChanged != null)
        {
            VolumeChanged(volume);
        }
    }

    public event Action<bool> onRumbleEnabledChanged;
    public void RumbleEnabledChanged(bool rumbleEnabled)
    {
        if (onRumbleEnabledChanged != null)
        {
            RumbleEnabledChanged(rumbleEnabled);
        }
    }

    public event Action<int> onGraphicQualityChanged;
    public void GraphicQualityChanged(int GraphicQualityIndex)
    {
        if (onGraphicQualityChanged != null)
        {
            GraphicQualityChanged(GraphicQualityIndex);
        }
    }
}
