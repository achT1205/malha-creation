//using Catalog.Application.Interfaces;
//using Catalog.Infrastructure.Data;

//namespace Catalog.Infrastructure.Repositories;
//public class OccasionRepository : IOccasionRepository
//{
//    private readonly ApplicationDbContext _context;

//    public OccasionRepository(ApplicationDbContext context)
//    {
//        _context = context ?? throw new ArgumentNullException(nameof(context));
//    }

//    // Récupérer une occasion par son ID
//    public async Task<Occasion> GetByIdAsync(OccasionId id)
//    {
//        return await _context.Occasions
//            .FirstOrDefaultAsync(occasion => occasion.Id == id)
//            ?? throw new KeyNotFoundException($"Occasion with ID {id.Value} not found.");
//    }

//    // Récupérer une liste d'occasions par leurs IDs
//    public async Task<List<Occasion>> GetByIdsAsync(List<OccasionId> ids)
//    {
//        return await _context.Occasions
//            .Where(occasion => ids.Contains(occasion.Id))
//            .ToListAsync();
//    }

//    // Ajouter une nouvelle occasion
//    public async Task AddAsync(Occasion occasion)
//    {
//        if (occasion == null)
//        {
//            throw new ArgumentNullException(nameof(occasion));
//        }
//        await _context.Occasions.AddAsync(occasion);
//    }

//    // Supprimer une occasion
//    public async Task RemoveAsync(Occasion occasion)
//    {
//        if (occasion == null)
//        {
//            throw new ArgumentNullException(nameof(occasion));
//        }
//        _context.Occasions.Remove(occasion);
//    }

//    // Sauvegarder les changements dans la base de données
//    public async Task SaveChangesAsync()
//    {
//        await _context.SaveChangesAsync();
//    }
//}
