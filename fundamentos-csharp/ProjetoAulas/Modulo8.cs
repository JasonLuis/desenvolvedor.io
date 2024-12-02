namespace Cadastro;

public class Produto {

    private int Id;
    public string Descricao { get; set; }

    public void ImprimirDescricao() {
        Console.WriteLine($"{GetId()} - {Descricao}");
    }

    public void SetId(int id) {
        Id = id;
    }

    public int GetId() {
        return Id;
    }
}

// Classe estatica
public static class Calculos {
    public static int SomarNumeros(int num1, int num2) {
        return num1 + num2;
    }
}