

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class Map
{
    int numTiles;
    JsonFileManager json;
    Dictionary<int, Tile> tiles;
    Dictionary<int, Dictionary<int, int>> map;
    Dictionary<int, Dictionary<int, bool>> grassMap;
    Dictionary<int, Dictionary<int, List<int>>> possibilities;
    Random Rand;
    int seed;
    List<int> debugActual;
    int pastX;
    int pastY;
    List<int[]> checkEdit;

    public Map()
    {
        pastX = 0;
        pastY = 0;

        debugActual = new List<int>(6) { 0, 0, 0, 0, 0, 0 };

        seed = 0;
        Rand = new Random(seed);

        json = new JsonFileManager();
        numTiles = JsonConvert.DeserializeObject<List<List<string>>>(json.LoadFile("map/tiles.json")).Count();
        tiles = new Dictionary<int, Tile>(numTiles * 4);

        map = new Dictionary<int, Dictionary<int, int>>();
        grassMap = new Dictionary<int, Dictionary<int, bool>>();
        possibilities = new Dictionary<int, Dictionary<int, List<int>>>();

        checkEdit = new List<int[]>();

        for (int i = 0; i < numTiles; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                tiles[(i << 2) + j] = new Tile(i, intToRotation(j));
            }
        }

        Console.SetCursorPosition(0, 0);
        Generate(0, 0, 15);
    }

    private rotationTile intToRotation(int rot)
    {
        if (rot == 3) return rotationTile.DOWN;
        else if (rot == 2) return rotationTile.LEFT;
        else if (rot == 1) return rotationTile.UP;
        else return rotationTile.RIGHT;
    }

    private void Generate(int x, int y, int dist)
    {
        Rand = new Random((seed ^ x) ^ y);
        if (x == 0 && y == 0)
        {
            var i = 0;
        }

        if (!map.ContainsKey(x))
        {
            map[x] = new Dictionary<int, int>();
            grassMap[x] = new Dictionary<int, bool>();
        }
        if (map.ContainsKey(x) && map[x].ContainsKey(y))
        {
            GenerateAround(x, y, dist);
            return;
        }
        if (possibilities.ContainsKey(x) && possibilities[x].ContainsKey(y) && possibilities[x][y].Count() > 0)
        {
            var r = Rand.Next(0, possibilities[x][y].Count());
            map[x][y] = possibilities[x][y][r];
            grassMap[x][y] = Rand.Next(0, 4) == 0;
        }
        else
        {
            map[x][y] = Rand.Next(0, tiles.Count());
            grassMap[x][y] = Rand.Next(0, 4) == 0;
        }
        var m_ascii = new Ascii();

        checkEdit.Add(new int[2] {x, y});
        while (checkEdit.Count() > 0)
        {
            CheckEdition();
        }
        //DebugCase();
        GenerateAround(x, y, dist);
    }

    private void CheckEdition()
    {
        List<int[]> edit = new List<int[]>();
        foreach (var e in checkEdit)
        {
            edit.Add(e);
        }
        checkEdit.Clear();
        foreach (var e in edit)
        {
            CheckEdition(e[0] - 1, e[1]);
            CheckEdition(e[0] + 1, e[1]);
            CheckEdition(e[0], e[1] - 1);
            CheckEdition(e[0], e[1] + 1);
        }
    }

    private void DebugCase()
    {
        int t = (int)(Math.Sqrt(tiles.Count() + 2) + .5f);
        for (int i = -2; i <= 2; i++)
        {
            for (int j = -2; j <= 2; j++)
            {
                Console.SetCursorPosition((2 + i) * 4 * t + (2 + i) * 2 + 1, (2 + j) * 4 * t - (2 + j) + 1);
                DebugCase(i, -j);
            }
        }
        Colored.ResetColor();
        Console.SetCursorPosition(0, 0);
    }

    private void DebugCase(int x, int y)
    {
        int offX = Console.CursorLeft, offY = Console.CursorTop;
        int t = (int)(Math.Sqrt(tiles.Count() + 2) + .5f);
        for (int i = 0; i < t; i++)
        {
            for (int j = 0; j < t; j++)
            {
                if (!tiles.ContainsKey(i + j * t)) continue;
                Colored.ResetColor();
                if (map.ContainsKey(x) && map[x].ContainsKey(y) && map[x][y] == i + j * t)
                {
                    Colored.SetStrictColor(allColor.TEXT_GREEN);
                }
                else if (map.ContainsKey(x) && map[x].ContainsKey(y) && map[x][y] != i + j * t) continue;
                if (possibilities.ContainsKey(x) && possibilities[x].ContainsKey(y) && !possibilities[x][y].Contains(i + j * t)) Colored.SetStrictColor(allColor.TEXT_GRAY);
                if (debugActual[3] == x && debugActual[4] == y && (debugActual[5] == i + j * t || debugActual[5] == -1)) Colored.SetColor(allColor.TEXT_BLUE);
                if (debugActual[0] == x && debugActual[1] == y && debugActual[2] == i + j * t) Colored.SetColor(allColor.TEXT_CYAN);
                Console.SetCursorPosition(i * 4 + offX - 1, j * 4 + offY - 1);
                Console.Write("XXXXX");
                Console.SetCursorPosition(i * 4 + offX - 1, j * 4 + offY);
                Console.Write("X   X");
                Console.SetCursorPosition(i * 4 + offX - 1, j * 4 + offY + 1);
                Console.Write("X   X");
                Console.SetCursorPosition(i * 4 + offX - 1, j * 4 + offY + 2);
                Console.Write("X   X");
                Console.SetCursorPosition(i * 4 + offX - 1, j * 4 + offY + 3);
                Console.Write("XXXXX");

                if (tiles[i + j * t].GetBorder(rotationTile.DOWN).Substring(3, 1) == " ")
                {
                    Console.SetCursorPosition(i * 4 + offX + 1, j * 4 + offY);
                    Console.Write('^');
                }
                if (tiles[i + j * t].GetBorder(rotationTile.RIGHT).Substring(3, 1) == " ")
                {
                    Console.SetCursorPosition(i * 4 + offX + 2, j * 4 + offY + 1);
                    Console.Write('>');
                }
                if (tiles[i + j * t].GetBorder(rotationTile.LEFT).Substring(3, 1) == " ")
                {
                    Console.SetCursorPosition(i * 4 + offX, j * 4 + offY + 1);
                    Console.Write('<');
                }
                if (tiles[i + j * t].GetBorder(rotationTile.UP).Substring(3, 1) == " ")
                {
                    Console.SetCursorPosition(i * 4 + offX + 1, j * 4 + offY + 2);
                    Console.Write('v');
                }
            }
        }
    }

    private void GenerateAround(int x, int y, int dist)
    {
        if (dist == 0) return;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0) continue;
                if (map.ContainsKey(x + i) && map[x + i].ContainsKey(y + j)) continue;
                Generate(x + i, y + j, dist -1);
            }
        }
    }

    private void CheckEdition(int x, int y)
    {
        bool edited = false;

        if (map.ContainsKey(x) && map[x].ContainsKey(y)) return;
        if (!possibilities.ContainsKey(x))
        {
            possibilities[x] = new Dictionary<int, List<int>>();
        }
        if (!possibilities[x].ContainsKey(y))
        {
            possibilities[x][y] = new List<int>();
            foreach (var i in tiles)
            {
                possibilities[x][y].Add(i.Key);
            }
        }
        for (int k = 0; k < 4; k++)
        {
            int _x = x, _y = y;
            int k1 = (k + 2) % 4;
            int k2 = (k + 2) % 4;

            switch (k)
            {
                case 0:
                    _x++;
                    k2 = k;
                    break;
                case 1:
                    _y++;
                    k1 = k;
                    break;
                case 2:
                    _x--;
                    k2 = k;
                    break;
                case 3:
                    _y--;
                    k1 = k;
                    break;
            }

            var possible = new List<int>(possibilities[x][y]);
            foreach (var i in possible)
            {
                debugActual[0] = x;
                debugActual[1] = y;
                debugActual[2] = i;
                debugActual[3] = _x;
                debugActual[4] = _y;
                debugActual[5] = -1;
                //DebugCase();

                if (x == -2 && y == 0)
                {
                    var t = 0;
                }
                if (map.ContainsKey(_x) && map[_x].ContainsKey(_y))
                {
                    debugActual[3] = _x;
                    debugActual[4] = _y;
                    debugActual[5] = map[_x][_y];
                    //DebugCase();
                    if (tiles[map[_x][_y]].GetBorder(intToRotation(k1)) != tiles[i].GetBorder(intToRotation(k2)))
                    {
                        edited = true;
                        possibilities[x][y].Remove(i);
                    }
                } else if (possibilities.ContainsKey(_x) && possibilities[_x].ContainsKey(_y))
                {
                    bool finded = false;
                    foreach (var j in possibilities[_x][_y])
                    {
                        debugActual[3] = _x;
                        debugActual[4] = _y;
                        debugActual[5] = j;
                        //DebugCase();
                        if (tiles[j].GetBorder(intToRotation(k1)) != tiles[i].GetBorder(intToRotation(k2)))
                        {
                            finded = true;
                        }
                    }
                    debugActual[5] = i;
                    if (!finded)
                    {
                        edited = true;
                        possibilities[x][y].Remove(i);
                    }
                }
            }
        }
        debugActual[0] = 0;
        debugActual[1] = 0;
        debugActual[2] = 0;
        debugActual[3] = 0;
        debugActual[4] = 0;
        debugActual[5] = 0;
        if (edited)
        {
            //DebugCase();
            checkEdit.Add(new int[2] { x, y });
        }
        if (possibilities[x][y].Count() >= tiles.Count())
        {
            possibilities[x].Remove(y);
        }
    }

    public string GetDraw(string bg, Ascii m_ascii, int x, int y)
    {
        if (x != pastX || y != pastY)
        {
            Generate(x, y, 5);
            pastX = x;
            pastY = y;
        }
        var size = m_ascii.GetSize(bg);
        var tile = m_ascii.LoadImg(grassMap[x][y] ? tiles[map[x][y]].GetGrassImg : tiles[map[x][y]].GetImg);
        bg = m_ascii.Adding(bg, tile, 0f, 0f, 100f, 100f);
        return bg;
    }

    public Tile getTile()
    {
        return getTile(pastX, pastY);
    }

    public Tile getTile(int x, int y)
    {
        return tiles[map[x][y]];
    }
}