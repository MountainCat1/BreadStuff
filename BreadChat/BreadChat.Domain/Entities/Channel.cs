using System.Reflection.Emit;
using BreadChat.Domain.Abstractions;

namespace BreadChat.Domain.Entities;

public class Channel : IEntity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public List<User> Users { get; private set; }

    public static Channel Create(string name, string description)
    {
        return new Channel()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            Users = new List<User>()
        };
    }
}