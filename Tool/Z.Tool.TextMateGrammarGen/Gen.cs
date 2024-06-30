namespace Z.Tool.TextMateGrammarGen;

public class Gen : Any
{
    public override bool Init()
    {
        this.ToolInfra = ToolInfra.This;
        return true;
    }

    public virtual Array Arg { get; set; }

    protected virtual ToolInfra ToolInfra { get; set; }

    public virtual int Execute()
    {
        if (this.Arg.Count < 1)
        {
            return 10;
        }

        string outputFilePath;
        outputFilePath = (string)this.Arg.Get(0);

        string keywordItemList;
        keywordItemList = this.ToolInfra.StorageTextRead("../../../Class/Tool/Z.Tool.Class.KeywordList/ToolData/ItemListKeyword.txt");
        
        string wordBoundaryLeft;
        wordBoundaryLeft = this.ToolInfra.StorageTextRead("ToolData/TextMate/WordBoundaryLeft.txt");

        string wordBoundaryRight;
        wordBoundaryRight = this.ToolInfra.StorageTextRead("ToolData/TextMate/WordBoundaryRight.txt");

        string keyword;
        keyword = this.ToolInfra.StorageTextRead("ToolData/TextMate/Keyword.txt");

        string intValue;
        intValue = this.ToolInfra.StorageTextRead("ToolData/TextMate/IntValue.txt");

        string grammar;
        grammar = this.ToolInfra.StorageTextRead("ToolData/TextMate/Grammar.json");

        string oa;
        oa = this.KeywordList(keywordItemList);

        string o;
        o = keyword;
        o = o.Replace("#WordList#", oa);
        o = o.Replace("#WordBoundaryLeft#", wordBoundaryLeft);
        o = o.Replace("#WordBoundaryRight#", wordBoundaryRight);

        string keywordA;
        keywordA = o;

        string ob;
        ob = keywordA;
        ob = this.EscapeSlash(ob);

        string keywordRegexString;
        keywordRegexString = ob;

        oa = intValue;
        oa = oa.Replace("#WordBoundaryLeft#", wordBoundaryLeft);
        oa = oa.Replace("#WordBoundaryRight#", wordBoundaryRight);
        oa = this.EscapeSlash(oa);

        string intValueRegexString;
        intValueRegexString = oa;

        string k;
        k = grammar;
        k = k.Replace("#KeywordRegexString#", keywordRegexString);
        k = k.Replace("#IntValueRegexString#", intValueRegexString);

        this.ToolInfra.StorageTextWrite(outputFilePath, k);
        return 0;
    }

    protected virtual string EscapeSlash(string o)
    {
        string a;
        a = o.Replace("\\", "\\\\");
        return a;
    }

    protected virtual string KeywordList(string o)
    {
        StringJoin join;
        join = new StringJoin();
        join.Init();

        Array array;
        array = this.ToolInfra.SplitLineList(o);

        int count;
        count = array.Count;
        int i;
        i = 0;
        while (i < count)
        {
            string line;
            line = (string)array.Get(i);

            string keyword;
            keyword = this.Keyword(line);

            if (0 < i)
            {
                join.Append("|");
            }
            join.Append(keyword);

            i = i + 1;
        }

        string a;
        a = join.Result();
        return a;
    }

    protected virtual string Keyword(string line)
    {
        string a;
        a = line;

        string ka;
        ka = "Item";
        if (a.StartsWith(ka))
        {
            a = a.Substring(ka.Length);
        }

        a = a.ToLower();

        return a;
    }
}