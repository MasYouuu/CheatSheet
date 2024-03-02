using CheatSheet.Domain.Entities;

public record FlowerDTO(string Name, List<Garden> Gardens, Species Species, Guid ID, string PetalColor, string BloomTime);