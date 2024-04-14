using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {

        // pegando o contexto  
        private readonly AppDbContext _context;

        //  construtor que recebe o contexto 
        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }


        // Request Get que retorna uma lista de categoria
        // só lembrando que IEnumerable é um tipo de lista, então poderia ser List()
        // que funcionaria do mesmo jeito

        [HttpGet("produtos")] // indicando que o nome da rota será produtos
                              // então o endpoint sera categoria/produtos 
        public ActionResult <IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            // pegando todas as categorias via list
            // e além das categorias usando o método INCLUDE pego também os produtos
            // atentar neste include a função lambda indicando que quero os Produtos
            var categoriasEProdutos = _context.Categorias.Include(p => p.Produtos).ToList();


            // tratando o erro caso categorias for null
            if (categoriasEProdutos is null)
            {
                return NotFound("Categorias não encontradas");
            }

            // retorna a lista de categorias
            return categoriasEProdutos;
        }


        // RequestGet que chama todas as categorias 
        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            // pegando todas as categorias via list
            var categorias = _context.Categorias.ToList();

            // 02 se eu quiser fazer uma consulta mais otimizada eu poderia usar AsNoTracking
            // porem ele só pode ser usado se eu não for alterar esses dados
            // essa é uma forma de otimizar consultas get
            var categoriasOtimizada = _context.Categorias.AsNoTracking().ToList();

            // tratando o erro caso categorias for null
            if (categorias is null)
            {
                return NotFound("Categorias não encontradas");
            }

            // retorna a lista de categorias
            return categorias;
        }

        // Request Get para retornar categoria especifica
        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            
            var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);


            // agora como dito assim vou tratar esse produto caso venha null(erro)
            if (categoria is null)
            {
                return NotFound("Categoria não encontrado");
            }

            // deu certo produto recebeu o dado
            return Ok(categoria);
        }


        // Request POST cria uma categoria
        [HttpPost]
        public ActionResult Post(Categoria categoria) // vai receber na request um tipo Categoria
        {

            // validação
            if (categoria is null)
            {
                // usando metodo do ActionResult o BadRequest
                return BadRequest();
            }


            // acessando a tabela Categoria atravez do contexto(_contexto)
            // usando o método Add adiciono o dado recebido no parametro a tabela categoria
            _context.Categorias.Add(categoria);


            // é necessario persistir dos dados na tabela do banco de dados
            // para isso usaremos o metodo SaveChanges
            _context.SaveChanges();


            // usando a classe CreatedAtRouteResult que é uma ActionResult
            // que retorna o status 201 created e os dados que foram adicionados
            // "ObterCategoria" = nome da rota
            // id que foi incluido {id = produto.CategoriaId}
            // e o objeto que foi adicionado = categoria
            return new CreatedAtRouteResult("ObterCategoria",
                new { id = categoria.CategoriaId }, categoria);

        }

        // Request Put para alterar uma categoria
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            // validação do id
            if (id != categoria.CategoriaId)
            {
                return BadRequest();
            }


            // usando a instancia do contexto(_context) estou informando que 
            // seu estado esta sendo modificado, até aqui só esta sendo feito 
            // na memoria
            _context.Entry(categoria).State = EntityState.Modified;

            // agora precisa persistir no banco de dados para de fato
            // fazer a atualização
            _context.SaveChanges();


            // deu certo retorno o metodo ok que gera status 200
            // e os dados atualizados
            return Ok(categoria);
        }


        // Request Delete para deletar uma categoria
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            // acessando atravez do context a tabela categorias 
            // usando o método First se o id do parametro bate acom algum id da tabela
            var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

            // uma outra alternativa para encontrar a categoria seria
            var categoria02 = _context.Categorias.Find(id);

            // validação
            if (categoria is null)
            {
                return NotFound("Categoria não encontrada");
            }

            // depois da validação remove o dado 
            _context.Categorias.Remove(categoria);

            // altera na tabela de fato
            _context.SaveChanges();

            // faz o retorno de sucesso 200 mostrando o item excluido
            return Ok(categoria);
        }
    }
}
