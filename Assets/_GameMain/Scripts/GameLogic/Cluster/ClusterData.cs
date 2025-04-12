using UniRx;

public class ClusterData
{
    public string Text { get; private set; }
    public string TargetWord { get; private set; }
    public int Index { get; private set; }

    public ReactiveProperty<bool> IsLocked { get; } = new(false);

    public void Initialize(string text, string targetWord, int index)
    {
        Text = text;
        TargetWord = targetWord;
        Index = index;
    }
}