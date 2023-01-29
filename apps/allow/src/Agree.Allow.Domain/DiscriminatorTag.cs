namespace Agree.Allow.Domain;

using System;
using System.Collections.Generic;
using System.Linq;
using Agree.SharedKernel;

/// <summary>
/// Represents a four digit number used to differentiate users with the same name.
/// </summary>
public class DiscriminatorTag : ValueObject
{
    /// <summary>
    /// The tag value as a ushort. Will not have four digits if it starts with zeros.
    /// </summary>
    /// <value>Gets the tag value as a ushort.</value>
    public ushort Value { get; private set; }

    /// <summary>
    /// Returns tag value as a string. Will always be a numeric string and have a length of four.
    /// </summary>
    /// <returns>The tag value as a string.</returns>
    public override string ToString() => Value.ToString().PadLeft(4, '0');

    private DiscriminatorTag(ushort value) => Value = value;

    /// <summary>
    /// Tries to parse a object into a <c>DiscriminatorTag</c>.
    /// </summary>
    /// <param name="value">The value to be parsed</param>
    /// <param name="tag">Outputs the parsed value if <c>value</c> was converted succesfully; otherwise, a zero-filled <c>DiscriminatorTag</c></param>
    /// <returns><c>true</c> if <c>value</c> converted succesfully; otherwise, <c>false</c>.</returns>
    public static bool TryParse(object value, out DiscriminatorTag tag)
    {
        tag = new DiscriminatorTag(0);
        var strValue = value.ToString();

        if (string.IsNullOrEmpty(strValue) || strValue.Length > 4 || !strValue.All(char.IsDigit))
        {
            return false;
        }

        if (ushort.TryParse(strValue.PadLeft(4, '0'), out var parsedValue))
        {
            tag = new DiscriminatorTag(parsedValue);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Parses a object into a <c>DiscriminatorTag</c>.
    /// </summary>
    /// <param name="value">The value to be parsed</param>
    /// <returns>The parsed value if <c>value</c> was converted succesfully; otherwise, a zero-filled <c>DiscriminatorTag</c>.</returns>
    public static DiscriminatorTag Parse(object value)
    {
        var tag = new DiscriminatorTag(0);
        var strValue = value.ToString();

        if (string.IsNullOrEmpty(strValue) || strValue.Length > 4 || !strValue.All(char.IsDigit))
        {
            return tag;
        }

        if (ushort.TryParse(strValue.PadLeft(4, '0'), out var parsedValue))
        {
            tag = new DiscriminatorTag(parsedValue);
            return tag;
        }

        return tag;
    }

    /// <summary>
    /// Generates a new <c>DiscriminatorTag</c> with a pseudo-random value between 1 and 9999.
    /// <para>
    /// This method does not checks if a user already has the same name and tag, it just generates a new random one.
    /// </para>
    /// </summary>
    /// <returns>A <c>DiscriminatorTag</c> between 1 and 9999.</returns>
    public static DiscriminatorTag NewTag()
    {
        var number = new Random().Next(1, 9999);
        return TryParse(number, out var tag) ? tag : NewTag();
    }

    /// <summary>
    /// Increments the tag value by one.
    /// </summary>
    public DiscriminatorTag Increment()
    {
        var newValue = Value + 1;
        return new DiscriminatorTag((ushort)newValue);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override int CompareTo(object obj)
    {
        if (obj == null)
        {
            return 1;
        }

        if (obj is DiscriminatorTag other)
        {
            return Value.CompareTo(other.Value);
        }

        throw new ArgumentException("Object is not a DiscriminatorTag");
    }
}