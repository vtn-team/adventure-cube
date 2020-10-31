using Block;

public class Counter : MonoBlock, ICounterBlock
{
    void ICounterBlock.Counter(IAttackBlock myAttacker)
    {
        myAttacker?.Attack();
    }
}
