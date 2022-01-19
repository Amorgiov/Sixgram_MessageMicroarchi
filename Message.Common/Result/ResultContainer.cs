﻿using Message.Common.Enums;

namespace Message.Common.Result
{
    public class ResultContainer<T>
    {
        public T Data { get; set; }
        public ErrorType? ErrorType { get; set; }
    }
}