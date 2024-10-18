//using Catalog.Application.Interfaces;
//using Catalog.Infrastructure.Data;

//namespace Catalog.Infrastructure.Repositories;

//public class MaterialRepository : IMaterialRepository
//{
//    private readonly ApplicationDbContext _context;

//    public MaterialRepository(ApplicationDbContext context)
//    {
//        _context = context ?? throw new ArgumentNullException(nameof(context));
//    }

//    // Récupérer un matériau par son ID
//    public async Task<Material> GetByIdAsync(MaterialId id)
//    {
//        return await _context.Materials
//            .FirstOrDefaultAsync(material => material.Id == id)
//            ?? throw new KeyNotFoundException($"Material with ID {id.Value} not found.");
//    }

//    // Récupérer une liste de matériaux par leurs IDs
//    public async Task<List<Material>> GetByIdsAsync(List<MaterialId> ids)
//    {
//        return await _context.Materials
//            .Where(material => ids.Contains(material.Id))
//            .ToListAsync();
//    }

//    // Ajouter un nouveau matériau
//    public async Task AddAsync(Material material)
//    {
//        if (material == null)
//        {
//            throw new ArgumentNullException(nameof(material));
//        }
//        await _context.Materials.AddAsync(material);
//    }

//    // Supprimer un matériau
//    public async Task RemoveAsync(Material material)
//    {
//        if (material == null)
//        {
//            throw new ArgumentNullException(nameof(material));
//        }
//        _context.Materials.Remove(material);
//    }

//    // Sauvegarder les changements dans la base de données
//    public async Task SaveChangesAsync()
//    {
//        await _context.SaveChangesAsync();
//    }
//}
