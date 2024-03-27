public class ChakimonCatched
{
    public List<Chakimon> chakimons { get; set; } = new List<Chakimon>();

    public virtual void Add(Chakimon chakimon, AllCatchedChakimon allCatchedChakimon) { }
    public virtual void Add(Chakimon chakimon) { }

    public virtual void Replace(Chakimon chakimonToReplace, Chakimon newChakimon)
    {
        chakimons.Insert(chakimons.IndexOf(chakimonToReplace), newChakimon);
        Remove(chakimonToReplace);
    }

    public virtual void Remove(Chakimon chakimon)
    {
        chakimons.Remove(chakimon);
    }
}



public class Team : ChakimonCatched
{
    public List<Chakimon> chakimons { get; set; } = new List<Chakimon>();

    public override void Add(Chakimon chakimon, AllCatchedChakimon allCatchedChakimon) 
    {
        if (chakimons.Count != 6)
        {
            chakimons.Add(chakimon);    
        }
        else
        {
            allCatchedChakimon.Add(chakimon);
        }
    }

    public void SaveTeam(JsonFileManager jsonFileManager)
    {
        jsonFileManager.SaveToJsonFile(chakimons, "team_data.json");
    }
}

public class AllCatchedChakimon : ChakimonCatched
{
    public List<Chakimon> chakimons { get; set; } = new List<Chakimon>();

    public override void Add(Chakimon chakimon)
    {
        chakimons.Add(chakimon);
    }
    public void SaveAllCatchedChakimon(JsonFileManager jsonFileManager)
    {
        jsonFileManager.SaveToJsonFile(chakimons, "all_catched_chakimon_data.json");
    }
}