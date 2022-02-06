using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace DataBase
{
    /// <summary>
    /// ����� ������������� �������, ������� ����� ���� ������� ��������� ������� �� ����. ��� ������������ ������� ���������� ����� StaticBalance.
    /// </summary>
    [CreateAssetMenu(fileName = "DataBases", menuName = "GameScriptableObjects/DataBases", order = 1)]
    public class DataBases : ScriptableObject
    {
        private static DataBases instance = null;

        public static GameResourcesDBWrapper GameResourcesWrapper { get; private set; }

        public GameResourcesDB GameResourcesDB = null;

        public static void Init()
        {
            instance = Resources.Load<DataBases>("DataBases/DataBases");
            InitWrappers();
        }

        private static void InitWrappers()
        {
            GameResourcesWrapper = new GameResourcesDBWrapper(instance.GameResourcesDB);
        }
    }
}