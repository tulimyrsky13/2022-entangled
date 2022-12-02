﻿using System.Collections;
using Cinemachine;
using UnityEngine;

namespace Rooms
{
    public class Room : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField] private int _inPriority;
        [SerializeField] private int _outPriority;
        [SerializeField] private CinemachineVirtualCamera _vCam;
        
        [field: SerializeField] public GameObject RoomContent { get; private set; }
        [field: SerializeField] public Transform Enemies { get; private set; }

        #endregion

        #region Non-Serialized Fields

        private Collider2D _collider;

        #endregion

        #region Properties

        [field: SerializeField]public RoomNode Node { get; set; }

        #endregion

        #region Function Events

        private void OnTriggerEnter2D(Collider2D col)
        {
            RoomManager.ChangeRoom(Node);
        }

        #endregion

        #region Public Methods

        public void Enter()
        {
            _vCam.Priority = _inPriority;
            RoomContent.SetActive(true);
            for (int i = 0; i < Enemies.childCount; ++i)
                Enemies.GetChild(i).gameObject.SetActive(true);
        }

        public void Exit(float sleepDelay = 0)
        {
            _vCam.Priority = _outPriority;
            if (sleepDelay <= 0)
                RoomContent.SetActive(false);
            else
                StartCoroutine(SleepWithDelay(sleepDelay));
        }

        public void ReplaceEnemies()
        {
            for (int i = 0; i < Enemies.childCount; ++i)
                Destroy(Enemies.GetChild(i).gameObject);
        }

        #endregion

        #region Private Methods

        private IEnumerator SleepWithDelay(float sleepDelay)
        {
            yield return new WaitForSeconds(sleepDelay);
            RoomContent.SetActive(false);
        }

        #endregion
    }
}