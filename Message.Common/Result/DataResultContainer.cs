namespace Message.Common.Result
{
    public class ResultContainer<T> : ResultContainer
    {
        public T Data { get; set; }
    }
}