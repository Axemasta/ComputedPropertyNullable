# ComputedPropertyNullable

[![Dotnet Runtime Issue Status](https://img.shields.io/github/issues/detail/state/dotnet/runtime/123004)](https://github.com/dotnet/runtime/issues/123004)

Showcasing how nullability and extension members interact in .NET 10,u sing .NET 10's extension properties, if you have a property that is nullable:
```csharp
public static class DisplayPropertiesExtension
{
    extension(DisplayProperties displayProperties)
    {
        public Color? TextColor => 
            !string.IsNullOrEmpty(displayProperties.ColorHex) 
                ? Color.FromHex(displayProperties.ColorHex) 
                : null;
    }
}
```

And you perform a null check, then pass the value to a method expecting a non nullable argument:
```csharp
#nullable enable
void PrintColor(Color color)
{
    Console.WriteLine($"The color is {color}");
}

var displayProperties = new DisplayProperties()
{
    ColorHex = "#FF5733"
};

if (displayProperties.TextColor is not null)
{
    PrintColor(displayProperties.TextColor);
}
```

The compiler will raise the following warning:
```
  Program.cs(21, 16): [CS8604] Possible null reference argument for parameter 'color' in 'void PrintColor(Color color)'.
```

This can be alleviated by using the null forgiving operator:

```csharp
PrintColor(displayProperties.TextColor!);
```

Or by capturing the computed property as a local variable before performing the null check:
```csharp
var localTextColor = displayProperties.TextColor;

if (localTextColor is not null)
{
    PrintColor(localTextColor);
}
```
