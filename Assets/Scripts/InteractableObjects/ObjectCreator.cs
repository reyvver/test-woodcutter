using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace InteractableObjects
{
    public class ObjectCreator : MonoBehaviour
    {
        [SerializeField] private Transform objectsContainer;
        [SerializeField] private List<Transform> treesPosition;
        [Space]
        [SerializeField] private GameObject prefabTree;
        [SerializeField] private GameObject prefabWood;
        
        private void Awake()
        {
            CreateTrees();
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit) && TreeWithinArea(hit.point))
                CreateTree(hit.point, true);
        }

        private void CreateTrees()
        {
            if (treesPosition.Count == 0 ) return;
            
            foreach (var treePos in treesPosition)
            {
                CreateTree(treePos.position);
            }
        }

        private void CreateTree(Vector3 position, bool playEffect = false)
        {
            var tree = ObjectsOnScene.objects.GetObject<TreeObject>();
            tree.Create(prefabTree, objectsContainer, position);
            tree.TreeDestroyed += CreateWood;
            
            if (playEffect)
                tree.PlayAppearEffect();
        }
        
        private void CreateWood(TreeObject tree)
        {
            tree.TreeDestroyed -= CreateWood;

            foreach (var woodPos in tree.treeScript.wood)
            {
                CreateLog(woodPos.position, woodPos.rotation);
            }
        }

        private void CreateLog(Vector3 position, Quaternion rotation)
        {
            var log = ObjectsOnScene.objects.GetObject<WoodObject>();
            log.Create(prefabWood, objectsContainer, position);
            log.gameObject.transform.rotation = rotation;
        }

        private bool TreeWithinArea(Vector3 targetDestination)
        { 
            return NavMesh.SamplePosition(targetDestination, out NavMeshHit hit, 0.1f, 3);
        }
    }
}