using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class UIPane : MonoBehaviour
{
    [SerializeField] bool InitVisibility = true;
    protected CanvasRenderer Renderer;
    public RectTransform RectTransform { get; protected set; }

    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
        Renderer = GetComponent<CanvasRenderer>();
        Renderer.cull = !InitVisibility;
        Setup();
    }

    protected virtual void Setup()
    {

    }

    public virtual void Open()
    {

    }

    public virtual void Close()
    {

    }
}
