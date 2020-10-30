using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace EnemyAction
{
    /// <summary>
    /// 待機
    /// </summary>
    class Stay : ICommand
    {
        public void Setup(MasterCube owner)
        {
        }

        public bool Execute()
        {
            return true;
        }
    }
}
