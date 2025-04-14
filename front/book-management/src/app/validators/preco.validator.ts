import { AbstractControl, ValidationErrors } from "@angular/forms";

export function precoValidator(control: AbstractControl): ValidationErrors | null {
    const value = control.value;
  
    const cleanValue = value?.toString().replace(/[R$\s.]/g, '').replace(',', '.');
  
    const parsed = parseFloat(cleanValue);
  
    const isValid = !isNaN(parsed) && parsed >= 0 && /^\d+(\.\d{1,2})?$/.test(cleanValue);
  
    return isValid ? null : { precoInvalido: true };
  }