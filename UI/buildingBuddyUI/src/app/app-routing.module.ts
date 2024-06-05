import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { InvitationListComponent } from './features/invitation/invitation-list/invitation-list.component';
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

const routes: Routes =
[

  {
    path: 'invitations/list',
    component: InvitationListComponent
  },

  {
    path: 'invitations/update/:id',
    component: InvitationUpdateComponent
  },
  {
    path : 'invitations/create',
    component : InvitationCreateComponent
  },
  {
    path: 'invitations/guests/list',
    component : InvitationListByEmailComponent
  },
  {
    path: 'managers/create',
    component : ManagerCreateComponent
  },
  {
    path: 'constructionCompanyAdmin/create',
    component : ConstructionCompanyAdminCreateByInvitationComponent
  },
  {
    path: '',
    component : LandingPageComponent
  },
  {
    path: 'admins/create',
    component : AdminCreateComponent
  },
  {
    path: 'managers/list',
    component : ManagerListComponent
  },
  {
    path : 'constructionCompany/create',
    component : ConstructionCompanyCreateComponent
  },
  {
    path: 'constructionCompanies/list',
    component : ConstructionCompanyListComponent
  },
  {
    path: 'constructionCompany/:id/update'
  },
  {
    path : 'buildings/list',
    component : BuildingListComponent
  },


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule 
{ 
}
