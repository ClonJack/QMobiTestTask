using System.Threading;

namespace ShipGame
{
    public interface ICancellationToken
    {
        CancellationTokenSource CancellationToken { get; set; }
    }
}