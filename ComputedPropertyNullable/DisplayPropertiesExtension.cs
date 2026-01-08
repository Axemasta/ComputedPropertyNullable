using System.Diagnostics.CodeAnalysis;

namespace ComputedPropertyNullable;

public static class DisplayPropertiesExtension
{
    extension(DisplayProperties displayProperties)
    {
        public Color? TextColor => 
            !string.IsNullOrEmpty(displayProperties.ColorHex) 
                ? Color.FromHex(displayProperties.ColorHex) 
                : null;
        
        public bool TryGetTextColor([NotNullWhen(true)] out Color? color)
        {
            color = displayProperties.TextColor;
            return color is not null;
        }
    }
}