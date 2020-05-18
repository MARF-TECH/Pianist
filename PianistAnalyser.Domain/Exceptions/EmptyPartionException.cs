using System;

namespace PianistAnalyser.Domain.Exceptions
{
    public class EmptyPartionException : Exception
    {
        public EmptyPartionException() : base("Your Partition is either empty or contains errors")
        {
        }

    }
}