import { Routes } from '@angular/router';
import { Login } from './features/auth/login/login';
import { Register } from './features/auth/register/register';
import { CreatePost } from './features/pages/create-post/create-post';
import { Home } from './features/pages/home/home';
import { authGuard } from './core/guards/auth-guard';
import { PostDetails } from './features/pages/post-details/post-details';
import { Profile } from './features/pages/profile/profile';

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
    },
    {
        path:'home',
        component: Home,
        canActivate:[authGuard]
    },
    {
        path:'post-details/:id',
        component: PostDetails,
        canActivate:[authGuard]
    },
    {
        path:'profile/:id',
        component: Profile,
        canActivate:[authGuard]
    }
];

