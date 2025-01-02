import { Component, ElementRef, inject, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Autor } from '../../models/autor';
import { AutorService } from '../../services/autor.service';
import { CommonModule } from '@angular/common';
import { ToastService } from '../../services/toast.service';

@Component({
  selector: 'app-autor',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './autor.component.html',
  styleUrl: './autor.component.css'
})
export class AutorComponent implements OnInit {
  @ViewChild('modalSave') model: ElementRef | undefined;
  autorList: Autor[] = [];
  empService = inject(AutorService);
  autorForm: FormGroup = new FormGroup({});

  constructor(private fb: FormBuilder, private toastService: ToastService) { }
  ngOnInit(): void {
    this.setFormState();
    this.getAutors();
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
  getAutors() {
    this.empService.getAllAutors().subscribe((res) => {

      this.autorList = res;
    })
  }
  setFormState() {
    this.autorForm = this.fb.group({

      cod: [0],
      nome: ['', [Validators.required]],

    });
  }
  formValues: any;
  onSubmit() {
    console.log(this.autorForm.value);
    if (this.autorForm.invalid) {
      this.toastService.showToast('Verifique os campos obrigatórios', 'warning');
      return;
    }
    if (this.autorForm.value.cod == 0) {
      this.formValues = this.autorForm.value;
      this.empService.addAutor(this.formValues).subscribe((res) => {
        this.toastService.showToast('Autor cadastrado com sucesso', 'success');
        this.getAutors();
        this.autorForm.reset();
        this.closeModal();

      });
    } else {
      this.formValues = this.autorForm.value;
      this.empService.updateAutor(this.formValues).subscribe((res) => {
        this.toastService.showToast('Autor atualizado com sucesso', 'success');
        this.getAutors();
        this.autorForm.reset();
        this.closeModal();

      });
    }

  }

  OnEdit(Autor: Autor) {
    this.openModal();
    this.autorForm.patchValue(Autor);
  }
  onDelete(autor: Autor) {
    const isConfirm = confirm("Tem certeza que deseja remover o autor " + autor.nome);
    if (isConfirm) {
      this.empService.deleteAutor(autor.cod).subscribe((res) => {
        this.toastService.showToast("Autor removido com sucesso", 'success');
        this.getAutors();
      });
    }
  }
}
