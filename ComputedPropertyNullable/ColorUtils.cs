using System.Globalization;

namespace ComputedPropertyNullable;

// Taken From https://github.com/dotnet/maui/blob/main/src/Graphics/src/Graphics/ColorUtils.cs
internal static class ColorUtils
{
    public static (float red, float green, float blue, float alpha) FromArgb(ReadOnlySpan<char> colorAsHex)
    {
        int red = 0;
        int green = 0;
        int blue = 0;
        int alpha = 255;

        if (!colorAsHex.IsEmpty)
        {
            //Skip # if present
            if (colorAsHex[0] == '#')
                colorAsHex = colorAsHex.Slice(1);

            if (colorAsHex.Length == 6)
            {
                //#RRGGBB
                red = ParseInt(colorAsHex.Slice(0, 2));
                green = ParseInt(colorAsHex.Slice(2, 2));
                blue = ParseInt(colorAsHex.Slice(4, 2));
            }
            else if (colorAsHex.Length == 3)
            {
                //#RGB
                Span<char> temp = stackalloc char[2];
                temp[0] = temp[1] = colorAsHex[0];
                red = ParseInt(temp);

                temp[0] = temp[1] = colorAsHex[1];
                green = ParseInt(temp);

                temp[0] = temp[1] = colorAsHex[2];
                blue = ParseInt(temp);
            }
            else if (colorAsHex.Length == 4)
            {
                //#ARGB
                Span<char> temp = stackalloc char[2];
                temp[0] = temp[1] = colorAsHex[0];
                alpha = ParseInt(temp);

                temp[0] = temp[1] = colorAsHex[1];
                red = ParseInt(temp);

                temp[0] = temp[1] = colorAsHex[2];
                green = ParseInt(temp);

                temp[0] = temp[1] = colorAsHex[3];
                blue = ParseInt(temp);
            }
            else if (colorAsHex.Length == 8)
            {
                //#AARRGGBB
                alpha = ParseInt(colorAsHex.Slice(0, 2));
                red = ParseInt(colorAsHex.Slice(2, 2));
                green = ParseInt(colorAsHex.Slice(4, 2));
                blue = ParseInt(colorAsHex.Slice(6, 2));
            }
        }

        return (red / 255f, green / 255f, blue / 255f, alpha / 255f);
    }
    
    private static int ParseInt(ReadOnlySpan<char> s) =>
        int.Parse(
#if NETSTANDARD2_0
			s.ToString(),
#else
            s,
#endif
            NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
}