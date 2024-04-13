using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")] // removi api/ para deixar o rota apenas com o nome do controller
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        // a primeira coisa é injetar  da classe AppDbcontext
        // definindo a variavel de somente leitura para nao ser alterada
        // depois que for atribuida
        private readonly AppDbContext _context;

        // Depois de criar a varaiel _context com o contexto
        // vou gerar um construtor que recebe o contexto como parametro e
        // adiciona na variavel _context
        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }


        // criando uma actions do tipo get que deve retornar todos os produtos
        // criando um método public  que retorna uma lista de produtos
        // vamos usar uma interface IEnumerable que é tipada com uma coleção de objetos
        // do tipo Produto
        // Obs poderia usar no lugar de IEnumerable List() pq vai retornar uma lista porém
        // neste caso IEnumrable é uma lista mais otimizada para este caso
        // o ActionResult é pq eu preciso tratar caso produtos venha como null
        // então com esse ActionResult esse método get retorna OU a lista tipo Produto
        // ou um metodo do tipo ActionResult e um desses métodos é o NotFound
        // que é um método que será usado para indicar o  erro
        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            // atravez do _context ele consegue acessar a tabela Produtos
            // e usando o método ToList() para pegar a lista de produtos
            var produtos = _context.Produtos.ToList();


            // tratando o erro se caso a lista de produtos vier null retorna
            // o método NotFound()  vem da classe ActionResult;
            if (produtos is null)
            {
                return NotFound("Produtos não encontrados");
            }

            // agora a variavel produtos tem a lista de todos os produtos
            // da tabela Produtos então vou dar um return nesta variavel
            return produtos;
        }



        // criando um metodo do tipo Get que retorna um unico produto
        // a mesma ideia do get acima tem ActionResult para tratar dado caso
        // tenha erro 
        // precisa passar o ID no request do tipo inteiro
        // nome da rota Name = "ObterProduto" 
        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            // aqui a mesma ideia do get acima crio uma vairavel que recebe
            // atravez do _context os dados da tabela Produto usando o metodo
            // FirstOrDefult esse método retorna o primeiro dado e se der erro
            // retorna um valor null, no parametro estou fazendo uma função lambda
            // indicando que eu quero o item com o produto com id igual ao do parametro
            // passado pelo metodo get
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);


            // agora como dito assim vou tratar esse produto caso venha null(erro)
            if (produto is null)
            {
                return NotFound("Produto não encontrado");
            }

            // deu certo produto recebeu o dado
            return produto;
        }



        // metodo tipo POST
        [HttpPost]
        public ActionResult Post(Produto produto) // vai receber na request um tipo Produto
        {

            // validação
            if (produto is null)
            {
                // usando metodo do ActionResult o BadRequest
                return BadRequest();
            }


            // acessando a tabela Produto atravez do contexto(_contexto)
            // usando o método Add adiciono o dado recebido no parametro a tabela produto
            // até aqui eu estou trabalhando na memoria
            _context.Produtos.Add(produto);


            // é necessario persistir dos dados na tabela do banco de dados
            // para isso usaremos o metodo SaveChanges
            _context.SaveChanges();


            // usando a classe CreatedAtRouteResult que é uma ActionResult
            // que retorna o status 201 created e os dados que foram adicionados
            // "ObterProduto" = nome da rota
            // id que foi incluido {id = produto.ProdutoId}
            // e o objeto que foi adicionado = produto
            return new CreatedAtRouteResult("ObterProduto",
                new { id = produto.ProdutoId }, produto);

        }


        // método tipo PUT
        // recebe um id que vai ser usado para identificar o produto que será alterado
        // e um objeto tipo Produto que vai substituir o produto que precisa ser alterado
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            // validação do id
            if (id != produto.ProdutoId)
            {
                return BadRequest();
            }


            // usando a instancia do contexto(_context) estou informando que 
            // seu estado esta sendo modificado, até aqui só esta sendo feito 
            // na memoria
            _context.Entry(produto).State = EntityState.Modified;

            // agora precisa persistir no banco de dados para de fato
            // fazer a atualização
            _context.SaveChanges();


            // deu certo retorno o metodo ok que gera status 200
            // e os dados atualizados
            return Ok(produto);
        }

        // metodo tipo DELETE
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            // acessando atravez do context a tabela produtos 
            // usando o método First se o id do parametro bate acom algum id da tabela
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

            // uma outra alternativa para encontrar o produto seria
            var produto02 = _context.Produtos.Find(id);

            // validação
            if(produto is null)
            {
                return NotFound("Produto não localizado");
            }

            // depois da validação remove o dado 
            _context.Produtos.Remove(produto);

            // altera na tabela de fato
            _context.SaveChanges();

            // faz o retorno de sucesso 200 mostrando o item excluido
            return Ok(produto);
        }
    }
}
