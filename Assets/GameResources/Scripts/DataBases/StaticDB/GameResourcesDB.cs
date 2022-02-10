using UnityEngine;

namespace DataBase
{
    [CreateAssetMenu(fileName = "GameResourcesDB", menuName = "GameScriptableObjects/GameResourcesDB", order = 1)]
    public class GameResourcesDB : ScriptableObject
    {
        [SerializeField] private ObjectPoint objectPoint = null;

        public ObjectPoint ObjectPoint
        {
            get
            {
                return objectPoint;
            }
        }
    }
}