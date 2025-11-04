import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AcessoRapido } from './acesso-rapido';

describe('AcessoRapido', () => {
  let component: AcessoRapido;
  let fixture: ComponentFixture<AcessoRapido>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AcessoRapido]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AcessoRapido);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
