namespace Z.Tool.TextMateGrammarGen;

public class Gen : ToolBase
{
    public virtual Array Arg { get; set; }

    public virtual long Execute()
    {
        if (this.Arg.Count < 1)
        {
            return 10;
        }

        String outputFilePath;
        outputFilePath = (String)this.Arg.GetAt(0);

        String indexItemList;
        indexItemList = this.ToolInfra.StorageTextRead(this.S("../../../Class/Tool/Z.Tool.Class.IndexList/ToolData/Class/ItemListIndex.txt"));

        String wordBoundaryLeft;
        wordBoundaryLeft = this.ToolInfra.StorageTextRead(this.S("ToolData/TextMate/WordBoundaryLeft.txt"));

        String wordBoundaryRight;
        wordBoundaryRight = this.ToolInfra.StorageTextRead(this.S("ToolData/TextMate/WordBoundaryRight.txt"));

        String keyword;
        keyword = this.ToolInfra.StorageTextRead(this.S("ToolData/TextMate/Index.txt"));

        String intValue;
        intValue = this.ToolInfra.StorageTextRead(this.S("ToolData/TextMate/IntValue.txt"));

        String name;
        name = this.ToolInfra.StorageTextRead(this.S("ToolData/TextMate/Name.txt"));

        String grammar;
        grammar = this.ToolInfra.StorageTextRead(this.S("ToolData/TextMate/Grammar.json"));

        String oa;
        oa = this.IndexList(indexItemList);

        Text k;
        k = this.TA(keyword);
        k = this.Replace(k, "#WordList#", oa);
        k = this.Replace(k, "#WordBoundaryLeft#", wordBoundaryLeft);
        k = this.Replace(k, "#WordBoundaryRight#", wordBoundaryRight);

        String indexA;
        indexA = this.StringCreate(k);

        k = this.TA(indexA);
        k = this.EscapeSlash(k);

        String indexRegexString;
        indexRegexString = this.StringCreate(k);

        k = this.TA(intValue);
        k = this.Replace(k, "#WordBoundaryLeft#", wordBoundaryLeft);
        k = this.Replace(k, "#WordBoundaryRight#", wordBoundaryRight);
        k = this.EscapeSlash(k);

        String intValueRegexString;
        intValueRegexString = this.StringCreate(k);

        k = this.TA(name);
        k = this.Replace(k, "#Index#", indexA);
        k = this.Replace(k, "#WordBoundaryLeft#", wordBoundaryLeft);
        k = this.Replace(k, "#WordBoundaryRight#", wordBoundaryRight);

        String nameRegexString;
        nameRegexString = this.StringCreate(k);

        k = this.TA(grammar);
        k = this.Replace(k, "#IndexRegexString#", indexRegexString);
        k = this.Replace(k, "#IntValueRegexString#", intValueRegexString);
        k = this.Replace(k, "#NameRegexString#", nameRegexString);
        k = this.Replace(k, "#WordBoundaryLeft#", wordBoundaryLeft);
        k = this.Replace(k, "#WordBoundaryRight#", wordBoundaryRight);

        String a;
        a = this.StringCreate(k);

        this.ToolInfra.StorageTextWrite(outputFilePath, a);
        return 0;
    }

    protected virtual Text EscapeSlash(Text k)
    {
        k = this.Replace(k, "\\", this.S("\\\\"));
        return k;
    }

    protected virtual String IndexList(String o)
    {
        this.AddClear();

        Array array;
        array = this.TextLimitLineString(this.TA(o));

        long count;
        count = array.Count;
        long i;
        i = 0;
        while (i < count)
        {
            String line;
            line = (String)array.GetAt(i);

            String index;
            index = this.Index(line);

            if (0 < i)
            {
                this.AddS("|");
            }

            this.Add(index);

            i = i + 1;
        }

        String a;
        a = this.AddResult();
        return a;
    }

    protected virtual String Index(String line)
    {
        String a;
        a = line;

        String ka;
        ka = this.S("Item");

        bool b;
        b = this.TextStart(this.TA(a), this.TB(ka));

        if (b)
        {
            a = this.StringCreateIndex(a, this.StringComp.Count(ka));
        }

        a = this.StringCreate(this.TextAlphaSite(this.TA(a)));

        return a;
    }
}