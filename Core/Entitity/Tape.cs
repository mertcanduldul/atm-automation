using System.ComponentModel.DataAnnotations;
using Automation.Core.Enumaration;

namespace Automation.Core.Entities;

public class Tape
{
    public Tape()
    {
    }

    public Tape(enumTapeStateType tapeStateType, enumMoneyType tapeMoneyType)
    {
        TAPE_STATE_TYPE_ID = tapeStateType;
        TAPE_MONEY_TYPE_ID = tapeMoneyType;
    }

    [Key] public int ID_TAPE { get; set; }
    public enumTapeStateType TAPE_STATE_TYPE_ID { get; set; }
    public enumMoneyType TAPE_MONEY_TYPE_ID { get; set; }
}