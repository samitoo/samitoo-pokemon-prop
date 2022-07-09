using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System;

[XmlRoot("pokemonCollection")]
public class PokemonContainerXML {

    [XmlArray("pokedex")]
    [XmlArrayItem("pokemon")]
    public List<PokemonXML> Pokemon = new List<PokemonXML>();



    /*public static PokemonContainerXML load(string path)
    {
        var serializer = new XmlSerializer(typeof(PokemonContainerXML));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as PokemonContainerXML;
        }
        
    }*/



    //Video Method
    public static PokemonContainerXML LoadFromText(string path)
    {
            TextAsset _xml = Resources.Load<TextAsset>(path);
            //Debug.Log(_xml);

            XmlSerializer serializer = new XmlSerializer(typeof(PokemonContainerXML));

            StringReader reader = new StringReader(_xml.text);

            PokemonContainerXML pokemon = serializer.Deserialize(reader) as PokemonContainerXML;

            reader.Close();

            return pokemon;
    
    }

    public void Save(string path)
    {
         var serializer = new XmlSerializer(typeof(PokemonContainerXML));
         using (var stream = new FileStream(path, FileMode.Create))
         {
             serializer.Serialize(stream, this);
         }

    }
}
