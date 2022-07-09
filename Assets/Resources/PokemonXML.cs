using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

public class PokemonXML {

    [XmlAttribute("id")]
    public string id;

    [XmlElement("name")]
    public string name;

    [XmlElement("type")]
    public string type;

    [XmlElement("level")]
    public int level;

    [XmlElement("xp")]
    public int xp;

    [XmlElement("hp")]
    public int hp;

    [XmlElement("currentHP")]
    public int currentHp;

    [XmlElement("attack")]
    public int attack;

    [XmlElement("defense")]
    public int defense;

    [XmlElement("speed")]
    public int speed;

    [XmlElement("special")]
    public int special;

    [XmlElement("total")]
    public int total;

    [XmlElement("average")]
    public float average;

    [XmlElement("imageBack")]
    public string imageBack;

    [XmlElement("imageFront")]
    public string imageFront;

    [XmlElement("imageFront2")]
    public string imageFront2;
}
