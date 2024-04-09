using System.Collections.ObjectModel;

namespace APICatalogo.Models;

public class Categoria // classe anemica pq só foi definido propriedades
{
    // 02 este construtor seria uma boa pratica 
    // no construtor da classe já se inicializa a propriedade Produtos
    // que é uma coleção de objeto Produto
    public Categoria()
    {
        Produtos = new Collection<Produto>();
    }


    // propriedades 01
    public int CategoriaId { get; set; } // indicando a chave primaria
    public string? Nome { get; set; } 
    public string? ImagemUrl { get; set; } 

    // 02 estou definindo que Categoria vai ter uma coleção de objeto Produto
    // que será chamada Produtos, cada categoria pode ter uma coleção de produtos
    // isso por si só ja seria suficiente mas agora vou la na tabela Produto deixar ainda
    // mais explicito
    public ICollection<Produto>? Produtos { get; set; }
}
