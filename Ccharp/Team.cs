public class ChakimonCatched
{
    public List<Chakimon> team { get; set; } = new List<Chakimon>();
    public List<Chakimon> allCatchedChakimon { get; set; } = new List<Chakimon>();

    public void Add(Chakimon chakimon)
    {
        if (team.Count <= 6)
        {
            team.Add(chakimon);
        }
        else
        {
            allCatchedChakimon.Add(chakimon);
        }
    }

    public void Replace(Chakimon chakimonToReplace, Chakimon newChakimon)
    {
        team.Insert(team.IndexOf(chakimonToReplace), newChakimon);
        Remove(chakimonToReplace);
    }

    public virtual void Remove(Chakimon chakimon)
    {
        team.Remove(chakimon);
    }
}