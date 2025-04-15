import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { TableComponent } from "../../../components/table/table.component";
import { Livro } from '../../../interfaces/cadastro/livro.model';
import { LivroService } from '../../../services/cadastro/livro/livro.service';
import { CommonModule } from '@angular/common';
import { precoValidator } from '../../../validators/preco.validator';

@Component({
  selector: 'app-livro',
  imports: [CommonModule, ReactiveFormsModule, TableComponent],
  templateUrl: './livro.component.html',
  styleUrl: './livro.component.css'
})
export class LivroComponent implements OnInit {

  form: FormGroup;
  livros: Livro[] = [];
  codL: number | null = null;
  colunas: object[] = [
    {
      data: 'codL',
      label: 'Código',
    },
    {
      data: 'titulo',
      label: 'Título',
    },
    {
      data: 'editora',
      label: 'Editora',
    },
    {
      data: 'edicao',
      label: 'Edição',
    },
    {
      data: 'anoPublicacao',
      label: 'Ano Publicação',
    },
    {
      data: 'preco',
      label: 'Preco',
    },
    {
      data: 'assunto',
      label: 'Assunto',
    },
    {
      data: 'formaCompra',
      label: 'Forma Compra',
    },
  ];

  constructor(private fb: FormBuilder, private service: LivroService) {
    this.form = this.fb.group({
      titulo: ['', [Validators.required, Validators.maxLength(40)]],
      autores: this.fb.array([
        this.createAutor()
      ]),
      assunto: ['', Validators.required],
      editora: ['', [Validators.required, Validators.maxLength(40)]],
      edicao: ['', Validators.required],
      anoPublicacao: ['', [Validators.required, Validators.pattern('^[0-9]{4}$')]],
      formacompra: ['', Validators.required],
      preco: ['', [Validators.required, precoValidator]]
    });
  }

  ngOnInit(): void {
    this.loadLivros();
  }

  loadLivros() {
    this.service.getAll().subscribe((livros: Livro[]) => {
      this.livros = livros;
    });
  }

  get autores(): FormArray {
    return this.form.get('autores') as FormArray;
  }

  createAutor(): FormGroup {
    return this.fb.group({
      nome: ['', Validators.required]
    });
  }

  addAutor() {
    this.autores.push(this.createAutor());
  }

  removeAutor(index: number) {
    this.autores.removeAt(index);
  }

  new() {
    this.form.reset();
    this.codL = null;
  }

  edit(livro: any) {
    this.form.setValue({
      titulo: livro.titulo,
      autores: livro.autores, 
      assunto: livro.assunto,
      editora: livro.editora,
      edicao: livro.edicao,
      anoPublicacao: livro.anoPublicacao,
      formaCompra: livro.formaCompra
    });
    this.codL = livro.codL;
  }

  remove(id: number) {
    this.service.delete(id);
    this.loadLivros();
  }

  cancel() {
    this.form.reset();
    this.codL = null;
  }

  submit() {
    if (this.form.invalid) return;

    const livro: Livro = {
      codL: this.codL ?? 0,
      ...this.form.value,
    };

    if (this.codL === null) {
      this.service.add(livro).subscribe(() => {
        this.cancel();
        this.loadLivros();
      });
    } else {
      this.service.update(livro).subscribe(() => {
        this.cancel();
        this.loadLivros();
      });
    }    
  }  
}
