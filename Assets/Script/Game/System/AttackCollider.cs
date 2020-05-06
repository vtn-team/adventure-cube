using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using Block;

public class AttackCollider : MonoBehaviour
{
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
        if (collision.gameObject.CompareTag("MonoBlock"))
        {
            //
            var mb = MonoBlockCache.GetCache(collision.gameObject);
            if (mb.IsFriend(FriendId)) return;
            
            Callback(mb);
        }
    }
}

