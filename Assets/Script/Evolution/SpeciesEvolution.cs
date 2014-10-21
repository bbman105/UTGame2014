using UnityEngine;
using System.Collections;

public class SpeciesEvolution  {
    public string SpeciesID { get; set; }
    public string[] EvoSpecies { get; set; }

    public SpeciesEvolution(string _speciesID,string[] _evoSpecies)
    {
        SpeciesID = _speciesID;
        EvoSpecies = _evoSpecies;
    }

}
