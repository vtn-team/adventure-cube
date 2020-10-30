using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// 敵行動管理クラス
/// 
/// NOTE: コマンドという概念を駆使して管理する
/// NOTE: SerializeReferenceを使っています
/// </summary>
class EnemyCommand : MonoBehaviour
{
    /// <summary>
    /// 敵コマンド管理単位
    /// </summary>
    [Serializable]
    class EnemyCommandSet
    {
        public bool IsWait = true;              // 行動が終わるまで待つか？
        [SerializeReference, SubclassSelector]
        public ICommand Command;                // コマンド
        public int Probability = 1;             // 実行確率
        public float CastTime = 0;              // 行動までの時間
        public float CoolTime = 0;              // 行動後のインターバル
        public int NextIndex = -1;              // 次に必ずそのIndexの行動をする。-1で抽選に戻る
    }

    [SerializeField]
    MasterCube MasterCube = null;

    [SerializeField]
    List<EnemyCommandSet> Commands = new List<EnemyCommandSet>();

    float Timer = 0.0f;
    bool IsEndExecute = false;
    EnemyCommandSet Current = null;
    LinkedList<EnemyCommandSet> Stack = new LinkedList<EnemyCommandSet>();
    List<EnemyCommandSet> StackRemove = new List<EnemyCommandSet>();

    private void Awake()
    {
        LifeCycleManager.AddUpdate(UnityUpdate, this.gameObject, 0);
        MasterCube = GetComponent<MasterCube>();
        StackRemove.Capacity = 10;
    }

    void UnityUpdate()
    {
        if (Stack.Count > 0)
        {
            StackRemove.Clear();
            foreach (var s in Stack)
            {
                if (s.Command.Execute())
                {
                    StackRemove.Add(s);
                }
            }
            foreach (var rm in StackRemove)
            {
                Stack.Remove(rm);
            }
        }

        if(Current == null)
        {
            var cmds = Commands.Where(c => c.Probability > 0);
            int total = cmds.Sum(c => c.Probability);
            int p = UnityEngine.Random.Range(0, total);
            foreach(var c in cmds)
            {
                if(p < c.Probability)
                {
                    Current = c;
                    break;
                }
                p -= c.Probability;
            }
            if(Current != null)
            {
                UpdateNextCommand();
            }
            return;
        }

        if (!IsEndExecute)
        {
            if (Current.CastTime > 0.0f)
            {
                Timer += Time.deltaTime;
                if (Timer < Current.CastTime) return;
            }

            if (Current.Command.Execute())
            {
                IsEndExecute = true;
                Timer = 0.0f;
            }
            else
            {
                return;
            }
        }

        if (Current.CoolTime > 0.0f)
        {
            Timer += Time.deltaTime;
            if (Timer < Current.CoolTime) return;
        }

        SearchNext();
    }

    void UpdateNextCommand()
    {
        //Debug.Log(Current.Command.ToString());
        Current.Command.Setup(MasterCube);

        if (Current.IsWait)
        {
            Timer = 0.0f;
            IsEndExecute = false;
        }
        else
        {
            Stack.AddLast(Current);
            SearchNext();
        }
    }

    void SearchNext()
    {
        if (Current.NextIndex != -1)
        {
            Current = Commands[Current.NextIndex];
            UpdateNextCommand();
        }
        else
        {
            Current = null;
        }
    }
}

