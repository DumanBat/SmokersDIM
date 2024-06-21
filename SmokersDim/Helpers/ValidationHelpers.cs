using System.Text.Json;

public static class ValidationHelpers
{
    public static bool IsEmptyString(string value, out string message)
    {
        if (string.IsNullOrEmpty(value))
        {
            message = $"{nameof(value)} is null or empty";
            return true;
        }

        message = null;
        return false;
    }

    public static bool IsJsonValueKindUndefined(JsonElement value, out string message)
    {
        if (value.ValueKind == JsonValueKind.Undefined)
        {
            message = $"{nameof(value)} is undefined";
            return true;
        }

        message = null;
        return false;
    }
}