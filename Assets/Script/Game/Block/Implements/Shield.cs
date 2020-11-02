using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Block
{
    public class Shield : MonoBlock, IShieldBlock
    {
        //シールドブロックはfigureとlifeがイコールになる
        void Start()
        {
            Life = Figure;
        }

        //ダメージを受ける
        public int Defence(int dmg)
        {
            if (Life > dmg)
            {
                Life -= dmg;
                return 0;
            }
            else
            {
                int ret = dmg - Life;
                Life = 0;
                return ret;
            }
        }
    }
}
