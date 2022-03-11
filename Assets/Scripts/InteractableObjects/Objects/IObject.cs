using UnityEngine;

namespace InteractableObjects
{
    public interface IObject
    {
        public GameObject gameObject { get; }
        public Vector3 position { get; }
        public void Create(GameObject prefab, Transform parent, Vector3 objPosition);
        public void Hide();
    }
}