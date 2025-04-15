import { CommonModule } from '@angular/common';
import { Component, ElementRef, ViewChild } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { TableComponent } from '../../../components/table/table.component';
import { Assunto } from '../../../interfaces/cadastro/assunto.model';
import { AssuntoService } from '../../../services/cadastro/assunto/assunto.service';

@Component({
  selector: 'app-assunto',
  imports: [CommonModule, ReactiveFormsModule, TableComponent],
  templateUrl: './assunto.component.html',
  styleUrl: './assunto.component.css',
})
export class AssuntoComponent {
  form: FormGroup;
  codAs: number | null = null;

  assuntos: Assunto[] = [];
  colunas: object[] = [
    {
      data: 'codAs',
      label: 'Código',
    },
    {
      data: 'descricao',
      label: 'Descrição',
    },
  ];

  @ViewChild('newButton') newButton!: ElementRef<HTMLElement>;

  constructor(private fb: FormBuilder, private service: AssuntoService) {
    this.form = this.fb.group({
      descricao: ['', [Validators.required, Validators.maxLength(20)]],
    });
  }

  ngOnInit(): void {
    this.loadAssuntos();
  }

  loadAssuntos() {
    this.service.getAll().subscribe((assuntos: Assunto[]) => {
      this.assuntos = assuntos;
    });
  }

  new() {
    this.form.reset();
    this.codAs = null;
  }

  edit(assunto: any) {
    this.form.setValue({
      descricao: assunto.descricao,
    });
    this.codAs = assunto.codAs;
  }

  remove(assunto: any) {
    this.service.delete(assunto.codAs).subscribe(() => {
      this.loadAssuntos();
    });
  }

  cancel() {
    this.form.reset();
    this.codAs = null;
  }

  submit() {
    if (this.form.invalid) return;

    const assunto: Assunto = {
      codAs: this.codAs ?? 0,
      ...this.form.value,
    };

    if (this.codAs === null) {
      this.service.add(assunto).subscribe(() => {
        this.cancel();
        this.loadAssuntos();
      });
    } else {
      this.service.update(assunto).subscribe(() => {
        this.cancel();
        this.loadAssuntos();
      });
    }
  }
}
