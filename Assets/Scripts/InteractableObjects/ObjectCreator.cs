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
        [SerializeField] private GameObject prefabLog;


        private void Awake()
        {
            CreateTrees();
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit) && TreeWithinArea(hit.point))
                CreateTree(hit.point);
        }

        private void CreateTrees()
        {
            if (treesPosition.Count == 0 ) return;
            
            foreach (var treePos in treesPosition)
            {
                CreateTree(treePos.position);
            }
        }

        private void CreateTree(Vector3 position)
        {
            var tree = ObjectsOnScene.objects.GetObject<TreeObject>();
            tree.Create(prefabTree, objectsContainer, position);
        }

        private bool TreeWithinArea(Vector3 targetDestination)
        { 
            return NavMesh.SamplePosition(targetDestination, out NavMeshHit hit, 0.1f, 3);
        }
    }
}