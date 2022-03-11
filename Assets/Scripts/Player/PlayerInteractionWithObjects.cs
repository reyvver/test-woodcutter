using InteractableObjects;
using UnityEngine;

namespace Player
{
    public class PlayerInteractionWithObjects
    {
        public IInteractable closestObject { get; private set; }
        public IInteractable GetClosestObject(Vector3 relativePosition)
        {
            closestObject = ObjectsOnScene.objects.GetClosetObject(relativePosition);
            return closestObject;
        }

    }
}