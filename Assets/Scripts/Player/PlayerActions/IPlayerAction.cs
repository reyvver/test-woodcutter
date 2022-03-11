using System;
using System.Collections;
using InteractableObjects;

namespace Player
{
    public interface IPlayerAction
    {
        public IEnumerator DoAnimation(Action<bool> callback, IInteractable obj);
    }
}