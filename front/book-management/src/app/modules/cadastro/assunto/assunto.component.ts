import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { TableComponent } from '../../../components/table/table.component';
import { Assunto } from '../../../interfaces/cadastro/assunto.model';
import { AssuntoService } from '../../../services/cadastro/assunto/assunto.service';

@Component({
  selector: 'app-assunto',
  imports: [CommonModule, ReactiveFormsModule, TableComponent],
  templateUrl: './assunto.component.html',
  styleUrl: './assunto.component.css'
})
export class AssuntoComponent {

  form: FormGroup;
  assuntos: Assunto[] = [];
  codAs: number | null = null;

  constructor(private fb: FormBuilder, private service: AssuntoService) {
    this.form = this.fb.group({
      descricao: ['', [Validators.required, Validators.maxLength(20)]]
    });
  }

  ngOnInit(): void {
    this.loadAssuntos();
  }

  loadAssuntos() {
    this.service.getAll().subscribe((assuntos) => {
      this.assuntos = assuntos;
    });
  }

  submit() {
      if (this.form.invalid) return;
  
      const assunto: Assunto = {
        id: this.codAs ?? 0,
        ...this.form.value,
      };
  
      if (this.codAs === null) {
        this.service.add(assunto);
      } else {
        this.service.update(assunto);
      }
  
      this.form.reset();
      this.codAs = null;
      this.loadAssuntos();
    }
  
    edit(assunto: Assunto) {
      this.form.setValue({
        descricao: assunto.descricao
      });
      this.codAs = assunto.codAs;
    }
  
    remove(id: number) {
      this.service.delete(id);
      this.loadAssuntos();
    }
  
    cancel() {
      this.form.reset();
      this.codAs = null;
    }
}
