using System.ComponentModel.DataAnnotations;

namespace PimpYourBlech_ClassLibrary.Enums;

public enum Gear
{
    [Display(Name = "5-Gang Manuell")]
    Manuell5Gang,
    [Display(Name = "6-Gang Manuell")]
    Manuell6Gang,
    [Display(Name = "6-Gang Automatik")]
    Automatik6Gang,
    [Display(Name = "6-Gang Automatik")]
    Automatik8Gang,
}