namespace Core.Domain.Studies;

public abstract class StudyInputs
{
}

public interface ISetAdvancedInputs
{
    public void SetAdvancedInputs(StudyAdvancedInputs advancedInputs);
}

public abstract class StudyInputs<T> : StudyInputs, ISetAdvancedInputs
    where T : StudyAdvancedInputs
{
    public T AdvancedInputs { get; private set; } = null!;
    
    public void SetAdvancedInputs(StudyAdvancedInputs advancedInputs)
    {
        if (advancedInputs is not T typed)
        {
            throw new ArgumentException(
                $"Tipo inválido para SetInputs. Esperado: {typeof(T).Name}, "
                + $"recebido: {advancedInputs.GetType().Name}."
            );
        }

        AdvancedInputs = typed;
    }
}

public abstract class StudyAdvancedInputs {} 