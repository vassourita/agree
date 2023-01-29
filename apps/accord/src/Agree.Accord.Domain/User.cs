namespace Agree.Accord.Domain;

using System;

public class User
{
    public Guid Id { get; private set; }
    public string Username { get; private set; }
    public string Tag { get; private set; }
    public virtual string NameTag => $"{Username}#{Tag}";
}