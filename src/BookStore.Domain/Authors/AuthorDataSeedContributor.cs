using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Volo.Abp.BlobStoring; // ESSENCIAL
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace BookStore.Authors;

public class AuthorDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IAuthorRepository _authorRepository;
    private readonly AuthorManager _authorManager;
    private readonly IBlobContainer _blobContainer;

    public AuthorDataSeedContributor(
        IAuthorRepository authorRepository, 
        AuthorManager authorManager,
        IBlobContainer blobContainer)
    {
        _authorRepository = authorRepository;
        _authorManager = authorManager;
        _blobContainer = blobContainer;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _authorRepository.GetCountAsync() > 50)
        {
            return;
        }

        var names = new[] { 
            "Machado de Assis", "Clarice Lispector", "Jorge Amado", "Guimarães Rosa", 
            "Carlos Drummond de Andrade", "Cecília Meireles", "Graciliano Ramos" 
            // ... (mantenha sua lista completa de nomes aqui)
        };

        using var httpClient = new HttpClient();

        foreach (var name in names)
        {
            var author = await _authorManager.CreateAsync(
                name,
                new DateTime(Random.Shared.Next(1850, 1990), Random.Shared.Next(1, 12), Random.Shared.Next(1, 28)),
                $"Biografia resumida do renomado autor {name}."
            );

            await _authorRepository.InsertAsync(author);

            try 
            {
                var imageUrl = $"https://api.dicebear.com/7.x/avataaars/png?seed={name.Replace(" ", "")}";
                var imageBytes = await httpClient.GetByteArrayAsync(imageUrl);
                
                // Salvando com o nome do arquivo e os bytes.
                // Para garantir o Content-Type no Azurite, o ABP usa a extensão do arquivo (.png) 
                // se o provedor de Azure estiver configurado corretamente.
                await _blobContainer.SaveAsync($"{author.Id}.png", imageBytes, overrideExisting: true);
            }
            catch (Exception ex)
            {
                // Log de erro silencioso para não travar o seed
                Console.WriteLine($"Falha ao carregar imagem para {name}: {ex.Message}");
            }
        }
    }
}