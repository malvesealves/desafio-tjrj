import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ReactiveFormsModule, FormGroup, FormBuilder, Validators } from '@angular/forms';

import { RelatorioService } from '../../services/relatorio/relatorio.service';

@Component({
  selector: 'app-relatorio',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './relatorio.component.html',
  styleUrl: './relatorio.component.css'
})
export class RelatorioComponent {

  form: FormGroup;
  codAs: number | null = null;

  constructor(private fb: FormBuilder, private service: RelatorioService) {
    this.form = this.fb.group({
      descricao: ['', [Validators.required, Validators.maxLength(20)]]
    });
  }

  submit() {
        if (this.form.invalid) return;
       
        this.service.getReport().subscribe(blob => {
          const link = document.createElement('a');
          link.href = URL.createObjectURL(blob);
          link.download = `RelatorioLivros.pdf`;
          link.click();
          URL.revokeObjectURL(link.href);
        });
    
        this.form.reset();
      }

}
