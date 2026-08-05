import { Routes } from '@angular/router';
import { Login } from './features/auth/login/login';
import { Register } from './features/auth/register/register';
import { CreatePost } from './features/pages/create-post/create-post';

export const routes: Routes = [
    {
        path: 'login',
        component: Login
    },
    {
        path: '',
        redirectTo: 'login',
        pathMatch: 'full'
    },
    {
        path:'register',
        component: Register
    },
    {
        path:'create-post',
        component: CreatePost
    }
];
