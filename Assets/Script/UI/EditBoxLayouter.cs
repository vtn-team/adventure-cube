using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EditBoxLayouter : MonoBehaviour
{
    [SerializeField] EditBox Template = null;
    List<EditBox> EditBoxList = new List<EditBox>();

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 15; ++i)
        {
            var Obj = GameObject.Instantiate(Template, this.transform);
            Obj.GetComponent<RectTransform>().localPosition = new Vector3(i % 3 * 300, -i / 3 * 80, 0);
            EditBoxList.Add(Obj);
        }

        int human = Random.Range(0, 15);
        EditBoxList[human].Select(0);

        for (int i = 0; i < 15; ++i)
        {
            if (human == i) continue;
            EditBoxList[i].Select(Random.Range(1, (int)MonoBlock.BlockType.MAX));
        }
    }

    public List<MonoBlock.BlockType> GetDeck()
    {
        List<MonoBlock.BlockType> deck = new List<MonoBlock.BlockType>();
        for (int i = 0; i < 15; ++i)
        {
            deck.Add((MonoBlock.BlockType)EditBoxList[i].Index);
        }
        return deck;
    }
}
