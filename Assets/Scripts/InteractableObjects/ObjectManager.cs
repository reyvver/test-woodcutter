using UnityEngine;

namespace InteractableObjects
{
    public static class ObjectManager
    {
        public static GameObject CreateNewObject(GameObject prefab, Transform parent, Vector3 position)
        {
            GameObject newObject = Object.Instantiate(prefab, parent);
            newObject.transform.position = position;
            return newObject;
        }
    }
}