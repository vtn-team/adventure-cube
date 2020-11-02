using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace EnemyAction
{
    /// <summary>
    /// 範囲内移動
    /// 
    /// NOTE: つくりかけ
    /// </summary>
    [Serializable]
    public class TerritoryMove : ICommand
    {
        MasterCube Target;

        [SerializeField]
        float Speed;

        [SerializeField]
        float Range = 5.0f;

        Vector3 HomePoint;

        public void Setup(MasterCube owner)
        {
            Target = owner;
        }

        public bool Execute()
        {

            return true;
        }
    }
}