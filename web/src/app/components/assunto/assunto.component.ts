import { Component, ElementRef, inject, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Assunto } from '../../models/assunto';
import { AssuntoService } from '../../services/assunto.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-assunto',
  //standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './assunto.component.html',
  styleUrl: './assunto.component.css'
})
export class AssuntoComponent implements OnInit {
  @ViewChild('modalSave') model: ElementRef | undefined;
  assuntoList: Assunto[] = [];
  empService = inject(AssuntoService);
  assuntoForm: FormGroup = new FormGroup({});

  constructor(private fb: FormBuilder) { }
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

      Cod: [0],
      Descricao: ['', [Validators.required]],

    });
  }
  formValues: any;
  onSubmit() {
    console.log(this.assuntoForm.value);
    if (this.assuntoForm.invalid) {
      alert('Verifique os campos obrigatórios');
      return;
    }
    if (this.assuntoForm.value.id == 0) {
      this.formValues = this.assuntoForm.value;
      this.empService.addAssunto(this.formValues).subscribe((res) => {
        alert('Assunto cadastrado com sucesso');
        this.getAssuntos();
        this.assuntoForm.reset();
        this.closeModal();

      });
    } else {
      this.formValues = this.assuntoForm.value;
      this.empService.updateAssunto(this.formValues).subscribe((res) => {
        alert('Assunto atualizado com sucesso');
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
    const isConfirm = confirm("Tem certeza que deseja remover o assunto " + assunto.Descricao);
    if (isConfirm) {
      this.empService.deleteAssunto(assunto.Cod).subscribe((res) => {
        alert("Assunto removido com sucesso");
        this.getAssuntos();
      });
    }
  }
}
