import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RelatorioPorAutorComponent } from './relatorio-por-autor.component';

describe('RelatorioPorAutorComponent', () => {
  let component: RelatorioPorAutorComponent;
  let fixture: ComponentFixture<RelatorioPorAutorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RelatorioPorAutorComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RelatorioPorAutorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
