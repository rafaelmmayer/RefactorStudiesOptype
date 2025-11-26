namespace Core.Domain.Studies;

[AttributeUsage(AttributeTargets.Class)]
public class StudyDefinitionAttribute : Attribute
{
    public Type InputsType { get; }
    public Type OutputsType { get; }

    public StudyDefinitionAttribute(Type inputsType, Type outputsType)
    {
        InputsType = inputsType;
        OutputsType = outputsType;
    }
}