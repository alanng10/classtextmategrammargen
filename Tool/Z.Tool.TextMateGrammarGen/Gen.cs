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
        keywordItemList = this.ToolInfra.StorageTextRead("../../../Class/Out/net8.0/ToolData/ItemListKeyword.txt");
        
        string keyword;
        keyword = this.ToolInfra.StorageTextRead("ToolData/TextMate/Keyword.txt");

        string wordClassKeyword;
        wordClassKeyword = this.ToolInfra.StorageTextRead("ToolData/TextMate/WordClassKeyword.txt");

        string name;
        name = this.ToolInfra.StorageTextRead("ToolData/TextMate/Name.txt");

        string className;
        className = this.ToolInfra.StorageTextRead("ToolData/TextMate/ClassName.txt");

        string grammar;
        grammar = this.ToolInfra.StorageTextRead("ToolData/TextMate/Grammar.txt");

        string oa;
        oa = this.KeywordList(keywordItemList);

        string o;
        o = keyword;
        o = o.Replace("#WordList#", oa);

        string keywordA;
        keywordA = o;

        o = name;
        o = o.Replace("#Keyword#", keywordA);

        oa = className;
        oa = oa.Replace("#WordClassKeyword#", wordClassKeyword);
        oa = oa.Replace("#Name#", o);
        oa = oa.Replace("\\", "\\\\");

        string classNameRegexString;
        classNameRegexString = oa;

        string ob;
        ob = keywordA;
        ob = ob.Replace("\\", "\\\\");

        string keywordRegexString;
        keywordRegexString = ob;

        string k;
        k = grammar;
        k = k.Replace("#KeywordRegexString#", keywordRegexString);
        k = k.Replace("#ClassNameRegexString#", classNameRegexString);

        this.ToolInfra.StorageTextWrite(outputFilePath, k);
        return 0;
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