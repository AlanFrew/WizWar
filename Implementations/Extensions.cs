using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WizWar1 {
static class Extensions {
    public static bool ContainsMouse(this System.Windows.Forms.Button tButton, Point mouse) {
        if (mouse.X >= tButton.ClientRectangle.Left && mouse.X <= tButton.ClientRectangle.Right &&
        mouse.Y >= tButton.ClientRectangle.Top && mouse.Y <= tButton.ClientRectangle.Bottom) {
            return true;
        }

        return false;
    }
}
}
