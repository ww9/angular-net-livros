import { Component, ElementRef, inject, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Livro } from '../../models/livro';
import { LivroService } from '../../services/livro.service';
import { CommonModule } from '@angular/common';
import { ToastService } from '../../services/toast.service';
import { Assunto } from '../../models/assunto';
import { AssuntoService } from '../../services/assunto.service';
import { LivroDto } from '../../models/livroDto';
import { AutorService } from '../../services/autor.service';
import { Autor } from '../../models/autor';
import { FormArray } from '@angular/forms';
import { FormaCompra } from '../../models/forma_compra';
import { FormaCompraService } from '../../services/forma_compra.service';

@Component({
  selector: 'app-livro',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './livro.component.html',
  styleUrl: './livro.component.css'
})
export class LivroComponent implements OnInit {
  @ViewChild('modalSave') model: ElementRef | undefined;
  livroList: LivroDto[] = [];
  allAssuntos: Assunto[] = [];
  allAutors: Autor[] = [];
  allFormaCompras: FormaCompra[] = [];
  empService = inject(LivroService);
  assuntoService = inject(AssuntoService);
  autorService = inject(AutorService);
  formaCompraService = inject(FormaCompraService);
  livroForm: FormGroup = new FormGroup({});

  constructor(private fb: FormBuilder, private toastService: ToastService) { }
  ngOnInit(): void {
    this.setFormState();
    this.getLivros();
    this.loadAssuntos();
    this.loadAutors();
    this.loadFormaCompras();
  }

  loadAssuntos() {
    this.assuntoService.getAllAssuntos()
      .subscribe(data => this.allAssuntos = data);
  }

  loadAutors() {
    this.autorService.getAllAutors().subscribe(data => this.allAutors = data);
  }

  loadFormaCompras() {
    this.formaCompraService.getAllFormaCompras().subscribe(data => {
      this.allFormaCompras = data;
    });
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
    });
  }
  populateFormaCompraVals(livro: LivroDto) {
    const array = this.livroForm.get('formaCompraVals') as FormArray;
    array.clear();
    this.allFormaCompras.forEach(fc => {
      const existing = livro.formaCompraVals?.find(x => x.formaCompraCod === fc.cod);
      array.push(this.fb.group({
        formaCompraCod: [fc.cod],
        valor: [existing?.valor || 0],
      }));
    });
  }
  setFormState() {
    this.livroForm = this.fb.group({
      cod: [0],
      titulo: ['', [Validators.required]],
      editora: ['', [Validators.required]],
      edicao: [1, [Validators.required]],
      anoPublicacao: [2025, [Validators.required]],
      assuntoCods: [[]],
      autorCods: [[]],
      formaCompraVals: this.fb.array([]),
    });
  }
  getControls() {
    return (this.livroForm.get('formaCompraVals') as FormArray).controls;
  }
  formValues: any;
  onSubmit() {
    console.log(this.livroForm.value);
    if (this.livroForm.invalid) {
      this.toastService.showToast('Verifique os campos obrigatórios', 'warning');
      return;
    }
    if (this.livroForm.value.cod == 0) {
      this.formValues = this.livroForm.value;
      this.formValues.formaCompraVals = this.livroForm.value.formaCompraVals;
      this.empService.addLivro(this.formValues).subscribe((res) => {
        this.toastService.showToast('Livro cadastrado com sucesso', 'success');
        this.getLivros();
        this.livroForm.reset();
        this.closeModal();

      });
    } else {
      this.formValues = this.livroForm.value;
      this.empService.updateLivro(this.formValues).subscribe((res) => {
        this.toastService.showToast('Livro atualizado com sucesso', 'success');
        this.getLivros();
        this.livroForm.reset();
        this.closeModal();

      });
    }

  }

  OnEdit(Livro: LivroDto) {
    this.openModal();
    this.livroForm.patchValue(Livro);
    this.populateFormaCompraVals(Livro);
  }
  onDelete(livro: LivroDto) {
    const isConfirm = confirm("Tem certeza que deseja remover o livro " + livro.titulo);
    if (isConfirm) {
      this.empService.deleteLivro(livro.cod).subscribe((res) => {
        this.toastService.showToast("Livro removido com sucesso", 'success');
        this.getLivros();
      });
    }
  }
}
