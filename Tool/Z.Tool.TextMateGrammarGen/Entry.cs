namespace Z.Tool.TextMateGrammarGen;

class Entry : EntryEntry
{
    protected override int ExecuteMain()
    {
        Gen gen;
        gen = new Gen();
        gen.Arg = this.Arg;
        gen.Init();
        int o;
        o = gen.Execute();
        return o;
    }

    [STAThread]
    static int Main()
    {
        EntryEntry a;
        a = new Entry();
        a.Init();
        int o;
        o = a.Execute();
        return o;
    }
}