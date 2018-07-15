using System;
using Fusillade;

namespace ORAGH.Services
{
    public interface IApiService<T>
    {
        T GetApi(Priority prority); 
    }
}
