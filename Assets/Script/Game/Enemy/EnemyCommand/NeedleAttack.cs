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
        MasterCube Owner;

        public void Setup(MasterCube owner)
        {
            Owner = owner;
        }

        public bool Execute()
        {
            return true;
        }
    }
}

