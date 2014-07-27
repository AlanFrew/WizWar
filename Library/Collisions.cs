using System.Drawing;
using System.Collections.Generic;

public class Sprite {
    public Image MyImage;
    public int MinX;
    public int MinY;
    public int MaxX;
    public int MaxY;

    public Sprite(Image tImage) {
        MyImage = tImage;
        MinX = 0;
        MinY = 0;
        MaxX = MyImage.Width;
        MaxY = MyImage.Height;
    }

    public Sprite(Image tImage, int tX, int tY) {
        MyImage = tImage;
        MinX = tX;
        MinY = tY;
        MaxX = MinX + MyImage.Width;
        MaxY = MinY + MyImage.Height;
    }

}

public static class CollisionDetector {
    public static List<Sprite> UnderMouseRect(List<Sprite> sprites, int mouseX, int mouseY) {
        List<Sprite> result = new List<Sprite>();

        foreach (Sprite s in sprites) {
            if (s.MinX <= mouseX) {
                if (s.MaxX >= mouseX) {
                    if (s.MinY <= mouseY) {
                        if (s.MaxY >= mouseY) {
                            result.Add(s);
                        }
                    }
                }
            }
        }

        return result;
    }

    public static List<Sprite> IntersectMyRect(List<Sprite> others, Sprite self) {
        List<Sprite> result = new List<Sprite>();

        foreach (Sprite s in others) {
            if (s.MinX > self.MaxX || s.MaxX < self.MinX || s.MinY > self.MaxY || s.MaxY < self.MinY) {
                continue;
            }
            else {
                result.Add(s);
            }
        }

        return result;
    }
}