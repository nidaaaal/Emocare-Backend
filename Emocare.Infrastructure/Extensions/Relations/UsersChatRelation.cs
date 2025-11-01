using Emocare.Domain.Entities.Private;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emocare.Infrastructure.Extensions.Relations
{
    public class UsersChatSessionRelation : IEntityTypeConfiguration<UserChatSession>
    {
        public void Configure(EntityTypeBuilder<UserChatSession> entity)
        {
            entity.HasMany(x => x.Messages).WithOne(x => x.ChatSession).HasForeignKey(x => x.ChatSessionId).OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(x => x.Participants).WithOne(x => x.ChatSession).HasForeignKey(x => x.ChatSessionId).OnDelete(DeleteBehavior.Cascade);
        }
    }


    public class UserChatMessageRelation : IEntityTypeConfiguration<UserChatMessage>
    {
        public void Configure(EntityTypeBuilder<UserChatMessage> entity)
        {
            entity.Property(x => x.SenderRole).HasConversion<string>();

            entity.HasOne(x => x.Sender).WithMany().HasForeignKey(x => x.SenderId).OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.Receiver).WithMany().HasForeignKey(x => x.ReceiverId).OnDelete(DeleteBehavior.Restrict);


        }
    }


    public class ChatParticipantRelation : IEntityTypeConfiguration<ChatParticipant>
    {
        public void Configure(EntityTypeBuilder<ChatParticipant> entity)
        {
            entity.Property(x => x.Role).HasConversion<string>();

            entity.HasOne(x => x.User).WithMany(x=>x.ChatParticipants).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
