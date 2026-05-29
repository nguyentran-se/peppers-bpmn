using PeppersBpmn.Domain.Exceptions;

namespace PeppersBpmn.Domain.ValueObjects;

public class ProcessKey : ValueObject
{
    public string Value { get; }

    public ProcessKey(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("Process key cannot be empty.");
        Value = value.Trim();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;

    public static implicit operator string(ProcessKey key) => key.Value;
}
