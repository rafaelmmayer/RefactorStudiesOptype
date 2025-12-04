namespace Core.Domain.Studies.Types.Alvenaria;

public class AlvenariaStudyInputs : StudyInputs<AlvenariaStudyAdvancedInputs>
{
    public string TipoDeBloco { get; set; } = "asdsadasd";
}

public class AlvenariaStudyAdvancedInputs : StudyAdvancedInputs
{
    public string TipoDeArmacaoDasLajes { get; set; } = string.Empty;
}