using LanchesMac.Context;
using LanchesMac.Models;
using LanchesMac.Models.Usuarios;
using LanchesMac.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Services
{
    public class SeedInitialService : ISeedInitial
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<Funcao> _roleManager;
        private readonly AppDbContext _context;

        public SeedInitialService(UserManager<Usuario> userManager, RoleManager<Funcao> roleManager, AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context; ;
        }

        public void Seed()
        {
            CreateRole().GetAwaiter().GetResult();
            CreateUser("admin@mac.com.br", "Admin", "Teste@2024").GetAwaiter().GetResult();
            CreateCategoria("Normal", "Lanche feito com ingredientes normais").GetAwaiter().GetResult();
            CreateCategoria("Natural", "Lanche feito com ingredientes integrais e naturais").GetAwaiter().GetResult();
            CreateLanche(1, "Pão, hambúrger, ovo, presunto, queijo e batata palha", "Delicioso pão de hambúrger com ovo frito; presunto e queijo de primeira qualidade acompanhado com batata palha", true, "http://www.macoratti.net/Imagens/lanches/cheesesalada1.jpg", "http://www.macoratti.net/Imagens/lanches/cheesesalada1.jpg", false, "Cheese Salada", 12.50m).GetAwaiter().GetResult();
            CreateLanche(1, "Pão, presunto, mussarela e tomate", "Delicioso pão francês quentinho na chapa com presunto e mussarela bem servidos com tomate preparado com carinho.", true, "http://www.macoratti.net/Imagens/lanches/mistoquente4.jpg", "http://www.macoratti.net/Imagens/lanches/mistoquente4.jpg", false, "Misto Quente", 8.00m).GetAwaiter().GetResult();
            CreateLanche(1, "Pão, hambúrger, presunto, mussarela e batalha palha", "Pão de hambúrger especial com hambúrger de nossa preparação e presunto e mussarela; acompanha batata palha.", true, "http://www.macoratti.net/Imagens/lanches/cheeseburger1.jpg", "http://www.macoratti.net/Imagens/lanches/cheeseburger1.jpg", false, "Cheese Burger", 11.00m).GetAwaiter().GetResult();
            CreateLanche(2, "Pão Integral, queijo branco, peito de peru, cenoura, alface, iogurte", "Pão integral natural com queijo branco, peito de peru e cenoura ralada com alface picado e iorgurte natural.", true, "http://www.macoratti.net/Imagens/lanches/lanchenatural.jpg", "http://www.macoratti.net/Imagens/lanches/lanchenatural.jpg", true, "Lanche Natural Peito Peru", 15.00m).GetAwaiter().GetResult();
        }

        private async Task CreateRole()
        {
            if (!_roleManager.RoleExistsAsync("Admin").Result)
            {
                Funcao role = new Funcao();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }
        }

        private async Task<IdentityResult> CreateUser(string email, string name, string password)
        {
            var retorno = await _userManager.FindByEmailAsync(email);
            if (retorno == null)
            {
                Usuario user = new Usuario();
                user.UserName = name;
                user.Email = email;
                IdentityResult result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Admin").Wait();
                }

                return result;
            }
            else
            {
                return default;
            }
        }

        private async Task CreateCategoria(string categoriaNome, string descricao)
        {
            var exists = await _context.Categorias.AnyAsync(c => c.CategoriaNome == categoriaNome);
            if (exists != true)
            {
                var categoria = new Categoria
                {
                    CategoriaNome = categoriaNome,
                    Descricao = descricao
                };

                _context.Categorias?.Add(categoria);
                await _context.SaveChangesAsync();
            }
        }

        private async Task CreateLanche(int categoriaId, string desCurta, string desDetalhada, bool emEstoque, string imagemThumbnailUrl, string imagemUrl, bool preferido, string nome, decimal preco)
        {
            var exists = await _context.Lanches.AnyAsync(c => c.Nome == nome);
            if (exists != true)
            {
                var lanche = new Lanche
                {
                    CategoriaId = categoriaId,
                    DescricaoCurta = desCurta,
                    DescricaoDetalhada = desDetalhada,
                    EmEstoque = emEstoque,
                    ImagemThumbnailUrl = imagemThumbnailUrl,
                    ImagemUrl = imagemUrl,
                    IsLanchePreferido = preferido,
                    Nome = nome,
                    Preco = preco
                };

                _context.Lanches?.Add(lanche);
                await _context.SaveChangesAsync();
            }
        }
    }
}
