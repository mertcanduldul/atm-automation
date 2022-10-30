using System.ComponentModel.DataAnnotations;
using Automation.Core.Enumaration;

namespace Automation.Core.Entities;

public class Tape
{
    public Tape()
    {
    }

    public Tape(List<Money> moneyList, enumTapeStateType tapeStateType, enumTapeMoneyType tapeMoneyType)
    {
        MONIES = moneyList;
        TAPE_STATE_TYPE_ID = tapeStateType;
        TAPE_MONEY_TYPE_ID = tapeMoneyType;
    }

    [Key] public int ID_TAPE { get; set; }
    [MaxLength(100)] public List<Money> MONIES { get; set; }
    public enumTapeStateType TAPE_STATE_TYPE_ID { get; set; }
    public enumTapeMoneyType TAPE_MONEY_TYPE_ID { get; set; }
}