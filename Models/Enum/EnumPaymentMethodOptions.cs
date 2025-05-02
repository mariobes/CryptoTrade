using System.ComponentModel.DataAnnotations;

public enum EnumPaymentMethodOptions
{
    [Display(Name = "Bank transfer")]
    BankTransfer = 0,

    [Display(Name = "Credit card")]
    CreditCard = 1,

    [Display(Name = "Google Pay")]
    GooglePay = 2
}