import { Component, ElementRef, inject, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { FormaCompra } from '../../models/forma_compra';
import { FormaCompraService } from '../../services/forma_compra.service';
import { CommonModule } from '@angular/common';
import { ToastService } from '../../services/toast.service';

@Component({
  selector: 'app-forma-compra',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './forma_compra.component.html',
  styleUrl: './forma_compra.component.css'
})
export class FormaCompraComponent implements OnInit {
  @ViewChild('modalSave') model: ElementRef | undefined;
  formacompraList: FormaCompra[] = [];
  empService = inject(FormaCompraService);
  formacompraForm: FormGroup = new FormGroup({});

  constructor(private fb: FormBuilder, private toastService: ToastService) { }
  ngOnInit(): void {
    this.setFormState();
    this.getFormaCompras();
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
  getFormaCompras() {
    this.empService.getAllFormaCompras().subscribe((res) => {

      this.formacompraList = res;
    })
  }
  setFormState() {
    this.formacompraForm = this.fb.group({

      cod: [0],
      descricao: ['', [Validators.required]],

    });
  }
  formValues: any;
  onSubmit() {
    console.log(this.formacompraForm.value);
    if (this.formacompraForm.invalid) {
      this.toastService.showToast('Verifique os campos obrigatórios', 'warning');
      return;
    }
    if (this.formacompraForm.value.cod == 0) {
      this.formValues = this.formacompraForm.value;
      this.empService.addFormaCompra(this.formValues).subscribe((res) => {
        this.toastService.showToast('FormaCompra cadastrado com sucesso', 'success');
        this.getFormaCompras();
        this.formacompraForm.reset();
        this.closeModal();

      });
    } else {
      this.formValues = this.formacompraForm.value;
      this.empService.updateFormaCompra(this.formValues).subscribe((res) => {
        this.toastService.showToast('FormaCompra atualizado com sucesso', 'success');
        this.getFormaCompras();
        this.formacompraForm.reset();
        this.closeModal();

      });
    }

  }

  OnEdit(FormaCompra: FormaCompra) {
    this.openModal();
    this.formacompraForm.patchValue(FormaCompra);
  }
  onDelete(formacompra: FormaCompra) {
    const isConfirm = confirm("Tem certeza que deseja remover o formacompra " + formacompra.descricao);
    if (isConfirm) {
      this.empService.deleteFormaCompra(formacompra.cod).subscribe((res) => {
        this.toastService.showToast("FormaCompra removido com sucesso", 'success');
        this.getFormaCompras();
      });
    }
  }
}
