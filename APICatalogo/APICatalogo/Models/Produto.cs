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

    
    // tabela relacional que vai indicar o id da categoria do produto
    // e esta tabela esta linkada(relacionada com a tabela Categoria)
    public int CategoriaId { get; set; }


    // criando uma propriedade de navegação aonde estou definindo
    // que Produto está mapeado em Categoria ou seja cada produto
    // terá uma Categoria(lembrando que Categoria tem o id nome e url da imagem)
    public Categoria? Categoria { get; set; }
}
