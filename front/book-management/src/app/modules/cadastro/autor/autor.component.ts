import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { TableComponent } from '../../../components/table/table.component';
import { Autor } from '../../../interfaces/cadastro/autor.model';
import { AutorService } from '../../../services/cadastro/autor/autor.service';

@Component({
  selector: 'app-autor',
  imports: [CommonModule, ReactiveFormsModule, TableComponent],
  templateUrl: './autor.component.html',
  styleUrl: './autor.component.css',
})
export class AutorComponent {
  form: FormGroup;
  autores: Autor[] = [];
  codAu: number | null = null;

  constructor(private fb: FormBuilder, private service: AutorService) {
    this.form = this.fb.group({
      nome: ['', [Validators.required, Validators.maxLength(40)]],
    });
  }

  ngOnInit(): void {
    this.loadAutores();
  }

  loadAutores() {
    this.service.getAll().subscribe((assuntos) => {
      this.autores = assuntos;
    });
  }

  submit() {
    if (this.form.invalid) return;

    const autor: Autor = {
      id: this.codAu ?? 0,
      ...this.form.value,
    };

    if (this.codAu === null) {
      this.service.add(autor);
    } else {
      this.service.update(autor);
    }

    this.form.reset();
    this.codAu = null;
    this.loadAutores();
  }

  edit(autor: Autor) {
    this.form.setValue({
      nome: autor.nome,
    });
    this.codAu = autor.codAu;
  }

  remove(id: number) {
    this.service.delete(id);
    this.loadAutores();
  }

  cancel() {
    this.form.reset();
    this.codAu = null;
  }
}
