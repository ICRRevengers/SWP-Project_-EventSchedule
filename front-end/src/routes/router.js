import Home from '../pages/home/Home';
import Error from '../pages/error/Error';
import ManagerEvents from '../pages/manager/events/ManagerEvent';
import Header from '../components/layout/defaultLayout/header/Header';
import Aboutus from '../pages/aboutus/Aboutus';
import Contact from '../pages/contact/Contact';
import Login from '../pages/login/Login';
<<<<<<< HEAD
=======
import Create from '../pages/manager/events/CreateEvent';
import UpdateEvent from '../pages/manager/events/UpdateEvent';
import ParticipatedList from '../pages/manager/events/ParticipatedList';
import AdminProfile from '../pages/manager/profile/AdminProfile';
>>>>>>> frontend

const publicRoutes = [
    { path: '/', component: Home },
    { path: '/aboutus', component: Aboutus },
    { path: '/contactus', component: Contact },
    { path: '/login', component: Login },
    {
        path: '/manage/events',
        component: ManagerEvents,
        layout: Header
    },
<<<<<<< HEAD
=======
    {
        path: '/manage/postevent',
        component: Create,
        layout: Header
    },
    {
        path: '/manage/update',
        component: UpdateEvent,
        layout: Header
    },
    {
        path: '/manage/participated',
        component: ParticipatedList,
        layout: Header
    },
    {
        path: '/manage/profile',
        component: AdminProfile,
        layout: Header
    },
>>>>>>> frontend
    { path: '/*', component: Error, layout: null },
];

const privateRoutes = [];

export { publicRoutes, privateRoutes };
