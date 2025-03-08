using MusicNotification.Common.Interfaces;
using MusicNotification.Events.Events;

namespace MusicNotification.Catalogs.Artists.Application.EventHandlers;

public interface IGetArtistByPropertiesRequestEventHandler: IRequestEventHandler<GetArtistByPropertiesRequestEvent, GetArtistByPropertiesResponse>
{

}
