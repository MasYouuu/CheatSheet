using AutoMapper;
using CheatSheet.Application.IRepos;
using CheatSheet.Domain.Entities;
using CheatSheet.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheatSheet.Infrastructure.Repos
{
    public class GardenRepo : IGardenRepo
    {
        private readonly GardenContext _dbContext;
        private readonly IMapper _mapper;


        public GardenRepo(GardenContext dbContext, IMapper mapper) 
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public async Task AddAsync(GardenDTO gardenDTO)
        {
            _dbContext.Add(_mapper.Map<Garden>(gardenDTO)); //Mapping von GardenDTO zu Garden, sonst kann es nicht in der db gespeichert werden
            await _dbContext.SaveChangesAsync();
        }


        public async Task<GardenDTO> GetByOwnerAsync(Guid ownerID)
        {
            var garden = _mapper.Map<GardenDTO>(await _dbContext.Gardens.FirstOrDefaultAsync(g => g.Owner.ID == ownerID)); //Mapping von Garden zu GardenDTO
            return garden;
        }


        public IEnumerable<GardenDTO> GetByFlower(FlowerDTO flowerDTO)
        {
            var flower = _mapper.Map<Flower>(flowerDTO);
            var gardens = _mapper.Map<List<GardenDTO>>(_dbContext.Gardens.Where(g => g.Plants.Contains(flower)));
            return gardens.AsEnumerable();
        }


        public IEnumerable<GardenDTO> GetByTree(TreeDTO treeDTO)
        {
            var tree = _mapper.Map<Flower>(treeDTO);
            var gardens = _mapper.Map<List<GardenDTO>>(_dbContext.Gardens.Where(g => g.Plants.Contains(tree)));
            return gardens.AsEnumerable();
        }


        public IEnumerable<GardenDTO> GetAll()
        {
            var gardens = _mapper.Map<List<GardenDTO>>(_dbContext.Gardens.ToList());
            return gardens.AsEnumerable();
        }


        public GardenDTO GetByID(Guid gardenID)
        {
            var garden = _mapper.Map<GardenDTO>(_dbContext.Gardens.Find(gardenID));
            return garden;
        }


        public async Task RemoveAsync(GardenDTO gardenDTO)
        {
            var garden = _mapper.Map<Garden>(gardenDTO);
            _dbContext.Gardens.Remove(garden);
            await _dbContext.SaveChangesAsync();
        }


        public async Task UpdateAsync(GardenDTO gardenDTO)
        {
            var garden = _mapper.Map<Garden>(gardenDTO);
            _dbContext.Gardens.Update(garden);
            await _dbContext.SaveChangesAsync();
        }
    }
}
