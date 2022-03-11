using System.Collections.Generic;
using UnityEngine;

namespace InteractableObjects
{
    public class ObjectsOnScene
    {
        private static ObjectsOnScene _instance;
        public static ObjectsOnScene objects => _instance ??= new ObjectsOnScene();

        private readonly List<IInteractable> _listOfObjects = new List<IInteractable>();
        private readonly List<IInteractable> _activeObjects = new List<IInteractable>();

        public int count => _activeObjects.Count;
        
        public void HideObject(IInteractable obj)
        {
            _listOfObjects.Add(obj);
            _activeObjects.Remove(obj);
        }

        public T GetObject<T>() where T : IInteractable, new()
        {
            foreach (var createdObj in _listOfObjects)
            {
                if (typeof(T) == createdObj.GetType())
                {
                    _listOfObjects.Remove(createdObj);
                    _activeObjects.Add(createdObj);

                    return (T) createdObj;
                }
            }
            
            var newObj = new T();
            _activeObjects.Add(newObj);
            Debug.Log($"Message: New object created {typeof(T).Name}");

            return newObj;
        }

        public IInteractable GetClosetObject(Vector3 relativePosition)
        {
            float distanceMin = float.PositiveInfinity;
            IInteractable closestObject = null;
            
            foreach (var obj in _activeObjects)
            {
                var distanceToObject = Vector3.Distance(relativePosition, obj.position);

                if (distanceToObject < distanceMin)
                {
                    closestObject = obj;
                    distanceMin = distanceToObject;
                }
            }

            return closestObject;
        }
    }
}