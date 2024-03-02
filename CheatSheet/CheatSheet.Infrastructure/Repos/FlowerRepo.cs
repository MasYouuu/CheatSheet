using AutoMapper;
using CheatSheet.Application.IRepos;
using CheatSheet.Domain.Entities;
using CheatSheet.Infrastructure.Context;


namespace CheatSheet.Infrastructure.Repos
{
    public class FlowerRepo : IFlowerRepo
    {
        private readonly GardenContext _dbContext;
        private readonly IMapper _mapper;


        public FlowerRepo(GardenContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public async Task AddAsync(FlowerDTO flowerDTO)
        {
            var flower = _mapper.Map<Flower>(flowerDTO);
            _dbContext.Add(flower);
            await _dbContext.SaveChangesAsync();
        }


        public IEnumerable<FlowerDTO> GetAll()
        {
            var flowers = _mapper.Map<List<FlowerDTO>>(_dbContext.Flowers.ToList());
            return flowers.AsEnumerable();
        }


        public IEnumerable<FlowerDTO> GetByGarden(Guid gardenID)
        {
            var garden = _dbContext.Gardens.Find(gardenID);
            var flowers = _mapper.Map<List<FlowerDTO>>(_dbContext.Flowers.Where(f => f.Gardens.Contains(garden)));
            return flowers.AsEnumerable();
        }


        public FlowerDTO GetByID(Guid flowerID)
        {
            var flower = _mapper.Map<FlowerDTO>(_dbContext.Flowers.Find(flowerID));
            return flower;
        }


        public async Task RemoveAsync(FlowerDTO flowerDTO)
        {
            var flower = _mapper.Map<Flower>(flowerDTO);
            _dbContext.Flowers.Remove(flower);
            await _dbContext.SaveChangesAsync();
        }


        public async Task UpdateAsync(FlowerDTO flowerDTO)
        {
            var flower = _mapper.Map<Flower>(flowerDTO);
            _dbContext.Flowers.Update(flower);
            await _dbContext.SaveChangesAsync();
        }
    }
}
