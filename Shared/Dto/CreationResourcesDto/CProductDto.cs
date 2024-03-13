namespace Shared.Dto.CreationResourcesDto;

public record CProductDto : ManipulationProductDto
{
    public string UserId { get; init; }
}