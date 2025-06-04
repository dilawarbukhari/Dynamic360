import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { FullComponent } from './layouts/full/full.component';
import { authGuard } from './AuthGuard/auth-guard';


export const Approutes: Routes = [
 
  {
    path: 'app',
    component: FullComponent,
    canActivate: [authGuard],
    children: [
      { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
      {
        path: 'dashboard',
        loadChildren: () => import('./dashboard/dashboard.module').then(m => m.DashboardModule)
      },
     
      {
        path: 'about',
        loadChildren: () => import('./about/about.module').then(m => m.AboutModule)
      },
      {
        path: 'component',
        loadChildren: () => import('./component/component.module').then(m => m.ComponentsModule)
      }
    ]
  },

  {
    path: '**',
    redirectTo: '/starter'
  },
  
  {path:'',loadChildren: () => import('./Auth/auth.module').then(m => m.AuthModule) },
  {path:'auth',loadChildren: () => import('./Auth/auth.module').then(m => m.AuthModule) },

];
