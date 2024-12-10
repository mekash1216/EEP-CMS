import { AbstractControl, ValidatorFn } from '@angular/forms';

export function cardNoValidator(): ValidatorFn {
  return (control: AbstractControl): { [key: string]: any } | null => {
    const cardNoPattern = /^ep\d{6}$/i; // Case insensitive 'EP' followed by 6 digits
    const valid = cardNoPattern.test(control.value);
    return valid ? null : { invalidCardNo: { value: control.value } };
  };
}
