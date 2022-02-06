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

        public Point Point
        {
            get
            {
                return gameResourcesDB.Point;
            }
        }
    }
}