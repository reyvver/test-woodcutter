using UnityEngine;

namespace InteractableObjects
{
    public class WoodObject : IInteractable
    {
        public GameObject gameObject => _woodObject;
        public Vector3 position => gameObject.transform.position;
        public int interactionPriority => (int)ObjectType;
        
        private GameObject _woodObject;
        private const InteractableObjectType ObjectType = InteractableObjectType.Wood;
        private const string GameObjectName = "Wood";

        public void Create(GameObject prefab, Transform parent, Vector3 objPosition)
        {
            if (_woodObject != null)
            {
                gameObject.transform.position = objPosition;
                gameObject.SetActive(true);

                return;
            }
            
            _woodObject = ObjectManager.CreateNewObject(prefab, parent, objPosition);
            _woodObject.name = GameObjectName;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Interact()
        {
            ObjectsOnScene.objects.HideObject(this);
        }
    }
}