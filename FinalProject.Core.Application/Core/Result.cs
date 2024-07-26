

namespace FinalProject.Core.Application.Core
{
    public class Result
    {
        public Result()
        {
            ISuccess = true;
        }
        public bool ISuccess { get; set; }
        public string Message { get; set; }
    }
    public class Result<TData> : Result
    {
        public TData? Data { get; set; }
    }
}
