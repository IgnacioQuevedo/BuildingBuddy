import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './core/navbar/navbar.component';
import { InvitationListComponent } from './features/invitation/invitation-list/invitation-list.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { InvitationUpdateComponent } from './features/invitation/invitation-update/invitation-update.component';
import { InvitationCreateComponent } from './features/invitation/invitation-create/invitation-create.component';
import { InvitationListByEmailComponent } from './features/invitation/invitation-list-by-email/invitation-list-by-email.component';
import { ManagerCreateComponent } from './features/manager/features/manager-create/manager-create.component';
import { LandingPageComponent } from './features/landingPage/landing-page/landing-page.component';
import { AdminCreateComponent } from './features/administrator/admin-create/admin-create.component';
import { ConstructionCompanyAdminCreateByInvitationComponent } from './features/constructionCompanyAdmin/construction-company-admin-create-by-invitation/construction-company-admin-create-by-invitation.component';
import { ManagerListComponent } from './features/manager/features/manager-list/manager-list.component';
import { ConstructionCompanyCreateComponent } from './features/constructionCompany/construction-company-create/construction-company-create.component';
import { ConstructionCompanyListComponent } from './features/constructionCompany/construction-company-list/construction-company-list.component';
import { BuildingListComponent } from './features/building/building-list/building-list.component';
import { ConstructionCompanyUpdateComponent } from './features/constructionCompany/construction-company-update/construction-company-update.component';
import { CategoryCreateComponent } from './features/category/category-create/category-create.component';
import { CategoryTreeComponent } from './features/category/category-tree/category-tree.component';
import { LoginComponent } from './features/login/login.component';
import { AuthInterceptor } from './core/interceptors/auth.interceptor';
import { ErrorInterceptor } from './core/interceptors/forbidden-handler.interceptor';
import { HomeComponent } from './core/home/home.component';
import { BuildingUpdateComponent } from './features/building/building-update/building-update.component';
import { BuildingCreateComponent } from './features/building/building-create/building-create.component';
import { OwnerCreateComponent } from './features/owner/owner-create/owner-create.component';



@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    InvitationListComponent,
    InvitationUpdateComponent,
    InvitationCreateComponent,
    InvitationListByEmailComponent,
    ManagerCreateComponent,
    ConstructionCompanyAdminCreateByInvitationComponent,
    LandingPageComponent,
    AdminCreateComponent,
    ManagerListComponent,
    ConstructionCompanyCreateComponent,
    ConstructionCompanyListComponent,
    BuildingListComponent,
    ConstructionCompanyUpdateComponent,
    CategoryCreateComponent,
    CategoryTreeComponent,
    LoginComponent,
    HomeComponent,
    BuildingUpdateComponent,
    BuildingCreateComponent,
    OwnerCreateComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
