using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using Block;

public class AttackCollider : MonoBehaviour
{
    //public delegate void CharacterHitCallback(MasterCube master);
    public delegate void MonoBlockHitCallback(MonoBlock block);
    MonoBlockHitCallback Callback { get; set; }
    int FriendId { get; set; }

    public void Setup(MonoBlockHitCallback cb, int fid)
    {
        Callback = cb;
        FriendId = fid;
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*
        //Characterレイヤの何かにあたった
        if (collision.gameObject.CompareTag("Character"))
        {
            //
            //Debug.Log(this.gameObject.name + "/" + collision.gameObject.name);
            var mc = GameObjectCache.GetCharacter(collision.gameObject);
            if (mc == null)
            {
                Debug.Log("不正な設定のオブジェクト:" + collision.gameObject.name);
                return;
            }
            if (mc.IsFriend(FriendId)) return;

            Callback(mc);
        }
        */

        //Debug.Log(this.gameObject.name + "/" + collision.gameObject.name);

        //MonoBlockは攻撃対象
        if (collision.gameObject.CompareTag("MonoBlock"))
        {
            //
            var mc = GameObjectCache.GetMonoBlock(collision.gameObject);
            if (mc == null)
            {
                Debug.Log("不正な設定のオブジェクト:" + collision.gameObject.name);
                return;
            }
            if (mc.IsFriend(FriendId)) return;

            Callback(mc);
        }
    }
}

