using Newtonsoft.Json;
using System.Drawing;

public enum rotationTile
{
    RIGHT,
    LEFT,
    UP,
    DOWN
}

public class Tile
{
    string tileAscii;
    Bitmap tileImg;
    JsonFileManager json;

    static Color roof = Color.FromArgb(0xff, 0x80, 0x80, 0x80);
    static Color wall = Color.FromArgb(0xff, 0xa9, 0xa9, 0xa9);
    static Color floor = Color.FromArgb(0xff, 0x58, 0x29, 0x00);
    static Color empty = Color.FromArgb(0x00, 0x00, 0x00, 0x00);

    public Bitmap GetImg { get => tileImg; }

    public Tile(int tileId, rotationTile rot = rotationTile.RIGHT)
    {
        tileImg = new Bitmap(7, 7);
        json = new JsonFileManager();
        tileAscii = JsonConvert.DeserializeObject<List<string>>(json.LoadFile("map/tiles.json"))[tileId];
        var lineTileAscii = tileAscii.Split(",");

        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                switch (lineTileAscii[i].Substring(j, 1))
                {
                    case "#":
                        tileImg.SetPixel(j, i, roof);
                        break;
                    case "-":
                        tileImg.SetPixel(j, i, wall);
                        break;
                    case "|":
                        tileImg.SetPixel(j, i, wall);
                        break;
                    case " ":
                        tileImg.SetPixel(j, i, floor);
                        break;
                    default:
                        tileImg.SetPixel(j, i, empty);
                        break;
                }
            }
        }
    }
}