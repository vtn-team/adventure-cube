using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface ICommand
{
    void Setup(MasterCube owner);

    //舞フレーム呼び出され、trueで終了する
    bool Execute();
}
