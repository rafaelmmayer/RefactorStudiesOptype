namespace Core.Domain.Studies.Types.ParedeFundacao;

public class ParedeFundacaoStudyType : IStudyType
{
    public string TypeName => "parede_e_fundacao";

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
