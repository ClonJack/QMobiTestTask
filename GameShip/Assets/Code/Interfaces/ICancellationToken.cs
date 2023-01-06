using System.Threading;

namespace Code.Interfaces
{
    public interface ICancellationToken
    {
        CancellationTokenSource CancellationToken { get; set; }
    }
}