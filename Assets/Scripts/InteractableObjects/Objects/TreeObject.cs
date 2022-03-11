using System;
using UnityEngine;

namespace InteractableObjects
{
    public class TreeObject : IInteractable
    {
        public Vector3 position => _treeObject.transform.position;
        public GameObject gameObject => _treeObject;
        public TreeGameObject treeScript { get; private set; }
        public event Action<TreeObject> treeDestroyed; 

        private const string GameObjectName = "Tree";
        private GameObject _treeObject;
        private MeshRenderer visual;

        public void Create(GameObject prefab, Transform parent, Vector3 objPosition)
        {
            if (_treeObject != null)
            {
                gameObject.transform.position = objPosition;
                visual.enabled = true;
                treeScript.PlayAppearEffect();
                
                return;
            }
            
            _treeObject = ObjectManager.CreateNewObject(prefab, parent, objPosition);
            treeScript = _treeObject.GetComponent<TreeGameObject>();
            visual = _treeObject.GetComponent<MeshRenderer>();
            _treeObject.name = GameObjectName;
        }

        public void Hide()
        {
            treeDestroyed?.Invoke(this);
            
            treeScript.PlayDisappearEffect();
            visual.enabled = false;
        }
        
        public void Interact()
        {
            ObjectsOnScene.objects.HideObject(this);
        }
        
    }
}