using UnityEngine;

namespace InteractableObjects
{
    public interface IInteractable : IObject
    {
        public int interactionPriority { get; }
        public void Interact();
    }

    public interface IObject
    {
        public GameObject gameObject { get; }
        public Vector3 position { get; }
        public void Create(GameObject prefab, Transform parent, Vector3 objPosition);
        public void Hide();
    }

    public enum InteractableObjectType
    {
        Tree = 0, // priority = 0
        Wood = 1 // priority = 1
    }

}