namespace PeppersBpmn.Domain.ValueObjects;

public enum ProcessStatusValue
{
    Active,
    Suspended,
    Completed,
    ExternallyTerminated,
    InternallyTerminated
}

public class ProcessStatus : ValueObject
{
    public ProcessStatusValue Value { get; }

    private ProcessStatus(ProcessStatusValue value) => Value = value;

    public static readonly ProcessStatus Active = new(ProcessStatusValue.Active);
    public static readonly ProcessStatus Suspended = new(ProcessStatusValue.Suspended);
    public static readonly ProcessStatus Completed = new(ProcessStatusValue.Completed);
    public static readonly ProcessStatus ExternallyTerminated = new(ProcessStatusValue.ExternallyTerminated);
    public static readonly ProcessStatus InternallyTerminated = new(ProcessStatusValue.InternallyTerminated);

    public static ProcessStatus From(string value) => value.ToLowerInvariant() switch
    {
        "active" => Active,
        "suspended" => Suspended,
        "completed" => Completed,
        "externally_terminated" => ExternallyTerminated,
        "internally_terminated" => InternallyTerminated,
        _ => Active
    };

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value.ToString();
}
