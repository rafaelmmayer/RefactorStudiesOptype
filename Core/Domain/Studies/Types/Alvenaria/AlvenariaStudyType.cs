namespace Core.Domain.Studies.Types.Alvenaria;

public class AlvenariaStudyType : StudyType
{
    public override string TypeName => "alvenaria";

    public override string DisplayName => "Estudo de torre: Alvenaria Estrutural";

    public override string Descriptions =>
        """
        Obtenha uma planta de modulação e taxas de torres de alvenaria estrutural\n
        Input: modelo IFC da arquitetura do pavimento tipo
        """;

    public override double Credits => 1;
    
    public override string Image => "/alv-mini.png";
    
    public override TimeSpan TimeToComplete => TimeSpan.FromDays(2);
}