using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using Block;

public interface IAttackLogic
{
    void SetupFigure(MonoBlock block);
    void Update();
    void Attack();
}
