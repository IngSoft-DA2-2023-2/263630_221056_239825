import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OpcionesPagoComponent } from './opciones-pago.component';

describe('OpcionesPagoComponent', () => {
  let component: OpcionesPagoComponent;
  let fixture: ComponentFixture<OpcionesPagoComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [OpcionesPagoComponent]
    });
    fixture = TestBed.createComponent(OpcionesPagoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
