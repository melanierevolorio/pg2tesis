import { ComponentFixture, TestBed } from '@angular/core/testing';
import { InsertOrderComponent } from './insert-order.component';


describe('InsertCustomerComponent', () => {
  let component: InsertOrderComponent;
  let fixture: ComponentFixture<InsertOrderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [InsertOrderComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InsertOrderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
