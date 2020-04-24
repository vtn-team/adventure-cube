using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using Block;

public class EditBox : MonoBehaviour
{
    Dropdown DropDown;

    public int Index => DropDown.value;

    private void Awake()
    {
        DropDown = GetComponent<Dropdown>();
        for(int i = 1; i < (int)MonoBlock.BlockType.MAX; ++i)
        {
            DropDown.options.Add(new Dropdown.OptionData() { text = ((MonoBlock.BlockType)i).ToString() });
        }
    }

    public void Select(int idx)
    {
        DropDown.value = idx;
    }
}
