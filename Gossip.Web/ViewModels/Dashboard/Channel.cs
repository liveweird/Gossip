using FluentValidation;

namespace Gossip.Web.ViewModels.Dashboard
{
    public class Channel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class ChannelValidator : AbstractValidator<Channel>
    {
        public ChannelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(16);
            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(32);
        }
    }
}
