namespace APICatalogo.Models;

public class Categoria // classe anemica pq só foi definido propriedades
{
    // propriedades
    public int CategoriaId { get; set; } // indicando a chave primaria
    public string? Nome { get; set; } 
    public string? ImagemUrl { get; set; } 
}
