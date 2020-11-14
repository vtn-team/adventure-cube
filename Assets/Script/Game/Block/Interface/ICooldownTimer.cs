using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ICooldownTimer
{
    float CurrentInterval { get; }  // 攻撃インターバルを知らせる実装が必要
}
