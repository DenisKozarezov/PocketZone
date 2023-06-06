using System.Collections.Generic;

namespace Core.Services.Loading
{
    internal interface ILoadingScreenProvider
    {
        void LoadAndDestroyAsync(Queue<LazyLoadingOperation> operations);
    }
}