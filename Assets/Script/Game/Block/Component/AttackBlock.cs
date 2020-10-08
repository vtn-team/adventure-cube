using UnityEngine;
using System.Collections;

namespace Block
{
    public class AttackBlock : MonoBlock
    {
        [SerializeField] float AttackInterval = 1.0f;
        [SerializeReference, SubclassSelector] IAttackLogic AttackLogic;

        private void Start()
        {
        }

    }
}
