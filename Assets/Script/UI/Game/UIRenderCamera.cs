using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class UIRenderCamera : MonoBehaviour
{
    [SerializeField] Vector3 Offset;
    [SerializeField] Camera UICamera;
    Player Player;

    private void Awake()
    {
        LifeCycleManager.AddUpdate(UnityUpdate, this.gameObject, 0);
    }

    private void UnityUpdate()
    {
        if (!enabled) return;

        if(Player == null)
        {
            Player = GameManager.GetPlayableChar();
        }
        if (Player == null) return;

        this.transform.position = Player.transform.position + Player.transform.rotation * Offset;
        UICamera.transform.LookAt(Player.transform);
    }
}
