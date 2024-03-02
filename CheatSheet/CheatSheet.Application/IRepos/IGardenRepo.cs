using CheatSheet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheatSheet.Application.IRepos
{
    public interface IGardenRepo
    {
        Task AddAsync(GardenDTO gardenDTO);
        Task UpdateAsync(GardenDTO gardenDTO);
        Task RemoveAsync(GardenDTO gardenDTO);
        GardenDTO GetByID(Guid gardenID);
        IEnumerable<GardenDTO> GetAll();
        Task<GardenDTO> GetByOwnerAsync(Guid ownerID);
        IEnumerable<GardenDTO> GetByFlower(FlowerDTO flowerDTO);
        IEnumerable<GardenDTO> GetByTree(TreeDTO treeDTO);
    }
}
