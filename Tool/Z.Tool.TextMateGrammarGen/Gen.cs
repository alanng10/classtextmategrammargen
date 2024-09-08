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

        String keywordItemList;
        keywordItemList = this.ToolInfra.StorageTextRead(this.S("../../../Class/Tool/Z.Tool.Class.IndexList/ToolData/ItemListIndex.txt"));
        
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
        oa = this.KeywordList(keywordItemList);

        Text o;
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

        o = name;
        o = o.Replace("#Keyword#", keywordA);
        o = o.Replace("#WordBoundaryLeft#", wordBoundaryLeft);
        o = o.Replace("#WordBoundaryRight#", wordBoundaryRight);

        string nameRegexString;
        nameRegexString = o;

        string k;
        k = grammar;
        k = k.Replace("#KeywordRegexString#", keywordRegexString);
        k = k.Replace("#IntValueRegexString#", intValueRegexString);
        k = k.Replace("#NameRegexString#", nameRegexString);
        k = k.Replace("#WordBoundaryLeft#", wordBoundaryLeft);
        k = k.Replace("#WordBoundaryRight#", wordBoundaryRight);

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
        ToolInfra toolInfra;
        toolInfra = this.ToolInfra;

        StringJoin join;
        join = new StringJoin();
        join.Init();

        Array array;
        array = toolInfra.SplitLineList(o);

        int count;
        count = array.Count;
        int i;
        i = 0;
        while (i < count)
        {
            string line;
            line = (string)array.GetAt(i);

            string keyword;
            keyword = this.Keyword(line);

            if (0 < i)
            {
                toolInfra.Append(join, "|");
            }
            toolInfra.Append(join, keyword);

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