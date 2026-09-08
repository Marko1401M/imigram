import { Routes } from '@angular/router';
import { Login } from './features/auth/login/login';
import { Register } from './features/auth/register/register';
import { CreatePost } from './features/pages/create-post/create-post';
import { Home } from './features/pages/home/home';
import { authGuard } from './core/guards/auth-guard';
import { PostDetails } from './features/pages/post-details/post-details';
import { Profile } from './features/pages/profile/profile';
import { FollowersPage } from './features/pages/followers-page/followers-page';
import { InboxPage } from './features/pages/inbox-page/inbox-page';
import { AdminPanel } from './features/pages/admin-panel/admin-panel';

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
    },
    {
        path:'followers',
        component: FollowersPage,
        canActivate:[authGuard]
    },
    {
        path:'inbox/:id',
        component: InboxPage,
        canActivate:[authGuard]
    },
    {
        path:'inbox',
        component: InboxPage,
        canActivate:[authGuard]
    },
    {
        path:'admin-panel',
        component: AdminPanel,
        canActivate:[authGuard]
    }
];

