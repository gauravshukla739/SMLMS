import { Directive, Input } from "@angular/core";
import { AbstractControl, NG_VALIDATORS, Validator, ValidatorFn } from "@angular/forms";
@Directive({
  selector: "[passwordvalidate]",
  providers: [
    { provide: NG_VALIDATORS, useExisting: PasswordValidator, multi: true }
  ]
})
export class PasswordValidator implements Validator {
  @Input() public password: string;
  public validate(c: AbstractControl): { [key: string]: any } | null {

    if (c.value !== "") {
      if (this.password !== c.value) {
        return {
          passwordvalidate: { value: "These passwords don't match. Try again." }
        };
      } else {
        return null;
      }
    } else {
      return {
        passwordvalidate: { value: "Confirm Password is required." }
      };
    }

  }
}
