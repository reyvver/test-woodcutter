using System;
using UnityEngine;

namespace InteractableObjects
{
    public class TreeObject : IInteractable
    {
        public Vector3 position => _treeObject.transform.position;
        public GameObject gameObject => _treeObject;
        public TreeGameObject treeScript { get; private set; }
        public event Action<TreeObject> TreeDestroyed; 

        private const string GameObjectName = "Tree";
        private GameObject _treeObject;
        private MeshRenderer _visual;

        public void Create(GameObject prefab, Transform parent, Vector3 objPosition)
        {
            if (_treeObject != null)
            {
                gameObject.transform.position = objPosition;
                treeScript.CollisionEnabled = true;
                _visual.enabled = true;

                return;
            }
            
            _treeObject = ObjectManager.CreateNewObject(prefab, parent, objPosition);
            _treeObject.name = GameObjectName;

            treeScript = _treeObject.GetComponent<TreeGameObject>();
            treeScript.CollisionEnabled = true;
            
            _visual = _treeObject.GetComponent<MeshRenderer>();
        }

        public void Hide()
        {
            TreeDestroyed?.Invoke(this);
            
            treeScript.PlayDisappearEffect();
            treeScript.CollisionEnabled = false;
            
            _visual.enabled = false;
        }
        
        public void Interact()
        {
            ObjectsOnScene.objects.HideObject(this);
        }

        public void PlayAppearEffect()
        {
            treeScript.PlayAppearEffect();
        }
        
    }
}