using Newtonsoft.Json;
using System.Drawing;

public enum rotationTile
{
    RIGHT = 0,
    UP = 1,
    LEFT = 2,
    DOWN = 3
}

public class Tile
{
    Dictionary<int, string> tileAscii;
    Bitmap tileImg;
    JsonFileManager json;

    static Color roof = Color.FromArgb(0xff, 0x80, 0x80, 0x80);
    static Color wall = Color.FromArgb(0xff, 0xa9, 0xa9, 0xa9);
    static Color floor = Color.FromArgb(0xff, 0x58, 0x29, 0x00);
    static Color empty = Color.FromArgb(0x00, 0x00, 0x00, 0x00);

    public Bitmap GetImg { get => tileImg; }

    private Dictionary<int, string> ReplaceVariants(Dictionary<int, string> toUpdate)
    {
        for (int i = 0; i < toUpdate.Count(); i++)
        {
            toUpdate[i] = toUpdate[i]
                .Replace("|", "-");
        }
        return toUpdate;
    }

    private Dictionary<int, string> ListToDico(List<string> list)
    {
        var res = new Dictionary<int, string>();
        int i = 0;

        foreach (var item in list)
        {
            res[i] = item;
            i++;
        }

        return res;
    }

    private Dictionary<int, string> Rotate(Dictionary<int, string> toRot)
    {
        var res = new Dictionary<int, string>(7);
        for (int i = 0; i < 7; i++)
        {
            res[i] = "";
            for (int j = 0; j < 7; j++)
            {
                res[i] += toRot[j].Substring(6 - i, 1);
            }
        }
        return res;
    }

    public Tile(int tileId, rotationTile rot = rotationTile.RIGHT)
    {
        int sizing = 100;
        tileImg = new Bitmap(7 * sizing, 7 * sizing);
        json = new JsonFileManager();
        tileAscii = ListToDico(JsonConvert.DeserializeObject<List<List<string>>>(json.LoadFile("map/tiles.json"))[tileId]);
        tileAscii = ReplaceVariants(tileAscii);

        for (int i = 0; i < (int)rot; i++)
        {
            tileAscii = Rotate(tileAscii);
        }

        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                for (int k = 0; k < sizing; k++)
                {
                    for (int kk = 0; kk < sizing; kk++)
                    {
                        switch (tileAscii[i].Substring(j, 1))
                        {
                            case "#":
                                tileImg.SetPixel(j * sizing + kk, i * sizing + k, roof);
                                break;
                            case "-":
                                tileImg.SetPixel(j * sizing + kk, i * sizing + k, wall);
                                break;
                            case " ":
                                tileImg.SetPixel(j * sizing + kk, i * sizing + k, floor);
                                break;
                            default:
                                tileImg.SetPixel(j * sizing + kk, i * sizing + k, empty);
                                break;
                        }
                    }
                }
            }
        }
    }

    public string GetBorder(rotationTile rot)
    {
        var ascii = tileAscii;

        for (int i = 0; i < ((int)rot +1) %4; i++)
        {
            ascii = Rotate(ascii);
        }

        return ascii[0];
    }
}