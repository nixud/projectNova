public interface IAccumulate
{
    int Accumulate { get; }
}

interface IConsume
{
    int EffectCount { get; }
    float Cd { get; }
}

public enum ItemType
{ 
    Plugin,
    Accumulate,
    Consume
}