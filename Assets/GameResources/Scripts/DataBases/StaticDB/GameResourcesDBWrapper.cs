using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace DataBase
{
    public class GameResourcesDBWrapper
    {
        private GameResourcesDB gameResourcesDB = null;

        public GameResourcesDBWrapper(GameResourcesDB gameResourcesDB)
        {
            this.gameResourcesDB = gameResourcesDB;
        }

        public ObjectPoint ObjectPoint
        {
            get
            {
                return gameResourcesDB.ObjectPoint;
            }
        }
    }
}