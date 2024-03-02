using CheatSheet.Domain.Entities;

public record GardenDTO(Guid ID, string Location, List<Plant> Plants, Owner Owner);