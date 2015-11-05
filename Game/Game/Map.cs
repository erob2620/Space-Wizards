using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using spacewizards.Utilities;
using spacewizards.Items;
using spacewizards.Models;
using spacewizards.Models.Monsters;

namespace spacewizards
{
    public class Map
    {
        public string[,] tiles;
        public List<Sprite> loadList { get; set; }
        public Game1 gameRef { get; set; }
        public TileSprite ts { get; set; }
        public Mapper mapper { get; set; }
        public List<string> enemyList { get; set; }
        Random r = new Random();

        public Map(string fileName, Game1 game, Mapper mapper, List<String> enemyList)
        {
            FileInputHandler fileIn = new FileInputHandler(game);
            tiles = fileIn.parseToArray(fileName);
            this.gameRef = game;
            this.mapper = mapper;
            this.enemyList = enemyList;

            loadList = new List<Sprite>();
            int dim = 16;
            string s = "";

            for (int x = 0; x < dim; x++)
            {
                for (int y = 0; y < dim; y++)
                {
                    Vector2 position = new Vector2(x * 64, y * 64);
                    s = this.tiles[y, x];
                    //if (s.Equals("0"))
                    //{
                    //    ts = new TileSprite(gameRef.Content.Load<Texture2D>(@"Images/tile" + s), position, new Point(32, 32), -25, new Point(0, 0), new Point(1, 1), Vector2.Zero);
                    //    loadList.Add(ts);
                    //}
                    if (s.Contains("map_"))
                    {
                        ts = new TileSprite(gameRef.Content.Load<Texture2D>(@"Images/tile" + (s.Substring(0, 1))), position, new Point(32, 32), -25, new Point(0, 0), new Point(1, 1), Vector2.Zero);
                        loadList.Add(ts);
                        string selectedBoss = s.Substring(s.IndexOf("_") + 1);
                        Enemy bossObject;
                        switch (selectedBoss)
                        {
                            case "clark":
                                bossObject = new Clark(gameRef.Character.Level, gameRef, gameRef.Character);
                                break;
                            case "pay":
                                bossObject = new JerryPay(gameRef.Character.Level, gameRef, gameRef.Character);
                                break;
                            case "fletcher":
                                bossObject = new Fletcher(gameRef.Character.Level, gameRef, gameRef.Character);
                                break;
                            case "halliday":
                                bossObject = new Halliday(gameRef.Character.Level, gameRef, gameRef.Character);
                                break;
                            case "king_james":
                                bossObject = new Boss(gameRef.Character.Level, gameRef, gameRef.Character);
                                break;
                            default:
                                bossObject = new Clark(gameRef.Character.Level, gameRef, gameRef.Character);
                                break;
                        }
                        BossSprite combatSprite = new BossSprite(gameRef.Content.Load<Texture2D>(@"Images/map" + s.Substring(s.IndexOf('_'))), position, new Point(32, 32), -25, new Point(0, 0), new Point(1, 1), Vector2.Zero, gameRef, bossObject);
                        loadList.Add(combatSprite);
                    }
                    else if (s.Contains("3d"))
                    {
                        ts = new TileSprite(gameRef.Content.Load<Texture2D>(@"Images/tile" + s), position, new Point(32, 32), -25, new Point(0, 0), new Point(1, 1), Vector2.Zero);
                        ts.passable = false;
                        loadList.Add(ts);
                    }
                    else if (s.Contains("i"))
                    {
                        ts = new TileSprite(gameRef.Content.Load<Texture2D>(@"Images/tile" + (s.Substring(0, 1))), position, new Point(32, 32), -25, new Point(0, 0), new Point(1, 1), Vector2.Zero);
                        Treasure t = new Treasure(gameRef.Content.Load<Texture2D>(@"Images/mapItem"), position, 0, mapper, gameRef);
                        loadList.Add(ts);
                        loadList.Add(t);
                    }
                    else if (s.Contains("d"))
                    {
                        int i = 0;
                        string mapID = s.Substring(1, 2);

                        float placeAtX = (float.Parse(s.Substring(s.IndexOf(";") + 1, 2))) * 64;
                        float placeAtY = (float.Parse(s.Substring(s.IndexOf("_") + 1, 2))) * 64;
                        Door d = new Door(gameRef.Content.Load<Texture2D>(@"Images/tiled"), position, 0, mapID, mapper, new Vector2(placeAtX, placeAtY), gameRef);
                        d.passable = false;
                        d.isInteractable = true;
                        loadList.Add(d);
                    }
                    else if (s.Contains("k"))
                    {
                        ts = new TileSprite(gameRef.Content.Load<Texture2D>(@"Images/tile" + (s.Substring(0, 1))), position, new Point(32, 32), -25, new Point(0, 0), new Point(1, 1), Vector2.Zero);
                        Key k = new Key(gameRef.Content.Load<Texture2D>(@"Images/key"), position, 0, mapper, gameRef);
                        loadList.Add(ts);
                        loadList.Add(k);
                    }

                    else if (s.Contains("l"))
                    {
                        ts = new TileSprite(gameRef.Content.Load<Texture2D>(@"Images/tile" + (s.Substring(0, 1))), position, new Point(32, 32), -25, new Point(0, 0), new Point(1, 1), Vector2.Zero);
                        LockedDoor ld = new LockedDoor(gameRef.Content.Load<Texture2D>(@"Images/lockedDoor"), position, 0, mapper, gameRef);
                        loadList.Add(ts);
                        loadList.Add(ld);
                    }
                    else if (s.Contains("s"))
                    {
                        ts = new TileSprite(gameRef.Content.Load<Texture2D>(@"Images/tile" + (s.Substring(0, 1))), position, new Point(32, 32), -25, new Point(0, 0), new Point(1, 1), Vector2.Zero);
                        ts.passable = false;
                        ShopSprite ss = new ShopSprite(gameRef.Content.Load<Texture2D>(@"Images/shop"), position, 25, mapper, gameRef);
                        loadList.Add(ts);
                        loadList.Add(ss);
                    }
                    else if (s == "0")
                    {
                        int wallType = r.Next(0, 9);
                        switch (wallType)
                        {
                            case 0:
                                break;
                            case 1:
                                wallType = 0;
                                break;
                            case 2:
                                wallType = 1;
                                break;
                            case 3:
                                wallType = 1;
                                break;
                            case 4:
                                wallType = 2;
                                break;
                            case 5:
                                wallType = 2;
                                break;
                            case 6:
                                wallType = 3;
                                break;
                            case 7:
                                wallType = 1;
                                break;
                            case 8:
                                wallType = 2;
                                break;
                        }
                        ts = new TileSprite(gameRef.Content.Load<Texture2D>(@"Images/wallTile" + wallType), position, new Point(32, 32), -25, new Point(0, 0), new Point(1, 1), Vector2.Zero);
                        ts.passable = false;
                        loadList.Add(ts);
                    }
                    else
                    {
                        ts = new TileSprite(gameRef.Content.Load<Texture2D>(@"Images/tile" + s), position, new Point(32, 32), -25, new Point(0, 0), new Point(1, 1), Vector2.Zero);
                        if (s == "0" || s == "b")
                        { ts.passable = false; }
                        loadList.Add(ts);
                    }
                }
            }
        }
    }
}
