using System.Drawing;
using System.Windows.Forms;

namespace WizWar1 {
static class Extensions {
    public static bool ContainsMouse(this Button tButton, Point mouse) {
        if (mouse.X >= tButton.ClientRectangle.Left && mouse.X <= tButton.ClientRectangle.Right &&
        mouse.Y >= tButton.ClientRectangle.Top && mouse.Y <= tButton.ClientRectangle.Bottom) {
            return true;
        }

        return false;
    }
}
}
