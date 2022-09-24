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
    public ApplicationUserViewModel From { get; private set; }
    public ApplicationUserViewModel To { get; private set; }
    public bool Read { get; private set; }

    /// <summary>
    /// Creates a new instance of the <see cref="DirectMessageViewModel"/> class from a direct message entity.
    /// </summary>
    /// <param name="entity">The direct message entity.</param>
    /// <returns>The view model.</returns>
    public static DirectMessageViewModel FromEntity(DirectMessage entity) => new()
    {
        Id = entity.Id,
        From = ApplicationUserViewModel.FromEntity(entity.From),
        To = ApplicationUserViewModel.FromEntity(entity.To),
        Read = entity.Read,
        Text = entity.Text
    };
}