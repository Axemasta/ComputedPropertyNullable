using ComputedPropertyNullable;

var displayProperties = new DisplayProperties()
{
    ColorHex = "#FF5733"
};

// Example 1: No null warning
if (displayProperties.TryGetTextColor(out var textColor))
{
    PrintColor(textColor);
}
else
{
    PrintError();
}

// Example 2: Null warning
if (displayProperties.TextColor is not null)
{
    //   Program.cs(21, 16): [CS8604] Possible null reference argument for parameter 'color' in 'void PrintColor(Color color)'.
    PrintColor(displayProperties.TextColor);
}
else
{
    PrintError();
}

// Solution 1: Use local variable
var localTextColor = displayProperties.TextColor;

if (localTextColor is not null)
{
    PrintColor(localTextColor);
}
else
{
    PrintError();
}

// Solution 2: Null forgiving operator
if (displayProperties.TextColor is not null)
{
    PrintColor(displayProperties.TextColor!);
}
else
{
    PrintError();
}

void PrintColor(Color color)
{
    Console.WriteLine($"The color is {color}");
}

void PrintError()
{
    Console.WriteLine("The color was null");
}