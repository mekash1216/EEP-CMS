import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
@Component({
  selector: 'app-sick-leave-form',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './sick-leave-form.component.html',
  styleUrl: './sick-leave-form.component.scss'
})
export class SickLeaveFormComponent {
  @Input() patientId!: string;
  @Output() onSave = new EventEmitter<any>();
  sickLeaveForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.sickLeaveForm = this.fb.group({
      startDate: this.getDefaultStartDate(),
      endDate: ['', Validators.required],
      reason: ['', Validators.required]
    });
  }


  getDefaultStartDate(): string {
    const today = new Date();
    const year = today.getFullYear();
    const month = String(today.getMonth() + 1).padStart(2, '0'); // Months are zero-based
    const day = String(today.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
  }
  saveSickLeave() {
    if (this.sickLeaveForm.valid && this.patientId !== null) {
      const sickLeaveData = {
        patientId: this.patientId,
        ...this.sickLeaveForm.value
      };
      this.onSave.emit(sickLeaveData);
    }
  }
}