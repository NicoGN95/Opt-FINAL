using _main.Scripts.Services;
using _main.Scripts.StaticClass;
using UnityEngine;

namespace _main.Scripts.Update
{
    public class UpdateManagerNew : MonoBehaviour
    {
        private static IEventService EventService => ServiceLocator.Get<IEventService>();
        private void Update()
        {
            EventService.DispatchEvent(EventsDefinitions.UPDATE_OBJECT_EVENT_ID);
        }

        private void FixedUpdate()
        {
            EventService.DispatchEvent(EventsDefinitions.FIXED_UPDATE_OBJECT_EVENT_ID);
        }
    }
}