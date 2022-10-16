import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MenuComponent } from './menu/menu.component';
import { NavbarComponent } from './navbar/navbar.component';
import { MatModule } from '../material.module';
import { AuthService } from '../services/authentication.service';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { LogoutComponent } from './logout/logout.component';
import { FlexLayoutModule } from '@angular/flex-layout';
import { SettingsComponent } from './settings/settings.component';
import { ProfileComponent } from './profile/profile.component';
import { UserDashboardComponent } from './dashboard-user/dashboard-user.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UserManagementService } from '../services/user-management.service';

import { MAT_FORM_FIELD_DEFAULT_OPTIONS } from '@angular/material/form-field';
import { ModalConfirmComponent } from './modals/confirm/modal-confirm.component';
import { InsertUserComponent } from './dashboard-user/insert-user/insert-user.component';
import { UpdateUserComponent } from './dashboard-user/update-user/update-user.component';
import { CustomerDashboardComponent } from './dashboard-customer/dashboard-customer.component';
import { UpdateCustomerComponent } from './dashboard-customer/update-customer/update-customer.component';
import { InsertCustomerComponent } from './dashboard-customer/insert-customer/insert-customer.component';
import { CustomerDbService } from '../services/customer-db.service';

@NgModule({
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FlexLayoutModule,
    MatModule,
    ReactiveFormsModule,
    FormsModule
  ],
  declarations: [
    AppComponent,
    NavbarComponent,
    MenuComponent,
    LoginComponent,
    HomeComponent,
    LogoutComponent,
    SettingsComponent,
    ProfileComponent,
    UserDashboardComponent,
    CustomerDashboardComponent,
    ModalConfirmComponent,
    InsertUserComponent,
    UpdateUserComponent,
    InsertCustomerComponent,
    UpdateCustomerComponent
  ],
  providers: [AuthService, UserManagementService, CustomerDbService,
    { provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: { appearance: 'fill' } }  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
