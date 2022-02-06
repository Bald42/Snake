using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace DataBase
{
    [CreateAssetMenu(fileName = "GameResourcesDB", menuName = "GameScriptableObjects/GameResourcesDB", order = 1)]
    public class GameResourcesDB : ScriptableObject
    {
        [SerializeField] private Point point = null;

        public Point Point
        {
            get
            {
                return point;
            }
        }
    }
}