import { Component, ElementRef, inject, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Livro } from '../../models/livro';
import { LivroService } from '../../services/livro.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-livro',
  //standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './livro.component.html',
  styleUrl: './livro.component.css'
})
export class LivroComponent implements OnInit {
  @ViewChild('modalSave') model: ElementRef | undefined;
  livroList: Livro[] = [];
  empService = inject(LivroService);
  livroForm: FormGroup = new FormGroup({});

  constructor(private fb: FormBuilder) { }
  ngOnInit(): void {
    this.setFormState();
    this.getLivros();
  }
  openModal() {
    const empModal = document.getElementById('modalSave');
    if (empModal != null) {
      empModal.style.display = 'block';
    }
  }

  closeModal() {
    this.setFormState();
    if (this.model != null) {
      this.model.nativeElement.style.display = 'none';
    }

  }
  getLivros() {
    this.empService.getAllLivros().subscribe((res) => {

      this.livroList = res;
    })
  }
  setFormState() {
    this.livroForm = this.fb.group({

      id: [0],
      titulo: ['', [Validators.required]],
      editora: ['', [Validators.required]],
      edicao: [0, [Validators.required]],
      anoPublicacao: [0, [Validators.required]],
    });
  }
  formValues: any;
  onSubmit() {
    console.log(this.livroForm.value);
    if (this.livroForm.invalid) {
      alert('Verifique os campos obrigatórios');
      return;
    }
    if (this.livroForm.value.cod == 0) {
      this.formValues = this.livroForm.value;
      this.empService.addLivro(this.formValues).subscribe((res) => {

        alert('Livro cadastrado com sucesso');
        this.getLivros();
        this.livroForm.reset();
        this.closeModal();

      });
    } else {
      this.formValues = this.livroForm.value;
      this.empService.updateLivro(this.formValues).subscribe((res) => {

        alert('Livro atualizado com sucesso');
        this.getLivros();
        this.livroForm.reset();
        this.closeModal();

      });
    }

  }

  OnEdit(Livro: Livro) {
    this.openModal();
    this.livroForm.patchValue(Livro);
  }
  onDelete(livro: Livro) {
    const isConfirm = confirm("Tem certeza que deseja remover o livro " + livro.titulo);
    if (isConfirm) {
      this.empService.deleteLivro(livro.cod).subscribe((res) => {
        alert("Livro removido com sucesso");
        this.getLivros();
      });
    }
  }
}
