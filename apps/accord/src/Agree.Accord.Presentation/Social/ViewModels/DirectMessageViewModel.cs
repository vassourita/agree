namespace Agree.Accord.Presentation.Social.ViewModels;

using System;
using Agree.Accord.Domain.Social;
using Agree.Accord.Presentation.Identity.ViewModels;

/// <summary>
/// The view model for a direct message.
/// </summary>
public class DirectMessageViewModel
{
    public Guid Id { get; private set; }
    public string Text { get; private set; }
    public UserAccountViewModel From { get; private set; }
    public UserAccountViewModel To { get; private set; }
    public bool Read { get; private set; }
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// Creates a new instance of the <see cref="DirectMessageViewModel"/> class from a direct message entity.
    /// </summary>
    /// <param name="entity">The direct message entity.</param>
    /// <returns>The view model.</returns>
    public static DirectMessageViewModel FromEntity(DirectMessage entity) => new()
    {
        Id = entity.Id,
        From = UserAccountViewModel.FromEntity(entity.From),
        To = UserAccountViewModel.FromEntity(entity.To),
        Read = entity.Read,
        Text = entity.Text,
        CreatedAt = entity.CreatedAt
    };
}