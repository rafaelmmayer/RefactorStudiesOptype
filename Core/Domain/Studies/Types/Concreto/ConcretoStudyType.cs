namespace Core.Domain.Studies.Types.Concreto;

public class ConcretoStudyType : IStudyType
{
    public string TypeName => "concreto";

    public string DisplayName => "Estudo de torre: Alvenaria Estrutural";

    public string Descriptions =>
        """
            Obtenha uma planta de modulação e taxas de torres de alvenaria estrutural\n
            Input: modelo IFC da arquitetura do pavimento tipo
            """;

    public double Credits => 1;

    public string Image => "/conc-mini.png";

    public TimeSpan TimeToComplete => TimeSpan.FromDays(2);
}
