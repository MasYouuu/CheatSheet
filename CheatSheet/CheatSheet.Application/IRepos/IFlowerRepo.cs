namespace CheatSheet.Application.IRepos
{
    public interface IFlowerRepo
    {
        Task AddAsync(FlowerDTO flowerDTO);
        Task UpdateAsync(FlowerDTO flowerDTO);
        Task RemoveAsync(FlowerDTO flowerDTO);
        FlowerDTO GetByID(Guid flowerID);
        IEnumerable<FlowerDTO> GetAll();
        IEnumerable<FlowerDTO> GetByGarden(Guid gardenID);
    }
}
