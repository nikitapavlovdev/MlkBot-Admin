using MlkAdmin._1_Domain.Models;
using MlkAdmin._2_Application.DTOs.Responses.Specialized;

namespace MlkAdmin._1_Domain.Interfaces.Mappers;

public interface IGuildMemberStatsMapper
{
    Task<GuildMemberStatisticDto> MapToDto (GuildMemberStatsData dto); 
}
