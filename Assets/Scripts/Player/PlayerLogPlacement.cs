using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerLogPlacement : MonoBehaviour
    {
        [SerializeField] private List<GameObject> logs;
        
        public int logPlacedCount { get; private set; }
        
        public void Place()
        {
            logPlacedCount++;
            logs[logPlacedCount-1].SetActive(true);
        }

        public void HideAll()
        {
            foreach (var log in logs)
            {
                log.SetActive(false);
            }

            logPlacedCount = 0;
        }

        #region Plumbing

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            logPlacedCount = 0;
        }


        #endregion

    }
}