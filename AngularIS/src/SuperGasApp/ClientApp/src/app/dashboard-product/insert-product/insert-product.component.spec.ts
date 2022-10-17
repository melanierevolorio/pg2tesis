import { ComponentFixture, TestBed } from '@angular/core/testing';
import { InsertProductComponent } from './insert-product.component';


describe('InsertCustomerComponent', () => {
  let component: InsertProductComponent;
  let fixture: ComponentFixture<InsertProductComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [InsertProductComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InsertProductComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
