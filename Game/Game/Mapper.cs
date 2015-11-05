using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using spacewizards.Utilities;
using spacewizards.Items;
using spacewizards.Models;

namespace spacewizards
{
    public class Mapper
    {
        public SpriteManager manager { get; set; }
        Game1 gameRef { get; set; }
        public FileInputHandler FileIn { get; set; }
        public Vector2 playerPosition = new Vector2(512, 890);
        public TileSprite ts { get; set; }
        public List<Sprite> loadList { get; set; }
        public Dictionary<string, Map> maps = new Dictionary<string, Map>();
        public Map activeMap { get; set; }
        Map map01;
        Map mapF1;
        Map mapF2;
        Map mapF3;
        Map mapW1;
        Map mapW2;
        Map mapW3;
        Map mapE1;
        Map mapE2;
        Map mapE3;
        Map mapA1;
        Map mapA2;
        Map mapA3;
        Map map06;
        Map map07;
        public Mapper(SpriteManager s, Game1 g)
        {
            manager = s;
            gameRef = g;
            FileIn = new FileInputHandler(gameRef);

            List<string> basicList = new List<string>();
            basicList.Add("SporeWeed");
            basicList.Add("VioletWing");
            List<string> earthList = new List<string>();
            earthList.Add("SporeWeed");
            earthList.Add("Titan");
            earthList.Add("ToxWyrm");
            List<string> fireList = new List<string>();
            fireList.Add("ToxWyrm");
            fireList.Add("Titan");
            List<string> windList = new List<string>();
            windList.Add("VioletWing");
            windList.Add("Naga");
            List<string> waterList = new List<string>();
            waterList.Add("Naga");
            waterList.Add("SporeWeed");

            map01 = new Map("Map01", gameRef, this, basicList);
            mapF1 = new Map("MapF1", gameRef, this, fireList);
            mapF2 = new Map("MapF2", gameRef, this, fireList);
            mapF3 = new Map("MapF3", gameRef, this, fireList);
            mapW1 = new Map("MapW1", gameRef, this, waterList);
            mapW2 = new Map("MapW2", gameRef, this, waterList);
            mapW3 = new Map("MapW3", gameRef, this, waterList);
            mapE1 = new Map("MapE1", gameRef, this, earthList);
            mapE2 = new Map("MapE2", gameRef, this, earthList);
            mapE3 = new Map("MapE3", gameRef, this, earthList);
            mapA1 = new Map("MapA1", gameRef, this, windList);
            mapA2 = new Map("MapA2", gameRef, this, windList);
            mapA3 = new Map("MapA3", gameRef, this, windList);
            map06 = new Map("Map06", gameRef, this, basicList);
            map07 = new Map("Map07", gameRef, this, basicList);
            maps.Add("01", map01);
            maps.Add("F1", mapF1);
            maps.Add("F2", mapF2);
            maps.Add("F3", mapF3);
            maps.Add("W1", mapW1);
            maps.Add("W2", mapW2);
            maps.Add("W3", mapW3);
            maps.Add("E1", mapE1);
            maps.Add("E2", mapE2);
            maps.Add("E3", mapE3);
            maps.Add("A1", mapA1);
            maps.Add("A2", mapA2);
            maps.Add("A3", mapA3);
            maps.Add("06", map06);
            maps.Add("07", map07);
        }

        public void doorLoader(string map, Vector2 placementOnMove)
        {
            Map m = null;
            maps.TryGetValue(map, out m);
            playerPosition = placementOnMove;
            LoadMap(m);
        }

        public void LoadMap(Map map)
        {
            manager.LoadContent(map.loadList, playerPosition);
            activeMap = map;
        }


    }
}
