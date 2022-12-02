﻿using System;
using Rooms.CardinalDirections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Rooms
{
    [Serializable]
    public class RoomNode
    {
        #region Serialized Fields

        [field: SerializeField] public Vector2Int Index { get; private set; }
        [field: SerializeField] public Room Room { get; set; }
        [field: SerializeField] public int Rank { get; set; }
        [field: SerializeField] public int[] Enemies { get; private set; }
        

        #endregion

        #region Non-Serialized Fields

        [NonSerialized] private RoomNode[] _nodes = new RoomNode[4];

        #endregion

        #region Indexers

        public RoomNode this[Direction dir]
        {
            get => _nodes[(byte)dir];
            set => _nodes[(byte)dir] = value;
        }

        #endregion

        #region Constructors

        public RoomNode(Room room, Vector2Int index, int rank)
        {
            Room = room;
            Index = index;
            Rank = rank;
            Enemies = new int[RoomManager.EnemyDictionary.Count];
            ChooseEnemies();
        }

        #endregion

        #region Private Methods

        private void ChooseEnemies()
        {
            var enemyDict = RoomManager.EnemyDictionary;
            var rankDelta = Rank;
            while (rankDelta > 0)
            {
                var chosenInd = Random.Range(0, enemyDict.GetMaxIndexForRank(rankDelta) + 1);
                Enemies[chosenInd]++;
                rankDelta -= enemyDict.GetRankByIndex(chosenInd);
            }
        }

        #endregion
    }
}