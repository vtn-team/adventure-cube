using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TargetHelper
{
    public enum SearchLogicType
    {
        RandomOne,
        NearestOne
    };

    static public MasterCube SearchTarget(MasterCube from, SearchLogicType logic)
    {
        List<MasterCube> targetList = null;
        if (from.FriendId == 1) targetList = GameManager.GetEnemyList();
        if (from.FriendId == 2) return GameManager.GetPlayableChar();

        if (targetList == null) return null;
        if (targetList.Count == 0) return null;

        MasterCube target = null;
        switch(logic)
        {
            case SearchLogicType.RandomOne:
                target = targetList[UnityEngine.Random.Range(0, targetList.Count)];
                break;

            case SearchLogicType.NearestOne:
                {
                    float length = -1;
                    foreach (var t in targetList)
                    {
                        if (length != -1 && length < (t.transform.position - from.transform.position).magnitude) continue;
                        length = (t.transform.position - from.transform.position).magnitude;
                        target = t;
                    }
                }
                break;
        }
        return target;
    }
}
