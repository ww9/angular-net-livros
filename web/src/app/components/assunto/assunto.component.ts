import { Component, ElementRef, inject, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Assunto } from '../../models/assunto';
import { AssuntoService } from '../../services/assunto.service';
import { CommonModule } from '@angular/common';
import { ToastService } from '../../services/toast.service';

@Component({
  selector: 'app-assunto',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './assunto.component.html',
  styleUrl: './assunto.component.css'
})
export class AssuntoComponent implements OnInit {
  @ViewChild('modalSave') model: ElementRef | undefined;
  assuntoList: Assunto[] = [];
  empService = inject(AssuntoService);
  assuntoForm: FormGroup = new FormGroup({});

  constructor(private fb: FormBuilder, private toastService: ToastService) { }
  ngOnInit(): void {
    this.setFormState();
    this.getAssuntos();
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
  getAssuntos() {
    this.empService.getAllAssuntos().subscribe((res) => {

      this.assuntoList = res;
    })
  }
  setFormState() {
    this.assuntoForm = this.fb.group({

      cod: [0],
      descricao: ['', [Validators.required]],

    });
  }
  formValues: any;
  onSubmit() {
    console.log(this.assuntoForm.value);
    if (this.assuntoForm.invalid) {
      this.toastService.showToast('Verifique os campos obrigatórios', 'warning');
      return;
    }
    if (this.assuntoForm.value.cod == 0) {
      this.formValues = this.assuntoForm.value;
      this.empService.addAssunto(this.formValues).subscribe((res) => {
        this.toastService.showToast('Assunto cadastrado com sucesso', 'success');
        this.getAssuntos();
        this.assuntoForm.reset();
        this.closeModal();

      });
    } else {
      this.formValues = this.assuntoForm.value;
      this.empService.updateAssunto(this.formValues).subscribe((res) => {
        this.toastService.showToast('Assunto atualizado com sucesso', 'success');
        this.getAssuntos();
        this.assuntoForm.reset();
        this.closeModal();

      });
    }

  }

  OnEdit(Assunto: Assunto) {
    this.openModal();
    this.assuntoForm.patchValue(Assunto);
  }
  onDelete(assunto: Assunto) {
    const isConfirm = confirm("Tem certeza que deseja remover o assunto " + assunto.descricao);
    if (isConfirm) {
      this.empService.deleteAssunto(assunto.cod).subscribe((res) => {
        this.toastService.showToast("Assunto removido com sucesso", 'success');
        this.getAssuntos();
      });
    }
  }
}
