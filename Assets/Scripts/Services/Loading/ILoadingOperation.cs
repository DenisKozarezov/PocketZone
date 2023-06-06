using System.Collections;

namespace Core.Services.Loading
{
    internal interface ILoadingOperation
    {
        string Description { get; }
        float Progress { get; }
        bool IsCompleted { get; }
        IEnumerator AwaitForLoad();
    }
}
