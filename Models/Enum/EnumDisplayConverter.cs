using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

public class EnumDisplayConverter<T> : JsonConverter<T> where T : Enum
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Lee el valor como string y convierte al enum
        var enumValue = reader.GetString();
        return (T)Enum.Parse(typeof(T), enumValue!);
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        // Obtiene el valor de Display del enum o su nombre por defecto
        var displayAttribute = value.GetType()
            .GetField(value.ToString())
            ?.GetCustomAttribute<DisplayAttribute>();

        var displayName = displayAttribute?.Name ?? value.ToString();
        writer.WriteStringValue(displayName);
    }
}
