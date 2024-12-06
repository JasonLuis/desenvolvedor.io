namespace Modulo11;

public class TrabalhandoComDatas
{
    public void AulaDateTime()
    {
        var date1 = new DateTime(2024, 3, 12, 12, 0, 0);
        var date2 = DateTime.Parse("2024/12/03 21:37:45");
        var date3 = DateTime.Now;
        var date4 = DateTime.Today; 

        Console.WriteLine(date1);
        Console.WriteLine(date2);
        Console.WriteLine(date3);
        Console.WriteLine(date4); 

        // Formatar datas
        Console.WriteLine(date1.ToString("yyyy-MM-dd HH:mm:ss"));

        var dateOffset1 = new DateTimeOffset(DateTime.Now, new TimeSpan(-3, 0, 0));
        Console.WriteLine(dateOffset1.LocalDateTime);
        Console.WriteLine(dateOffset1.UtcDateTime);
    }

    public void AulaSubtraindoDatas()
    {
        var date1 = DateTime.Now;
        var date2 = DateTime.Parse("2024-12-03");

        //var diff = date1 - date2;
        var diff = date1.Subtract(date2);
        Console.WriteLine((int)diff.TotalDays);
        Console.WriteLine((int)diff.TotalHours);
    }

    public void AulaAdicionandoDiasMesAno()
    {
       var date1 = DateTime.Now;
       
       Console.WriteLine(date1.AddDays(1).ToString("yyyy-MM-dd"));
       Console.WriteLine(date1.AddMonths(1).ToString("yyyy-MM-dd"));
       Console.WriteLine(date1.AddYears(1).ToString("yyyy-MM-dd"));
    }    

    public void AulaAdicionandoHoraMinutoSegundo()
    {
       var date1 = DateTime.Now;
       
       Console.WriteLine(date1.AddHours(1).ToString("HH:mm:ss"));
       Console.WriteLine(date1.AddMinutes(10).ToString("HH:mm:ss"));
       Console.WriteLine(date1.AddSeconds(10).ToString("HH:mm:ss"));
    }

    public void AulaDiaDaSemana()
    {
       var date1 = DateTime.Now;
       
       Console.WriteLine(date1.DayOfWeek);
    }

    public void AulaDateOnly() {
        //var somentaData = new DateOnly(2024,02,01);
        var somenteData = DateOnly.Parse("2024-02-01");
        Console.WriteLine(somenteData.ToString("dd/MM/yyyy"));
    }

    public void AulaTimeOnly() {
        //var somenteHora = new TimeOnly(22,53,00,00);
        var somenteHora = TimeOnly.Parse("22:53:00");
        Console.WriteLine(somenteHora.ToString("HH:mm:ss"));
    }
}