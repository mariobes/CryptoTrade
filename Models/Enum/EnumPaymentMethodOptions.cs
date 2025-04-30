using System.ComponentModel.DataAnnotations;

public enum EnumPaymentMethodOptions
{
    [Display(Name = "Transferencia bancaria")]
    BankTransfer = 0,

    [Display(Name = "Tarjeta de crédito")]
    CreditCard = 1,

    [Display(Name = "Google Pay")]
    GooglePay = 2
}