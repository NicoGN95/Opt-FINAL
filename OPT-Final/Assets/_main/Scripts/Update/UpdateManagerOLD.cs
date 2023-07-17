using System.Collections.Generic;
using _main.Scripts.Services;
using _main.Scripts.Services.MicroServices.EventsServices;
using UnityEngine;

namespace _main.Scripts.Update
{
    public class UpdateManagerOLD : MonoBehaviour
    {
        private List<IUpdateObject> m_updateObjects;
        private List<IFixedUpdateObject> m_fixedUpdateObjects;

        private static IEventService EventService => ServiceLocator.Get<IEventService>();

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void OnEnable()
        {
            EventService.AddListener<AddListenerUpdateEventData>(AddListenerUpdate);
            EventService.AddListener<RemoveListenerUpdateEventData>(RemoveListenerUpdate);
            
            EventService.AddListener<AddListenerFixedUpdateEventData>(AddListenerFixedUpdate);
            EventService.AddListener<RemoveListenerFixedUpdateEventData>(RemoveListenerFixedUpdate);
        }

        private void OnDisable()
        {
            EventService.RemoveListener<AddListenerUpdateEventData>(AddListenerUpdate);
            EventService.RemoveListener<RemoveListenerUpdateEventData>(RemoveListenerUpdate);
            
            EventService.RemoveListener<AddListenerFixedUpdateEventData>(AddListenerFixedUpdate);
            EventService.RemoveListener<RemoveListenerFixedUpdateEventData>(RemoveListenerFixedUpdate);
        }

        private void Update()
        {
            if (m_updateObjects == default)
                return;
            
            for (var l_i = 0; l_i < m_updateObjects.Count; l_i++)
            {
                m_updateObjects[l_i].MyUpdate();
            }
        }

        private void FixedUpdate()
        {
            if (m_fixedUpdateObjects == default)
                return;
            
            for (var l_i = 0; l_i < m_fixedUpdateObjects.Count; l_i++)
            {
                m_fixedUpdateObjects[l_i].MyFixedUpdate();
            }
        }

        private void AddListenerUpdate(AddListenerUpdateEventData p_data)
        {
            m_updateObjects ??= new List<IUpdateObject>();

            if (!m_updateObjects.Contains(p_data.UpdateObject))
                m_updateObjects.Add(p_data.UpdateObject);
        }
        
        private void AddListenerFixedUpdate(AddListenerFixedUpdateEventData p_data)
        {
            m_fixedUpdateObjects ??= new List<IFixedUpdateObject>();

            if (!m_fixedUpdateObjects.Contains(p_data.UpdateObject))
                m_fixedUpdateObjects.Add(p_data.UpdateObject);
        }

        private void RemoveListenerUpdate(RemoveListenerUpdateEventData p_data)
        {
            if (m_updateObjects.Contains(p_data.UpdateObject))
                m_updateObjects.Remove(p_data.UpdateObject);

            if (m_updateObjects.Count <= 0)
                m_updateObjects = default;
        }
        private void RemoveListenerFixedUpdate(RemoveListenerFixedUpdateEventData p_data)
        {
            if (m_fixedUpdateObjects.Contains(p_data.UpdateObject))
                m_fixedUpdateObjects.Remove(p_data.UpdateObject);

            if (m_fixedUpdateObjects.Count <= 0)
                m_fixedUpdateObjects = default;
        }
    }

    public struct AddListenerUpdateEventData : ICustomEventData
    {
        public IUpdateObject UpdateObject { get; }
        
        public AddListenerUpdateEventData(IUpdateObject p_updateObject)
        {
            UpdateObject = p_updateObject;
        }
    }
    
    public struct RemoveListenerUpdateEventData : ICustomEventData
    {
        public IUpdateObject UpdateObject { get; }
        
        public RemoveListenerUpdateEventData(IUpdateObject p_updateObject)
        {
            UpdateObject = p_updateObject;
        }
    }
    
    public struct AddListenerFixedUpdateEventData : ICustomEventData
    {
        public IFixedUpdateObject UpdateObject { get; }
        
        public AddListenerFixedUpdateEventData(IFixedUpdateObject p_updateObject)
        {
            UpdateObject = p_updateObject;
        }
    }
    
    public struct RemoveListenerFixedUpdateEventData : ICustomEventData
    {
        public IFixedUpdateObject UpdateObject { get; }
        
        public RemoveListenerFixedUpdateEventData(IFixedUpdateObject p_updateObject)
        {
            UpdateObject = p_updateObject;
        }
    }
}