﻿namespace FakeBloggingSystem.CustomExceptions
{
    public class ResourceNotFoundException:Exception
    {
        public ResourceNotFoundException(string message) : base(message) { }
    }
}
