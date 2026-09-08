import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ToastAction } from './toast-action';

describe('ToastAction', () => {
  let component: ToastAction;
  let fixture: ComponentFixture<ToastAction>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ToastAction],
    }).compileComponents();

    fixture = TestBed.createComponent(ToastAction);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
