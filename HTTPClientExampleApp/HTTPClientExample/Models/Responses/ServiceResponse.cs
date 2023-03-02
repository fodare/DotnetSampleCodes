﻿namespace HTTPClientExample.Models.Responses
{
    public class ServiceResponse<T>
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public T? Data { get; set; }
    }
}
