using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace EnemyAction
{
    [Serializable]
    public class TerritoryMove : ICommand
    {
        GameObject Target;

        [SerializeField]
        float Speed;

        [SerializeField]
        float Range = 5.0f;

        Vector3 HomePoint;

        public void Setup(GameObject owner)
        {
            Target = owner;
        }

        public bool Execute()
        {

            return true;
        }
    }
}