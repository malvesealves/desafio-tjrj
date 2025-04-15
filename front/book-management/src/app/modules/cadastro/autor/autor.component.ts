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
  codAu: number | null = null;

  autores: Autor[] = [];
  colunas: object[] = [
    {
      data: 'codAu',
      label: 'Código',
    },
    {
      data: 'nome',
      label: 'Nome',
    },
  ];

  constructor(private fb: FormBuilder, private service: AutorService) {
    this.form = this.fb.group({
      nome: ['', [Validators.required, Validators.maxLength(40)]],
    });
  }

  ngOnInit(): void {
    this.loadAutores();
  }

  loadAutores() {
    this.service.getAll().subscribe((autores: Autor[]) => {
      this.autores = autores;
    });
  }

  new() {
    this.form.reset();
    this.codAu = null;
  }

  edit(assunto: any) {
    this.form.setValue({
      nome: assunto.nome,
    });
    this.codAu = assunto.codAu;
  }

  remove(assunto: any) {
    this.service.delete(assunto.codAu).subscribe(() => {
      this.loadAutores();
    });
  }

  cancel() {
    this.form.reset();
    this.codAu = null;
  }

  submit() {
    if (this.form.invalid) return;

    const autor: Autor = {
      codAu: this.codAu ?? 0,
      ...this.form.value,
    };

    if (this.codAu === null) {
      this.service.add(autor).subscribe(() => {
        this.cancel();
        this.loadAutores();
      });
    } else {
      this.service.update(autor).subscribe(() => {
        this.cancel();
        this.loadAutores();
      });
    }
  }
}
