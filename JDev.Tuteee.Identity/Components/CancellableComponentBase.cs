namespace JDev.Tuteee.Identity.Components;

using Microsoft.AspNetCore.Components;

public abstract class CancellableComponentBase : ComponentBase, IDisposable  
{  
    private CancellationTokenSource? _cts;  

    protected CancellationToken CancellationToken => (_cts ??= new CancellationTokenSource()).Token;  

    public virtual void Dispose()  
    {  
        _cts?.Cancel();  
        _cts?.Dispose();  
        _cts = null;  
    }  
}  
