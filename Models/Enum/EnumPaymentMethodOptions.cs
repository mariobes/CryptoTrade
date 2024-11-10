using System.ComponentModel.DataAnnotations;

public enum EnumPaymentMethodOptions
{
    [Display(Name = "Transferencia bancaria")]
    TransferenciaBancaria = 0,

    [Display(Name = "Tarjeta de crédito")]
    TarjetaCredito = 1,

    [Display(Name = "Google Pay")]
    GooglePay = 2
}