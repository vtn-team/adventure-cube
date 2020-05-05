using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class EnemyCommand : MonoBehaviour, IUpdatable
{
    [Serializable]
    class EnemyCommandSet
    {
        public bool IsWait = true;
        [SerializeReference, SubclassSelector]
        public ICommand Command;
        public int Probability = 1;
        public float CastTime = 0;
        public float CoolTime = 0;
        public int NextIndex = -1;
    }

    [SerializeField]
    List<EnemyCommandSet> Commands = new List<EnemyCommandSet>();

    float Timer = 0.0f;
    bool IsEndExecute = false;
    EnemyCommandSet Current = null;
    List<EnemyCommandSet> Stack = new List<EnemyCommandSet>();

    private void Awake()
    {
        UpdateManager.Add(this, 0);
    }

    public bool CastWait()
    {
        return true;
    }

    public bool CoolDown()
    {
        return true;
    }

    public void UnityUpdate()
    {
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
                Debug.Log(Current);
                Current.Command.Setup(this.gameObject);
                Timer = 0.0f;
                IsEndExecute = false;
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
        
        if(Current.NextIndex != -1)
        {
            Current = Commands[Current.NextIndex];
        }
        else
        {
            Current = null;
        }
    }
}

