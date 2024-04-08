namespace APICatalogo.Models;

public class Produto // classe anemica pq só foi definido propriedades
{
    // propriedades
    public int ProdutoId { get; set; } // chave primaria
    public  string? Nome { get; set; }
    public  string? Descricao { get; set; }
    public  decimal Preco { get; set; }
    public  string? ImageUrl { get; set; }
    public  float Estoque { get; set; }
    public  DateTime DataCadastro { get; set; }
}
