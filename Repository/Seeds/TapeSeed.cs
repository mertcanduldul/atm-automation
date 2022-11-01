using Automation.Core.Entities;
using Automation.Core.Enumaration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Repository.Seeds
{
    public class TapeSeed : IEntityTypeConfiguration<Tape>
    {
        public void Configure(EntityTypeBuilder<Tape> builder)
        {
            builder.HasData(
                new Tape { ID_TAPE = 1, TAPE_MONEY_TYPE_ID = enumTapeMoneyType.ALL, TAPE_STATE_TYPE_ID = enumTapeStateType.Inbound },
                new Tape { ID_TAPE = 2, TAPE_MONEY_TYPE_ID = enumTapeMoneyType.TRY, TAPE_STATE_TYPE_ID = enumTapeStateType.InboundOutbound },
                new Tape { ID_TAPE = 3, TAPE_MONEY_TYPE_ID = enumTapeMoneyType.USD, TAPE_STATE_TYPE_ID = enumTapeStateType.InboundOutbound },
                new Tape { ID_TAPE = 4, TAPE_MONEY_TYPE_ID = enumTapeMoneyType.EUR, TAPE_STATE_TYPE_ID = enumTapeStateType.InboundOutbound },
                new Tape { ID_TAPE = 5, TAPE_MONEY_TYPE_ID = enumTapeMoneyType.ALL, TAPE_STATE_TYPE_ID = enumTapeStateType.InboundOutbound }
            );
        }
    }
}
