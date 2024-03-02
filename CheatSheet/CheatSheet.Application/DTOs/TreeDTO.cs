using CheatSheet.Domain.Entities;

public record TreeDTO(string Name, List<Garden> Gardens, Species Species, Guid ID, string BarkType, string LeafType);