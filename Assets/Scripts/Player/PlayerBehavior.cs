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
        [SerializeField] private GameObject axeGameObject;

        private PlayerMovement _movement;
        private PlayerAnimation _animation;
        private PlayerInteractionWithObjects _interaction;
        
        private void Start()
        {
            MoveToNextObject();
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
            
            var playerAction = _interaction.GetAction(_animation);

            StartCoroutine(playerAction.DoAnimation((actionCompleted =>
            {
                if (!actionCompleted) return;
                MoveToNextObject();
                
            }), interactableObject));

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
            
            _interaction = new PlayerInteractionWithObjects(axeGameObject);
        }

        #endregion

    }
}
