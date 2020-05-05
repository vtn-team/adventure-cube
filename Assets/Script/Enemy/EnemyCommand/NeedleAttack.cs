using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace EnemyAction
{
    class NeedleAttack : ICommand
    {
        [SerializeField] int Atk;
        GameObject Owner;

        public void Setup(GameObject owner)
        {
            Owner = owner;
        }

        public bool Execute()
        {
            return true;
        }
    }
}

