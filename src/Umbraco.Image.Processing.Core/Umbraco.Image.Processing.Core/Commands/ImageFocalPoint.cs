using System.Globalization;

namespace Umbraco.Image.Processing.Core.Commands;

/// <summary>
/// ImageSharp.Web's <c>rxy</c> command: the point of the image, as normalized (0..1) distances from its
/// left and top edges, that <see cref="ResizeMode.Crop" /> keeps centered.
/// </summary>
public readonly record struct ImageFocalPoint(float X, float Y)
{
    public static bool TryParse(string? value, out ImageFocalPoint focalPoint)
    {
        focalPoint = default;
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        string[] parts = value.Split(',', StringSplitOptions.TrimEntries);
        if (parts.Length != 2 ||
            !float.TryParse(parts[0], NumberStyles.Float, CultureInfo.InvariantCulture, out float x) ||
            !float.TryParse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out float y) ||
            !float.IsFinite(x) || !float.IsFinite(y))
        {
            return false;
        }

        focalPoint = new ImageFocalPoint(Math.Clamp(x, 0, 1), Math.Clamp(y, 0, 1));
        return true;
    }
}
