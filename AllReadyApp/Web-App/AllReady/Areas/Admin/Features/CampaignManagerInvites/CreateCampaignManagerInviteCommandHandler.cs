using AllReady.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AllReady.Areas.Admin.Features.CampaignManagerInvites
{
    public class CreateCampaignManagerInviteCommandHandler : IAsyncRequestHandler<CreateCampaignManagerInviteCommand, int>
    {
        private AllReadyContext _context;

        public CreateCampaignManagerInviteCommandHandler(AllReadyContext context)
        {
            _context = context;
        }

        public Func<DateTime> DateTimeUtcNow = () => DateTime.UtcNow;

        public async Task<int> Handle(CreateCampaignManagerInviteCommand message)
        {
            var campaignManagerInvite = new CampaignManagerInvite
            {
                InviteeEmailAddress = message.Invite.InviteeEmailAddress,
                SentDateTimeUtc = DateTimeUtcNow(),
                CustomMessage = message.Invite.CustomMessage,
                SenderUserId = message.UserId,
                CampaignId = message.Invite.CampaignId
            };
            _context.CampaignManagerInvites.Add(campaignManagerInvite);
            await _context.SaveChangesAsync();
            return campaignManagerInvite.Id;
        }
    }
}
