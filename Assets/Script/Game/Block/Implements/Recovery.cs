using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Block
{
    /// <summary> 回復させてみよう </summary>
    public class Recovery : MonoBlock , IPassiveBlock
    {
        public PassiveType PassiveType => PassiveType.Recovery;

        public int PassiveEvent(int param, int subparam = 0)
        {
            return param;
        }

    }

}
