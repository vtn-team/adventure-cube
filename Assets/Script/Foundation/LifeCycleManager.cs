using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Unityの重たいライフサイクル処理を一括実行して早くするためのクラス
/// 
/// NOTE: Unity用の最適化コード。一括化したいライフサイクルがあれば追記する。
/// NOTE: delegate/委譲について覚えるのに最適
/// 
/// NOTE: なぜ削除対象の登録が必要かというと、それぞれのUpdate処理で削除すると、順番次第で動いたり動かなくなったりするスクリプトが出てしまうから。
///       最後、Update後に一括で処理をすることで、順番の問題を解決する。
/// </summary>
public class LifeCycleManager
{
    public delegate void UnityUpdate();

    /// <summary>
    /// 優先度付きキュー
    /// 
    /// NOTE: 優先度で並び替えが行われる
    /// </summary>
    class PriorityQueue
    {
        public int Priority { get; private set; }   //優先度
        GameObject Object;                          //NULL判定用のゲームオブジェクト
        UnityUpdate Target;                         //イベント対象

        /// <summary>
        /// 初期化
        /// </summary>
        public PriorityQueue(int p, GameObject o, UnityUpdate t)
        {
            Priority = p;
            Target = t;
            Object = o;
        }

        /// <summary>
        /// Update処理
        /// 
        /// NOTE: 対象がすでになくなってた場合は処理をしない
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            if (Target == null) return true;
            if (Object == null) return true;
            Target();
            return false;
        }
    }

    static BehaviourAttachment attachment = null;   // Unityのイベントが必要なのでそれを拾ってくるためのクラス
    static LifeCycleManager instance = new LifeCycleManager();    // シングルトン
    private LifeCycleManager(){ }

    /// <summary>
    /// シングルトン処理 - 自分自身を返す
    /// 
    /// NOTE: 初回アクセスでイベントを動かすためのスクリプトを検索している
    ///       この処理はシーンが変わっても大丈夫
    /// </summary>
    static public LifeCycleManager Instance { get
        {
            if(attachment == null)
            {
                attachment = GameObject.FindObjectOfType<BehaviourAttachment>();
                if(attachment != null)
                {
                    attachment.SetUpdateCallback(instance.UpdateCallback);
                }
            }
            return instance;
        }
    }

    LinkedList<PriorityQueue> UpdateQueue = new LinkedList<PriorityQueue>();    // 更新処理のキュー
    List<PriorityQueue> AddUpdateQueue = new List<PriorityQueue>();             // 更新処理の追加用キュー
    List<GameObject> DestroyQueue = new List<GameObject>();                     // 削除命令キュー
    List<PriorityQueue> Remove = new List<PriorityQueue>();                     // 除外リスト
    
    /// <summary>
    /// Update関数の登録
    /// </summary>
    /// <param name="target">Update関数</param>
    /// <param name="o">Unityのオブジェクト</param>
    /// <param name="priority">更新優先度</param>
    static public void AddUpdate(UnityUpdate target, GameObject o, int priority)
    {
        Instance.AddUpdateQueue.Add(new PriorityQueue(priority, o, target));
    }

    /// <summary>
    /// オブジェクト削除の登録
    /// </summary>
    /// <param name="obj">削除対象のオブジェクト</param>
    static public void RegisterDestroy(GameObject obj)
    {
        Instance.DestroyQueue.Add(obj);
    }

    /// <summary>
    /// Updateタイミングの処理の実行
    /// </summary>
    void UpdateCallback()
    {
        //NOTE: リスト処理はfor/foreachで回してる最中に更新してはいけない

        //削除用配列のクリア
        Remove.Clear();

        //更新処理を実行する
        foreach (var q in UpdateQueue)
        {
            //更新処理が終わり(true)を返したら、削除対象に入れる
            if (q.Update())
            {
                Remove.Add(q);
            }
        }
        //更新処理から削除対象のものを削除
        foreach (var q in Remove)
        {
            UpdateQueue.Remove(q);
        }

        //追加する更新処理があれば追加する
        if (AddUpdateQueue.Count > 0)
        {
            //優先度に応じて追加
            foreach (var q in AddUpdateQueue)
            {
                var node = UpdateQueue.LastOrDefault(u => u.Priority <= q.Priority);
                if (node == null)
                {
                    UpdateQueue.AddFirst(q);
                }
                else
                {
                    UpdateQueue.AddAfter(UpdateQueue.Find(node), q);
                }
            }
            AddUpdateQueue.Clear();
        }

        //オブジェクトの削除処理を走らせる
        foreach (var obj in DestroyQueue)
        {
            GameObject.Destroy(obj);
        }
    }
}
