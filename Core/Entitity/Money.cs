using System.ComponentModel.DataAnnotations;
using Automation.Core.Enumaration;

namespace Automation.Core.Entities;

public class Money
{
    public Money()
    {
    }

    public Money(enumMoneyType moneyType, enumMoneyName moneyName, int moneyValue, int idTape)
    {
        MONEY_TYPE_ID = moneyType;
        MONEY_NAME = moneyName;
        MONEY_VALUE = moneyValue;
        ID_TAPE = idTape;
    }

    [Key] public int ID_MONEY { get; set; }
    public enumMoneyType MONEY_TYPE_ID { get; set; }
    public enumMoneyName MONEY_NAME { get; set; }
    public int MONEY_VALUE { get; set; }
    public int ID_TAPE { get; set; }
}