using UnityEngine;

namespace InteractableObjects
{
    public class TreeObject : IInteractable
    {
        public int interactionPriority => (int) ObjectType;
        public Vector3 position => _treeObject.transform.position;
        public GameObject gameObject => _treeObject;

        private const InteractableObjectType ObjectType = InteractableObjectType.Tree;
        private const string GameObjectName = "Tree";

        private GameObject _treeObject;

        public void Create(GameObject prefab, Transform parent, Vector3 objPosition)
        {
            if (_treeObject != null)
            {
                gameObject.transform.position = objPosition;
                gameObject.SetActive(true);
                return;
            }
            
            _treeObject = ObjectManager.CreateNewObject(prefab, parent, objPosition);
            
            _treeObject.name = GameObjectName;
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