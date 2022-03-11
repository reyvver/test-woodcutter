using InteractableObjects;
using UnityEngine;

namespace Player
{
    public class PlayerInteractionWithObjects
    {
        private GameObject _axe;
        private TreeAction _chopTree;
        private WoodAction _collectWood;
        private PlayerLogPlacement _placement;
        
        public PlayerInteractionWithObjects(GameObject axe, PlayerLogPlacement placement)
        {
            _axe = axe;
            _placement = placement;
        }
        public IInteractable closestObject { get; private set; }
        public IInteractable GetClosestObject(Vector3 relativePosition)
        {
            ClearInteraction();
            
            closestObject = ObjectsOnScene.objects.GetClosetObject(relativePosition);
            return closestObject;
        }
        public IPlayerAction GetAction(PlayerAnimation animation)
        {
            switch (closestObject)
            {
                case TreeObject tree:
                {
                    _axe.SetActive(true);

                    return _chopTree ??= new TreeAction(animation);
                }

                case WoodObject wood:
                {
                    return _collectWood ??= new WoodAction(animation, _placement);
                }
            }

            return null;
        }
        private void ClearInteraction()
        {
            _axe.SetActive(false);
        }
    }
}