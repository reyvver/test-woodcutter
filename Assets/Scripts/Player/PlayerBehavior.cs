using System.Collections;
using UnityEngine;
using UnityEngine.AI;


namespace Player
{
    [RequireComponent(typeof(PlayerLogPlacement))]
    public class PlayerBehavior : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Animator animator;
        [Space]
        [SerializeField] private Transform startPoint;
        [SerializeField] private GameObject axeGameObject;

        private PlayerMovement _movement;
        private PlayerAnimation _animation;
        private PlayerLogPlacement _placement;
        private PlayerInteractionWithObjects _interaction;
        
        private void Start()
        {
            MoveToNextObject();
        }

        void MoveToNextObject()
        {
            var closestInteractableObject = _interaction.GetClosestObject(agent.transform.position);
            var destination = startPoint.position;
            
            if (closestInteractableObject != null &&  _placement.logPlacedCount != 3)
            {
                destination = closestInteractableObject.position;
            }

            StartMovementTo(destination);
        }

        void StartMovementTo(Vector3 destination)
        {
            _animation.PlayAnimation(PlayerAnimation.AnimationType.Walk);
            _movement.MoveTo(destination);
            
            StartCoroutine(_movement.WaitToStop());
        }

        void OnDestinationReached(Vector3 destination)
        {
            if (destination == startPoint.position)
            {
                _placement.HideAll();
                _animation.PlayAnimation(PlayerAnimation.AnimationType.Idle);

                if (_interaction.closestObject != null)
                {
                    MoveToNextObject();
                    return;
                }
                StartCoroutine(WaitObjectToAppear());
            }

            InteractWithObject();
        }

        void InteractWithObject()
        {
            var interactableObject = _interaction.closestObject;
            if (_interaction.closestObject == null) return;
            
            var playerAction = _interaction.GetAction(_animation);

            StartCoroutine(_movement.LookAtInteractableObject(interactableObject.position));
            StartCoroutine(playerAction.DoAnimation((actionCompleted =>
            {
                if (!actionCompleted) return;
                MoveToNextObject();
                
            }), interactableObject));
        }

        IEnumerator WaitObjectToAppear()
        {
            yield return new WaitUntil(() => _interaction.newObjectAppeared);
            MoveToNextObject();
        }
        
        #region Plumbing

        private void Awake()
        {
            Init();
        }
        
        void Init()
        {
            _movement = new PlayerMovement(agent);
            _movement.PlayerStopped += OnDestinationReached;
            _movement.PlaceAt(startPoint.position, startPoint.rotation);
            
            _animation = new PlayerAnimation(animator);
            _animation.PlayAnimation(PlayerAnimation.AnimationType.Idle);

            _placement = gameObject.GetComponent<PlayerLogPlacement>();
            _placement.HideAll();
            _interaction = new PlayerInteractionWithObjects(axeGameObject,_placement);
        }

        #endregion

    }
}
