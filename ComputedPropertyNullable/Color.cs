using System.Globalization;

namespace ComputedPropertyNullable;

public class Color(float red, float green, float blue, float alpha = 255)
{
    public float Red { get; } = red;
    
    public float Green { get; } = green;
    
    public float Blue { get; } = blue;
    
    public float Alpha { get; } = alpha;
    
    public static Color FromHex(string hex)
    {
        var rgba = ColorUtils.FromArgb(hex);

        return new Color(rgba.red, rgba.green, rgba.blue, rgba.alpha);
    }

    public override string ToString()
    {
        var r = Red.ToString(CultureInfo.InvariantCulture);
        var g = Green.ToString(CultureInfo.InvariantCulture);
        var b = Blue.ToString(CultureInfo.InvariantCulture);
        var a = Alpha.ToString(CultureInfo.InvariantCulture);
        return $"[Color: Red={r}, Green={g}, Blue={b}, Alpha={a}]";
    }
}

