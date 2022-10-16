import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomerDashboardComponent } from './dashboard-customer/dashboard-customer.component';
import { InsertCustomerComponent } from './dashboard-customer/insert-customer/insert-customer.component';
import { UpdateCustomerComponent } from './dashboard-customer/update-customer/update-customer.component';
import { UserDashboardComponent } from './dashboard-user/dashboard-user.component';
import { InsertUserComponent } from './dashboard-user/insert-user/insert-user.component';
import { UpdateUserComponent } from './dashboard-user/update-user/update-user.component';
import { LoginComponent } from './login/login.component';
import { LogoutComponent } from './logout/logout.component';
import { SettingsComponent } from './settings/settings.component';

//Guard
import { AuthGuard } from './shared/auth.guard';

const routes: Routes = [
  {
    path: '', canActivate: [AuthGuard], children:
      [
        {
          path: 'settings',
          pathMatch: 'full',
          component: SettingsComponent
        },
        { path: 'logout', pathMatch: 'full', component: LogoutComponent },
        { path: 'user', pathMatch: 'full', component: UserDashboardComponent },
        { path: 'user/edit/:id', pathMatch: 'full', component: UpdateUserComponent },
        { path: 'user/add', pathMatch: 'full', component: InsertUserComponent },
        { path: 'customer', pathMatch: 'full', component: CustomerDashboardComponent },
        { path: 'customer/edit/:id', pathMatch: 'full', component: UpdateCustomerComponent },
        { path: 'customer/add', pathMatch: 'full', component: InsertCustomerComponent },
      ]
  },
  { path: 'login', pathMatch: 'full', component: LoginComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
