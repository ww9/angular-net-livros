import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormaCompraComponent } from './forma_compra.component';

describe('FormaComponent', () => {
  let component: FormaCompraComponent;
  let fixture: ComponentFixture<FormaCompraComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FormaCompraComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(FormaCompraComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
