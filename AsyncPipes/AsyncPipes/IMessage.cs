using System;

namespace AsyncPipes
{
    /// <summary>
    /// Interface for Async Pipes Message objects.
    /// </summary>
    public interface IMessage
    {
        Guid MessageId { get; set; }        
        Type MessageType { get; set;}
    }
}