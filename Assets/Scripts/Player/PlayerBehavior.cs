using InteractableObjects;
using UnityEngine;
using UnityEngine.AI;


namespace Player
{
    public class PlayerBehavior : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Animator animator;
        [Space]
        [SerializeField] private Transform startPoint;

        private PlayerMovement _movement;
        private PlayerAnimation _animation;
        private PlayerInteractionWithObjects _interaction;

        private TreeAction _chopTree;
        private Coroutine interaction;

        private void Awake()
        {
            Init();
        }

        private void Start()
        {
            MoveToNextObject();
        }

        void Init()
        {
            _movement = new PlayerMovement(agent);
            _movement.PlayerStopped += OnDestinationReached;
            _movement.PlaceAt(startPoint.position, startPoint.rotation);
            
            _animation = new PlayerAnimation(animator);
            _animation.PlayAnimation(PlayerAnimation.AnimationType.Idle);
            
            _interaction = new PlayerInteractionWithObjects();
            _chopTree = new TreeAction(_animation);
        }

        void MoveToNextObject()
        {
            var closestInteractableObject = _interaction.GetClosestObject(agent.transform.position);
            var destination = startPoint.position;

            if (closestInteractableObject != null)
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

        void OnDestinationReached()
        {
            var interactableObject = _interaction.closestObject;
            
            if (interactableObject == null)
            {
                _animation.PlayAnimation(PlayerAnimation.AnimationType.Idle);
                return;
            }
            
            if (interaction != null)
                StopCoroutine(interaction);
            
            IPlayerAction action = null;

            switch (interactableObject)
            {
                case TreeObject tree:
                {
                    action = _chopTree;
                    break;
                }
            }

            StartCoroutine(action.DoAnimation((myReturnValue) => {
                if (myReturnValue)
                {
                    interactableObject.Interact();
                    MoveToNextObject();
                }
            }));

        }

    }
}
